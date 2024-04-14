using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Domain.Models;

namespace Infrastructure.Content.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<InsuranceCoyEntity> InsuranceCompany { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<UserProfileEntity> UserProfiles { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ClaimEntity> Claims { get; set; }
        public DbSet<PolicyEntity> Policies { get; set; }
        public DbSet<ClaimsFormEntity> ClaimsForms { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<CategoryBenefitEntity> Benefits { get; set; }
        public DbSet<VehiclePremiumEntity> VehiclePremiums { get; set; }
        public DbSet<CoyBenefitEntity> CoyBenefits { get; set; }
        public DbSet<MotorClaimEntity> MotorClaims { get; set; }
        public DbSet<kycEntity> KYCs { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //    base.OnModelCreating(builder);
        //}

    }
}