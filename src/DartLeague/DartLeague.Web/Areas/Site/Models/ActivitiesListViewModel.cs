using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Site.Models
{
    public class ActivitiesListViewModel
    {
        public List<ActivityViewModel> Activities { get; set; } = new List<ActivityViewModel>();
    }

    public class ActivityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileId { get; set; }

        [DataType(DataType.Date)]
        public DateTime ActivityDate { get; set; }
        public bool Active { get; set; }

    }
}
