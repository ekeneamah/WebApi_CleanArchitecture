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

        public DbSet<InsuranceCoy> InsuranceCompany { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<ClaimsForm> ClaimsForms { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CategoryBenefit> Benefits { get; set; }
        public DbSet<VehiclePremium> VehiclePremiums { get; set; }
        public DbSet<CoyBenefit> CoyBenefits { get; set; }
        public DbSet<MotorClaim> MotorClaims { get; set; }




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