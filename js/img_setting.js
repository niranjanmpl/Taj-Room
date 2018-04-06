
       $(window).load(function() {
         setTimeout(function() {
        
        var maxh = $(document).height();
        
        var maxw = $(document).width();
         
         $(".section").append('<img src="images/mainimg.jpg" id="main_img" /><img src="images/mob_banner1.jpg" id="mob_img" />');
          
          var nav  =   $("#nav").outerHeight();  
          
          
          
          var section = $(".section").outerWidth();  
          
          var section1 = $('.section').height(nav);
          
            
           var heightsetter = function(){ 
           
                $('.section img').height(nav); 
                
                 $('.section img').width(section); 

           }
                    
          if(maxw <= 480){ 
            
          $('#mob_img').show();
          
          $('#main_img').hide();
            
            
          }
          
          else{
          
           $('#mob_img').hide();
          
           $('#main_img').show();
           
           
            heightsetter();
             
          }
           
 }, 1);

        });
