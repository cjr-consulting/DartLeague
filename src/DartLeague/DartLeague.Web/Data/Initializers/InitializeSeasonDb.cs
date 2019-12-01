using DartLeague.Repositories.SeasonData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Data.Initializers
{
    public static class InitializeSeasonDb
    {
        public static async Task InitializeAsync(IServiceScope serviceScope)
        {
            Contract.Requires(serviceScope != null);

            var context = serviceScope.ServiceProvider.GetService<SeasonContext>();
            var positions = await context.BoardPositions.ToListAsync();
            AddPosition(context, positions, position: "President", order: 1);
            AddPosition(context, positions, position: "Vice-President", order: 2);
            AddPosition(context, positions, position: "Treasurer", order: 3);
            AddPosition(context, positions, position: "Officer", order: 4);
            AddPosition(context, positions, position: "Statistician", order: 5);
            AddPosition(context, positions, position: "Special Events Coordinator", order: 6);
            await context.SaveChangesAsync();
        }

        private static void AddPosition(SeasonContext context, List<BoardPosition> positions, string position, int order)
        {
            if (!positions.Any(x => x.Name == position))
                context.BoardPositions.Add(new BoardPosition
                {
                    Name = position,
                    Order = order
                });
        }
    }
}