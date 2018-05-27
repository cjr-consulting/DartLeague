namespace DartLeague.Web.Models
{
    public class PlayerActivityModel
    {
        public int Id { get; set; }
        public string DartEventName { get; set; }
        public string SpecificEventName { get; set; }
        public string MemberName { get; set; }
        public string Finished { get; set; }
    }
}