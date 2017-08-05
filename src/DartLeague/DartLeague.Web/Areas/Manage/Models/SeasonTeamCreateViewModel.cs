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
        public string Abbreviation { get; set; }
        public int BannerFileId { get; set; }
        public int LogoFileId { get; set; }
        public int TeamPictureFileId { get; set; }

        [DisplayName("Captain")]
        [Required]
        public int CaptainMemberId { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Members { get; set; }
    }
}
