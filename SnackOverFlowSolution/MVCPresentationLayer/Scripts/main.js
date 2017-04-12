(function(){
	'use strict';

	/*------------------------------------------
	GLOBAL VARIABLES
	------------------------------------------*/

	/* Window Object and variables */
	var $window = $(window),
		$document = $(document),
		_windowH = $window.height(),
		_windowW = $window.width(),
		_windowST = $window.scrollTop();

	/* Global objects */
	var $html = $('html'),
		$body = $('body');

	/* Update global variables when window is resized */
	$window.on('load resize', function() {
		$window = $(window);
		_windowH = $window.height();
		_windowW = $window.width();
		_windowST = $window.scrollTop();		
	});

	/* Update scrollTop on window scroll */
	$window.on('scroll', function() {
		_windowST = $window.scrollTop();		
	});

	$document.ready(function() {
		/*------------------------------------------
		SHOW THE DOCUMENT
		------------------------------------------*/
		$('html').show();

		/*------------------------------------------
		INITIALIZING FUNCTIONS
		------------------------------------------*/

		smoothScroll(1.3, 300); // Enable page smooth scroll   ==(firstValue: scroll time, secondValue: distance)==

		mainNavNicescroll(); // Enable main nav niceScroll

		navTrigger(); // Enable main nav trigger

		mobileNav(); // Enable mobile nav

		tweets(); // Enable Twitter

		teamCarousel(); // Enable team carousel

		galleryStyle1(); // Enable gallery style 1

		galleryStyle2(); // Enable gallery style 2

		menuMeals(); // Enable menu meals

		contactForm(); // Enable contact form

		// headerBanner(); // Enable top header banner

		membersCarousel(); // Enable Member Carousel

		testimonialsCarousel(); // Enable testimonials carousel

		clientsCarousel(); //Enable clients carousel

		itemSlideshow(); // Enable item slideshow

		mobileNavStickyInit(); // Enable sticky mobile nav

		doubleView(); // Enable double view

		storeItems(); // Enable store items mixitup

		blogPost(); // Enable blog post mixitup

		counter(); // Enable counter

		skills(); // Enable skills bar

		mainFooter2(); // Enable main-foote style2

		promoVideo(); // Enable promo video
	});

	/*------------------------------------------
	ENABLE SMOOTH SCROLL
	------------------------------------------*/
	function smoothScroll(scrollTime, scrollDistance){
		
		var scrollTime = scrollTime;			//Scroll time
		var scrollDistance = scrollDistance;		//Distance. Use smaller value for shorter scroll and greater value for longer scroll

		if (navigator.userAgent.indexOf('Mac OS X') != -1) {
			return false;
		} else {
			$window.on("mousewheel DOMMouseScroll", function(event){
				
				event.preventDefault();	
												
				var delta = event.originalEvent.wheelDelta/120 || -event.originalEvent.detail/3;
				var scrollTop = $window.scrollTop();
				var finalScroll = scrollTop - parseInt(delta*scrollDistance);
					
				TweenMax.to($window, scrollTime, {
					scrollTo : { y: finalScroll, autoKill:true },
						ease: Expo.easeOut,	//For more easing functions see http://api.greensock.com/js/com/greensock/easing/package-detail.html
						autoKill: true,
						overwrite: 5							
					});
							
			});
		}
			
	}

	/*-----------------------------------------
	BACKGROUND PARALLAX INIT
	------------------------------------------*/
	function parallaxBg(){
		var parallaxEl = $('.parallax-bg');
		if ( _windowW >= 992 ) {	
			parallaxEl.fadeIn('fast');
			$.stellar({
				horizontalOffset: 0,
				verticalOffset: 0,
				positionProperty: 'transform',
				responsive: true
			});
		};
	};

	/*-----------------------------------------
	MAIN NAV INIT
	------------------------------------------*/
	function navTrigger(){
		var navTrigger = $('.main-nav-trigger').not('.mobile-nav-trigger'),
			mainNavContainer = $('.main-nav-container'),
			wrapper = $('.wrapper');

		navTrigger.on('click', function(event) {
			event.preventDefault();
			mainNavContainer.toggleClass('slide-in');
			$(this).toggleClass('slide-out');
		});

		wrapper.on('click', function() {
			mainNavContainer.removeClass('slide-in');
			navTrigger.removeClass('slide-out');
		});

		if ( $('.main-nav-trigger.style2').length ) {
			navTrigger.on('click', function() {
				$('body, html').toggleClass('menu-opened');
			});
		};
	};

	/*-----------------------------------------
	MOBILE NAV INIT
	------------------------------------------*/
	function mobileNav(){
		var mobileNavContainer = $('.mobile-nav-container'),
			mainNavContainer = $('.main-nav-container'),
			navTrigger = $('.mobile-nav-trigger');

		if ( !mobileNavContainer.find('.logo-container').length ) {
			mainNavContainer.find('.logo-container').clone().appendTo(mobileNavContainer);
		};
		mainNavContainer.find('.main-nav').clone().appendTo(mobileNavContainer);

		navTrigger.on('click', function(event) {
			event.preventDefault();
			$(this).siblings('.main-nav').slideToggle();
		});
	};

	/*-----------------------------------------
	SCROLLTO INIT
	------------------------------------------*/
	$('.onepage-nav').onePageNav();

	/*-----------------------------------------
	TEAM CAROUSEL
	------------------------------------------*/
	function teamCarousel(){

		var memberInfo = $(".members-details-container"),
			memberImage = $(".members-images-container");

		memberInfo.owlCarousel({
			items : 1,
			singleItem : true,
			loop: false,
			nav: true,
			mouseDrag: false,
			animateIn: 'fadeIn',
			animateOut: 'fadeOutRight',
			navContainer: '.team-carousel-nav',
			navText: ['<span></span>', '<span></span>']
		});
		memberImage.owlCarousel({
			items : 1,
			singleItem : true,
			mouseDrag: false,
			animateIn: 'fadeIn',
			animateOut: 'fadeOutRight',
			loop: false
		});

		memberInfo.on('changed.owl.carousel', function(event) {
			memberImage.trigger('to.owl.carousel', event.item.index);
		});

	};

	/*-----------------------------------------
	TWEETER FEED
	------------------------------------------*/
	function tweets(){
		if( $('.tweets-container').length ) {
	        $('.tweet').twittie({
	            // username: 'google',
	            dateFormat: '%B %d, %Y',
	            template: '<p>{{tweet}}<span>{{date}}</span></p>',
	            count: 1,
	            hideReplies: true,
	            apiPath: 'php/twitter-feed/tweet.php'
	        });
        };
    };

	/*-----------------------------------------
	GALLERY INIT
	------------------------------------------*/
	function galleryStyle1(){
		if ( $('.gallery:not(.style2)').length ) {
			$('.gallery-items-container').find('ul').mixItUp({
				animation: {
					duration: 550,
					effects: 'fade stagger(20ms) translateZ(-300px)',
					easing: 'cubic-bezier(0.645, 0.045, 0.355, 1)'
				}
			});
		};
	};

	/*-----------------------------------------
	GALLERY CAROUSEL
	------------------------------------------*/
	function galleryStyle2(){
		var itemsContainer = $('.gallery.style2').find('.gallery-items-container'),
			tallItem = itemsContainer.find('.tall').first(),
			shortItem = itemsContainer.find('.short');

		if ( $('.gallery.style2').length ) {

			itemsContainer.imagesLoaded(function(){
				function equalizing(){
					var halfLargeItemH = tallItem.height() / 2;
					shortItem.height(halfLargeItemH);
				};
				equalizing();
				
				var itemsMasonry = itemsContainer.isotope({
					itemSelector: 'li',
					layoutMode: 'masonry',
					transitionDuration: '0.85s',
					hiddenStyle: {
					      opacity: 0,
					      transform: 'scale(0.2)'
					    },
					    visibleStyle: {
					      opacity: 1,
					      transform: 'scale(1)'
					    }
				});
				$('.gallery-filter').on( 'click', 'li', function() {
					var filterValue = $( this ).attr('data-filter');
					itemsContainer.isotope({ filter: filterValue });
				});
				$('.gallery-filter').each( function( i, buttonGroup ) {
					var $buttonGroup = $( buttonGroup );
					$buttonGroup.on( 'click', 'li', function() {
						$buttonGroup.find('.active').removeClass('active');
						$( this ).addClass('active');
					});
				});
				itemsMasonry.isotope('on', 'layoutComplete', function(laidOutItems){
					equalizing();
				});
			});
		};
	};

	/*-----------------------------------------
	POPUP INIT
	------------------------------------------*/
	if ( $('.popup-trigger').length ) {
		$('.popup-trigger').magnificPopup({
			type: 'image',
		  	gallery: {
		    	enabled: true
		  	},
			removalDelay: 500,
			mainClass: 'mfp-fade'
		});
	};

	/*-----------------------------------------
	MENU CAROUSEL INIT
	------------------------------------------*/
	if ( $('.menus').length ) {
		$('.menus').imagesLoaded(function(){
			var menuCarousel = $('.menus').not('.style3').find('.menu-carousel').owlCarousel({
				singleItem: true,
				items: 1,
				nav: true,
				mouseDrag: false,
				navSpeed: 1000,
				animateIn: 'fadeIn',
				animateOut: 'fadeOutDown',
				navContainer: '.menu-carousel-nav',
				navText: ['<span></span>', '<span></span>']
			});	
		})
	};

	function menuMeals(){
		
		var menuCarousel = $('.menus').not('.style3').find('.menu-carousel');
		if ( $('.menu-meals').length ) {
			var menuMeals = $('.menu-meals'),
				menuMealsThumbnail = menuMeals.owlCarousel({
				items: 1,
				singleItem: true,
				mouseDrag: false,
				touchDrag: false
			});

			menuMeals.find('.owl-item').on('click', function(event) {
				var $this = $(this);
				$this.addClass('active').siblings().removeClass('active');
				menuCarousel.trigger('to.owl.carousel', $this.index());
			});

			menuCarousel.on('changed.owl.carousel', function(event) {
				var activeMenu = event.item.index;
				console.log(activeMenu);
				menuMeals.find('.owl-item:nth-child('+ (activeMenu + 1) + ')' ).addClass('active').siblings().removeClass('active')
			});

		};
	};

	/*-----------------------------------------
	CONTACT FORM INIT
	------------------------------------------*/
	function contactForm() {

	    var form = $('#contact-form');
	    var formMessages = $('#form-messages');
        $(formMessages).slideUp();

	    $(form).submit(function(event) {
	        event.preventDefault();
	        var formData = $(form).serialize();

	        if ( !$('#name').val() || !$('#email').val() || !$('#message').val() ) {
	        	$('#form-messages').text('Please Complete All inputs');
	        } else {
	        	$('#form-messages').text('Sending your message. Please wait...').slideDown();
	        };

	        $(formMessages).removeClass('error').removeClass('success');

	        $.ajax({
	            type: 'POST',
	            url: $(form).attr('action'),
	            data: formData
	        })
	        .done(function(response) {
	            $(formMessages).removeClass('error').delay(2000).slideUp();
	            $(formMessages).addClass('success').delay(2000).slideUp();

	            $(formMessages).text(response);

	            $('#name').val('');
	            $('#email').val('');
	            $('#message').val('');
	        })
	        .fail(function(data) {
	            // Make sure that the formMessages div has the 'error' class.
	            $(formMessages).removeClass('success').delay(2000).slideUp();
	            $(formMessages).addClass('error').delay(2000).slideUp();

	            // Set the message text.
	            if (data.responseText !== '') {
	                $(formMessages).text(data.responseText);
	            } else {
	                $(formMessages).text('Oops! An error occured and your message could not be sent.');
	            }
	        });
	    });
	};

	/*-----------------------------------------
	HEADER BANNER FADE EFFECT
	------------------------------------------*/
	function headerBanner(){
		var introSection = $('.top-banner-bg'),
			topBanner = $('.top-banner'),
			topImage = topBanner.find('.top-image'),
			bottomImage = topBanner.find('.bottom-image'),
			introSectionHeight = introSection.height(),
			scaleSpeed = 0.3,
			opacitySpeed = 1; 
		var MQ = 991;

		function triggerAnimation(){
			if($window.width()>= MQ) {
				$window.on('scroll', function(){
					window.requestAnimationFrame(animateIntro);
				});
			} else {
				$window.off('scroll');
			}
		}
		function animateIntro () {
			var scrollPercentage = (_windowST/introSectionHeight).toFixed(5),
				scaleValue = 1 - scrollPercentage*scaleSpeed;
			if( _windowST < introSectionHeight) {
				topBanner.css({
					'opacity': 1 - scrollPercentage * 1.6
				});
			}
		};
		if ( $('.top-banner-container').length ) {
			triggerAnimation();
			$window.on('resize', function(){
				triggerAnimation();
			});
			topBanner.imagesLoaded(function(){
				topImage.addClass('animated fadeInDown')
				bottomImage.addClass('animated fadeInDown')
			});
		};
	};

	/*-----------------------------------------
	MEMBERS CAROUSEL
	------------------------------------------*/
	function membersCarousel(){
		var membersCarousel = $('.members-carousel');
		if ( membersCarousel.length ) {
			membersCarousel.imagesLoaded(function(){
				membersCarousel.find('ul').owlCarousel({
					items: 1,
					loop: true,
					responsive : {
					    496: {
					        items: 2
					    },
					    767: {
					        items: 3
					    },
					    992 : {
					        items: 4
					    },
					    1199 : {
					        items: 5
					    }
					}
				});
			});
		};
	};

	/*-----------------------------------------
	TESTIMONIALS CAROUSEL
	------------------------------------------*/
	function testimonialsCarousel(){
		var testimonialsCarousel = $('.testimonial-carousel');
		if ( testimonialsCarousel.length ) {
			testimonialsCarousel.imagesLoaded(function(){
				testimonialsCarousel.owlCarousel({
					singleItem: true,
					items: 1,
					loop: true,
					dots: true,
					autoplay: true,
					autoplayTimeout: 3500,
					autoplayHoverPause: true,
					animateIn: 'fadeInLeft',
					animateOut: 'fadeOutRight',
					dotsContainer: '.testimonial-carousel-nav'
				});
			});
		};
	};

	/*-----------------------------------------
	CLIENTS CAROUSEL
	------------------------------------------*/
	function clientsCarousel(){
		var clientsCarousel = $('.clients-carousel');
		if ( clientsCarousel.length ) {
			clientsCarousel.imagesLoaded(function(){
				clientsCarousel.owlCarousel({
					items: 1,
					autoplay: true,
					autoplayTimeout: 3500,
					autoplayHoverPause: true,
					responsive : {
					    496: {
					        items: 2
					    },
					    767: {
					        items: 3
					    },
					    992 : {
					        items: 4
					    },
					    1199 : {
					        items: 5
					    }
					}
				});
			});
		};
	};

	/*-----------------------------------------
	BLOG POST CATEGORY FILTER INIT
	------------------------------------------*/
	function blogPost(){

		if ( $('.blog-category-filter').length ) {
			$('.blog-post').mixItUp({
				animation: {
					duration: 550,
					effects: 'fade stagger(20ms) translateZ(-300px)',
					easing: 'cubic-bezier(0.645, 0.045, 0.355, 1)'
				}
			});
		};
	};

	/*-----------------------------------------
	STORE CATEGORY FILTER INIT
	------------------------------------------*/
	function storeItems(){

		if ( $('.store-category-filter').length ) {
			$('.store-items').mixItUp({
				animation: {
					duration: 550,
					effects: 'fade stagger(20ms) translateZ(-300px)',
					easing: 'cubic-bezier(0.645, 0.045, 0.355, 1)'
				}
			});
		};
	};

	/*-----------------------------------------
	ITEM SLIDESHOW
	------------------------------------------*/
	function itemSlideshow(){

		var itemSlideshow = $(".item-slideshow"),
			mainImage = itemSlideshow.find('.main-image'),
			thumbnails = itemSlideshow.find('.thumbnails');

		if ( itemSlideshow.length ) {
			itemSlideshow.imagesLoaded(function(){
				mainImage.owlCarousel({
					items : 1,
					singleItem : true,
					animateIn: 'fadeInLeft',
					animateOut: 'fadeOutRight'
				});
				thumbnails.owlCarousel({
					items : 2,
					margin: 15,
					responsive : {
					    768: {
					        items: 3 
					    }
					}
				});

				thumbnails.find('.owl-item').on('click', function() {
					$(this).addClass('active').siblings().removeClass('active');
					mainImage.trigger('to.owl.carousel', $(this).index());
				});
			});
		};

	};

	/*------------------------------------------
	MEDIAELEMENTS INIT
	------------------------------------------*/
	if ( $('video').length ) {
		new MediaElementPlayer('video');
	};
	if ( $('audio').length ) {
		$('audio').mediaelementplayer();
	};

	/*------------------------------------------
	PROMO VIDEO
	------------------------------------------*/
	function promoVideo(){
		var promoVideo = $('.promo-video');
		if ( promoVideo.length ) {
			promoVideo.each(function() {
				var $this = $(this),
				promoVideoContent = $this.find('.promo-contents');
				$this.find('video').on('play', function(){
					promoVideoContent.fadeOut('200');
				});
				$this.find('video').on('pause', function(){
					promoVideoContent.fadeIn('200');
				});
			});
		};
	}


	/*------------------------------------------
	SCROLL REVEAL INIT
	------------------------------------------*/
	if ( $('.wow').length ) {
		$('.wow').show()
		var wow = new WOW({mobile: false});
		wow.init();
	};


	/*------------------------------------------
	CART ITEM COUNT INIT
	------------------------------------------*/
	if ( $('.cart-item-count').length ) {
		$('.cart-item-count').stepper();
	};

	/*------------------------------------------
	MAIN NAV NICESCROLL INIT
	------------------------------------------*/
	function mainNavNicescroll(){
		if ( $('.main-nav-container').length ) {
			$('.main-nav-container').niceScroll({
		        cursoropacitymax: 0.3
			});
		};
	};

	/*------------------------------------------
	MOBILE NAV STICKY INIT
	------------------------------------------*/
	function mobileNavStickyInit(){
		var mobileNavContainer = $('.mobile-nav-container'),
			mobileNavHeight = mobileNavContainer.outerHeight(),
			wrapper = $('.wrapper');

		$('.main-header .logo-container').imagesLoaded(function(){
			$window.on('scroll', function(){
				//The window.requestAnimationFrame() method tells the browser that you wish to perform an animation- the browser can optimize it so animations will be smoother
				// window.requestAnimationFrame(mobileNav);
			});
		});

		function mobileNav(){

			if ( _windowST > mobileNavHeight && _windowW <= 991 ) {
				$('.mobile-nav-container').addClass('sticky animated fadeInDown');
				wrapper.css('padding-top', mobileNavHeight);
			} else {
				$('.mobile-nav-container').removeClass('sticky animated fadeInDown');
				if ($('.frame-on').length && _windowW >= 992) {
					wrapper.css('padding-top', '1.8%');
				} else{
					wrapper.css('padding-top', 0);
				}
			};
		};
	};

	/*------------------------------------------
	FOOTER STYLE 2
	------------------------------------------*/
	function mainFooter2(){
		var mainFooter2 = $('.main-footer.style2');
		if ( mainFooter2.length ) {
			mainFooter2.imagesLoaded(function(){
				mainFooter2.find('.left-sec').height( mainFooter2.find('.right-sec').height() )
			})
		};
	};

	/*------------------------------------------
	FOOTER STYLE 2 CONTACT FORM
	------------------------------------------*/
	$('.contact-form-trigger').on('click', function(event) {
		event.preventDefault();
		var contactFormContainer = $('.contact-form-container'),
			removeBtn = $('<span class="remove-btn"></span>');
		contactFormContainer.toggleClass('slide-in');

		if ( contactFormContainer.hasClass('slide-in') == true ) {
			$('html, body').animate({scrollTop: contactFormContainer.offset().top})
		};

		if ( contactFormContainer.find('.remove-btn').length ) return;
		removeBtn.appendTo(contactFormContainer)
				 .on('click', function() {
				 	$('.contact-form-container').removeClass('slide-in');
				 });
	});
	
    /*-----------------------------------------
    COUNTER INIT
    ------------------------------------------*/
    function counter(){
    	var counter = $('.counter');
	    if ( counter.not('.no-animate').length ) {
			counter.appear(function(){
				var $this = $(this),
					counterNumber = $this.find('.number');
				counterNumber.countTo({to: $this.find('.number').text(), refreshInterval: 3});
			});
		};
	};

    /*-----------------------------------------
    SKILLS INIT
    ------------------------------------------*/
    function skills(){
    	var skill = $('.skill');
	    if ( skill.length ) {
			skill.appear(function(){
				var $this = $(this),
					skillBar = $this.find('.skill-bar'),
					skillPercentage = $this.find('.skill-percentage'),
					skillPercentageText = skillPercentage.text();
				skillBar.width(skillPercentageText + '%');
				skillPercentage.countTo({
					to: skillPercentageText,
					refreshInterval: 3
				});
			});
		};
    };

    /*-----------------------------------------
    DOUBLE VIEW
    ------------------------------------------*/
    function doubleView(){
    	var doubleViewContainer = $('.double-view'),
    		imageContainer = doubleViewContainer.find('.image-container'),
    		contentsContainer = doubleViewContainer.find('.contents-container'),
    		mainImage = imageContainer.find('img'),
    		imageSrc = mainImage.attr('src');
    	if (doubleViewContainer.length && _windowW >= 992) {
    		doubleViewContainer.imagesLoaded(function(){
    			imageContainer.height(contentsContainer.outerHeight()).parent('[class^=col-]').height(contentsContainer.outerHeight());
    			imageContainer.find('figure').css('background-image', 'url(' + imageSrc + ')');
    			mainImage.hide();
    		});
    	};
    };

    /*------------------------------------------
    SUB MENUS
    ------------------------------------------*/
    $('.dropdown .dropdown-toggle').on('click', function(event) {
    	event.preventDefault();
    	$(this).parent('.dropdown').toggleClass('open').end().siblings('.sub-menu').slideToggle(200, function(){
	    	$(".main-nav-container").getNiceScroll().resize();
    	});
    });

    /*------------------------------------------------------------
    FUNCTIONS THAT NEED TO RUN WHEN THE dOCUMENT IS FULLY LOADED
    -------------------------------------------------------------*/
    $window.on('load', function() {
    	parallaxBg(); // Enable parallax Background
    });

    /*------------------------------------------------------------
    FUNCTIONS THAT NEED TO RUN WHEN WINDOW IS RESIZED
    -------------------------------------------------------------*/
    $window.on('resize', function() {

    	mobileNavStickyInit();

    	doubleView();
		
		if ( $('.main-footer.style2').length ) {
			$('.main-footer.style2').find('.left-sec').height( $('.main-footer.style2').find('.right-sec').height() )
		};
    });


})();