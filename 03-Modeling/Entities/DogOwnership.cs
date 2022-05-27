namespace EFCoreDemo.Entities;

public class DogOwnership
{
    /// <summary>
    /// A tulajdonlás azonosítója.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A tulajdonolt kutya azonosítója.
    /// </summary>
    public int DogId { get; set; }

    /// <summary>
    /// A tulajdonolt kutyára mutató referencia, "navigation property".
    /// </summary>
    public Dog Dog { get; set; }

    /// <summary>
    /// A tulajdonolt gazdi azonosítója.
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// A tuljadonolt gazdira mutató navigation property.
    /// </summary>
    public Person Person { get; set; }

    public override string ToString() => $"({Id}): Dog {DogId} - Person {PersonId}";
}
