using System;
using System.Data.Entity;

namespace Eshop.Data.DatabaseContext
{
    public class MySqlDbInitializer : CreateDatabaseIfNotExists<MySqlDbContext>
    {
        protected override void Seed(MySqlDbContext context)
        {
            var account = new Entities.Account
            {
                Email = "admin",
                Password = "AGcKUWus739SQnwydMsR1tGcWtiLfyDK/++ufh4FeQftTzhL7cZ+Ov+YrCG9DltZ4Q==",  // admin
                Role = Entities.Account.AccRole.admin,
                CreationDate = DateTime.Now
            };
            context.Accounts.Add(account);
            context.AccountInfos.Add(new Entities.AccountInfo
            {
                Account = account,
                AccountId = 0
            });
            context.SaveChanges();
        }
    }
}