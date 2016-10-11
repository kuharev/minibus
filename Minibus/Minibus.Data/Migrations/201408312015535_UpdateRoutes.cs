namespace Minibus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoutes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RouteBases", "Time", c => c.String());
            AlterColumn("dbo.RouteBases", "From", c => c.String());
            AlterColumn("dbo.RouteBases", "To", c => c.String());
            AlterColumn("dbo.RouteBases", "CarNumber", c => c.String());
            AlterColumn("dbo.RouteBases", "DriverFullName", c => c.String());
            AlterColumn("dbo.RouteBases", "DriverPhone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RouteBases", "DriverPhone", c => c.String(maxLength: 20));
            AlterColumn("dbo.RouteBases", "DriverFullName", c => c.String(maxLength: 200));
            AlterColumn("dbo.RouteBases", "CarNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.RouteBases", "To", c => c.String(maxLength: 100));
            AlterColumn("dbo.RouteBases", "From", c => c.String(maxLength: 100));
            AlterColumn("dbo.RouteBases", "Time", c => c.String(maxLength: 20));
        }
    }
}
