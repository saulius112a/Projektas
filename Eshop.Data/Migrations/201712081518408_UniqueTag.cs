namespace Eshop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueTag : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Manufacturer", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Manufacturer", new[] { "Name" });
        }
    }
}
