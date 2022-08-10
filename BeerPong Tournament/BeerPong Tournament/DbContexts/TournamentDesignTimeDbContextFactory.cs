using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournaments.DbContexts
{
    public class TournamentDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TournamentDbContext>
    {
        public TournamentDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=tournament.db").Options;

            return new TournamentDbContext(options);
        }
    }
}
