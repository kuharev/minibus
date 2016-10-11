app.controller('UserHomeController', function ($scope, $controller, options) {
	$controller('RoutesListController', { $scope: $scope, options: options });	
});

app.controller('SettingsController', function($scope) {

});