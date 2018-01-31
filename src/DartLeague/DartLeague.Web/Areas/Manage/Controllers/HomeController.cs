using DartLeague.Domain.BrowsableFiles;
using DartLeague.Repositories.LeagueData;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
        private LeagueContext _leagueContext;
        private IBrowsableFileService _browsableFileService;

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