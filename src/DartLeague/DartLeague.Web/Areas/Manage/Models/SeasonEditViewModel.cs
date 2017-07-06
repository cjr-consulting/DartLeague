using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SeasonEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
    }
}