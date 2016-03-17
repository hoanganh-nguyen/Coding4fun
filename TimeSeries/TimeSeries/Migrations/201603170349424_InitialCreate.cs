namespace TimeSeries.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataAssets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Asset = c.String(),
                        ImportedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        Date = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        Asset_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Date)
                .ForeignKey("dbo.DataAssets", t => t.Asset_Id)
                .Index(t => t.Asset_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Login = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Login);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prices", "Asset_Id", "dbo.DataAssets");
            DropIndex("dbo.Prices", new[] { "Asset_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Prices");
            DropTable("dbo.DataAssets");
        }
    }
}
