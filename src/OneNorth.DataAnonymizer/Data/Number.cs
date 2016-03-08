namespace OneNorth.DataAnonymizer.Data
{
    public class Number : INumber
    {
        private static readonly INumber _instance = new Number();
        public static INumber Instance { get { return _instance; } }

        private Number()
        {
            
        }

        public int Integer()
        {
            return RandomProvider.GetThreadRandom().Next(100);
        }
    }
}