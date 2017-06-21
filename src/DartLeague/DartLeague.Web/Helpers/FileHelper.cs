using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Helpers
{
    public static class FileHelper
    {
        public static string CleanString(string value)
        {
            return string.Join("", value.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries)).Replace(' ', '-');
        }
    }
}
