using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.DAL
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BenimProjemDbContext>
    {
        public  BenimProjemDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BenimProjemDbContext>();
            builder.UseSqlServer("Server=.;DataBase=BenimProjem;Trusted_Connection=True;");
            return new BenimProjemDbContext(builder.Options);
        }
    }
}
