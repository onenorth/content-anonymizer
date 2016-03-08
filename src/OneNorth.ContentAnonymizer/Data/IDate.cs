using System;

namespace OneNorth.ContentAnonymizer.Data
{
    public interface IDate
    {
        DateTime Past();
        DateTime Future();
    }
}