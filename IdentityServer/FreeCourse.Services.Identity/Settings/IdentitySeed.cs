using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreeCourse.Services.Identity.Settings
{
    public static class IdentitySeed
    {
        public static async Task SeedDefaultUserAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var users = await userManager.Users.ToListAsync();

            if (!users.Any())
            {
                var user = new IdentityUser
                {
                    UserName = "enes",
                    Email = "enes@gmail.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "EnesKilic123!");

                if (result.Succeeded)
                {
                    Console.WriteLine("✅ Varsayılan kullanıcı eklendi: enes / EnesKilic123!");
                }
                else
                {
                    Console.WriteLine("❌ Kullanıcı eklenemedi:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"- {error.Description}");
                    }
                }
            }
        }
    }
}
