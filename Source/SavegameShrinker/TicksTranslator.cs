namespace BetterRimworlds
{
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
}