using System;
using System.Collections.Generic;
using System.Text;
using Asset.Web;
//using Asset.Entity;
using Asset.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Asset.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<LiabilityType> LiabilityTypes { get; set; }
        public DbSet<KopAsset> KopAssets { get; set; }
        public DbSet<Liability> Liabilities { get; set; }
        public DbSet<RoleMenuPermission> AspNetRoleMenuPermission { get; set; }

    }

}