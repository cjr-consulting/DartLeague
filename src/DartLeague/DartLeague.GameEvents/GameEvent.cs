using DartLeague.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DartLeague.GameEvents
{
    public class GameEvent
    {
        private List<Schedule> schedules = new List<Schedule>();
        private List<Team> teams = new List<Team>();

        public void AddTeam(Team team)
        {
            if (teams.Any(x => string.Equals(x.Name, team.Name, StringComparison.CurrentCultureIgnoreCase)))
            {
                return;
            }

            teams.Add(team);
        }

        public IList<Team> GetTeams()
        {
            return teams.ToArray();
        }
    }


    public class Team : Entity<int>
    {
        public string Name { get; }
        public Venue HomeVenue { get; }

        public Team(int id, string name, Venue homeVenue)
        {
            Id = id;
            Name = name;
            HomeVenue = homeVenue;
        }
    }

    public class Venue
    {
        public string Name { get; }
        public StreetAddress Address { get; }

        public Venue(string name, StreetAddress address)
        {
            Name = name;
            Address = address;
        }
    }

    public class StreetAddress
    {
        public string Street { get; }
        public string Street2 { get; }
        public string City { get; }
        public string State { get; }
        public string Zip { get; }
        public StreetAddress(string street, string street2, string city, string state, string zip)
        {
            Street = street;
            Street2 = street2;
            City = city;
            State = state;
            Zip = zip;
        }

        public static StreetAddress Empty()
        {
            return new StreetAddress(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }
    }

    public class Player
    {
        public string Name { get; }

    }

    public class Schedule
    {
        public int Id { get; }
        public string Name { get; }
        public int SortId { get; set; }
    }

    public class WeeklySchedule
    {
        public string DayOfWeek { get; set; }

        private List<ScheduledMatch> matches = new List<ScheduledMatch>();

        public void AddMatch(ScheduledMatch match)
        {
            matches.Add(match);
        }
    }

    public class ScheduledMatch
    {
        public DateTime Date { get; }
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }
        public Venue Venue { get; }

        public MatchRule Rules { get; }
    }

    public class MatchRule
    {
        public string Name { get; }
    }
}
