using Microsoft.EntityFrameworkCore;
using QLess.Models;

namespace QLess.Db
{
    public class QLessDbContext : DbContext
    {
        public QLessDbContext(DbContextOptions<QLessDbContext> options) : base(options) { }

        public DbSet<TransportCard> TransportCards { get; set; }
        public DbSet<TransportLine> TransportLines { get; set; }
        public DbSet<TransportStation> TransportStations { get; set; }
        public DbSet<TransportFare> TransportFares { get; set; }
    }
}
