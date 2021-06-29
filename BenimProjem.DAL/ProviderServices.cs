using BenimProjem.Entities.Authentications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace BenimProjem.DAL
{
    public static class ProviderServices
    {
        public static void AddDbContextExtension(this IServiceCollection services)
        {
            services.AddDbContext<BenimProjemDbContext>(options => options.UseSqlServer("Server=.;DataBase=BenimProjem;Trusted_Connection=True;"));
            
        }

        public static ServiceProvider GetProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContextExtension();
            return services.BuildServiceProvider();
        }

        public static BenimProjemDbContext GetContext()
        {
            return (BenimProjemDbContext)GetProvider().GetService(typeof(BenimProjemDbContext));
        }

    }
}
