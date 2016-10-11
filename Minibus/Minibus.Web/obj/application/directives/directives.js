(function (app) {

	app.directive('popup', function ($rootScope) {

		return {
			restrict: "A",
				link: function($scope, element, attrs) {
					$rootScope.popups = $rootScope.popups || {};
					$rootScope.popups[attrs.popup] = new Popup(element);
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
			link: function link($scope, element, attrs) {
				element.on('click', function () {
					$rootScope.popups = $rootScope.popups || {};
					var popup = $rootScope.popups[attrs.openPopup];
					if (popup)
						popup.show();
					else
						loadPopup(attrs);
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
			link: function ($scope, element, attrs) {
				$scope[attrs.ajaxForm] = new AjaxForm(element);
			}
		};

		function AjaxForm(form) {
			
			var self = this;
			form = form instanceof jQuery ? form : $(form);

			$.validator.unobtrusive.parse(form);

			self.isValid = function () {
				return form.validate().form();
			};

			self.submit = function (url, callback) {
				$.ajax({
					url: url || form.attr('action'),
					type: 'POST',
					async: true,
					data: form.serialize(),
					success: function (data) {
						if (typeof callback == 'function')
							callback(data);
					},
				});
			};
			
		}
	});


	app.directive('datepicker', function () {
		return {
			restrict: "A",
			link: function ($scope, element, attrs) {
				element.datepicker($.datepicker.regional["ru"]);
			}
		};
	});
})(app);