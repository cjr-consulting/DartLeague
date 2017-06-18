using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartLeague.Domain.BrowsableFiles
{
    public interface IBrowsableFileService
    {
        Task<int> AddAsync(BrowsableFile file);
        Task<BrowsableFile> GetAsync(int id);
        Task<BrowsableFile> GetByCategoryAndNameAsync(string category, string fileName);
        Task DeleteAsync(int Id);
    }
}