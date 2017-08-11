namespace DartLeague.Repositories.SeasonData
{
    public class TeamPlayer
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int MemberId { get; set; }
        public int RoleId { get; set; }

        public Team Team { get; set; }
    }
}