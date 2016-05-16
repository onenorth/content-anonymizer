using System;

namespace OneNorth.ContentAnonymizer.Data
{
    public class Datetime : IDate
    {
        private static readonly IDate _instance = new Datetime();
        public static IDate Instance { get { return _instance; } }

        private Datetime()
        {

        }

        public DateTime Past()
        {
            var days = RandomProvider.GetThreadRandom().Next(0, 3650);
            var minutes = RandomProvider.GetThreadRandom().Next(0, 1440);
            return DateTime.Now.AddDays(-days).AddMinutes(-minutes);
        }

        public DateTime Recent()
        {
            var days = RandomProvider.GetThreadRandom().Next(0, 30);
            var minutes = RandomProvider.GetThreadRandom().Next(0, 1440);
            return DateTime.Now.AddDays(-days).AddMinutes(-minutes);
        }

        public DateTime Future()
        {
            var days = RandomProvider.GetThreadRandom().Next(0, 365);
            var minutes = RandomProvider.GetThreadRandom().Next(0, 1440);
            return DateTime.Now.AddDays(days).AddMinutes(minutes);
        }
    }
}