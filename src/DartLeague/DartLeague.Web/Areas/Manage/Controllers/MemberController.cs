using DartLeague.Repositories.LeagueData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MemberController : Controller
    {
        private LeagueContext _leagueContext;

        public MemberController(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public IActionResult Index()
        {
            ViewData["LeagueNavPage"] = "Members";
            var playersList = new MembersListViewModel();
            playersList.Members = _leagueContext.Members.Select(x =>
                new MemberViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    CellPhone = x.CellPhone,
                    ShirtSize = x.ShirtSize
                }).ToList();

            return View(playersList);
        }

        public IActionResult Create()
        {
            var model = new MemberViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberViewModel player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var p = new Member
                    {
                        FirstName = player.FirstName,
                        LastName = player.LastName,
                        Email = player.Email,
                        CellPhone = player.CellPhone,
                        HomePhone = player.HomePhone,
                        AcceptEmail = player.AcceptEmail,
                        AcceptText = player.AcceptText,
                        LeagueId = 1,
                        Nickname = player.Nickname,
                        Notes = "",
                        Address1 = player.Address1,
                        Address2 = player.Address2,
                        City = player.City,
                        State = player.State,
                        Zip = player.Zip,
                        ShirtSize = player.ShirtSize
                    };
                    _leagueContext.Members.Add(p);
                    await _leagueContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch(DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(player);
        }
        
        [Route("manage/player/{id}/edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var p = await _leagueContext.Members.FirstOrDefaultAsync(x => x.Id == id);
            if (p == null)
                return NotFound();

            var player = new MemberViewModel
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                CellPhone = p.CellPhone,
                HomePhone = p.HomePhone,
                AcceptEmail = p.AcceptEmail,
                AcceptText = p.AcceptText,
                Nickname = p.Nickname,
                Address1 = p.Address1,
                Address2 = p.Address2,
                City = p.City,
                State = p.State,
                Zip = p.Zip,
                ShirtSize = p.ShirtSize
            };

            return View(player);
        }
        
        [HttpPost("manage/player/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, MemberViewModel player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var p = await _leagueContext.Members.FirstOrDefaultAsync(x => x.Id == id);
                    p.FirstName = player.FirstName;
                    p.LastName = player.LastName;
                    p.Email = player.Email;
                    p.CellPhone = player.CellPhone;
                    p.HomePhone = player.HomePhone;
                    p.AcceptEmail = player.AcceptEmail;
                    p.AcceptText = player.AcceptText;
                    p.LeagueId = 1;
                    p.Nickname = player.Nickname;
                    p.Notes = "";
                    p.Address1 = player.Address1;
                    p.Address2 = player.Address2;
                    p.City = player.City;
                    p.State = player.State;
                    p.Zip = player.Zip;
                    p.ShirtSize = player.ShirtSize;

                    await _leagueContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(player);
        }

        [Route("manage/player/{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _leagueContext.Members.FirstOrDefaultAsync(x => x.Id == id);
            if (p == null)
                return RedirectToAction("Index");

            _leagueContext.Members.Remove(p);
            _leagueContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}