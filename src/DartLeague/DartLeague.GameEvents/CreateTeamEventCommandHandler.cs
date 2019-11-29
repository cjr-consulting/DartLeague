using System;
using System.Collections.Generic;

namespace DartLeague.GameEvents
{
    public class CreateTeamEventCommandHandler
    {
        public CreateTeamEventCommandHandler()
        {

        }
        
        public TeamEvent Handle(CreateTeamEventCommand teamEvent)
        {
            return new TeamEvent();
        }
    }

    public class CreateTeamEventCommand
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

        public TeamEvent()
        {

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
