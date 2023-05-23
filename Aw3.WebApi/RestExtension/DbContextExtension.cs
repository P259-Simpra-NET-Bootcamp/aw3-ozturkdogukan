using Aw3.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Aw3.WebApi.RestExtension
{
    public static class DbContextExtension
    {
        public static void AddDbContextExtension(this IServiceCollection services, IConfiguration Configuration)
        {
            var dbType = Configuration.GetConnectionString("DbType");
            if (dbType == "SQL")
            {
                var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
                services.AddDbContext<Aw3DbContext>(opts =>
                opts.UseSqlServer(dbConfig));
            }
            else if (dbType == "PostgreSql")
            {
                var dbConfig = Configuration.GetConnectionString("PostgreSqlConnection");
                services.AddDbContext<Aw3DbContext>(opts =>
                  opts.UseNpgsql(dbConfig));
            }

        }
    }

}
