using System.Collections.Generic;

namespace DartLeague.Web.Areas.Manage.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class MembersListViewModel
    {
        public List<MemberViewModel> Members { get; set; }       
    }
}