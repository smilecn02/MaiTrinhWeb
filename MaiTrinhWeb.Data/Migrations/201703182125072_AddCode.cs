namespace MaiTrinhWeb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Code");
        }
    }
}
