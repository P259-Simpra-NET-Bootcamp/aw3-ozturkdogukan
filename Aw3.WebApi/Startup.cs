
using Aw3.Data.Context;
using Aw3.DataAccess.UnitOfWork;
using Aw3.WebApi.RestExtension;
using System.Text.Json.Serialization;

namespace Aw3.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCustomSwaggerExtension();
            services.AddMapperExtension();
            services.AddDbContextExtension(Configuration);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using IServiceScope serviceScope = serviceScopeFactory.CreateScope();
            Aw3DbContext dbContext = serviceScope.ServiceProvider.GetService<Aw3DbContext>();
            dbContext.Database.EnsureCreated();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            // add auth 
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
