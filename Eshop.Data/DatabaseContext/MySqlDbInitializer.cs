using System.Data.Entity;

namespace Eshop.Data.DatabaseContext
{
    public class MySqlDbInitializer : CreateDatabaseIfNotExists<MySqlDbContext>
    {
        protected override void Seed(MySqlDbContext context)
        {
            context.Categories.Add(new Entities.Category { Name = "yo" });
            context.SaveChanges();
        }
    }
}