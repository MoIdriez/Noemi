/*------------------------------
 * Copyright 2016 ThemeJumbo.com
 * http://www.themejumbo.com
 *
 * Roxie theme v1.31
 * Pixelized studio - Front-end developers from Czech Republic
 * info@themejumbo.com
 * We do not support theme customizations beyond it's original functionality & appearance.
------------------------------*/

$(window).scroll(function(){
	
	/*------------------------------
		FIXED NAVBAR
	------------------------------*/	
	if($(window).width() > 767) {
		
		if($(window).scrollTop() > 50) {
			if($('header.navbar-default').hasClass("navbar-static-top")) {
				$('header.navbar-default').removeClass('navbar-static-top');
				$('header.navbar-default').addClass('navbar-fixed-top navbar-offset');
				$('body').css("padding-top","70px");
			}
		}
				
		else {
			$('header.navbar-default').removeClass('navbar-fixed-top navbar-offset');
			$('header.navbar-default').addClass('navbar-static-top');
			$('body').css("padding-top","0px");
		}
	}
	
	else {
		if($(window).scrollTop()) {
			if($('header.navbar-default').hasClass("navbar-static-top")) {
				$('header.navbar-default').addClass('navbar-fixed-top navbar-offset');
				$('header.navbar-default').removeClass('navbar-static-top');
				$('body').css("padding-top","40px");
			}
		}
	}
	
	/*------------------------------
		TRANSPARENT NAVBAR
	------------------------------*/
	if($(window).width() > 1199) {
		if($(window).scrollTop() > 300) {
			$('header.navbar-transparent').addClass('navbar-offset');
		}
		else {
			$('header.navbar-transparent').removeClass('navbar-offset');
		}
	}
	else if($(window).width() > 991) {
		if($(window).scrollTop() > 200) {
			$('header.navbar-transparent').addClass('navbar-offset');
		}
		else {
			$('header.navbar-transparent').removeClass('navbar-offset');
		}
	}
	else if($(window).width() > 767) {
		if($(window).scrollTop() > 100) {
			$('header.navbar-transparent').addClass('navbar-offset');
		}
		else {
			$('header.navbar-transparent').removeClass('navbar-offset');
		}
	}
	else {
		if($(window).scrollTop()) {
			$('header.navbar-transparent').addClass('navbar-offset');
		}
	}
	
	/*------------------------------
		SCROLL TOP
	------------------------------*/
	if($(window).scrollTop() > 300) {
		$("#scrolltop").addClass("in");
	}
	else {
		$("#scrolltop").removeClass("in");
	}
	
});

