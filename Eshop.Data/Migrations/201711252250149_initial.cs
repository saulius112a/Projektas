namespace Eshop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Description = c.String(maxLength: 255, storeType: "nvarchar"),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Attribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 32, storeType: "nvarchar"),
                        IsTrait = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 32, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductAttribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Attribute_Id = c.Int(nullable: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attribute", t => t.Attribute_Id, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Attribute_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Measurement",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Unit = c.String(nullable: false, unicode: false),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductAttribute", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Weight = c.Double(nullable: false),
                        ProductCoode = c.String(unicode: false),
                        Price = c.Double(nullable: false),
                        Color = c.String(unicode: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                        UpdateDate = c.DateTime(nullable: false, precision: 0),
                        IsDiscounted = c.Boolean(nullable: false),
                        Category_Id = c.Int(),
                        Manufacturer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .ForeignKey("dbo.Manufacturer", t => t.Manufacturer_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Manufacturer_Id);
            
            CreateTable(
                "dbo.Manufacturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        WebLink = c.String(unicode: false),
                        IconLocation = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductPic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        IconLocation = c.String(unicode: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.TraitValue",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Value = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductAttribute", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AttributeCategory",
                c => new
                    {
                        Attribute_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Attribute_Id, t.Category_Id })
                .ForeignKey("dbo.Attribute", t => t.Attribute_Id, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Attribute_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "Parent_Id", "dbo.Category");
            DropForeignKey("dbo.TraitValue", "Id", "dbo.ProductAttribute");
            DropForeignKey("dbo.ProductPic", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Product", "Manufacturer_Id", "dbo.Manufacturer");
            DropForeignKey("dbo.Product", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.ProductAttribute", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Measurement", "Id", "dbo.ProductAttribute");
            DropForeignKey("dbo.ProductAttribute", "Attribute_Id", "dbo.Attribute");
            DropForeignKey("dbo.AttributeCategory", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.AttributeCategory", "Attribute_Id", "dbo.Attribute");
            DropIndex("dbo.AttributeCategory", new[] { "Category_Id" });
            DropIndex("dbo.AttributeCategory", new[] { "Attribute_Id" });
            DropIndex("dbo.TraitValue", new[] { "Id" });
            DropIndex("dbo.ProductPic", new[] { "Product_Id" });
            DropIndex("dbo.Product", new[] { "Manufacturer_Id" });
            DropIndex("dbo.Product", new[] { "Category_Id" });
            DropIndex("dbo.Measurement", new[] { "Id" });
            DropIndex("dbo.ProductAttribute", new[] { "Product_Id" });
            DropIndex("dbo.ProductAttribute", new[] { "Attribute_Id" });
            DropIndex("dbo.Category", new[] { "Parent_Id" });
            DropTable("dbo.AttributeCategory");
            DropTable("dbo.TraitValue");
            DropTable("dbo.ProductPic");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.Product");
            DropTable("dbo.Measurement");
            DropTable("dbo.ProductAttribute");
            DropTable("dbo.Attribute");
            DropTable("dbo.Category");
        }
    }
}
