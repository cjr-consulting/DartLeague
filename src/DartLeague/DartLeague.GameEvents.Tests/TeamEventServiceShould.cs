using Shouldly;
using System;
using Xunit;

namespace DartLeague.GameEvents.Tests
{
    public class Given_a_new_team_event
    {
        private GameEvent teamEvent = new GameEvent();
        private Team firstTeam = new Team(1, "Team 1", new Venue("Venue Name", StreetAddress.Empty()));

        private Team duplicateFirstTeam = new Team(1, "Team 1", new Venue("Venue Name", StreetAddress.Empty()));

        [Fact]
        public void When_adding_a_team_Then_success()
        {
            teamEvent.AddTeam(firstTeam);

            teamEvent.GetTeams().ShouldHaveSingleItem();
        }

        [Fact]
        public void When_adding_a_duplicate_team_Then_ignore()
        {
            teamEvent.AddTeam(firstTeam);
            teamEvent.AddTeam(duplicateFirstTeam);

            teamEvent.GetTeams().ShouldHaveSingleItem();
        }
    }

    public class Given_an_event_is_exists
    {
        [Fact]
        public void When_adding_new_team_ensure_not_duplicated()
        {


        }
    }
}
