using Caravan.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Caravan.Api.Controllers.LayerConfigurations
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this WebApplicationBuilder builder)
        {
            string connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
