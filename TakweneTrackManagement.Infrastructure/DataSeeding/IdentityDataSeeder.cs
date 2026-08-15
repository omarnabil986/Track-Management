using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Contracts;
using TakweneTrackManagement.Infrastructure.Identity.Data;
using TakweneTrackManagement.Infrastructure.Identity.Entities;

namespace TakweneTrackManagement.Infrastructure.DataSeeding
{
    internal class IdentityDataSeeder : IDataSeeder
    {
        private readonly TrackManagementIdentityDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataSeeder> _logger;

        public IdentityDataSeeder(TrackManagementIdentityDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityDataSeeder> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(ct);

                if (!await _roleManager.Roles.AnyAsync(ct))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }


                if (!await _userManager.Users.AnyAsync(ct))
                {
                    var admin = new ApplicationUser()
                    {
                        DisplayName = "Omar Nabil",
                        Email = "omar@gmail.com",
                        UserName = "OmarNabil",
                        PhoneNumber = "01203807372"
                    };

                    var createResult = await _userManager.CreateAsync(admin, "P@ssw0rd");

                    if (createResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(admin, "SuperAdmin");
                    }
                    else
                    {
                        var Errors = string.Join(';', createResult.Errors.Select(e => e.Description));
                        _logger.LogWarning($"Can Not Seed Default Admin {Errors}");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Identity Data Seeding Failed");
                return;
            }
        }
    }
}
