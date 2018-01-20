using System.Collections.Generic;

namespace Satobot.SatoshiMines
{
    public class ValueCollection : Dictionary<string, string>
    {
        public ValueCollection() : base(new NoCompare()) { }

        private class NoCompare : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return false;
            }

            public int GetHashCode(string obj)
            {
                return 0;
            }
        }
    }
}
