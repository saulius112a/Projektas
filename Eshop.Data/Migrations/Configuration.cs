namespace Eshop.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Eshop.Data.DatabaseContext.MySqlDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Eshop.Data.DatabaseContext.MySqlDbContext context)
        {

        }
    }
}
