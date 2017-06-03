using System.ComponentModel;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class Player
    {
        public string ID { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Nickname { get; set; }
        public string Email { get; set; }

        [DisplayName("Home Phone")]
        public string HomePhone { get; set; }

        [DisplayName("Cell Phone")]
        public string CellPhone { get; set; }

        [DisplayName("Shirt Size")]
        public string ShirtSize { get; set; }

        [DisplayName("Street")]
        public string Address1 { get; set; }

        [DisplayName("Apartment / Building Number")]
        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        [DisplayName("Accept Email")]
        public bool AcceptEmail { get; set; }

        [DisplayName("Accept Text")]
        public bool AcceptText { get; set; }
    }
}