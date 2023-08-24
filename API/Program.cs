using API.Extensions;
using API.Middleware;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Services abstracted out into extension methods
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Middleware to handle exceptions and send back to client
app.UseMiddleware<ExceptionMiddleware>();

// Redirects to error controller when non existent end point is visited
app.UseStatusCodePagesWithReExecute("/errors/{0}");

// Using swagger service
app.UseSwaggerDocumentation();

// Server static files
app.UseStaticFiles();

// Using created CORS policy
app.UseCors("CorsPolicy");

// Using authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Accesses the local scope of the program and pulls out the contexts and logger from the services container. Logger is logging against Program
// Using keyword is used as the scope is IDisposable, and must be disposed of after use
// Pulls out userManager to create a user with seed method
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

// Creates the database based on migration, and uses the DataContextSeed SeedAsync  and AppIdentityDbContextSeed SeedUsersAsync methods to seed the databases when the program starts
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await DataContextSeed.SeedAsync(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
