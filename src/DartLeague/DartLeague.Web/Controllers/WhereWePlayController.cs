using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class WhereWePlayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}