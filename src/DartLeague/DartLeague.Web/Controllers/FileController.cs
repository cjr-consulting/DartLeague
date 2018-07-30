using System.Threading.Tasks;
using DartLeague.Domain.BrowsableFiles;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FileController : Controller
    {
        private readonly IBrowsableFileService _browsableFileService;

        public FileController(IBrowsableFileService browsableFileService)
        {
            _browsableFileService = browsableFileService;

        }

        [Route("file/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            try
            {
                var numberId = NumberObfuscation.Decode(id);
                var file = await _browsableFileService.GetAsync(numberId);
                return new FileStreamResult(file.Stream, file.ContentType);
            }
            catch (BrowsableFileNotFoundException)
            {
                return NotFound();
            }
        }

        [Route("file/{category}/{fileName}")]
        public async Task<IActionResult> Index(string category, string fileName)
        {
            try
            {
                var file = await _browsableFileService.GetByCategoryAndNameAsync(category, fileName);
                return new FileStreamResult(file.Stream, file.ContentType);
            }
            catch (BrowsableFileNotFoundException)
            {
                return NotFound();
            }
        }
    }
}