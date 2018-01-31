using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Remotion.Linq.Clauses;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SeasonTeamPlayerCreateViewModel
    {
        public int TeamId { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int RoleId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        
        public string CellPhone { get; set; }
        public string ShirtSize { get; set; }


        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Members { get; set; }
    }
}