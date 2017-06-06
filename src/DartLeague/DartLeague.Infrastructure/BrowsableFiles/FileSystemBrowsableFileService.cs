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
        public async Task Add(BrowsableFile file)
        {
            if (await _leagueContext.BrowsableFiles
                .AnyAsync(x => x.FileName == file.FullName && x.Category == file.Category))
                throw new BrowsableFileAlreadyExistsException($"The file {file.FullName} already exists.");
            
            var f = new EF.BrowsableFile
            {
                FileName = file.FullName,
                Category = file.Category,
                ContentType = file.ContentType,
                RelativePath = ""
            };

            _leagueContext.BrowsableFiles.Add(f);
            _leagueContext.SaveChanges();

            var relativePath = $"{f.Id}-{f.Category}-{f.FileName}";
            var filePath = Path.Combine(RootPath, relativePath);
            f.RelativePath = relativePath;
            _leagueContext.SaveChanges();

            using (var fileStream = File.Create(filePath))
            {
                file.Stream.Seek(0, SeekOrigin.Begin);
                await file.Stream.CopyToAsync(fileStream);
            }
        }

        public async Task<BrowsableFile> Get(int id)
        {
            var f = await _leagueContext.BrowsableFiles.FirstOrDefaultAsync(x => x.Id == id);
            var file = new BrowsableFile
            {
                Id = f.Id,
                FileName = Path.GetFileNameWithoutExtension(f.FileName),
                Stream = File.OpenRead(Path.Combine(RootPath, f.RelativePath)),
                ContentType = f.ContentType,
                Category = f.Category,
                FullName = f.FileName,
                Extension = Path.GetExtension(f.FileName)
            };

            return file;
        }
    }
}
