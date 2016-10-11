namespace Minibus.Web.Core
{
	public static class AppRoutes
	{
		public static class Home
		{
			public const string Index = "Index";
			public const string User = "User";
			public const string Operator = "Operator";
		}

		public static class RoutesApi
		{
			public const string Routes = "Routes";
			public const string RoutesTemplates = "RoutesTemplates";
			public const string DaysTemplates = "DaysTemplates";
		}

		public static class Authentication
		{
			public const string Registration = "Registration";
			public const string Login = "Login";
			public const string Logout = "Logout";
		}

		public static class Settings
		{
			public const string User = "UserSettings";
			public const string Operator = "OperatorSettings";
		}

		public static class Templates
		{
			public const string Operator = "OperatorTemplates";
			public const string CreateRouteTemplate = "CreateRouteTemplate";
			public const string SaveRouteTemplate = "SaveRouteTemplate";
			public const string DeleteRouteTemplate = "DeleteRouteTemplate";
			public const string CreateDayTemplate = "CreateDayTemplate";
			public const string SaveDayTemplate = "SaveDayTemplate";
			public const string DeleteDayTemplate = "DeleteDayTemplate";
		}

		public static class OperatorSchedule
		{
			public const string Index = "ScheduleIndex";
			public const string GetDay = "GetDay";
			public const string SaveTemplate = "SaveTemplate";
		}

		public static class ScheduleApi
		{
			public const string ScheduleDay = "ScheduleDay";
		}
	}
}