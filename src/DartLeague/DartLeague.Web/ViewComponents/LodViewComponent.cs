using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.ViewComponents.Models;
using DartLeague.Web.ViewComponents.Models.Lod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.ViewComponents
{
    public class LodViewComponent : ViewComponent
    {
        private readonly LeagueContext _leagueContext;

        public LodViewComponent(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lodEvents = await GetLodEvents();
            var model = new LodEventsListModel
            {
                LodEvents = lodEvents
            };
            return View(model);
        }

        private async Task<List<LodModel>> GetLodEvents()
        {
            return await _leagueContext.LuckofTheDraws
                .Where(x => x.Active)
                .Select(x => new LodModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageFileId = x.FileId > 0 ? NumberObfuscation.Encode(x.FileId) : string.Empty
                }).ToListAsync();
        }
    }
}