using System.Text.RegularExpressions;
using OneNorth.ContentAnonymizer.Data.Locales;

namespace OneNorth.ContentAnonymizer.Data
{
    public class Internet : IInternet
    {
        private static readonly IInternet _instance = new Internet();
        public static IInternet Instance { get { return _instance; } }

        private readonly IName _name;

        private Internet() : this(
            Name.Instance)
        {
            
        }

        internal Internet(
            IName name)
        {
            _name = name;
        }

        public string Email(ILocale locale, string firstName = null, string lastName = null, string provider = null)
        {
            provider = provider ?? locale.InternetExampleEmail.Random();
            return UserName(locale, firstName, lastName) + "@" + provider;
        }

        public string UserName(ILocale locale, string firstName = null, string lastName = null)
        {
            string result = null;
            firstName = firstName ?? _name.FirstName(locale);
            lastName = lastName ?? _name.LastName(locale);

            switch (RandomProvider.GetThreadRandom().Next(2))
            {
                case 0:
                    result = firstName + RandomProvider.GetThreadRandom().Next(99).ToString("G");
                    break;
                case 1:
                    result = firstName + new [] {".", "_"}.Random() + lastName;
                    break;

                case 2:
                    result = firstName + new[] { ".", "_" }.Random() + lastName + RandomProvider.GetThreadRandom().Next(99).ToString("G");
                    break;
            }

            if (result == null)
                return string.Empty;

            result = result.Replace("'", "");
            result = result.Replace(" ", "");
            return result;
        }

        public string Url(ILocale locale)
        {
            return string.Format("http://{0}/", DomainName(locale));
        }

        private string DomainName(ILocale locale)
        {
            return DomainWord(locale) + "." + DomainSuffix(locale);
        }

        private string DomainSuffix(ILocale locale)
        {
            return locale.InternetDomainSuffix.Random();
        }

        private string DomainWord(ILocale locale)
        {
            return Regex.Replace(locale.NameFirst.Random(), "[^A-Z0-9._%+-]", "", RegexOptions.IgnoreCase).ToLower();
        }
    }
}