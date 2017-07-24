using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SeasonTeamEditViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }
        public int BannerFileId { get; set; }
        public int LogoFileId { get; set; }
        public int TeamPictureFileId { get; set; }

        public List<SeasonTeamPlayerViewModel> Players { get; set; } = new List<SeasonTeamPlayerViewModel>();
        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Members { get; set; }
    }

    public class SeasonTeamPlayerViewModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int RoleId { get; set; }
    }
}
