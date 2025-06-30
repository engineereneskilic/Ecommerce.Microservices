using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

using System.IO;


namespace FreeCourse.Services.Order.Infrastructure
{
    public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContext CreateDbContext(string[] args)
        {
            // appsettings.json yolunu belirle
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json") // veya "appsettings.json"
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();

            // Bağlantı cümlesini oku
            var connectionString = configuration.GetConnectionString("SqlServer");

            optionsBuilder.UseSqlServer(connectionString);

            return new OrderDbContext(optionsBuilder.Options);
        }
    }
}
