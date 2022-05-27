using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDemo.Entities;

/// <summary>
/// A kutyákat tároló táblát reprezentáló osztály.
/// </summary>
public class Dog
{
    /// <summary>
    /// Az adatbázis által automatikusan generált azonosító.
    /// </summary>
    // Megadhatjuk, hogy az oszlopot az adatbázis generálja, számítja, vagy kézzel adjuk meg.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key] // Az entitás kulcsa
    public int Id { get; set; }

    /// <summary>
    /// A kutya neve (adatbázisban NVARCHAR(max) típus lesz).
    /// </summary>
    [StringLength(32)] // A kutya neve maximum 32 karakter hosszú lehet, így NVARCHAR(32) típust kap.
    [Column("DogName")] // Az adatbázis oszlop nevét adhatjuk vele meg.
    public string Name { get; set; }

    /// <summary>
    /// A kutya születési dátuma (ha ismert).
    /// </summary>
    [Required] // Az oszlop/tulajdonság kötelező (de a modellben null értéket is felvehet majd).
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// A kutyához tartozó tulajdonlások.
    /// </summary>
    [InverseProperty(nameof(DogOwnership.Dog))] // A navigáció másik oldalát jelezzük vele
    public ICollection<DogOwnership> DogOwnerships { get; set; }
    
    [NotMapped] // Ez a tulajdonság nem lesz az adatbázistábla része.
    public int Barks { get; set; }

    [Timestamp] // Ez a tulajdonság fogja jelezni az adatsor verzióját, ami minden módosításkor frissül.
    [ConcurrencyCheck] // Ezt az oszlopot konkurenciakezeléshez fogjuk alkalmazni.
    public byte[] RowVersion { get; set; }

    public override string ToString() => 
        $"({Id}) {Name}{(BirthDate != null ? $" [D.o.B: {BirthDate.Value:yyyy-MM-dd}]" : "")}";
}
