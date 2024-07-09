using Microsoft.EntityFrameworkCore;

namespace PracenjeTroskova.Models
{
    public class AplikacijaBP:DbContext
    {
        public AplikacijaBP(DbContextOptions opcije):base(opcije)
        {
            
        }

        public DbSet<Transakcija> Transakcije { get; set; }

        public DbSet<Kategorija> Kategorije { get; set; }


    }
}
