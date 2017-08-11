using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SeasonTeamPlayerEditViewModel
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }

        public List<SelectListItem> Roles { get; set; }
    }
}
