using System;

namespace OneNorth.DataAnonymizer.Data
{
    public interface IDate
    {
        DateTime Past();
        DateTime Future();
    }
}