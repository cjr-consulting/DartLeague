namespace DartLeague.Web.Areas.Site.Models
{
    public class DartEventResultViewModel
    {
        public int Id { get; set; }

        public bool IsTitleEvent { get; set; } = true;

        public string SpecificEventName { get; set; }

        public string MemberName { get; set; }

        public int Finished { get; set; }
    }
}