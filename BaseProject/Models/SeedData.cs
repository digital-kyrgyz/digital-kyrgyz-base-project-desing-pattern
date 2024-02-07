using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using BaseProject.Models;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await SeedRoles(roleManager);
        await SeedUsers(userManager);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "User" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Create the roles and seed them to the database
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    private static async Task SeedUsers(UserManager<AppUser> userManager)
    {
        if (userManager.FindByEmailAsync("admin@example.com").Result == null)
        {
            var user = new AppUser()
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                // Add other properties as needed
            };

            var result = await userManager.CreateAsync(user, "YourPassword");
            
            if (result.Succeeded)
            {
                // Assign the user to the Admin role if creation is successful
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}