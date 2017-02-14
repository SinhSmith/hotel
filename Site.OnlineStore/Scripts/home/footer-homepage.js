// JavaScript Document
  
    var h = document.getElementById("col-r").offsetHeight;
    document.getElementById("col-l").style.height = h + "px";
    
    // news
    var curPos1 = 0;
    var timer1 = setTimeout("Slidenews(0)", 9000);

    function Slidenews(index1) {
        clearTimeout(timer1);

        document.getElementById("itm-news" + curPos1).style.display = "none";
        document.getElementById("itm-news" + index1).style.display = "";

        curPos1 = index1;
        index1 = (index1 == 4) ? 0 : (index1 + 1);
        timer1 = setTimeout("Slidenews(" + index1 + ")", 9000);
    }
    
    $(function() {

        var filterList = {
            init: function() {
                $('#portfoliolist').mixitup({
                    targetSelector: '.portfolio',
                    filterSelector: '.filter',
                    effects: ['fade'],
                    easing: 'snap',
                    onMixEnd: filterList.hoverEffect()
                });
            },
            hoverEffect: function() {
                $('#portfoliolist .portfolio').hover(
                    function() {
                        $(this).find('.label').stop().animate({
                            bottom: 0
                        }, 200, 'easeOutQuad');
                        $(this).find('img').stop().animate({
                            top: 0
                        }, 500, 'easeOutQuad');
                    },
                    function() {
                        $(this).find('.label').stop().animate({
                            bottom: -90
                        }, 200, 'easeInQuad');
                        $(this).find('img').stop().animate({
                            top: 0
                        }, 300, 'easeOutQuad');
                    } 
                ); 
            }
        };
        filterList.init();
    });
    
    
    $(document).ready(function() {

        var owl = $("#portfoliolist");

        owl.owlCarousel({
            items: 4,
            navigation: true,
            pagination : false,
            navigationText: ["<span class='glyphicon glyphicon-chevron-left' aria-hidden='true'></span>", "<span class='glyphicon glyphicon-chevron-right' aria-hidden='true'></span>"],
            itemsCustom: [
                [0, 1],
                [450, 1],
                [600, 2],
                [700, 3],
                [1000, 5],
                [1200, 5],
                [1400, 5],
                [1600, 5]
            ]

        });
        
        
        $('.modal').each(function() {
            var src = $(this).find('iframe').attr('src');

            $(this).on('click', function() {

                $(this).find('iframe').attr('src', '');
                $(this).find('iframe').attr('src', src);

            });
        });
        
        // youtube play
        autoPlayYouTubeModal();

    
        var owl = $("#owl-demo");
        owl.owlCarousel({
			autoHeight: false,
            items: 3,
            itemsCustom: [
                [0, 1],
                [450, 1],
                [600, 2],
                [700, 3],
                [1024, 3],
                [1200, 3],
                [1400, 3],
                [1600, 3]
            ]
        });
   

        var owl = $("#owl-demo4");

        owl.owlCarousel({
            items: 4,
            itemsCustom: [
                [0, 1],
                [450, 1],
                [600, 2],
                [700, 3],
                [1000, 5],
                [1200, 5],
                [1400, 5],
                [1600, 5]
            ]

        });

    });
    

    //FUNCTION TO GET AND AUTO PLAY YOUTUBE VIDEO FROM DATATAG
    function autoPlayYouTubeModal() {
        var trigger = $("body").find('[data-toggle="modal"]');
        trigger.click(function() {
            var theModal = $(this).data("target"),
                videoSRC = $(this).attr("data-theVideo"),
                videoSRCauto = videoSRC + "?autoplay=1";
            $(theModal + ' iframe').attr('src', videoSRCauto);
            $(theModal + ' button.close').click(function() {
                $(theModal + ' iframe').attr('src', videoSRC);
            });
        });
    }
