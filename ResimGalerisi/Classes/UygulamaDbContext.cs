using Microsoft.EntityFrameworkCore;

namespace ResimGalerisi.Classes
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {

        }
        public DbSet<Resim> resimler =>Set<Resim>();
    }
}
