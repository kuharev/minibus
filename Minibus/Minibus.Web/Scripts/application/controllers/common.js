app.controller('AccountController', function ($rootScope, $scope, options) {

	setUser(options.userName);

	$rootScope.$on('login', function (event, login) {
		setUser(login);
		if (!$scope.$$phase)
			$scope.$apply();
	});

	$scope.logout = function () {
		$.ajax({
			url: options.logoutUrl,
			type: 'POST',
		}).success(function (data) {
			if (data.ok) {
				setUser('');
				$scope.$apply();
			}
		});
	};

	function setUser(login) {
		$scope.isAuthenticated = Boolean(login);
		$scope.login = login;
	}

});

app.controller('RegistrationController', function ($rootScope, $scope) {

	$scope.register = function () {
		if ($scope.registrationForm.isValid())
			$scope.registrationForm.submit('', function (data) {
				if (data.ok) {
					$rootScope.$broadcast('login', data.login);
					$scope.popup.hide();
				} else {
					$scope.message = data.message;
					$scope.$apply();
				}
			});
	};

});

app.controller('LoginController', function ($rootScope, $scope) {

	$scope.login = function () {
		if ($scope.loginForm.isValid())
			$scope.loginForm.submit('', function (data) {
				if (data.ok) {
					$rootScope.$broadcast('login', data.login);
					$scope.popup.hide();
				} else {
					$scope.message = data.message;
					$scope.$apply();
				}
			});
	};

});


app.controller('RoutesListController', function ($scope, options) {

	$scope.routes = [];

	$scope.filter = new Filter();

	$scope.search = function () {
		getRoutes();
	};


	function getRoutes() {
		$.ajax({
			type: 'GET',
			url: $scope.filter.getFilterUrl(),
		}).done(function (routes) {
			$scope.routes = routes;
			$scope.$apply();
		});
	}

	function Filter() {
		var self = this;

		self.from = '';
		self.getEncodedFrom = function () {
			return encodeURIComponent(self.from);
		};

		self.to = '';
		self.getEncodedTo = function () {
			return encodeURIComponent(self.to);
		};

		self.date = '';
		self.getEncodedDate = function () {
			return encodeURIComponent(self.date);
		};

		self.getFilterUrl = function () {
			return options.routesUrl + '?from=' + self.getEncodedFrom()
				+ "&to=" + self.getEncodedTo()
				+ "&date" + self.getEncodedDate();
		};
	}

});

app.controller('SettingsController', function ($scope, $rootScope) {
	$rootScope.header_tab = "settings";

	$scope.save_settings = function () {
		if ($scope.settingsForm.isValid())
			$scope.settingsForm.submit('', function (data) {
				if (data.ok) {
				} else {
				}
			});
	};
});