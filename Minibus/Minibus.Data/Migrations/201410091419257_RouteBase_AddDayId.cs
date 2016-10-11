namespace Minibus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RouteBase_AddDayId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RouteBases", name: "Day_Id", newName: "DayId");
            RenameIndex(table: "dbo.RouteBases", name: "IX_Day_Id", newName: "IX_DayId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.RouteBases", name: "IX_DayId", newName: "IX_Day_Id");
            RenameColumn(table: "dbo.RouteBases", name: "DayId", newName: "Day_Id");
        }
    }
}
