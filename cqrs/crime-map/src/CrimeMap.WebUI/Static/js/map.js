window.CrimeMap = window.CrimeMap || {};
CrimeMap.crimeMap = CrimeMap.crimeMap || {};

var CrimeMap = (function() {

	function CrimeMap(mapContainer) {
		var self = this;

		this.mapContainer = mapContainer;
		this.googleMap = null;
		this.mapContainer = mapContainer;
		this.infoBoxes = [];
		this.marker = null;
		this.infobox = null;

		//google map custom marker icon - .png fallback for IE11
		var is_internetExplorer11 = navigator.userAgent.toLowerCase().indexOf('trident') > -1;
		self.marker_url = (is_internetExplorer11) ? 'static/libs/custom-google-map/img/cd-icon-location.png' : 'static/libs/custom-google-map/img/cd-icon-location.svg';


		this.setUp = function () {
			CrimeMap.location.getUserLocation(function (coords) {

				var options = {
					center: new google.maps.LatLng(coords.latitude, coords.longitude),
					zoom: 12,
					mapTypeId: google.maps.MapTypeId.ROADMAP,
					draggableCursor: 'default',
					panControl: false,
					zoomControl: false,
					mapTypeControl: false,
					scaleControl: false,
					streetViewControl: false,
					overviewMapControl: false,
					rotateControl: false,
					styles: CrimeMap.mapStyle.getBlueStyle()
				};

				var map = new google.maps.Map(this.mapContainer, options);

				google.maps.event.addListener(map, 'click', function (event) {
					addMarker(event.latLng);
				});

				var zoomControlDiv = document.createElement('div');
				var controlUIzoomIn = document.getElementById('cd-zoom-in');
				var controlUIzoomOut = document.getElementById('cd-zoom-out');

				zoomControlDiv.appendChild(controlUIzoomIn);
				zoomControlDiv.appendChild(controlUIzoomOut);

				// Setup the click event listeners and zoom-in or out according to the clicked element
				google.maps.event.addDomListener(controlUIzoomIn, 'click', function () {
					map.setZoom(map.getZoom() + 1)
				});

				google.maps.event.addDomListener(controlUIzoomOut, 'click', function () {
					map.setZoom(map.getZoom() - 1)
				});

				//insert the zoom div on the top left of the map
				map.controls[google.maps.ControlPosition.LEFT_TOP].push(zoomControlDiv);

				self.googleMap = map;
				
				setUpSearchBox();
			});
		}

		function addMarker(location) {

			if (self.infobox != null && typeof self.infobox != 'undefined')
				self.infobox.close();

			if (self.marker != null && typeof self.marker != 'undefined')
				self.marker.setMap(null);

			var marker = new google.maps.Marker({
				id: -1,
				position: location,
				map: self.googleMap,
				icon: self.marker_url,
			});
			
			var infoboxContent = $('#action-box').html();
			var info_box_options = {
				content: infoboxContent,
				pixelOffset: new google.maps.Size(-150, -150)
			};

			var infobox = new InfoBox(info_box_options);

			self.infoBoxes[marker.id] = infobox;
			self.infoBoxes[marker.id].marker = marker;
			self.infoBoxes[marker.id].listener = google.maps.event.addListener(marker, 'click', function (e) {
				openInfobox(marker.id, marker);
			});

			self.marker = marker;
			self.infobox = infobox;

			openInfobox(marker.id, marker);
		};

		function openInfobox(id, marker) {
			self.infoBoxes[id].open(self.googleMap, marker);
			self.isInfoboxOpen = true;
		};

		function setUpSearchBox() {
			var markers = [];
			var map = self.googleMap;

			var input = /** @type {HTMLInputElement} */(
				document.getElementById('places-searcher'));

			var btn = /** @type {HTMLInputElement} */(
				document.getElementById('places-searcher-action'));

			var searchBox = new google.maps.places.SearchBox(
				/** @type {HTMLInputElement} */(input));

			google.maps.event.addListener(searchBox, 'places_changed', function () {
				var places = searchBox.getPlaces();

				if (places.length == 0) {
					return;
				}

				for (var i = 0, marker; marker = markers[i]; i++) {
					marker.setMap(null);
				}

				markers = [];
				var bounds = new google.maps.LatLngBounds();

				for (var i = 0, place; place = places[i]; i++) {
					var image = {
						url: place.icon,
						size: new google.maps.Size(71, 71),
						origin: new google.maps.Point(0, 0),
						anchor: new google.maps.Point(17, 34),
						scaledSize: new google.maps.Size(25, 25)
					};

					var marker = new google.maps.Marker({
						map: map,
						icon: image,
						title: place.name,
						position: place.geometry.location
					});

					markers.push(marker);
					bounds.extend(place.geometry.location);
				}

				map.fitBounds(bounds);
				map.setZoom(12);
			});

			google.maps.event.addListener(map, 'bounds_changed', function () {
				var bounds = map.getBounds();
				searchBox.setBounds(bounds);
			});
		}
	};

	return CrimeMap;

})();

