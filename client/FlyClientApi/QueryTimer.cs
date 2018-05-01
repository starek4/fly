using System;

namespace FlyClientApi
{
    public static class QueryTimer
    {
        public static int TimeBetweenQuery { get; } = 5;

        public static bool CompareTimes(DateTime time)
        {
            if (DateTime.Now.Subtract(time).TotalSeconds < 0)
                return true;
            return DateTime.Now.Subtract(time).TotalSeconds < TimeBetweenQuery * 2;
        }
    }
}
