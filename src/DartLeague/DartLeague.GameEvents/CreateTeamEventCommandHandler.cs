using System;

namespace DartLeague.GameEvents
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Marker Interface")]
    public interface ICommand { }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Marker Interface")]
    public interface IQuery { }

    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand
    {
        TResult Handle(TCommand command);
    }

    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }

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

    public class EventSchedule
    {

    }


    /*
     * add team
     * add schedule
     * add match to schedule
     * add match results
     *
     */
}
