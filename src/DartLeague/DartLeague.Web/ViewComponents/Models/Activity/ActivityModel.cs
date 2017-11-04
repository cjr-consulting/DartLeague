using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.ViewComponents.Models.Activity
{
    public class ActivityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FileId { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Active { get; set; }
    }
}
