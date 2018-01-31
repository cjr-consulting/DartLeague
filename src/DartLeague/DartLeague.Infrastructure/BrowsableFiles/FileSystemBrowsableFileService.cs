using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Domain.BrowsableFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EF = DartLeague.Repositories.LeagueData;

namespace DartLeague.Infrastructure.BrowsableFiles
{
    public class FileSystemBrowsableFileService : IBrowsableFileService
    {
        private EF.LeagueContext _leagueContext;
        private readonly IOptions<BrowsableFileOptions> _options;

        public FileSystemBrowsableFileService(EF.LeagueContext leagueContext, IOptions<BrowsableFileOptions> options)
        {
            _leagueContext = leagueContext;
            _options = options;
        }
        public async Task<int> AddAsync(BrowsableFile file)
        {
            if (_leagueContext.BrowsableFiles.Any(x => x.Category == file.Category && x.FileName == file.FileName))
                throw new BrowsableFileAlreadyExistsException($"{file.FileName} already exists in the category {file.Category}");

            var f = new EF.BrowsableFile
            {
                FileName = file.FileName,
                Category = file.Category,
                ContentType = file.ContentType,
                RelativePath = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{file.Extension}"
            };

            _leagueContext.BrowsableFiles.Add(f);
            await _leagueContext.SaveChangesAsync();
            
            var filePath = Path.Combine(_options.Value.Storage, f.RelativePath);

            using (var fileStream = File.Create(filePath))
            {
                file.Stream.Seek(0, SeekOrigin.Begin);
                await file.Stream.CopyToAsync(fileStream);
            }

            return f.Id;
        }

        public async Task<BrowsableFile> GetAsync(int id)
        {
            var f = await _leagueContext.BrowsableFiles.FirstOrDefaultAsync(x => x.Id == id);
            if (f == null)
                throw new BrowsableFileNotFoundException($"{id} doesn't exists");

            var file = new BrowsableFile
            {
                Id = f.Id,
                FileName = f.FileName,
                Stream = File.OpenRead(Path.Combine(_options.Value.Storage, f.RelativePath)),
                ContentType = f.ContentType,
                Category = f.Category,
                Extension = Path.GetExtension(f.RelativePath)
            };

            return file;
        }

        public async Task<BrowsableFile> GetByCategoryAndNameAsync(string category, string fileName)
        {
            var f = await _leagueContext.BrowsableFiles.FirstOrDefaultAsync(x => x.Category == category && x.FileName == fileName);
            if (f == null)
                throw new BrowsableFileNotFoundException($"{fileName} wasn't found in the category {category}");

            var file = new BrowsableFile
            {
                Id = f.Id,
                FileName = f.FileName,
                Stream = File.OpenRead(Path.Combine(_options.Value.Storage, f.RelativePath)),
                ContentType = f.ContentType,
                Category = f.Category,
                Extension = Path.GetExtension(f.RelativePath)
            };

            return file;
        }

        public async Task DeleteAsync(int id)
        {
            var file = await _leagueContext.BrowsableFiles.FirstOrDefaultAsync(x => x.Id == id);
            if (file == null)
                return;

            File.Delete(file.RelativePath);
            _leagueContext.BrowsableFiles.Remove(file);
            await _leagueContext.SaveChangesAsync();
        }
    }
}
