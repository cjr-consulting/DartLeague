using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Internal;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SponsorViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        public string Type { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [Required]
        [DisplayName("Street")]
        public string Address1 { get; set; }

        [DisplayName("Apt./Bldg.")]
        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        [DisplayName("Web Site Url")]
        public string Url { get; set; }

        [DisplayName("Facebook")]
        [Url]
        public string FacebookUrl { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Map")]
        [Url]
        public string MapUrl { get; set; }

        public string Description { get; set; }

        public string Comments { get; set; }

        public IEnumerable<TeamViewModel> Teams{get;set;}
    }

    public class TeamViewModel
    {
    }
}