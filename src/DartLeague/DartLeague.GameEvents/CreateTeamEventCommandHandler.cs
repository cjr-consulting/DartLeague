using DartLeague.Common.Messaging;
using System;

namespace DartLeague.GameEvents
{
    public class CreateTeamEventCommandHandler : ICommandHandler<CreateTeamEventCommand, TeamEvent>
    {
        public CreateTeamEventCommandHandler()
        {

        }

        public TeamEvent Handle(CreateTeamEventCommand command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var teamEvent = new TeamEvent(command.Name);
            return teamEvent;
        }
    }

    public class CreateTeamEventCommand : ICommand
    {
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }

    public class TeamEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime? DateStart { get; }
        public DateTime? DateEnd { get; }

        public TeamEvent(string name)
        {
            Name = name;
        }
    }

    /*
     * add team
     * add schedule
     * add match to schedule
     * add match results
     *
     */
}
