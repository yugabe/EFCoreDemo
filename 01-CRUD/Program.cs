using EFCoreDemo.Entities;
using System;
using System.Linq;

namespace EFCoreDemo
{
    public class Program
    {
        public static void Main()
        {
            do
            {
                Console.WriteLine("Provide a name for your dog:");
                var name = Console.ReadLine();
                var myDog = new Dog { Name = name };
                AddDogToDatabase(myDog);
                PrintDogs();

                Console.WriteLine("Provide the dog's birth date:");
                DateTime.TryParse(Console.ReadLine(), out var birthDate);
                UpdateDogBirthDate(myDog.Id, birthDate);
                PrintDogs();

                Console.WriteLine("Press Enter to delete the dog.");
                if (Console.ReadLine() == "")
                    DeleteDog(myDog.Id);
                PrintDogs();

                Console.WriteLine("Press Enter to exit.");
                if (Console.ReadLine() == "")
                    break;
            }
            while (true);
        }

        private static void AddDogToDatabase(Dog newDog)
        {
            using var dbContext = new DogFarmDbContext();
            dbContext.Dogs.Add(newDog);
            dbContext.SaveChanges();
        }

        private static void PrintDogs()
        {
            using var dbContext = new DogFarmDbContext();
            var dogs = dbContext.Dogs.ToList();
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

        private static void UpdateDogBirthDate(int dogId, DateTime? birthDate)
        {
            using var dbContext = new DogFarmDbContext();
            var dog = dbContext.Dogs.Find(dogId);
            dog.BirthDate = birthDate;
            dbContext.SaveChanges();
        }

        private static void DeleteDog(int dogId)
        {
            using var dbContext = new DogFarmDbContext();
            var dog = dbContext.Dogs.Find(dogId);
            dbContext.Dogs.Remove(dog);
            dbContext.SaveChanges();
        }
    }
}
