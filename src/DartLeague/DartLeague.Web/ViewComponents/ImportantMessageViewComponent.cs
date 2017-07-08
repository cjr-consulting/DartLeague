using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.Areas.Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.ViewComponents
{
    public class ImportantMessageViewComponent : ViewComponent
    {
        private readonly LeagueContext _leagueContext;

        public ImportantMessageViewComponent(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var greetingPagePart =
                await _leagueContext.PageParts.FirstOrDefaultAsync(x => x.Name == "Important Message");
            if (greetingPagePart == null || string.IsNullOrEmpty(greetingPagePart.Html))
                return View(new PagePartViewModel {Html = "Welcome to the GTDL Home Page!"});
            return View(new PagePartViewModel {Html = greetingPagePart.Html});
        }
    }
}