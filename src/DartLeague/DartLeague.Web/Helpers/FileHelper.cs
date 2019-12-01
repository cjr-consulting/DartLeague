using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Helpers
{
    public static class FileHelper
    {
        public static string CleanString([NotNull] string value)
        {
            Contract.Requires(value != null, "Clean string requires a string.");

            return string.Join("", value.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries)).Replace(' ', '-');
        }
    }
}
