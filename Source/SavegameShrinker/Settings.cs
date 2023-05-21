using System.Collections;
using UnityEngine;
using Verse;

namespace BetterRimworlds.SavegameShrinker
{
    public class Settings: ModSettings
    {
        public bool shrinkHistoricalArchives;
        public bool shrinkTales;
        public bool shrinkPlayLog;
        public bool shrinkBattleLog;
        public bool shrinkQuestsAutomatic;
        public bool shrinkQuestsUnaccepted;
        public bool shrinkWorldPawnsMothballed;
        public bool shrinkWorldPawnsDead;
        public bool removeFilth;

        public bool debugMode = false;

        override public void ExposeData()
        {
            Scribe_Values.Look(ref shrinkHistoricalArchives,   "brw.saveshrinker.shrinkHistoricalArchives", true);
            Scribe_Values.Look(ref shrinkTales,                "brw.saveshrinker.shrinkTales", false);
            Scribe_Values.Look(ref shrinkPlayLog,              "brw.saveshrinker.shrinkPlayLog", true);
            Scribe_Values.Look(ref shrinkBattleLog,            "brw.saveshrinker.shrinkBattleLog", true);
            Scribe_Values.Look(ref shrinkQuestsAutomatic,      "brw.saveshrinker.shrinkQuestsAutomatic", true);
            Scribe_Values.Look(ref shrinkQuestsUnaccepted,     "brw.saveshrinker.shrinkQuestsUnaccepted", false);
            Scribe_Values.Look(ref shrinkWorldPawnsMothballed, "brw.saveshrinker.shrinkWorldPawnsMothballed", false);
            Scribe_Values.Look(ref shrinkWorldPawnsDead,       "brw.saveshrinker.shrinkWorldPawnsDead", true);
            Scribe_Values.Look(ref removeFilth,                "brw.saveshrinker.removeFilth", true);
            Scribe_Values.Look(ref debugMode,                  "brw.saveshrinker.debugMode", false);
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);

            string[] labels = {
                "(safe) Shrink historical archives?",
                "(safe) Shrink play log?",
                "(safe) Shrink battle log?",
                "(safe) Shrink automatic quests?",
                "(safe) Remove filth?",
                "(less safe) Shrink tales?",
                "(less safe) Shrink unaccepted quests?",
                "(huge savings) Shrink Dead World Pawns?",
                "(dangerous) Shrink Mothballed World Pawns?",
                "Print debug messages?",
            };
            
            listing_Standard.CheckboxLabeled(labels[0], ref shrinkHistoricalArchives);
            listing_Standard.CheckboxLabeled(labels[1], ref shrinkPlayLog);
            listing_Standard.CheckboxLabeled(labels[2], ref shrinkBattleLog);
            listing_Standard.CheckboxLabeled(labels[3], ref shrinkQuestsAutomatic);
            listing_Standard.CheckboxLabeled(labels[4], ref removeFilth);
            listing_Standard.CheckboxLabeled(labels[5], ref shrinkTales);
            listing_Standard.CheckboxLabeled(labels[6], ref shrinkQuestsUnaccepted);
            listing_Standard.CheckboxLabeled(labels[7], ref shrinkWorldPawnsDead);
            listing_Standard.CheckboxLabeled(labels[8], ref shrinkWorldPawnsMothballed);
            listing_Standard.CheckboxLabeled(labels[9], ref debugMode);

            listing_Standard.End();
        }
    }
}