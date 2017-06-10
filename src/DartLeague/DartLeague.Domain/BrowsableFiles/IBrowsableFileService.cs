using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartLeague.Domain.BrowsableFiles
{
    public interface IBrowsableFileService
    {
        Task<int> Add(BrowsableFile file);
        Task<BrowsableFile> Get(int id);
    }
}
