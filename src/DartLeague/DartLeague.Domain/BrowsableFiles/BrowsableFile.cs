using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DartLeague.Domain.BrowsableFiles
{
    public class BrowsableFile
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public Stream Stream { get; set; }
    }
}
