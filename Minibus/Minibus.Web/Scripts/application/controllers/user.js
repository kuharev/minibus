app.controller('UserHomeController', function ($scope, $controller, options, $rootScope) {
	$controller('RoutesListController', { $scope: $scope, options: options });

	$rootScope.header_tab = "routes";
});
