using CategoryWebsite_MS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsitesCategoryTests
{
    public static class FakeData
    {
        public static async Task<ApplicationContext> InitializeContextAsync()
        {
            try
            {
                ApplicationContext context;

                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

                var builder = new DbContextOptionsBuilder<ApplicationContext>();

                builder.UseSqlServer($"Server=tcp:127.0.0.1, 1433;Initial Catalog=ShoppingWebsites_WebsitesCategory_tests_{Guid.NewGuid()};User Id=sa;Password=ShoppingWebsitesTests.v1", b => b.MigrationsAssembly("CategoryWebsite_MS"))
                        .UseInternalServiceProvider(serviceProvider);

                context = new(builder.Options);
                await context.Database.MigrateAsync();

                //if (seed) context = await SeedFakeData(context);

                return context;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
