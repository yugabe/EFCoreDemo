namespace EFCoreDemo.Entities;

/// <summary>
/// A személy táblát reprezentáló entitás.
/// </summary>
public class Person
{
    /// <summary>
    /// A személy azonosítója.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A személy neve.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// A személyhez tartozó tulajdonlások.
    /// </summary>
    public ICollection<DogOwnership> DogOwnerships { get; set; }

    public override string ToString() => $"({Id}) {Name}";
}
