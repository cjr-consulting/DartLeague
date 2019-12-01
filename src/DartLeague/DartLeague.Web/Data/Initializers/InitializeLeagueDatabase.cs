using DartLeague.Repositories.LeagueData;
using Microsoft.Extensions.DependencyInjection;

namespace DartLeague.Web.Data.Initializers
{
    public static class InitializeLeagueDb
    {

        public static void Initialize(IServiceScope serviceScope)
        {
            serviceScope.ServiceProvider.GetService<LeagueContext>();
        }
    }
}