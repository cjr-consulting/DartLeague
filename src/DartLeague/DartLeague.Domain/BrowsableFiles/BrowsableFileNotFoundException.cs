using System;

namespace DartLeague.Domain.BrowsableFiles
{
    public class BrowsableFileNotFoundException : Exception
    {
        public BrowsableFileNotFoundException()
        {
        }

        public BrowsableFileNotFoundException(string message) : base(message)
        {
        }

        public BrowsableFileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}