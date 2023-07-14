using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    // Extension method that provides all services
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Add DbContext to the dependency injection container to be used throughout the project. Connection string retrieved from appsettings.json
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            // Adds the implementation of the PostRespository as a Scoped service that will survive for the life of the Http call
            services.AddScoped<IPostRepository, PostRepository>();

            // Adds the implementation of the PostRespository as a Scoped service that will survive for the life of the Http call. Uses typeof due to generic types
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Adds the auto mapper service to be used throughtout application. Pulls mapping profiles from Assembly
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Uses LINQ to extract the errors from the model state and send those errors back as a bad request object
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}