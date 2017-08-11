using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Domain.BrowsableFiles
{
    public class BrowsableFileOptions
    {
        public BrowsableFileOptions()
        {
            Storage = "/Storage";
        }

        public string Storage { get; set; }
    }
}
