using Tournaments.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournaments.DbContexts
{
    public class TournamentDbContext : DbContext
    {
        public TournamentDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TeamDTO> Teams { get; set; }
    }
}
