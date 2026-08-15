using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Infrastructure.Identity.Entities;

namespace TakweneTrackManagement.Infrastructure.Identity.Data
{
    internal class TrackManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public TrackManagementIdentityDbContext(DbContextOptions<TrackManagementIdentityDbContext> options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
           

        }
    }
}
