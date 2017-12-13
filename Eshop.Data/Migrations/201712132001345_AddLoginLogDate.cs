namespace Eshop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoginLogDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoginLog", "LoginDate", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoginLog", "LoginDate");
        }
    }
}
