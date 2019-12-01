using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DartLeague.Web.Areas.Site.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
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
