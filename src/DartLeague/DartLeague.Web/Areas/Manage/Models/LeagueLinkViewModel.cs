namespace DartLeague.Web.Areas.Manage.Models
{
    public class LeagueLinkViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LinkType { get; set; }
        public int FileId { get; set; }
        public string Url { get; set; }
    }
}