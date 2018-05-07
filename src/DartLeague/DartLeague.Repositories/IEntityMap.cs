using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories
{
    interface IEntityMap<TDbContext> 
        where TDbContext : DbContext
    {
    }
}
