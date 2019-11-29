using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SponsorViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        public string Type { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [DisplayName("Street")]
        public string Address1 { get; set; }

        [DisplayName("Apt./Bldg.")]
        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        [DisplayName("Web Site Url")]
        public string Url { get; set; }

        [DisplayName("Facebook")]
        public string FacebookUrl { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Map")]
        public string MapUrl { get; set; }

        public string Description { get; set; }

        public string Comments { get; set; }
    }
}