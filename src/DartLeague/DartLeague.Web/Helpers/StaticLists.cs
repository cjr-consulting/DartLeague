using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DartLeague.Web.Helpers
{
    public static class StaticLists
    {
        public static readonly List<SelectListItem> DartEventTypes = new List<SelectListItem>
        {
            new SelectListItem {Text = "GTDL Event", Value = "1"},
            new SelectListItem {Text = "Charity Dart Event", Value = "2"},
            new SelectListItem {Text = "Regional Event", Value = "3"},
            new SelectListItem {Text = "GTDL Sponsored Event", Value = "4"},
            new SelectListItem {Text = "GTDL All Stars", Value = "5"},
            new SelectListItem {Text = "GTDL Player Event", Value = "6"},
            new SelectListItem {Text = "Charity Event", Value = "7"},
            new SelectListItem {Text = "DPNY Series", Value = "8"},
            new SelectListItem {Text = "CDC Series", Value = "9"},
            new SelectListItem {Text = "DPNJ Series", Value = "10"},
            new SelectListItem {Text = "Qualifier", Value = "11"}
        };
    }
}