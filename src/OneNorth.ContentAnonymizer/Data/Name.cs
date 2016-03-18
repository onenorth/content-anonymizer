using System;
using System.Linq;
using OneNorth.ContentAnonymizer.Data.Locales;

namespace OneNorth.ContentAnonymizer.Data
{
    public class Name : IName
    {
        private static readonly IName _instance = new Name();
        public static IName Instance { get { return _instance; } }

        private Name()
        {
            
        }

        public string FirstName(ILocale locale, string original = null)
        {
            if (string.IsNullOrEmpty(original))
                return locale.NameFirst.Random();

            var firstLetter = original[0].ToString();
            var filtered = locale.NameFirst.Where(x => x.StartsWith(firstLetter, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filtered.Any())
                filtered = locale.NameFirst;

            return filtered.Random();
        }

        public string LastName(ILocale locale, string original = null)
        {
            if (string.IsNullOrEmpty(original))
                return locale.NameLast.Random();

            var firstLetter = original[0].ToString();
            var filtered = locale.NameLast.Where(x => x.StartsWith(firstLetter, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filtered.Any())
                filtered = locale.NameLast;

            return filtered.Random();
        }

        public string Prefix(ILocale locale)
        {
            return locale.NamePrefix.Random();
        }

        public string Suffix(ILocale locale)
        {
            return locale.NameSuffix.Random();
        }

        public string FullName(ILocale locale)
        {
            var firstName = FirstName(locale);
            var lastName = LastName(locale);

            switch (RandomProvider.GetThreadRandom().Next(8))
            {
                case 0:
                    return Prefix(locale) + " " + firstName + " " + lastName;
                case 1:
                    return firstName + " " + lastName + " " + Suffix(locale);
                case 2:
                    return Prefix(locale) + " " + firstName + " " + lastName + " " + Suffix(locale);
            }
            return firstName + " " + lastName;
        }
    }
}