$(document).ready(function() {	

	/*------------------------------
		SLIDER RANGE
	------------------------------*/
	var minimum = 1250;
	var maximum = 1500;
	
	$( "#slider-range" ).slider({
      range: true,
      min: minimum,
      max: maximum,
      values: [ minimum, maximum ],
      slide: function( event, ui ) {
        $( "#amount" ).val( "$" + ui.values[ 0 ] );
		$( "#amount2" ).val( "$" + ui.values[ 1 ] );
      }
    });
    $( "#amount" ).val( "$" + $( "#slider-range" ).slider( "values", 0 ));
	$( "#amount2" ).val( "$" + $( "#slider-range" ).slider( "values", 1 ));
		
	/*------------------------------
		SCROLL FUNCTION
	------------------------------*/
	function scrollToObj(target, offset, time) {
		$('html, body').animate({scrollTop: $( target ).offset().top - offset}, time);
	}
	
	$("a.scroll[href^='#']").click(function(){
		scrollToObj($.attr(this, 'href'), 100, 1000);
		return false;
	});
	
	$("#scrolltop").click(function() {
		scrollToObj('body',0, 1000);
    });
	
	/*------------------------------
		TOOLTIP
	------------------------------*/
	$('.tooltip-init').tooltip();
	
	/*------------------------------
		POPOVER
	------------------------------*/
	$('.popover-init').popover();
	
	/*------------------------------
		PORTFOLIO - ISOTOPE
	------------------------------*/
	var $container = $('.portfolio-wrapper');
	$container.isotope({
	  	itemSelector: '.portfolio-item',
	});
	
	$('.portfolio-filter li a').click(function(e) {
		$('.portfolio-filter li a').removeClass('active');
		$(this).addClass('active');
		
        var category = $(this).attr('data-filter');
		$container.isotope({
			filter: category
		});
    });
	
	/*------------------------------
		BLOG - ISOTOPE
	------------------------------*/
	var $container2 = $('.blog-wrapper');
	$container2.isotope({
	  	itemSelector: '.blog-item',
		layoutMode: 'fitRows'
	});
	
	$('.blog-filter li a').click(function(e) {
		$('.blog-filter li a').removeClass('active');
		$(this).addClass('active');
		
        var category = $(this).attr('data-filter');
		$container2.isotope({
			filter: category
		});
    });
	
	/*------------------------------
		LOST PASSWORD TOGGLE
	------------------------------*/
	$('#reset-password-toggle').click(function() {
        $('#reset-password').slideToggle(500);
    });
	
	/*------------------------------
		ESHOP TOGGLE
	------------------------------*/
	$(".addtocart").click(function() {
        $("#eshop-cart-alert").toggleClass("active");
    });
	
	$("#eshop-cart-alert .close").click(function() {
        $("#eshop-cart-alert").toggleClass("active");
    });
	
	$('#billing-address-toggle').click(function() {
        $('#billing-address').slideToggle(500);
    });	
		
	/*------------------------------
		MAGNIFIC POPUP
	------------------------------*/
	$('.show-image').magnificPopup({type:'image'});
		
	/*------------------------------
		OWL CAROUSEL
	------------------------------*/
	
	
	$("#jumbotron-slider").owlCarousel({
    	items : 1,
		loop : true,
		autoplay : true,
		dots : false,
		nav : true,
		navText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
		animateOut: 'fadeOut',
    	animateIn: 'fadeIn'
  	});
	
	$("#partners-slider").owlCarousel({
		dots : false,
		margin: 20,
		mouseDrag: true,
		nav: true,
		navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
		autoplay : true,
		responsive:{
			0:{
				items:1,
			},
			480:{
				items:2,
			},
			768:{
				items:3,
			},
			992:{
				items:4,
			},
			1200:{
				items:5,
			}
		}
	});

	$("#partners-slider-f").owlCarousel({
	    dots: false,
	    margin: 20,
	    autoplay: true,
	    responsive: {
	        0: {
	            items: 1,
	        },
	        480: {
	            items: 2,
	        },
	        768: {
	            items: 3,
	        },
	        992: {
	            items: 4,
	        },
	        1200: {
	            items: 5,
	        }
	    }
	});
			
	$(".about-slider").owlCarousel({
		autoplay: true, 
		items : 1,
  	});
	
	$("#jumbotron-eshop-slider").owlCarousel({
		items : 1,
		autoplay : true,
		dots : false,
		nav : true,
		navText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
		animateOut: 'fadeOut',
    	animateIn: 'fadeIn'
  	});
	
	$("#eshop-slider").owlCarousel({
		autoplay : true,
		responsive:{
			0:{
				items:2,
			},
			480:{
				items:3,
			},
			768:{
				items:4,
			},
			992:{
				items:3,
			},
		}
  	});
	
	$('#eshop-slider .item img').mouseenter(function(e) {
		var source = $(this).attr("src");
		$("#product-detail-image").attr("src",source);
		$("#product-detail-image-link").attr("href",source);
    });
	
	$("#portfolio-slider").owlCarousel({
		items : 1,
		loop : true,
		autoplay : true,
		nav : true,
		navText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
  	});
	
	$("#blog-slider").owlCarousel({
		items : 1,
		loop : true,
		autoplay : true,
		dots : false,
		nav : true,
		navText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
  	});
	
	$("#about-us-slider").owlCarousel({
		items : 1,
		autoplay : true,
		animateOut: 'fadeOut',
    	animateIn: 'fadeIn'
  	});
	
	$("#testimonials-slider").owlCarousel({
		items : 1,
		autoplay : true,
    	animateIn: 'flipInX'
  	});
	
	$("#features-default-carousel #owl-carousel-default").owlCarousel({
		items : 1,
		autoplay : true,
		nav : true,
		navText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
		animateOut: 'fadeOut',
    	animateIn: 'fadeIn'
  	});
	
	$("#features-fade-carousel #owl-carousel-fade").owlCarousel({
		items : 1,
		autoplay : true,
		nav : true,
		navText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
  	});
	
	$("#features-owl-carousel #carousel-wrapper").owlCarousel({
		autoplay : true,
		responsive:{
			0:{
				items:2,
			},
			768:{
				items:3,
			},
			1200:{
				items:4,
			},
		}
  	});
	
	/*------------------------------
		OWL CAROUSEL - HIDDEN CONTROLS
	------------------------------*/
	$(".owl-carousel.hidden-control").mouseenter(function(e) {
		$(this).find(".owl-prev").animate({opacity:1,left:"20px"});
		$(this).find(".owl-next").animate({opacity:1,right:"20px"});
    });
	
	$(".owl-carousel.hidden-control").mouseleave(function(e) {
		$(this).find(".owl-prev").animate({opacity:0,left:"40px"});
		$(this).find(".owl-next").animate({opacity:0,right:"40px"});
    });
		
	/*------------------------------
		PARTNER BRANDS
	------------------------------*/
	$('#partner_001').mouseleave(function () { $(this).attr("src", "../images/partners/partner_001.jpg"); });
	$('#partner_001').mouseenter(function () { $(this).attr("src", "../images/partners/partner_001_bw.jpg"); });
	$('#partner_002').mouseleave(function () { $(this).attr("src", "../images/partners/partner_002.jpg"); });
	$('#partner_002').mouseenter(function () { $(this).attr("src", "../images/partners/partner_002_bw.jpg"); });
	$('#partner_003').mouseleave(function () { $(this).attr("src", "../images/partners/partner_003.jpg"); });
	$('#partner_003').mouseenter(function () { $(this).attr("src", "../images/partners/partner_003_bw.jpg"); });
	$('#partner_004').mouseleave(function () { $(this).attr("src", "../images/partners/partner_004.jpg"); });
	$('#partner_004').mouseenter(function () { $(this).attr("src", "../images/partners/partner_004_bw.jpg"); });
	$('#partner_005').mouseleave(function () { $(this).attr("src", "../images/partners/partner_005.jpg"); });
	$('#partner_005').mouseenter(function () { $(this).attr("src", "../images/partners/partner_005_bw.jpg"); });
	$('#partner_006').mouseleave(function () { $(this).attr("src", "../images/partners/partner_006.jpg"); });
	$('#partner_006').mouseenter(function () { $(this).attr("src", "../images/partners/partner_006_bw.jpg"); });
	$('#partner_007').mouseleave(function () { $(this).attr("src", "../images/partners/partner_007.jpg"); });
	$('#partner_007').mouseenter(function () { $(this).attr("src", "../images/partners/partner_007_bw.jpg"); });
	
	/*------------------------------
		COUNTDOWN - EVENT
	------------------------------*/
	$('.jumbotron-event .countdown').countdown('2016/09/22', function(event) {
	    var $this = $(this).html(event.strftime(''
	      + '<div><span class="countdown-number">%w</span><span class="countdown-title">Weeks</span></div> '
	      + '<div><span class="countdown-number">%d</span><span class="countdown-title">Days</span></div> '
	      + '<div><span class="countdown-number">%H</span><span class="countdown-title">Hours</span></div> '
	      + '<div><span class="countdown-number">%M</span><span class="countdown-title">Minutes</span></div> '
	      + '<div><span class="countdown-number">%S</span><span class="countdown-title">Seconds</span></div>'));
	});
	
	/*------------------------------
		COUNTDOWN - COMING SOON
	------------------------------*/
	$('.comingsoon .countdown').countdown('2016/09/22', function (event) {
	    var $this = $(this).html(event.strftime(''
	      + '<div><span class="countdown-number">%w</span><span class="countdown-title">Weeks</span></div> '
	      + '<div><span class="countdown-number">%d</span><span class="countdown-title">Days</span></div> '
	      + '<div><span class="countdown-number">%H</span><span class="countdown-title">Hours</span></div> '
	      + '<div><span class="countdown-number">%M</span><span class="countdown-title">Minutes</span></div> '
	      + '<div><span class="countdown-number">%S</span><span class="countdown-title">Seconds</span></div>'));
	});
	
	/*------------------------------
		COUNTER UP
	------------------------------*/
	$('#section-statistics .number').counterUp({
		delay: 100,
		time: 3000
	});
		
	/*------------------------------
		YOUTUBE VIDEO BACKGROUND
	------------------------------*/
	$(".player").YTPlayer();
	
	/*------------------------------
		GOOGLE MAP
	------------------------------*/	
	var map;
	
	var mapInfo = {
		'lat' : 40.710968,
		'lng' : -74.0084713,
		'zoom' : 16
	};
	
	var markerInfo = {
		'lat' : 40.710968, 
		'lng' : -74.0084713,
		'title' : 'Our Office'
	};
	
	// GOOGLE MAP INIT
	function initialize($) {
		var mapOptions = {
		  	zoom: mapInfo.zoom,
		  	center: mapInfo,
		  	navigationControl: false,
		  	mapTypeControl: false,
		  	scaleControl: false,
		  	draggable: true,
			scrollwheel: false
		}
		map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
		
		
		var marker = new google.maps.Marker({
			position: markerInfo,
			map: map,
			title: markerInfo.title
		});
	}
		
	if($("#map-canvas").length) {
		google.maps.event.addDomListener(window, 'load', initialize);
	}		
});