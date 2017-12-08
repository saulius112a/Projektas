namespace Eshop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeUnitReq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Measurement", "Unit", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Measurement", "Unit", c => c.String(nullable: false, unicode: false));
        }
    }
}
