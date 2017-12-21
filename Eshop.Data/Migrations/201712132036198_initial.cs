namespace Eshop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountInfo",
                c => new
                    {
                        AccountId = c.Int(nullable: false),
                        Name = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Phone = c.String(unicode: false),
                        ZipCode = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, unicode: false),
                        Password = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Role = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoginLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IPAddress = c.String(nullable: false, unicode: false),
                        Status = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        LoginDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Attribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 32, storeType: "nvarchar"),
                        IsTrait = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 32, storeType: "nvarchar"),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Description = c.String(maxLength: 255, storeType: "nvarchar"),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Weight = c.Double(nullable: false),
                        ProductCode = c.String(unicode: false),
                        Price = c.Double(nullable: false),
                        Color = c.String(unicode: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                        UpdateDate = c.DateTime(nullable: false, precision: 0),
                        IsDiscounted = c.Boolean(nullable: false),
                        ManufacturerId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.ManufacturerId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductAttribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        AttributeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attribute", t => t.AttributeId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.AttributeId);
            
            CreateTable(
                "dbo.Measurement",
                c => new
                    {
                        ProductAttributeId = c.Int(nullable: false),
                        Unit = c.String(unicode: false),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProductAttributeId)
                .ForeignKey("dbo.ProductAttribute", t => t.ProductAttributeId)
                .Index(t => t.ProductAttributeId);
            
            CreateTable(
                "dbo.TraitValue",
                c => new
                    {
                        ProductAttributeId = c.Int(nullable: false),
                        Value = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.ProductAttributeId)
                .ForeignKey("dbo.ProductAttribute", t => t.ProductAttributeId)
                .Index(t => t.ProductAttributeId);
            
            CreateTable(
                "dbo.CartInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                        CartId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cart", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CartId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Discount",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ProductPic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        IconLocation = c.String(unicode: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.PurchaseInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        ProductId = c.Int(nullable: false),
                        PurchaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Purchase", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.PurchaseId);
            
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        status = c.String(unicode: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                        FinishDate = c.DateTime(nullable: false, precision: 0),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.WishListProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(unicode: false),
                        WishListId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.WishList", t => t.WishListId, cascadeDelete: true)
                .Index(t => t.WishListId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.WishList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attribute", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.WishListProduct", "WishListId", "dbo.WishList");
            DropForeignKey("dbo.WishList", "AccountId", "dbo.Account");
            DropForeignKey("dbo.WishListProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.PurchaseInfo", "PurchaseId", "dbo.Purchase");
            DropForeignKey("dbo.Purchase", "AccountId", "dbo.Account");
            DropForeignKey("dbo.PurchaseInfo", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductPic", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.Discount", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.CartInfo", "ProductId", "dbo.Product");
            DropForeignKey("dbo.CartInfo", "CartId", "dbo.Cart");
            DropForeignKey("dbo.Cart", "AccountId", "dbo.Account");
            DropForeignKey("dbo.TraitValue", "ProductAttributeId", "dbo.ProductAttribute");
            DropForeignKey("dbo.ProductAttribute", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Measurement", "ProductAttributeId", "dbo.ProductAttribute");
            DropForeignKey("dbo.ProductAttribute", "AttributeId", "dbo.Attribute");
            DropForeignKey("dbo.Category", "ParentId", "dbo.Category");
            DropForeignKey("dbo.AccountInfo", "AccountId", "dbo.Account");
            DropForeignKey("dbo.LoginLog", "AccountId", "dbo.Account");
            DropIndex("dbo.WishList", new[] { "AccountId" });
            DropIndex("dbo.WishListProduct", new[] { "ProductId" });
            DropIndex("dbo.WishListProduct", new[] { "WishListId" });
            DropIndex("dbo.Purchase", new[] { "AccountId" });
            DropIndex("dbo.PurchaseInfo", new[] { "PurchaseId" });
            DropIndex("dbo.PurchaseInfo", new[] { "ProductId" });
            DropIndex("dbo.ProductPic", new[] { "ProductId" });
            DropIndex("dbo.Manufacturer", new[] { "Name" });
            DropIndex("dbo.Discount", new[] { "ProductId" });
            DropIndex("dbo.Cart", new[] { "AccountId" });
            DropIndex("dbo.CartInfo", new[] { "ProductId" });
            DropIndex("dbo.CartInfo", new[] { "CartId" });
            DropIndex("dbo.TraitValue", new[] { "ProductAttributeId" });
            DropIndex("dbo.Measurement", new[] { "ProductAttributeId" });
            DropIndex("dbo.ProductAttribute", new[] { "AttributeId" });
            DropIndex("dbo.ProductAttribute", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropIndex("dbo.Product", new[] { "ManufacturerId" });
            DropIndex("dbo.Category", new[] { "ParentId" });
            DropIndex("dbo.Attribute", new[] { "CategoryId" });
            DropIndex("dbo.LoginLog", new[] { "AccountId" });
            DropIndex("dbo.AccountInfo", new[] { "AccountId" });
            DropTable("dbo.WishList");
            DropTable("dbo.WishListProduct");
            DropTable("dbo.Purchase");
            DropTable("dbo.PurchaseInfo");
            DropTable("dbo.ProductPic");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.Discount");
            DropTable("dbo.Cart");
            DropTable("dbo.CartInfo");
            DropTable("dbo.TraitValue");
            DropTable("dbo.Measurement");
            DropTable("dbo.ProductAttribute");
            DropTable("dbo.Product");
            DropTable("dbo.Category");
            DropTable("dbo.Attribute");
            DropTable("dbo.LoginLog");
            DropTable("dbo.Account");
            DropTable("dbo.AccountInfo");
        }
    }
}
