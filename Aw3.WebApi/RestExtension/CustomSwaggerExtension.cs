using Microsoft.OpenApi.Models;

namespace Aw3.WebApi.RestExtension
{
    public static class CustomSwaggerExtension
    {
        public static void AddCustomSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen();
        }
    }
}