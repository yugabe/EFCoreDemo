using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo;

/// <summary>
/// Az adatbáziskapcsolatot, -műveleteket reprezentáló osztály.
/// </summary>
public class DogFarmDbContext : DbContext
{
    public DogFarmDbContext(DbContextOptions<DogFarmDbContext> options)
        : base(options) { }

    /// <summary>
    /// Az opciók határozzák meg például, hol található az adatbázis 
    /// (pl. connectionString).
    /// Érdemes nem beégetni az osztályhoz, hogy tesztelhető maradjon.
    /// </summary>
    public DogFarmDbContext()
        : base(new DbContextOptionsBuilder().UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=DogFarmDB;Trusted_Connection=True;"
            ).Options)
    { }

    /// <summary>
    /// A kutyák tábláját reprezentáló tulajdonság.
    /// </summary>
    public DbSet<Dog> Dogs { get; set; }

    /// <summary>
    /// A személyek táblája.
    /// </summary>
    public DbSet<Person> People { get; set; }

    /// <summary>
    /// A tulajdonlások táblája.
    /// </summary>
    public DbSet<DogOwnership> DogOwnerships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dog>(dog => // A Dog entitást konfiguráljuk.
        {
            dog.HasKey(d => d.Id); // Az entitás kulcsa

            dog.Property(d => d.Id)
                .UseIdentityColumn(); // Megadhatjuk, hogy az oszlopot az adatbázis generálja, számítja, vagy kézzel adjuk meg.

            dog.Property(d => d.Name)
                .HasColumnName("DogName") // Az adatbázis oszlop nevét adhatjuk vele meg.
                .HasMaxLength(32); // A kutya neve maximum 32 karakter hosszú lehet, így NVARCHAR(32) típust kap.

            dog.Property(d => d.BirthDate)
                .IsRequired(); // Az oszlop/tulajdonság kötelező (de a modellben null értéket is felvehet majd).

            dog.Property(d => d.RowVersion)
                .IsRowVersion() // Ez a tulajdonság fogja jelezni az adatsor verzióját, ami minden módosításkor frissül.
                .IsConcurrencyToken(); // Ezt az oszlopot konkurenciakezeléshez fogjuk alkalmazni.

            dog.Ignore(d => d.Barks); // Ez a tulajdonság nem lesz az adatbázistábla része.

            dog.HasMany(p => p.DogOwnerships) // A navigáció másik oldalát jelezzük vele...
                .WithOne(o => o.Dog) // ...megadva, hogy melyik tulajdonság jelzi ezt a kapcsolatot a másik oldalon...
                .HasForeignKey(o => o.DogId) // ...mit használunk a külső kulcs tárolására...
                .HasPrincipalKey(d => d.Id); // ...és melyik mezőre mutat itt a külső kulcs.
        });
    }
}
