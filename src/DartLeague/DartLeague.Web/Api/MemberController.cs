using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.Api
{
    [Produces("application/json")]
    [Route("api/Member")]
    public class MemberController : Controller
    {
        private readonly LeagueContext _leagueContext;

        public MemberController(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }
        public async Task<IActionResult> Get(string search)
        {
            var members =
                _leagueContext.Members.Where(x => search == null || x.FirstName.Contains(search) || x.LastName.Contains(search) ||
                                                        x.Nickname.Contains(search) || x.Email.Contains(search));
            return Json(new ApiResult<List<Member>>()
            {
                Data = await members.ToListAsync()
            });
        }
    }

    public class ApiResult<T>
    {
        public string ErrorMessage;
        public T Data;
    }
}