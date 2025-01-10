using AIMSV2.Entities;
using AIMSV2.Models;
using Entities;
using Microsoft.EntityFrameworkCore;


namespace AIMSV2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        //Table
        public DbSet<Users> Users { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public virtual DbSet<UserDetailsList> vw_Users => Set<UserDetailsList>();
        public virtual DbSet<ProductDetails> vw_Products => Set<ProductDetails>();


        //public DbSet<UserProducts> UserProducts { get; set; }
        public virtual DbSet<UserProducts> UserProducts => Set<UserProducts>();

        //public virtual DbSet<Users> Users => Set<Entities.Users>();

        //View Table
        //public virtual DbSet<UserDetails> vw_Users => Set<UserDetails>();

        //Store Procedure

        public virtual DbSet<Users> usp_UpdateUserPermissions => Set<Users>();
        public virtual DbSet<Users> usp_UpdateUseQuantity => Set<Users>();
        public virtual DbSet<Users> usp_UpdateAvailableQuantityAndUseQuantity => Set<Users>();
        public virtual DbSet<Users> usp_UpdateAvailableQuantity => Set<Users>();
        public virtual DbSet<Users> usp_UpdateUserCodes => Set<Users>();
        
        public virtual DbSet<UserDetailsList> usp_GetUsersWithPagination => Set<UserDetailsList>();
        public virtual DbSet<ProductDetails> usp_GetProductsWithPagination => Set<ProductDetails>();

        //public virtual DbSet<UsersWithPagination> usp_GetUsersWithPagination => Set<UsersWithPagination>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Configure the "Users" table
            //modelBuilder.Entity<Users>()
            //    .HasKey(u => u.ID); // Define Id as the primary key

            //modelBuilder.Entity<Users>()
            //    .HasKey(u => u.Name); // Define Id as the primary key

            //modelBuilder.Entity<Users>()
            //    .Property(u => u.ID)
            //    .ValueGeneratedOnAdd(); // Configure Id to auto-increment
        }
    }
}


