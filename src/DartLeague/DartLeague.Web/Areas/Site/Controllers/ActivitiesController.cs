using System.Linq;
using DartLeague.Domain.BrowsableFiles;
using DartLeague.Web.Areas.Site.Models;
using Microsoft.AspNetCore.Mvc;
using EF = DartLeague.Repositories.LeagueData;

namespace DartLeague.Web.Areas.Site.Controllers
{
    [Area("Site")]
    public class ActivitiesController : Controller
    {
        private readonly IBrowsableFileService _browsableFileService;
        private readonly Repositories.LeagueData.LeagueContext _leagueContext;

        public ActivitiesController(EF.LeagueContext leagueContext, IBrowsableFileService browseableFileService)
        {
            _leagueContext = leagueContext;
            _browsableFileService = browseableFileService;
        }

        public IActionResult Index()
        {
            var model = new ActivitiesListViewModel();
            model.Activities = _leagueContext.Activities
                .Select(x => new ActivityViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    FileId = x.FileId > 0 ? NumberObfuscation.Encode(x.FileId) : string.Empty,
                    Active = x.Active
                }).ToList();
            return View();
        }
    }
}