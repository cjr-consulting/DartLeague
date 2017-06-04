using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DartLeague.Web.Areas.Site.Models
{
    public class DartEventViewModel
    {
        public int Id { get; set; }

        [DisplayName("Event Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Contact")]
        [Required]
        public string EventContact { get; set; }

        [DisplayName("Additional Contact")]
        public string EventContact2 { get; set; }

        [DisplayName("Event Date")]
        [Required]
        public DateTime EventDate { get; set; }

        [DisplayName("Event End Date")]
        public DateTime? EventEndDate { get; set; }

        [DisplayName("Event Type")]
        [Required]
        public int EventTypeId { get; set; }

        public bool IsTitleEvent { get; set; }

        [DisplayName("Registration Start Time")]
        public string RegistrationStartTime { get; set; }

        [DisplayName("Registration End Time")]
        public string RegistrationEndTime { get; set; }

        [DisplayName("Darts Start Time")]
        public string DartStart { get; set; } 

        [DisplayName("Dart Type")]
        public string DartType { get; set; }

        [DisplayName("Host Name")]
        public string HostName { get; set; }

        [DisplayName("Host Phone")]
        [Phone]
        public string HostPhone { get; set; }

        [DisplayName("Host URL")]
        public string HostUrl { get; set; }

        [DisplayName("Location Name")]
        public string LocationName { get; set; }

        [DisplayName("Address")]
        public string Address1 { get; set; }
        
        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        [DisplayName("Web Site Url")]
        public string Url { get; set; }

        [DisplayName("Facebook")]
        public string FacebookUrl { get; set; }
        
        [DisplayName("Map")]
        public string MapUrl { get; set; }

        public string Description { get; set; }

    }
}