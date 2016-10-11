namespace Minibus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RouteBase_AddDayTemplateId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RouteBases", name: "DayTemplate_Id", newName: "DayTemplateId");
            RenameIndex(table: "dbo.RouteBases", name: "IX_DayTemplate_Id", newName: "IX_DayTemplateId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.RouteBases", name: "IX_DayTemplateId", newName: "IX_DayTemplate_Id");
            RenameColumn(table: "dbo.RouteBases", name: "DayTemplateId", newName: "DayTemplate_Id");
        }
    }
}
