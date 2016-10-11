(function (app) {

	app.directive('popup', function ($rootScope) {

		return {
			restrict: "A",
				link: function($scope, $element, $attrs) {
					$rootScope.popups = $rootScope.popups || {};
					$rootScope.popups[$attrs.popup] = new Popup($element);
				}
		};
		
	});

	app.directive('openPopup', function ($rootScope, $compile) {

		function compileHtml(popupHtml) {
			var template = angular.element(popupHtml);
			var $popupElement = $compile(template)($rootScope);
			$(document.body).append($popupElement);
			return $popupElement;
		}
		
		function initPopup($popupElement, attrs) {
			var popup = new Popup($popupElement);
			$rootScope.popups[attrs.openPopup] = popup;
			popup.show();
			return popup;
		}
		
		function initScope($popupElement, popup) {
			var popupScope = $popupElement.scope();
			popupScope.popup = popup;
			popupScope.$apply();
		}
		
		function loadPopup(attrs) {
			$.ajax({
				type: 'GET',
				url: attrs.openPopup,
				async: true
			}).done(function (popupHtml) {
				var $popupElement = compileHtml(popupHtml);
				var popup = initPopup($popupElement, attrs);
				initScope($popupElement, popup);
			});
		}

		return {
			restrict: "A",
			link: function link($scope, $element, $attrs) {
				$element.on('click', function () {
					$rootScope.popups = $rootScope.popups || {};
					var popup = $rootScope.popups[$attrs.openPopup];
					if (popup)
						popup.show();
					else
						loadPopup($attrs);
				});
			}
		};
		
	});

	function Popup(element, callback) {
		
		var popup = element instanceof jQuery ? element : $(element),
			$this = this,
			closeCallback = callback;

		init();

		$this.show = function () {
			popup.removeClass('hidden-element');
		};

		$this.hide = function () {
			popup.addClass('hidden-element');
			if (closeCallback)
				closeCallback();
		};

		$this.setCloseCallback = function (callback) {
			if (typeof callback != 'function')
				throw 'callback should be a function';
			closeCallback = callback;
		};

		function init() {
			var closeElements = popup.find('.close-popup, .popup-back');
			closeElements.on('click', function () {
				$this.hide();
			});
		}
		
	}



	app.directive('ajaxForm', function () {

		return {
			restrict: "A",
			link: function ($scope, $element, $attrs) {
				$scope[$attrs.ajaxForm] = new AjaxForm($element);
				if ($scope.ignore && $scope.ignore[$attrs.ajaxForm])
					$scope[$attrs.ajaxForm].ignoreRange($scope.ignore[$attrs.ajaxForm]);
			}
		};

		function AjaxForm(form) {

			var self = this,
				ignoreFields = [];
			form = form instanceof jQuery ? form : $(form);

			$.validator.unobtrusive.parse(form);

			self.ignore = function (field) {
				field = field instanceof jQuery ? field : $(field);
				ignoreFields.push(field);
			};

			self.ignoreRange = function(fields) {
				if (fields)
					fields.forEach(function(field) {
						self.ignore(field);
					});
			};

			self.isValid = function () {
				disableIgnore();
				var result = form.validate().form();
				enableIgnore();
				return result;
			};

			self.submit = function (url, callback) {
				disableIgnore();
				$.ajax({
					url: url || form.attr('action'),
					type: 'POST',
					data: form.serialize(),
				}).success(function (data) {
					enableIgnore();
					if (typeof callback == 'function')
						callback(data);
				}).error(function () {
					enableIgnore();
					throw "Ajax form submit has failed";
				});
			};
			
			function disableIgnore() {
				ignoreFields.forEach(function(field) {
					field.prop('disabled', true);
				});
			}
			
			function enableIgnore() {
				ignoreFields.forEach(function (field) {
					field.prop('disabled', false);
				});
			}
			
		}
	});

	app.directive('ignoreForm', function() {
		return {
			restrict: 'A',
			link: function($scope, $element, $attrs) {
				var form = $scope[$attrs.ignoreForm];
				if (form)
					form.ignore($element);
				else {
					$scope.ignore = $scope.ignore || {};
					$scope.ignore[$attrs.ignoreForm] = $scope.ignore[$attrs.ignoreForm] || [];
					$scope.ignore[$attrs.ignoreForm].push($element);
				}
			}
		};
	});


	app.directive('datepicker', function () {
		return {
			restrict: "A",
			link: function ($scope, $element) {
				$element.datepicker($.datepicker.regional["ru"]);
			}
		};
	});

	app.directive('accountButton', function () {
		return {
			restrict: "A",
			link: function ($scope, element, attrs) {
				var $menu = $('#' + attrs.accountButton);
				element.on('mouseover', show);
				$menu.on('mouseover', show);

				element.on('mouseout', hide);
				$menu.on('mouseout', hide);
				$menu.on('click', hide);
				
				function show() {
					var offset = element.offset();
					$menu.css('top', offset.top + element.outerHeight() + 'px');
					$menu.css('left', offset.left + 'px');
					$menu.css('display', 'block');
				}
				
				function hide() {
					$menu.css('display', 'none');
				}
			}
		};
	});
})(app);