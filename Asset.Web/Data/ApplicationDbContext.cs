using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Asset.Web.Models;
using Asset.Web.ViewModels;

namespace Asset.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //}

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<LiabilityType> LiabilityTypes { get; set; }
        public DbSet<state> State { get; set; }
        public DbSet<lga> LGA { get; set; }
        public DbSet<KopAsset> KopAssets { get; set; }
        public DbSet<Liability> Liabilities { get; set; }
        public DbSet<CorpReg> corpRegs { get; set; }
        public DbSet<Assetno> AssetNumber { get; set; }
        public DbSet<MyService> ServiceTbl { get; set; }
       // public DbSet<AverageMaintanableEarnings>  AverageMaintanableEarnings { get; set; }
        public DbSet<AverageMaintanableEarningsWeighted> AverageMaintanableEarningsWeighteds { get; set; }
       // public DbSet<CorpEarningSetUp> Earning { get; set; }
        public DbSet<OurCorpSetUp> OurCorpEarningSetup { get; set; }
        public DbSet<EarninWeight> EarninWeights { get; set; }
        public DbSet<Family> families { get; set; }

        public DbSet<NavigationMenu> AspNetNavigationMenu { get; set; }
        public DbSet<RoleMenuPermission> AspNetRoleMenuPermission { get; set; }
        public DbSet<ActivationMail> ActivationMail { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
              base.OnModelCreating(builder);
            builder.Entity<RoleMenuPermission>()
            .HasKey(c => new { c.RoleId, c.NavigationMenuId });

            // builder.Entity<GradeAll>()
            //  .HasKey(c => new { c.Id, c.Id });


          
        }

       public DbSet<Asset.Web.ViewModels.AssetLiabilityView> AssetLiabilityView { get; set; }
    }
}
