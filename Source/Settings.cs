using System.Collections;
using UnityEngine;
using Verse;

namespace BetterRimworlds.SavegameShrinker;

public class Settings: ModSettings
{
    public bool shrinkHistoricalArchives = true;
    public bool shrinkTales;
    public bool shrinkPlayLog = true;
    public bool shrinkBattleLog = true;
    public bool shrinkQuestsAutomatic = true;
    public bool shrinkQuestsUnaccepted = true;
    public bool shrinkWorldPawnsMothballed;
    public bool shrinkWorldPawnsDead;
    public bool removeFilth = true;

    public bool debugMode = false;

    public override void ExposeData()
    {
        base.ExposeData();

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

        listing_Standard.CheckboxLabeled("SavegameShrinker.ShrinkHistoricalArchives".Translate(), ref shrinkHistoricalArchives);
        listing_Standard.CheckboxLabeled("SavegameShrinker.ShrinkPlayLog".Translate(), ref shrinkPlayLog);
        listing_Standard.CheckboxLabeled("SavegameShrinker.ShrinkBattleLog".Translate(), ref shrinkBattleLog);
        listing_Standard.CheckboxLabeled("SavegameShrinker.ShrinkQuestsAutomatic".Translate(), ref shrinkQuestsAutomatic);
        listing_Standard.CheckboxLabeled("SavegameShrinker.RemoveFilth".Translate(), ref removeFilth);
        listing_Standard.CheckboxLabeled("SavegameShrinker.ShrinkTales".Translate(), ref shrinkTales);
        listing_Standard.CheckboxLabeled("SavegameShrinker.ShrinkQuestsUnaccepted".Translate(), ref shrinkQuestsUnaccepted);
        listing_Standard.CheckboxLabeled("SavegameShrinker.ShrinkWorldPawnsDead".Translate(), ref shrinkWorldPawnsDead);
        listing_Standard.CheckboxLabeled("SavegameShrinker.ShrinkWorldPawnsMothballed".Translate(), ref shrinkWorldPawnsMothballed);
        listing_Standard.CheckboxLabeled("SavegameShrinker.DebugMode".Translate(), ref debugMode);

        listing_Standard.End();
    }
}
