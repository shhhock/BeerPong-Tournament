using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerPong_Tournament.DbContexts
{
    public class TournamentDbContextFactory
    {
        private readonly string _connectionString;

        public TournamentDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TournamentDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new TournamentDbContext(options);
        }
    }
}
