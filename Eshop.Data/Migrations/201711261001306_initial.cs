namespace Eshop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
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
                "dbo.CartInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                        Cart_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cart", t => t.Cart_Id, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Cart_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, unicode: false),
                        Password = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Role = c.String(nullable: false, unicode: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                        Status = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountInfo",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Phone = c.String(unicode: false),
                        ZipCode = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.LoginLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IPAddress = c.String(nullable: false, unicode: false),
                        Status = c.String(unicode: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Discount",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Id)
                .Index(t => t.Id);
            
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
                "dbo.PurchaseInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Product_Id = c.Int(nullable: false),
                        Purchase_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Purchase", t => t.Purchase_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Purchase_Id);
            
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        status = c.String(unicode: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                        FinishDate = c.DateTime(nullable: false, precision: 0),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.WishListProduct",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Importance = c.Int(nullable: false),
                        Status = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.Importance })
                .ForeignKey("dbo.Product", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.WishList", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.WishList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Account_Id);
            
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
            
            AddColumn("dbo.Category", "Description", c => c.String(maxLength: 255, storeType: "nvarchar"));
            AddColumn("dbo.Category", "Parent_Id", c => c.Int());
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false, maxLength: 32, storeType: "nvarchar"));
            CreateIndex("dbo.Category", "Parent_Id");
            AddForeignKey("dbo.Category", "Parent_Id", "dbo.Category", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "Parent_Id", "dbo.Category");
            DropForeignKey("dbo.TraitValue", "Id", "dbo.ProductAttribute");
            DropForeignKey("dbo.WishListProduct", "Id", "dbo.WishList");
            DropForeignKey("dbo.WishList", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.WishListProduct", "Id", "dbo.Product");
            DropForeignKey("dbo.PurchaseInfo", "Purchase_Id", "dbo.Purchase");
            DropForeignKey("dbo.Purchase", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.PurchaseInfo", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.ProductPic", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Product", "Manufacturer_Id", "dbo.Manufacturer");
            DropForeignKey("dbo.Discount", "Id", "dbo.Product");
            DropForeignKey("dbo.Product", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.CartInfo", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.CartInfo", "Cart_Id", "dbo.Cart");
            DropForeignKey("dbo.Cart", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.LoginLog", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.AccountInfo", "Id", "dbo.Account");
            DropForeignKey("dbo.ProductAttribute", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Measurement", "Id", "dbo.ProductAttribute");
            DropForeignKey("dbo.ProductAttribute", "Attribute_Id", "dbo.Attribute");
            DropForeignKey("dbo.AttributeCategory", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.AttributeCategory", "Attribute_Id", "dbo.Attribute");
            DropIndex("dbo.AttributeCategory", new[] { "Category_Id" });
            DropIndex("dbo.AttributeCategory", new[] { "Attribute_Id" });
            DropIndex("dbo.TraitValue", new[] { "Id" });
            DropIndex("dbo.WishList", new[] { "Account_Id" });
            DropIndex("dbo.WishListProduct", new[] { "Id" });
            DropIndex("dbo.Purchase", new[] { "Account_Id" });
            DropIndex("dbo.PurchaseInfo", new[] { "Purchase_Id" });
            DropIndex("dbo.PurchaseInfo", new[] { "Product_Id" });
            DropIndex("dbo.ProductPic", new[] { "Product_Id" });
            DropIndex("dbo.Discount", new[] { "Id" });
            DropIndex("dbo.LoginLog", new[] { "Account_Id" });
            DropIndex("dbo.AccountInfo", new[] { "Id" });
            DropIndex("dbo.Cart", new[] { "Account_Id" });
            DropIndex("dbo.CartInfo", new[] { "Product_Id" });
            DropIndex("dbo.CartInfo", new[] { "Cart_Id" });
            DropIndex("dbo.Product", new[] { "Manufacturer_Id" });
            DropIndex("dbo.Product", new[] { "Category_Id" });
            DropIndex("dbo.Measurement", new[] { "Id" });
            DropIndex("dbo.ProductAttribute", new[] { "Product_Id" });
            DropIndex("dbo.ProductAttribute", new[] { "Attribute_Id" });
            DropIndex("dbo.Category", new[] { "Parent_Id" });
            AlterColumn("dbo.Category", "Name", c => c.String(unicode: false));
            DropColumn("dbo.Category", "Parent_Id");
            DropColumn("dbo.Category", "Description");
            DropTable("dbo.AttributeCategory");
            DropTable("dbo.TraitValue");
            DropTable("dbo.WishList");
            DropTable("dbo.WishListProduct");
            DropTable("dbo.Purchase");
            DropTable("dbo.PurchaseInfo");
            DropTable("dbo.ProductPic");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.Discount");
            DropTable("dbo.LoginLog");
            DropTable("dbo.AccountInfo");
            DropTable("dbo.Account");
            DropTable("dbo.Cart");
            DropTable("dbo.CartInfo");
            DropTable("dbo.Product");
            DropTable("dbo.Measurement");
            DropTable("dbo.ProductAttribute");
            DropTable("dbo.Attribute");
        }
    }
}
