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

        public virtual DbSet<SqlResult> usp_UpdateUserPermissions => Set<SqlResult>();
        public virtual DbSet<SqlResult> usp_UpdateUseQuantity => Set<SqlResult>();
        public virtual DbSet<SqlResult> usp_UpdateAvailableQuantity => Set<SqlResult>();
        public virtual DbSet<SqlResult> usp_UpdateUserCodes => Set<SqlResult>();
        public virtual DbSet<SqlResult> usp_IsExistProductResult => Set<SqlResult>();
        public virtual DbSet<SqlResult> usp_UpdateAvailableQuantityAndUseQuantity => Set<SqlResult>();
        public virtual DbSet<SqlResult> usp_UpdateProductCodes => Set<SqlResult>();
        //pagination
        public virtual DbSet<UserDetailsList> usp_GetUsersWithPagination => Set<UserDetailsList>();
        public virtual DbSet<ProductDetails> usp_GetProductsWithPagination => Set<ProductDetails>();
        public virtual DbSet<UserByProductID> usp_getUserListbyProductID => Set<UserByProductID>();
        public virtual DbSet<ProductByUserID> usp_getProductListbyUserID => Set<ProductByUserID>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // You can configure more entities here if necessary.
        modelBuilder.Entity<SqlResult>().HasNoKey();
    }
    }
}


