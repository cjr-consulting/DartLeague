using DartLeague.Domain.BrowsableFiles;
using DartLeague.Repositories.LeagueData;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        readonly LeagueContext _leagueContext;
        readonly IBrowsableFileService _browsableFileService;

        public HomeController(LeagueContext leagueContext, IBrowsableFileService browsableFileService)
        {
            _leagueContext = leagueContext;
            _browsableFileService = browsableFileService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "LeagueLink");
        }
    }
}