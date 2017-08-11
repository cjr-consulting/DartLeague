using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SeasonTeamCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Sponsor")]
        public int SponsorId { get; set; }
        [Required]
        public string Abbreviation { get; set; }
        [DisplayName("Banner Image")]
        public int BannerFileId { get; set; }
        [DisplayName("Logo Image")]
        public int LogoFileId { get; set; }
        [DisplayName("Team Picture")]
        public int TeamPictureFileId { get; set; }
        [DisplayName("Captain")]
        [Required]
        public int CaptainMemberId { get; set; }

        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Members { get; set; }
        public List<SelectListItem> Sponsors { get; set; }
    }
}
