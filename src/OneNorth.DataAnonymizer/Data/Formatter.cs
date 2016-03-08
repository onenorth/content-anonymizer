namespace OneNorth.DataAnonymizer.Data
{
    public class Formatter : IFormatter
    {
        private static readonly IFormatter _instance = new Formatter();
        public static IFormatter Instance { get { return _instance; } }

        private Formatter()
        {
            
        }

        public string ReplaceSymbols(string value)
        {
            var chars = value.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                switch (chars[i])
                {
                    case '#':
                        chars[i] = (char)RandomProvider.GetThreadRandom().Next(48, 57); // 0 - 9
                        break;
                }
            }
            return new string(chars);
        }
    }
}