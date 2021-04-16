using Application.Interfaces.Services.Domain;
using Application.Interfaces.Services.Standard;
using Application.Services.Domain;
using Application.Services.Standard;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC.Services
{
    public static class ServiceIoC
    {
        public static void ApplicationServicesIoC(this IServiceCollection services)
        {
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminstratorService, AdministratorService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IMovieService, MovieService>();
        }
    }
}
