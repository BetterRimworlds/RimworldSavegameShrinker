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

using HarmonyLib;
using RimWorld;
using Verse;
using UnityEngine;

namespace BetterRimworlds.SavegameShrinker;
public class RimSavegameShrinker : Mod
{
    public static Settings Settings;

    public RimSavegameShrinker(ModContentPack content) : base(content)
    {
        base.GetSettings<Settings>();
        var hinstance = new Harmony("BetterRimworlds.SavegameShrinker");

        Settings = GetSettings<Settings>();

        hinstance.PatchAll();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        Settings.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "Savegame Shrinker";
    }
}

[HarmonyPatch(typeof(Dialog_FileList), "DrawDateAndVersion")]
public class Patch_FileList
{
    private static double GetFileSize(string fileLocation)
    {
        return new FileInfo(fileLocation).Length / (1024.0 * 1024.0);
    }

    public static void Prefix(Dialog_FileList __instance, SaveFileInfo sfi, Rect rect)
    {
        Rect TargetRect = new Rect(rect.x - 120f, 4f, 112f, 30f);

        string fileLocation = sfi.FileInfo.FullName;
        double fileSize = Math.Round(GetFileSize(fileLocation));

        var settings = LoadedModManager.GetMod<RimSavegameShrinker>().GetSettings<Settings>();

        var shrinker = new SaveGameShrinker(settings);

        if (Widgets.ButtonText(TargetRect, "SavegameShrinker.Shrink".Translate(fileSize.ToString())))
        {
            string shortFileName = sfi.FileInfo.Name;
            Log.Error($"++++++++++ Shrinking SaveGame {shortFileName} +++++++");

            shrinker.shrinkSavegame(fileLocation);
        }
    }
}

