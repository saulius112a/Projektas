using Eshop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.DatabaseContext
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext() : base("Default")
        {
            //Here we can add some data to db when we initialize db, for now we do not need it
            //Database.SetInitializer<MySqlDbContext>(new MySqlDbInitializer());
            Database.SetInitializer<MySqlDbContext>(new MySqlDbInitializer());
        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
        }
    }
    
}
