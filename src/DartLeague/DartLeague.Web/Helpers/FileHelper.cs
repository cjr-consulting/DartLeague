using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

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
