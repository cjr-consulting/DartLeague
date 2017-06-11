using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashidsNet;

namespace DartLeague.Web
{
    public static class NumberObfuscation
    {
        private static string salt = "This is my salty salt salt";
        private static Hashids hashids = new Hashids(salt);


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
