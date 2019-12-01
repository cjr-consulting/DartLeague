using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DartLeague.Web.Areas.Manage.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
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