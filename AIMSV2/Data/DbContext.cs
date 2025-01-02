using Entities;
using Microsoft.EntityFrameworkCore;


namespace AIMSV2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        //Table
        public DbSet<Entities.Users> Users { get; set; }
        //public DbSet<Entities.UserProducts> UserProducts { get; set; }
        public virtual DbSet<Entities.UserProducts> UserProducts => Set<Entities.UserProducts>();
        //public virtual DbSet<Entities.Users> Users => Set<Entities.Users>();

        //View Table
        //public virtual DbSet<UserDetails> vw_Users => Set<UserDetails>();

        //Store Procedure

        public virtual DbSet<Entities.Users> usp_UpdateUserPermissions => Set<Entities.Users>();
        public virtual DbSet<Entities.Users> usp_UpdateUseQuantity => Set<Entities.Users>();
        public virtual DbSet<Entities.Users> usp_UpdateAvailableQuantityAndUseQuantity => Set<Entities.Users>();
        public virtual DbSet<Entities.Users> usp_UpdateAvailableQuantity => Set<Entities.Users>();

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


