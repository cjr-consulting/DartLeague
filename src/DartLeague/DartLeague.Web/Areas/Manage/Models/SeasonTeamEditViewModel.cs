﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string BannerUrl { get; set; }
        public int LogoFileId { get; set; }
        public string LogoFileUrl { get; set; }
        public int TeamPictureFileId { get; set; }
        public string TeamFileUrl { get; set; }
        public List<SeasonTeamPlayerListViewModel> Players { get; set; } = new List<SeasonTeamPlayerListViewModel>();
        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Members { get; set; }
    }
}