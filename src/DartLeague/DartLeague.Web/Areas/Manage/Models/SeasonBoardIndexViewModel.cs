using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SeasonBoardIndexViewModel
    {
        public List<SeasonBoardListViewModel> BoardMembers { get; set; }
        public bool IsCopyAvailable { get; set; }
    }
}
