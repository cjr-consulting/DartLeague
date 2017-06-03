using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.LeagueData
{
    public partial class Player
    {
        public int Id { get; set; }
        public bool AcceptEmail { get; set; }
        public bool AcceptText { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CellPhone { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string HomePhone { get; set; }
        public string LastName { get; set; }
        public int LeagueId { get; set; }
        public string Nickname { get; set; }
        public string Notes { get; set; }
        public string ShirtSize { get; set; }
        public string State { get; set; }
        public int UserId { get; set; }
        public string Zip { get; set; }
    }
}
