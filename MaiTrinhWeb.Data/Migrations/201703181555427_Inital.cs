namespace MaiTrinhWeb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExportProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExportDate = c.DateTime(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Images = c.String(),
                        Size = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Int(nullable: false),
                        Color = c.Int(nullable: false),
                        Tags = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImportProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImportDate = c.DateTime(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImportProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ExportProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.ImportProducts", new[] { "ProductId" });
            DropIndex("dbo.ExportProducts", new[] { "ProductId" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.ImportProducts");
            DropTable("dbo.Products");
            DropTable("dbo.ExportProducts");
            DropTable("dbo.Customers");
        }
    }
}
