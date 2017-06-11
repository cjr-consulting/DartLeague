using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DartLeague.Domain.BrowsableFiles;
using Microsoft.EntityFrameworkCore;
using EF = DartLeague.Repositories.LeagueData;

namespace DartLeague.Infrastructure.BrowsableFiles
{
    public class FileSystemBrowsableFileService : IBrowsableFileService
    {
        public static string RootPath;

        private EF.LeagueContext _leagueContext;
        
        public FileSystemBrowsableFileService(EF.LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }
        public async Task<int> Add(BrowsableFile file)
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
            _leagueContext.SaveChanges();
            
            var filePath = Path.Combine(RootPath, f.RelativePath);

            using (var fileStream = File.Create(filePath))
            {
                file.Stream.Seek(0, SeekOrigin.Begin);
                await file.Stream.CopyToAsync(fileStream);
            }

            return f.Id;
        }

        public async Task<BrowsableFile> Get(int id)
        {
            var f = await _leagueContext.BrowsableFiles.FirstOrDefaultAsync(x => x.Id == id);
            if (f == null)
                throw new BrowsableFileNotFoundException($"{id} doesn't exists");

            var file = new BrowsableFile
            {
                Id = f.Id,
                FileName = f.FileName,
                Stream = File.OpenRead(Path.Combine(RootPath, f.RelativePath)),
                ContentType = f.ContentType,
                Category = f.Category,
                Extension = Path.GetExtension(f.RelativePath)
            };

            return file;
        }

        public async Task<BrowsableFile> GetByCategoryAndName(string category, string fileName)
        {
            var f = await _leagueContext.BrowsableFiles.FirstOrDefaultAsync(x => x.Category == category && x.FileName == fileName);
            if (f == null)
                throw new BrowsableFileNotFoundException($"{fileName} wasn't found in the category {category}");

            var file = new BrowsableFile
            {
                Id = f.Id,
                FileName = f.FileName,
                Stream = File.OpenRead(Path.Combine(RootPath, f.RelativePath)),
                ContentType = f.ContentType,
                Category = f.Category,
                Extension = Path.GetExtension(f.RelativePath)
            };

            return file;
        }
    }
}