//var CrimeMap;
//(function (CrimeMap) {

//	var Map = (function () {

//		function Map(mapContainer) {
//			this.googleMap = null;
//			this.mapContainer = mapContainer;
//			this.isInfoboxOpen = false;
//			this.openInfoboxId = 0;
//			this.infoBoxes = new Array();

//			this.setUp = function () {
//				CrimeMap.location.getUserLocation(function (coords) {
//					var style = CrimeMap.mapStyle.getBlueStyle();
//					var styledMap = new google.maps.StyledMapType(style, { name: 'Occurrences' });

//					var options = {
//						center: new google.maps.LatLng(coords.latitude, coords.longitude),
//						zoom: 12,
//						mapTypeControlOptions: {
//							mapTypeIds: [google.maps.MapTypeId.ROADMAP, 'map_style']
//						},
//						mapTypeId: google.maps.MapTypeId.ROADMAP,
//						draggableCursor: 'default',
//						panControl: !1,
//						zoomControl: !0,
//						mapTypeControl: !1,
//						scaleControl: !1,
//						streetViewControl: !1,
//						overviewMapControl: !1,
//						rotateControl: !1
//					};

//					var map = new google.maps.Map(this.mapContainer, options);

//					map.mapTypes.set('map_style', styledMap);
//					map.setMapTypeId('map_style');

//					google.maps.event.addListener(map, 'click', function (event) {
//						placeNewMarker(event.latLng);
//					});

//					this.googleMap = map;

//					var markers = [];

//					var input = /** @type {HTMLInputElement} */(
//						document.getElementById('places-searcher'));

//					var btn = /** @type {HTMLInputElement} */(
//						document.getElementById('places-searcher-action'));

//					var searchBox = new google.maps.places.SearchBox(
//						/** @type {HTMLInputElement} */(input));

//					google.maps.event.addListener(searchBox, 'places_changed', function () {
//						var places = searchBox.getPlaces();

//						if (places.length == 0) {
//							return;
//						}

//						for (var i = 0, marker; marker = markers[i]; i++) {
//							marker.setMap(null);
//						}

//						markers = [];
//						var bounds = new google.maps.LatLngBounds();

//						for (var i = 0, place; place = places[i]; i++) {
//							var image = {
//								url: place.icon,
//								size: new google.maps.Size(71, 71),
//								origin: new google.maps.Point(0, 0),
//								anchor: new google.maps.Point(17, 34),
//								scaledSize: new google.maps.Size(25, 25)
//							};

//							var marker = new google.maps.Marker({
//								map: map,
//								icon: image,
//								title: place.name,
//								position: place.geometry.location
//							});

//							markers.push(marker);
//							bounds.extend(place.geometry.location);
//						}

//						map.fitBounds(bounds);
//						map.setZoom(12);
//					});

//					google.maps.event.addListener(map, 'bounds_changed', function () {
//						var bounds = map.getBounds();
//						searchBox.setBounds(bounds);
//					});
//				});
//			}

//			function placeNewMarker(location) {
//				if (this.isInfoboxOpen)
//					return;

//				console.log(CrimeMap.Map.isInfoboxOpen);
//				console.log(CrimeMap.Map);
//				console.log(CrimeMap.Map.infoBoxes);

//				var marker = new google.maps.Marker({
//					id: 9999,
//					position: location,
//					map: this.googleMap
//				});

//				var info_box_options = {
//					content: "<p>Box Content</p>",
//					pixelOffset: new google.maps.Size(-150, 0)
//				};

//				var infobox = new InfoBox(info_box_options);

//				this.infoBoxes[marker.id] = new InfoBox(info_box_options);
//				this.infoBoxes[marker.id].marker = marker;
//				this.infoBoxes[marker.id].listener = google.maps.event.addListener(marker, 'click', function (e) {
//					openInfobox(ponto.id, marker);
//				});
//			};

//			function openInfobox(id, marker) {

//				if (this.isInfoboxOpen)
//					return;

//				if (typeof this.infoBoxes[this.openInfoboxId] == 'object')
//					this.infoBoxes[this.openInfoboxId].close();

//				this.infoBoxes[id].open(this.googleMap, marker);
//				this.isInfoboxOpen = true;
//			};
//		}

//		return Map;

//	})();

//	CrimeMap.Map = Map;

//})(CrimeMap || (CrimeMap = {}));
