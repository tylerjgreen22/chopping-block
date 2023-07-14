using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext to the dependency injection container to be used throughout the project. Connection string retrieved from appsettings.json
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Adds the implementation of the PostRespository as a Scoped service that will survive for the life of the Http call
builder.Services.AddScoped<IPostRepository, PostRepository>();

// Adds the implementation of the PostRespository as a Scoped service that will survive for the life of the Http call. Uses typeof due to generic types
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Adds the auto mapper service to be used throughtout application. Pulls mapping profiles from Assembly
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

// Accesses the local scope of the program and pulls out the context and logger from the services container. Logger is logging against Program
// Using keyword is used as the scope is IDisposable, and must be disposed of after use
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<DataContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

// Creates the database based on migration, and used the DataContextSeed SeedAsync method to seed the database when the program starts
try
{
    await context.Database.MigrateAsync();
    await DataContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
