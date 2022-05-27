namespace EFCoreDemo.Entities;

/// <summary>
/// A kutyákat tároló táblát reprezentáló osztály.
/// </summary>
public class Dog
{
    /// <summary>
    /// Az adatbázis által automatikusan generált azonosító.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A kutya neve (adatbázisban NVARCHAR(max) típus lesz).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// A kutya születési dátuma (ha ismert).
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// A kutyához tartozó tulajdonlások.
    /// </summary>
    public ICollection<DogOwnership> DogOwnerships { get; set; }

    public override string ToString() => 
        $"({Id}) {Name}{(BirthDate != null ? $" [D.o.B: {BirthDate.Value:yyyy-MM-dd}]" : "")}";
}
