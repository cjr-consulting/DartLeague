using HashidsNet;
using System;
using System.Linq;

namespace DartLeague.Web
{
    public static class NumberObfuscation
    {
        private const string salt = "This is my salty salt";
        private static readonly Hashids hashids = new Hashids(salt);


        public static string Encode(int number)
        {
            return hashids.Encode(number);
        }

        public static int Decode(string hash)
        {
            return hashids.Decode(hash)[0];
        }
    }
}
