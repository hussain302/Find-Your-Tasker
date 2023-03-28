using Microsoft.EntityFrameworkCore;
using Models.DbModels;

namespace DataAccessLayer.DataBaseContext
{
    public class TaskerContext :DbContext
    {


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }        
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Models.DbModels.TaskStatus> TaskStatus { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(true).UseSqlServer(
                connectionString: "Data Source = muhammadhussain; Initial Catalog = Tasker_DB; Trusted_Connection = true;"
               );
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
