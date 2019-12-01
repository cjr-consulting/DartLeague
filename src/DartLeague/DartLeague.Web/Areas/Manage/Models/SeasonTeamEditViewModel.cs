using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DartLeague.Web.Areas.Manage.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class SeasonTeamEditViewModel
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Sponsor")]
        public int SponsorId { get; set; }
        [Required]
        public string Abbreviation { get; set; }
        [DisplayName("Banner Image")]
        public int BannerFileId { get; set; }
        public string BannerUrl { get; set; }
        [DisplayName("Logo Image")]
        public int LogoFileId { get; set; }
        public string LogoFileUrl { get; set; }
        [DisplayName("Team Picture")]
        public int TeamPictureFileId { get; set; }
        public string TeamFileUrl { get; set; }
        public List<SeasonTeamPlayerListViewModel> Players { get; set; } = new List<SeasonTeamPlayerListViewModel>();

        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Members { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Sponsors { get; set; } = new List<SelectListItem>();
    }
}