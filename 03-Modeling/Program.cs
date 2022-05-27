using EFCoreDemo;
using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using var dbContext = new DogFarmDbContext();

var exit = false;
while (!exit)
{
    Console.WriteLine("What do you want to do?\n[new|list|update|delete|exit] [dog|person|ownership]");
    var command = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    Type entityType;
    object entity;
    //using var dbContext = new DogFarmDbContext();
    switch (command[0])
    {
        case "new":
            entityType = GetEntityType();
            entity = Activator.CreateInstance(entityType);
            SetPropertiesOrSave(entity);
            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();
            break;
        case "list":
            entityType = GetEntityType();
            var entities = (IQueryable<object>)typeof(DogFarmDbContext).GetMethod(nameof(DogFarmDbContext.Set), Array.Empty<Type>()).MakeGenericMethod(entityType).Invoke(dbContext, null);
            if (!await entities.AnyAsync())
                Console.WriteLine($"No entities stored of type {entityType}.");
            else
                foreach (var item in entities)
                    Console.WriteLine(item);
            break;
        case "update":
            entityType = GetEntityType();
            entity = await dbContext.FindAsync(entityType, ReadId());
            SetPropertiesOrSave(entity);
            await dbContext.SaveChangesAsync();
            break;
        case "delete":
            entityType = GetEntityType();
            entity = await dbContext.FindAsync(entityType, ReadId());
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
            break;
        case "exit":
            exit = true;
            break;
        default:
            Console.WriteLine($"Unknown command: '{command}'.");
            break;
    }

    static int ReadId()
    {
        Console.Write("Id: ");
        _ = int.TryParse(Console.ReadLine().Trim(), out var id);
        return id;
    }
    static void SetPropertiesOrSave(object entity)
    {
        var exit = false;
        while (!exit)
        {
            Console.WriteLine("[set {propertyName}={jsonValue}|save]");
            Console.WriteLine($"Properties:\n  {string.Join("\n  ", entity.GetType().GetProperties().Select(p => $"{p.Name}={p.GetValue(entity)} ({p.PropertyType})"))}");
            var command = Console.ReadLine();
            var commandSplit = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            switch (commandSplit[0])
            {
                case "save":
                    exit = true;
                    break;
                case "set":
                    var setCommand = commandSplit[1].Split('=');
                    var property = entity.GetType().GetProperty(setCommand[0]);
                    property.SetValue(entity, property.PropertyType.IsAssignableFrom(typeof(int)) ? Convert.ChangeType(setCommand[1], property.PropertyType) : JsonSerializer.Deserialize($"\"{setCommand[1]}\"", property.PropertyType));
                    break;
                default:
                    Console.WriteLine($"Unknown command: '{command}'.");
                    break;
            }
        }

    }
    Type GetEntityType() =>
        command.Length <= 1 ? throw new InvalidOperationException("No entity provided.") :
        command[1] switch
        {
            "dog" => typeof(Dog),
            "person" => typeof(Person),
            "ownership" => typeof(DogOwnership),
            _ => throw new InvalidOperationException($"Unknown entity type: {command[1]}")
        };
}
