 	$('#inner-content-div').slimScroll({ height: '120px' });

    (function($) {
        $(document).ready(function() {
            $('ul.dropdown-menu [data-toggle=dropdown]').on('click', function(event) {
                event.preventDefault();
                event.stopPropagation();
                $(this).parent().siblings().removeClass('open');
                $(this).parent().toggleClass('open');
            });
        });
    })(jQuery);

    $(function() {
        function onScrollInit(items, trigger) {
            items.each(function() {
                var osElement = $(this),
                    osTuyendartClass = osElement.attr('data-os-tuyendart'),
                    osTuyendartDelay = osElement.attr('data-os-tuyendart-delay');

                osElement.css({
                    '-webkit-animation-delay': osTuyendartDelay,
                    '-moz-animation-delay': osTuyendartDelay,
                    'animation-delay': osTuyendartDelay
                });
                var osTrigger = (trigger) ? trigger : osElement;

                osTrigger.waypoint(function() {
                    osElement.addClass('w3danima').addClass(osTuyendartClass);
                }, {
                    triggerOnce: true,
                    offset: '90%'
                });
            });
        }
        onScrollInit($('.os-tuyendart'));
        onScrollInit($('.staggered-tuyendart'), $('.staggered-tuyendart-container'));
    });

    
    $('#menu').hcSticky({ top: -1, bottomEnd: 100, wrapperClassName: 'sidebar-sticky' });
    
    $(document).ready(function() {
        
        var owl_partner = $("#owl-demo2");
        owl_partner.owlCarousel({
            items: 5,
            nav: false,
            navigation: true,
            navigationText: ["<img src='images/icon1.jpg'>", "<img src='images/icon2.jpg'>"] 
        });
        
        var owl_client = $("#owl-demo3");
        owl_client.owlCarousel({
            items: 5,
            nav: false,
            navigation: true,
            navigationText: ["<img src='images/icon1.jpg'>", "<img src='images/icon2.jpg'>"]
        });
		
        // Menu View More
        $('.mega-dropdown ul.mega-dropdown-menu li ul').each(function(){
          var max = 5
          if ($(this).find("li").length > max) {
                $(this)
                  .find('li:gt('+max+')')
                  .hide()
                  .end()
                  .append(
                        $('<li class="mn_custom_sub"><a href="javascript:void(0);">'+read_more+'...</a></li>').mouseover( function(){
                          $(this).siblings(':hidden').show().end().remove();
                        })
                );
          }
        });
        
    });
    
    $(function(){
        
        $(window).scroll(function(){
                scroll_event_handler();
        });

        $('.sub_top').click(function(){
            $('html, body').animate({scrollTop : 0},800);
            return false;
        });

        function scroll_event_handler(){
            $("#menu").each(function(){
                    var pos_y = $(this).offset().top - $(window).scrollTop();
            });
            fixmenu();   
        }

        function fixmenu() {
            var $doc = $(document);
            var mainvisual = $('#menu').offset().top - $(window).scrollTop();

            if(mainvisual <= 50){
                    $('.sub_top').fadeIn();
            } else {
                    $('.sub_top').fadeOut();
            }
        }
    });