app.controller('OperatorHomeController', function ($scope, $controller, options, $rootScope) {
	$controller('RoutesListController', { $scope: $scope, options: options });

	$rootScope.header_tab = "routes";

	$scope.searchScheduleDay = function (form) {
	    if (form.isValid()) {
	        var url = options.operatorScheduleGetDayUrl;
	        form.submit(url, function (data) {
	            if (data.ok) {
	                $scope.day = data.dayShedule;
	                $scope.$apply(function () {
	                });

	            }
	        });
	    }
	};
});

app.controller('TemplatesController', function ($rootScope) {

	$rootScope.header_tab = "templates";

});

app.controller('RouteTemplateController', function ($scope, $http, options) {

	//-----------------init scope

	$scope.saveTemplateRoute = function (route, form, index) {
		if (form.isValid()) {
			var url = route.templateId ? options.saveRouteTemplateUrl : options.createRouteTemplateUrl;
			var isNew = route.templateId ? false : true;
			form.submit(url, function (data) {
				if (data.ok) {
					route.id = data.id || route.id;
					route.templateId = data.templateId || route.templateId;
					if (isNew)
						$scope.routes.splice(0, 0, {});

					$scope.$parent.$broadcast(isNew ? 'routeTemplateCreated' : 'routeTemplateSaved', angular.copy(route), index - 1);
					$scope.$apply();
				} else
					throw "Route's saving has failed.";
			});
		}
	};

	$scope.deleteRoute = function (route, form, index) {
		if (form.isValid()) {
			var url = options.deleteRouteTemplateUrl;
			form.submit(url, function (data) {
				if (data.ok) {
					$scope.routes.splice(index, 1);
					$scope.$apply();
					$scope.$parent.$broadcast('routeDeleted', route, index - 1);
				} else
					throw "Route's deletion has failed.";
			});
		}
	};

	//-----------------init data

	$http({
		method: 'GET',
		url: options.routesTemplatesUrl
	}).success(function (routes) {
		routesLoaded(routes || []);
	}).error(function () {
		routesLoaded([]);
		throw 'Routes loading has failed.';
	});


	//-----------------private functionality

	function routesLoaded(routes) {
		routes.splice(0, 0, {name: 'Пустой'});
		$scope.$parent.$broadcast('routeTemplatesLoaded', routes.slice(0));
		$scope.routes = routes;
	}

});

app.controller('DayTemplateController', function ($scope, $http, options) {

	//-----------------init scope

	$scope.saveDayTemplate = function (day, form) {
		if (form.isValid()) {
			var url = day.templateId ? options.saveDayTemplateUrl : options.createDayTemplateUrl;
			var isNew = day.templateId ? false : true;
			form.submit(url, function (data) {
				if (data.ok) {
					day.templateId = data.templateId || day.templateId;
					if (isNew)
						$scope.days.splice(0, 0, { routes: [] });
					$scope.$apply();
				} else
					throw "Day template's saving has failed.";
			});
		}
	};

	$scope.deleteDayTemplate = function (index, day, form) {
		if (form.isValid()) {
			var url = options.deleteDayTemplateUrl;
			form.submit(url, function (data) {
				if (data.ok) {
					$scope.days.splice(index, 1);
					$scope.$apply();
				} else
					throw "Day template's deletion has failed.";
			});
		}
	};

	$scope.addRouteToDayTemplate = function (day, route) {
		day.routeToDayTemplate = $scope.routes[0];
		var newRoute = angular.copy(route);
		newRoute.id = null;
		newRoute.templateId = null;
		day.routes.splice(0, 0, newRoute);
	};

	$scope.deleteRouteFromDayTemplate = function (day, index) {
		day.routes.splice(index, 1);
	};

	//-----------------events

	$scope.$on('routeTemplatesLoaded', function (event, routes) {
		$scope.routeToDayTemplate = routes[0];
		$scope.routes = routes;
	});

	$scope.$on('routeTemplateSaved', function (event, route, index) {
		$scope.routes[index] = route;
	});

	$scope.$on('routeTemplateCreated', function (event, route, index) {
		$scope.routes.splice(index, 0, route);
	});

	$scope.$on('routeDeleted', function (event, route, index) {
		$scope.routes[index] = route;
	});

	//-----------------init data

	$http({
		method: 'GET',
		url: options.daysTemplatesUrl
	}).success(function (days) {
		daysLoaded(days || []);
	}).error(function () {
		daysLoaded([]);
	});

	//-----------------private functionality

	function daysLoaded(days) {
		$scope.days = days || [];
		$scope.days.splice(0, 0, { routes: [] });
		$scope.days.forEach(function (entry) {
			entry.routes = entry.routes || [];
		});
	}
});

app.controller('ScheduleController', function ($scope, $http, options, $rootScope) {
	$rootScope.header_tab = "schedule";

	$scope.searchScheduleDay = function (form) {
		if (form.isValid()) {
			var url = options.operatorScheduleGetDayUrl;
			form.submit(url, function (data) {
				if (data.ok) {
				    $scope.dateShedule = $scope.filter.date;
				    $scope.day = data.dayShedule;
				    $scope.$apply(function () {
				    });

				}
			});
		}
	};

	$scope.addDayToSchedule = function (day, date, form) {
		if (form.isValid()) {
			var url = options.operatorScheduleSaveTemplateUrl;
			form.submit(url, function (data) {
				if (data.ok) {
				}
			});
		}
	};

	$scope.addDayTemplateToSheduleDay = function (dayTemplate) {
	  var dayId = $scope.day.id;
	  $scope.day = angular.copy(dayTemplate);
	  $scope.day.id = dayId;
	};

	$scope.addRouteToSheduleDay = function (day, route) {
	    day.routeToDayTemplate = $scope.routesTemplates[0];
	    var newRoute = angular.copy(route);
	    newRoute.id = null;
	    newRoute.templateId = null;
	    day.routes.splice(0, 0, newRoute);
	};

	$scope.deleteRouteFromSheduleDay = function (day, index) {
	    day.routes.splice(index, 1);
	};

	$http({
		method: 'GET',
		url: options.daysTemplatesUrl
	}).success(function (daysTemplates) {
		$scope.daysTemplates = daysTemplates || [];
	}).error(function () {
		$scope.daysTemplates = [];
	});

	$http({
		method: 'GET',
		url: options.routesTemplatesUrl
	}).success(function (routesTemplates) {
		$scope.routesTemplates = routesTemplates || [];
	}).error(function () {
		$scope.routesTemplates = [];
		throw 'Routes loading has failed.';
	});
});