using System;

namespace OneNorth.DataAnonymizer.Data
{
    public class Date : IDate
    {
        private static readonly IDate _instance = new Date();
        public static IDate Instance { get { return _instance; } }

        private Date()
        {
            
        }

        public DateTime Past()
        {
            var days = RandomProvider.GetThreadRandom().Next(0, 3650);
            return DateTime.Now.AddDays(-days);
        }

        public DateTime Future()
        {
            var days = RandomProvider.GetThreadRandom().Next(0, 365);
            return DateTime.Now.AddDays(days);
        }
    }
}