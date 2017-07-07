using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Models
{
    public abstract class SeasonManagementRootViewModelBase
    {
        public SeasonEditViewModel SeasonEdit { get; set; }
    }

    public class SeasonManagementRootViewModel<T> : SeasonManagementRootViewModelBase
    {
        public T Data { get; set; }
    }
}