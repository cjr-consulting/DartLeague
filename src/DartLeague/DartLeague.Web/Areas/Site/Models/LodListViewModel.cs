using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DartLeague.Web.Areas.Site.Models
{
    public class LodListViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
        public List<LodViewModel> LuckOfTheDraws { get; set; } = new List<LodViewModel>();
    }

    public class LodViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileId { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }
        public bool Active { get; set; }    
    }
}