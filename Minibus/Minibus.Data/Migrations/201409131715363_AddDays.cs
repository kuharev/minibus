namespace Minibus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDays : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RouteBases", "Day_Id", c => c.Int());
            CreateIndex("dbo.RouteBases", "Day_Id");
            AddForeignKey("dbo.RouteBases", "Day_Id", "dbo.Days", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteBases", "Day_Id", "dbo.Days");
            DropIndex("dbo.RouteBases", new[] { "Day_Id" });
            DropColumn("dbo.RouteBases", "Day_Id");
            DropTable("dbo.Days");
        }
    }
}
