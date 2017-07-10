using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SeasonLinkViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int LinkType { get; set; }
        [Required]
        [Display(Name = "Display Order")]
        public int Order { get; set; }
        public string Url { get; set; }
        public string FileLink { get; internal set; }
    }
}
