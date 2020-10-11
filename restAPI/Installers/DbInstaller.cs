using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using restAPI.Data;
using restAPI.Services;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restAPI.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var config = new StringBuilder(configuration["ConnectionStrings:DbConnection"]);
            string conn = config.Replace("ENVID", configuration["DB_UserId"])
                .Replace("ENVPW", configuration["DB_PW"]).ToString();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(conn));

            services.AddDefaultIdentity<IdentityUser>()
               .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IPostService, PostService>();
        }
    }
}
