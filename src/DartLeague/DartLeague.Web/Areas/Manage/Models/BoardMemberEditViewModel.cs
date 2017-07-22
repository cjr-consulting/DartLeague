using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class BoardMemberEditViewModel
    {
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int PositionId { get; set; }

        public List<SelectListItem> Positions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Members { get; set; } = new List<SelectListItem>();
    }
}