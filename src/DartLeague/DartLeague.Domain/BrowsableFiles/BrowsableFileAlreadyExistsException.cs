using System;

namespace DartLeague.Domain.BrowsableFiles
{
    public class BrowsableFileAlreadyExistsException : Exception
    {
        public BrowsableFileAlreadyExistsException()
        {
        }

        public BrowsableFileAlreadyExistsException(string message) : base(message)
        {
        }

        public BrowsableFileAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}