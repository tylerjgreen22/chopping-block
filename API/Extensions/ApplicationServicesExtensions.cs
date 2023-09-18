using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Images;
using Infrastructure.Security;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    // Extension class that provides all services
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
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });

            // Add redis connection multiplexer
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });

            // Adding generic repo and unit of work for dependency injection
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Adding scoped services for dependency injection
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();

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

            // CORS policy to accept requests from client
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            // Add Http context accessor and user accessor
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();

            // Cloudinary accessor and adding cloudinary information to dependency injection container
            services.AddScoped<IImageAccessor, ImageAccessor>();
            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));

            return services;
        }
    }
}