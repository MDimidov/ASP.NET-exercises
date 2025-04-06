using Practice.Contracts;
using Practice.ModelBinders;
using Practice.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }

        public static IServiceCollection AddCustomModelBinders(this IServiceCollection services)
        {
            services.AddControllersWithViews(opt =>
            {
                opt.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
            });
            return services;
        }
    }
}
