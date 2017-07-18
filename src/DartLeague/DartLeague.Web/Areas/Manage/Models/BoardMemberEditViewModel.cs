using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class BoardMemberEditViewModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int PositionId { get; set; }

        public List<SelectListItem> Positions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Members { get; set; } = new List<SelectListItem>();
    }
}