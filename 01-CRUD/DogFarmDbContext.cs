using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo
{
    /// <summary>
    /// Az adatbáziskapcsolatot, -műveleteket reprezentáló osztály.
    /// </summary>
    public class DogFarmDbContext : DbContext
    {
        /// <summary>
        /// Az opciók határozzák meg például, hol található az adatbázis 
        /// (pl. connectionString).
        /// Érdemes nem beégetni az osztályhoz, hogy tesztelhető maradjon.
        /// </summary>
        public DogFarmDbContext() 
            : base(new DbContextOptionsBuilder().UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=DogFarmDB;Trusted_Connection=True;"
                ).Options) { }

        /// <summary>
        /// A kutyák tábláját reprezentáló tulajdonság.
        /// </summary>
        public DbSet<Dog> Dogs { get; set; }
    }
}
