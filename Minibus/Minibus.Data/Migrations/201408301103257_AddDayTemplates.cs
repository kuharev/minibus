namespace Minibus.Data.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class AddDayTemplates : DbMigration
	{
		public override void Up()
		{
			CreateTable(
					"dbo.DayTemplates",
					c => new
							{
								Id = c.Int(nullable: false, identity: true),
								Name = c.String(maxLength: 100),
							})
					.PrimaryKey(t => t.Id);

			CreateTable(
					"dbo.RouteBases",
					c => new
							{
								Id = c.Int(nullable: false, identity: true),
								Time = c.String(maxLength: 20),
								From = c.String(maxLength: 100),
								To = c.String(maxLength: 100),
								CarNumber = c.String(maxLength: 20),
								PlaceNumber = c.Int(nullable: false),
								DriverFullName = c.String(maxLength: 200),
								DriverPhone = c.String(maxLength: 20),
								DayTemplate_Id = c.Int(),
							})
					.PrimaryKey(t => t.Id)
					.ForeignKey("dbo.DayTemplates", t => t.DayTemplate_Id)
					.Index(t => t.DayTemplate_Id);

			CreateTable(
					"dbo.Routes",
					c => new
							{
								Id = c.Int(nullable: false, identity: true),
								Date = c.DateTime(nullable: false),
								RouteBase_Id = c.Int(),
							})
					.PrimaryKey(t => t.Id)
					.ForeignKey("dbo.RouteBases", t => t.RouteBase_Id)
					.Index(t => t.RouteBase_Id);

			CreateTable(
					"dbo.RouteTemplates",
					c => new
							{
								Id = c.Int(nullable: false, identity: true),
								Name = c.String(maxLength: 100),
								RouteBase_Id = c.Int(),
							})
					.PrimaryKey(t => t.Id)
					.ForeignKey("dbo.RouteBases", t => t.RouteBase_Id)
					.Index(t => t.RouteBase_Id);

		}

		public override void Down()
		{
			DropForeignKey("dbo.RouteTemplates", "RouteBase_Id", "dbo.RouteBases");
			DropForeignKey("dbo.Routes", "RouteBase_Id", "dbo.RouteBases");
			DropForeignKey("dbo.RouteBases", "DayTemplate_Id", "dbo.DayTemplates");
			DropIndex("dbo.RouteTemplates", new[] { "RouteBase_Id" });
			DropIndex("dbo.Routes", new[] { "RouteBase_Id" });
			DropIndex("dbo.RouteBases", new[] { "DayTemplate_Id" });
			DropTable("dbo.RouteTemplates");
			DropTable("dbo.Routes");
			DropTable("dbo.RouteBases");
			DropTable("dbo.DayTemplates");
		}
	}
}
