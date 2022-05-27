using EFCoreDemo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// A konfiguráció alapértelmezetten pl. az appsettings.json fájlból jön.
var connectionString = builder.Configuration.GetConnectionString("DogFarmDB");
// Beregisztráljuk a DbContext típusunkat a DI-ba.
builder.Services.AddDbContext<DogFarmDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// A "/" végpontra saját kezelőfüggvényt regisztrálunk.
// Nem tesszük using-ba a DbContext-et, mert erről (így a kapcsolat bontásáról)
// a keretrendszer fog gondoskodni; ezzel a megoldással a DI-tól kérjük el.
app.MapGet("/", async (DogFarmDbContext dbContext) =>
{
    var dog = await dbContext.Dogs.FirstOrDefaultAsync();
    return Results.Ok(dog == null ? ":(" : $"{dog.Name} says: bark-bark!");
});

app.Run();
