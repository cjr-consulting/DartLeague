using System;

namespace DartLeague.Repositories.LeagueData
{
    public partial class DartEvent
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string DartStart { get; set; }
        public string DartType { get; set; }
        public string Description { get; set; }
        public string EventContact { get; set; }
        public string EventContact2 { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public int EventTypeId { get; set; }
        public string FacebookUrl { get; set; }
        public string HostName { get; set; }
        public string HostPhone { get; set; }
        public string HostUrl { get; set; }
        public int ImageFileId { get; set; }
        public bool IsTitleEvent { get; set; }
        public string LocationName { get; set; }
        public string MapUrl { get; set; }
        public string Name { get; set; }
        public string PosterFile { get; set; }
        public int PosterFileId { get; set; }
        public string RegistrationEndTime { get; set; }
        public string RegistrationStartTime { get; set; }
        public string State { get; set; }
        public string Url { get; set; }
        public string Zip { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
