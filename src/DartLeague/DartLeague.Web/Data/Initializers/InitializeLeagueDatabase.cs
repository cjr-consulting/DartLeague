using DartLeague.Repositories.LeagueData;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.Contracts;

namespace DartLeague.Web.Data.Initializers
{
    public static class InitializeLeagueDb
    {

        public static void Initialize(IServiceScope serviceScope)
        {
            Contract.Requires(serviceScope != null);
            serviceScope.ServiceProvider.GetService<LeagueContext>();
        }
    }
}