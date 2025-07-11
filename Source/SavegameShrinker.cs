/*
 * This file is part of Rimworld Savegame Shrinker, a Better Rimworlds Project.
 *
 * Copyright © 2021-2024 Theodore R. Smith
 * Author: Theodore R. Smith <hopeseekr@gmail.com>
 *   GPG Fingerprint: D8EA 6E4D 5952 159D 7759  2BB4 EEB6 CE72 F441 EC41
 *   https://github.com/BetterRimworlds/RimworldSavegameShrinker
 *   https://steamcommunity.com/sharedfiles/filedetails/?id=2978713095
 *
 * This file is licensed under the MIT License.
 */

using System.Text;
using System.Xml;
using Verse;

namespace BetterRimworlds.SavegameShrinker;

public class SaveGameShrinker
{
    protected Settings settings;
    protected XmlNode root;
    protected int currentTick;

    public SaveGameShrinker(Settings settings)
    {
        this.settings = settings;
    }

    public void shrinkSavegame(string fileLocation)
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        File.Copy(fileLocation, fileLocation.Replace(".rws", $".orig.{timestamp}.rws"), false);

        XmlDocument doc = new XmlDocument();
        doc.Load(fileLocation);
        this.root = doc.DocumentElement;

        this.currentTick = int.Parse(this.grabNodes("/savegame/game/tickManager/ticksGame").Item(0)?.InnerText ?? "0") + 15_000;
        if (this. settings.debugMode) Log.Message("Current game tick: " + currentTick);

        if (this.settings.shrinkHistoricalArchives)
        {
            Log.Warning("Shrinking Archives: Standard Letters");
            this.shrinkHistoricalArchives("StandardLetter");
            Log.Warning("Shrinking Archives: Messages");
            this.shrinkHistoricalArchives("Message");
            Log.Warning("Shrinking Archives: Archived Dialogs");
            this.shrinkHistoricalArchives("ArchivedDialog");
            Log.Warning("Shrinking Archives: Death Letters");
            this.shrinkHistoricalArchives("DeathLetter");
        }

        if (this.settings.shrinkTales)
        {
            Log.Warning("Shrinking Tales...");
            this.shrinkTales();
        }

        if (this.settings.shrinkPlayLog)
        {
            Log.Warning("Shrinking the Play Log...");
            this.shrinkPlayLog();
        }

        if (this.settings.shrinkBattleLog) {
            Log.Warning("Shrinking the Battle Log...");
            this.shrinkBattleLog();
        }

        if (this.settings.shrinkQuestsAutomatic)
        {
            Log.Warning("Shrinking automatically-accepted quests...");
            this.shrinkQuestsLog("automatic");
        }

        if (this.settings.shrinkQuestsUnaccepted)
        {
            Log.Warning("Shrinking Unaccepted Supply Quests...");
            this.shrinkQuestsLog("supplies not accepted");
        }

        if (this.settings.shrinkWorldPawnsMothballed)
        {
            Log.Error("Shrinking Mothballed World Pawns...");
            this.shrinkWorldPawns("Mothballed");
        }

        if (this.settings.shrinkWorldPawnsDead)
        {
            Log.Warning("Shrinking Dead World Pawns...");
            this.shrinkWorldPawns("Dead");
        }

        if (this.settings.removeFilth)
        {
            Log.Warning("Removing all Filth...");
            this.removeFilth();
        }

