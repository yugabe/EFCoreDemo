using EFCoreDemo;
using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;

do
{
    Console.WriteLine("Provide a name for your dog:");
    var name = Console.ReadLine();
    var myDog = new Dog { Name = name };
    await AddDogToDatabaseAsync(myDog);
    await PrintDogsAsync();

    Console.WriteLine("Provide the dog's birth date:");
    _ = DateTime.TryParse(Console.ReadLine(), out var birthDate);
    await UpdateDogBirthDateAsync(myDog.Id, birthDate);
    await PrintDogsAsync();

    Console.WriteLine("Press Enter to delete the dog.");
    if (Console.ReadLine() == "")
        await DeleteDogAsync(myDog.Id);
    await PrintDogsAsync();

    Console.WriteLine("Press Enter to exit.");
    if (Console.ReadLine() == "")
        break;
}
while (true);

static async Task AddDogToDatabaseAsync(Dog newDog)
{
    using var dbContext = new DogFarmDbContext();
    dbContext.Dogs.Add(newDog);
    await dbContext.SaveChangesAsync();
}

static async Task PrintDogsAsync()
{
    using var dbContext = new DogFarmDbContext();
    var dogs = await dbContext.Dogs.ToListAsync();
    if (dogs.Count == 0)
    {
        Console.WriteLine("No dogs are stored in the database.");
    }
    else
    {
        foreach (var dog in dogs)
            Console.WriteLine(dog);
    }
    Console.WriteLine();
}

static async Task UpdateDogBirthDateAsync(int dogId, DateTime? birthDate)
{
    using var dbContext = new DogFarmDbContext();
    var dog = await dbContext.Dogs.FindAsync(dogId);
    dog.BirthDate = birthDate;
    await dbContext.SaveChangesAsync();
}

static async Task DeleteDogAsync(int dogId)
{
    using var dbContext = new DogFarmDbContext();
    // Nem szükséges lekérnünk a DB-től, egyszerűen ID alapján törölhetjük.
    dbContext.Dogs.Remove(new() { Id = dogId });
    await dbContext.SaveChangesAsync();
}
