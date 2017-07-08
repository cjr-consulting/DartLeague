using System;

namespace DartLeague.Web.Areas.Site.Models
{
    public class PagePartViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Html { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}