using System;
using System.Collections.Generic;
using System.Linq;

namespace DartLeague.Web.Areas.Manage.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class SeasonBoardIndexViewModel
    {
        public List<SeasonBoardListViewModel> BoardMembers { get; set; }
        public bool IsCopyAvailable { get; set; }
    }
}
