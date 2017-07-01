using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SeasonLinkViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LinkType { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
    }
}
