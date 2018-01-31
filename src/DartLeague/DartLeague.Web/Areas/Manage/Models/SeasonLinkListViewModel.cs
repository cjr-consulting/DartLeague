using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SeasonLinkListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LinkType { get; set; }
        public string Url { get; set; }
        public int Order { get; internal set; }
    }
}
