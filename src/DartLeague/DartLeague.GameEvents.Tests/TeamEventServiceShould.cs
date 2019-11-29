using Shouldly;
using System;
using Xunit;

namespace DartLeague.GameEvents.Tests
{
    public class Given_a_new_team_to_create
    {
        private CreateTeamEventCommand EVENT_DATA = new CreateTeamEventCommand();

        [Fact]
        public void When_creating_a_team_with_required_configuration()
        {
            var handler = new CreateTeamEventCommandHandler();

            var teamEvent = handler.Handle(EVENT_DATA);

            teamEvent.Id.ShouldNotBeNull();
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
