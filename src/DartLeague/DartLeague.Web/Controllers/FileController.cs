using System.Threading.Tasks;
using DartLeague.Domain.BrowsableFiles;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.Controllers
{
    public class FileController : Controller
    {
        private readonly IBrowsableFileService _browsableFileService;

        public FileController(IBrowsableFileService browsableFileService)
        {
            _browsableFileService = browsableFileService;

        }

        [Route("file/{Id}")]
        public async Task<IActionResult> Index(string id)
        {
            var numberId = NumberObfuscation.Decode(id);
            var file = await _browsableFileService.Get(numberId);
            return new FileStreamResult(file.Stream, file.ContentType);
        }
    }
}