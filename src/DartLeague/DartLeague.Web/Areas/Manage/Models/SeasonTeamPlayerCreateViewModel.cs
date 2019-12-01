using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DartLeague.Web.Areas.Manage.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
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


        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Members { get; set; } = new List<SelectListItem>();
    }
}