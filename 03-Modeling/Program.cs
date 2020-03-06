using EFCoreDemo.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace EFCoreDemo
{
    public class Program
    {
        public static void Main()
        {
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("What do you want to do?\n[new|list|update|delete|exit] [dog|person|ownership]");
                var command = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Type entityType;
                object entity;
                using var dbContext = new DogFarmDbContext();
                switch (command[0])
                {
                    case "new":
                        entityType = GetEntityType();
                        entity = Activator.CreateInstance(entityType);
                        SetPropertiesOrSave(entity);
                        dbContext.Add(entity);
                        dbContext.SaveChanges();
                        break;
                    case "list":
                        entityType = GetEntityType();
                        var entities = (IEnumerable)typeof(DogFarmDbContext).GetMethod(nameof(DogFarmDbContext.Set)).MakeGenericMethod(entityType).Invoke(dbContext, null);
                        if (entities.Cast<object>().Count() == 0)
                            Console.WriteLine($"No entities stored of type {entityType}.");
                        else foreach (var item in entities)
                                Console.WriteLine(item);
                        break;
                    case "update":
                        entityType = GetEntityType();
                        entity = dbContext.Find(entityType, ReadId());
                        SetPropertiesOrSave(entity);
                        dbContext.SaveChanges();
                        break;
                    case "delete":
                        entityType = GetEntityType();
                        entity = dbContext.Find(entityType, ReadId());
                        dbContext.Remove(entity);
                        dbContext.SaveChanges();
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
                    int.TryParse(Console.ReadLine().Trim(), out var id);
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
        }
    }
}