        using var writer = XmlWriter.Create(
            fileLocation,
            new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NewLineChars = "\n",
                // Ensures the BOM is added.
                Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true)
            }
        );

        doc.Save(writer);
    }

    protected XmlNodeList grabNodes(string xpath)
    {
        if (xpath == "")
        {
            return null;
        }
        if (this. settings.debugMode) Log.Message(xpath);
        return root.SelectNodes(xpath);
    }

    protected Dictionary<string, bool> grabLetterStackIds()
    {
        var letterStackIds = new Dictionary<string, bool>();
        XmlNodeList letterStackNodes = this.grabNodes("/savegame/game/letterStack/letters/li");

        if (letterStackNodes != null) {
            foreach (XmlNode letterStackNode in letterStackNodes)
            {
                string letterId = letterStackNode.InnerText.Replace("Letter_", "");
                letterStackIds.Add(letterId, true);
            }
        }

        return letterStackIds;
    }

    protected void removeNodes(XmlNodeList currentNodes, string idKey, Dictionary<string, bool> nodesToKeepIds = null)
    {
        var historicalLetterIdsBefore = new List<string>();
        var historicalLetterIdsAfter = new List<string>();

        if (nodesToKeepIds == null)
        {
            nodesToKeepIds = new Dictionary<string, bool>();
        }

        if (currentNodes != null)
        {
            for (int i = currentNodes.Count - 1; i >= 0; --i)
            {
                var letterNode = currentNodes[i];
                string letterId = letterNode[idKey]?.InnerText;
                historicalLetterIdsBefore.Add(letterId);

                if (letterId == null || !nodesToKeepIds.ContainsKey(letterId))
                {
                    if (this. settings.debugMode) Log.Message($"----- REMOVING {letterId} @ {currentNodes.Count - i} ------");
                    letterNode.ParentNode?.RemoveChild(letterNode);
                }
                else
                {
                    historicalLetterIdsAfter.Add(letterId);
                }
            }

            if (this. settings.debugMode) Log.Message("Historical Letters (Before): " + string.Join(", ", historicalLetterIdsBefore));
            if (this. settings.debugMode) Log.Message("Historical Letters (After): " + string.Join(", ", historicalLetterIdsAfter));
        }
    }

    public bool shrinkHistoricalArchives(string archiveType)
    {
        var archivesToKeepIds = new Dictionary<string, bool>();
        XmlNodeList currentNodes;

        switch (archiveType)
        {
            case "StandardLetter":
                archivesToKeepIds = this.grabLetterStackIds();
                if (this. settings.debugMode) Log.Message("Node IDs to Keep: " + string.Join(", ", archivesToKeepIds.Keys));
                break;
        }

        currentNodes = this.grabArchiveNodes(archiveType);
        this.removeNodes(currentNodes, "ID", archivesToKeepIds);

        return true;
    }

    protected XmlNodeList grabArchiveNodes(string type)
    {
        return this.grabNodes($"/savegame/game/history/archive/archivables/li[@Class='{type}']");
    }

    public void shrinkTales()
    {
        string xpath = "";

        var talesToKeep = new Dictionary<string, bool>()
        {
            ["Marriage"] = true,
        };

        xpath = "/savegame/game/taleManager/tales/li";
        var taleNodes = this.grabNodes(xpath);

        for (int i = taleNodes.Count - 1; i >= 0; --i)
        {
            var taleNode = taleNodes[i];
            string taleName = taleNode.SelectSingleNode("def").InnerText;
            int taleTick = int.Parse(taleNode.SelectSingleNode("date").InnerText);

            int ticksDiff = this.currentTick - taleTick;
            string timeDiff = TicksTranslator.TicksToTime(ticksDiff);

            string removedOrSkipped = " --- SKIPPED ---";
            if (ticksDiff > TicksTranslator.TicksPerDay && talesToKeep.ContainsKey(taleName) == false)
            {
                removedOrSkipped = " -- REMOVED --";
                taleNodes[i].ParentNode?.RemoveChild(taleNode);
            }

            if (this. settings.debugMode) Log.Message($"Tale #{i + 1}: {taleName} @ Tick {taleTick}: {timeDiff} ago {removedOrSkipped}");
        }
    }

    public void shrinkPlayLog()
    {
        this.removeNodes(this.grabNodes("/savegame/game/playLog/entries/li"), "logID");
    }

    public void shrinkBattleLog()
    {
        this.removeNodes(this.grabNodes("/savegame/game/battleLog/battles/li"), "loadID");
    }

    public void shrinkQuestsLog(string type)
    {
        string xpath = "/savegame/game/questManager/quests/li";

        switch (type)
        {
            case "automatic": xpath += "/initiallyAccepted/../endOutcome[text()='Fail']/.."; break;
            // case "supplies not accepted": xpath += "/root[text()='TradeRequest']/../endOutcome[not(not(text()='Success')]/.."; break;
            case "supplies not accepted": xpath += "[not(./endOutcome)]/root[text()='TradeRequest']/.."; break;
        }

        this.removeNodes(this.grabNodes(xpath), "name");
    }

    public void shrinkWorldPawns(string type)
    {
        XmlNodeList currentNodes;
        var pawnsToKeepIds = new Dictionary<string, bool>();

        string xpath = $"/savegame/game/world/worldPawns/pawns{type}/li[not(./kindDef[text()='Colonist'])]";
        currentNodes = this.grabNodes(xpath);


        if (type == "Dead")
        {
            string corpsesXpath = "/savegame/game/maps/li/things/thing[@Class='Corpse']";
            var corpseNodes = this.grabNodes(corpsesXpath);
            foreach (XmlNode node in corpseNodes)
            {
                // Add its pawn to the Keep list to avoid "bugged corpse" that disappears.
                string thingId = node.SelectSingleNode("innerContainer/innerList/li").InnerText
                     .Replace("Thing_", "");
                // string thingId = node.SelectSingleNode("id").InnerText;
                if (this. settings.debugMode) Log.Message(thingId);
                if (!pawnsToKeepIds.ContainsKey(thingId))
                    pawnsToKeepIds[thingId] = true;            }
        }

        this.removeNodes(currentNodes, "id", pawnsToKeepIds);
    }

    public void removeFilth()
    {
        string xpath = "/savegame/game/maps/li/things/thing[@Class='Filth']";

        this.removeNodes(this.grabNodes(xpath), "id");
    }
}

