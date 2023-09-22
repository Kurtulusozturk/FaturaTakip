using Microsoft.EntityFrameworkCore;

namespace FaturaTakip.Models
{
    public class FaturaTakipDbContext : DbContext
    {
        public FaturaTakipDbContext(DbContextOptions<FaturaTakipDbContext> options) : base(options)
        {
        }

        // DbSet'ler burada tanımlanır
        public DbSet<Sirketler>? Sirketler { get; set; }
        public DbSet<Musteriler>? Musteriler { get; set; }
        public DbSet<Faturalar>? Faturalar { get; set; }
    }
}
