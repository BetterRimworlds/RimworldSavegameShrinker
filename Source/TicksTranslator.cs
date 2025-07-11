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

namespace BetterRimworlds;

public class TicksTranslator
{
    public const int TicksPerHour = 2500;
    public const int HoursPerDay = 24;
    public const int DaysPerQuadrum = 15;
    public const int QuadrumsPerYear = 4;

    public const int TicksPerDay = TicksPerHour * HoursPerDay;
    public const int TicksPerQuadrum = TicksPerDay * DaysPerQuadrum;
    public const int TicksPerYear = TicksPerQuadrum * QuadrumsPerYear;

    public static string TicksToTime(int ticks)
    {
        int origTicks = ticks;
        int years = ticks / TicksPerYear;
        ticks %= TicksPerYear;

        int quadrums = ticks / TicksPerQuadrum;
        ticks %= TicksPerQuadrum;

        int days = ticks / TicksPerDay;
        ticks %= TicksPerDay;

        int hours = ticks / TicksPerHour;

        string time = string.Empty;
        if (origTicks >= TicksPerYear) time += $"{years} year(s), ";
        if (origTicks >= TicksPerQuadrum) time += $"{quadrums} quadrum(s), ";
        if (origTicks >= TicksPerDay) time += $"{days} day(s), ";
        time += $"{hours} hour(s)";

        return time;
    }

}
