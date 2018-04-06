<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="services.aspx.vb"  Inherits="mpl.Taj.services" %> 
<!DOCTYPE html>
<html>
<head>
    <title id="PageTitle" runat="server"></title>
     <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <link href="taj.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="jquery.jscrollpane.css" rel="stylesheet" media="all" />
</head>
<body>
    <form>
    <div align="center" id="wrapper">
        <div id="header" align="left">
           <!-- #include file = "header.htm" -->
        </div>
        <div id="main">
            <div class="quick_links">
                <span>Menu</span>
            </div>
            <div class="srv_links highlight">
               <!-- #include file = "left_Service.htm" -->
            </div>
            <div id="nav" align="left"  class="support_section">
                <div class="login_section srv_hdr">
                    Services</div>
                <div class="services">
                    <div>
                        <p>
                            <span class="style4">Food and Wine<br>
                            </span>From casual, all-day eateries to formal, fine dining, guests can look forward to a wide selection of delectable dishes and refreshing drinks.
                        </p><br />
                        <p>At Taj, we offer a distinctive dining experience, one that explores the nuances of the finest Indian and international cuisines, serving traditional and contemporary favourites. </p>
                    </div>
                    <div>
                        <span class="style4"><strong>» Caf Mozaic:  </strong></span>A modern fine
                        This lively and inviting caf serves a spectrum of delectable dishes.<br />
                        Attire: Casuals 
                    </div>
                    <div>
                        <span class="style4"><strong>» Graze:  </strong></span>Speciality Mediterrancan
                       A specially restaurant with European sophistication and Asian flair, and an exhilarating menu waiting to be explored.<br />
                        Attire: Smart Casuals 
                    </div>
                    <div>
                        <span class="style4"><strong>» Ice: </strong></span>The high energy bar with cutting edge design, with ultra blue hues and comfortable seating. This is the perfect place for to take in the bartending skills on display producing the best Martinis to add some zing into any evening. Every evening offer a live Teppanyaki grill.Bangalores popular DJs frequently play here.Bangalores trendiest and high energy bar offering signature martinis. 
                    </div>
                    <div>
                        <span class="style4"><strong>» Memories of China:  </strong></span>Guests can experience grand Chinese cooking from our master chef.<br />
                        Attire: Casuals 
                    </div>
                    <div>
                        <span class="style4"><strong>» Sugar &Spice:  </strong></span>The pastry shop of Taj Residency, located off the hotel body, stocks a tempting range of pastries, deserts and fruit tarts.<br />
                        Attire: Casuals 
                    </div>
                    <div>
                        <span class="style4"><strong>Facilities & Services at Taj Residency, Bangalore : </strong></span>Guests can look forward to our quality facilities and distinctive services. 
                    </div>
                    <div>
                        <span class="style4"><strong>Hotel Business Services include:  </strong></span>
                        Business Centre<br />
                        Business Centre Board Room<br />
                        Color Copier<br />
                        Photocopier<br />
                        Secretarial services<br />
                        Translation/interpretation services (Russian/French)<br />
                        Wireless Internet access<br />
                        Workstations<br />

                    </div>
                    <div align="justify">
                        <span class="style4"><strong>Hotel Leisure and Other Services include:  </strong></span>
                        Babysitting <br />
                        Beauty parlour/Hair salon<br />
                        Car hire service <br />
                        Currency exchange<br />
                        Doctor-on-call<br />
                        Jiva Spa<br />
                        Florist <br />
                        Travel assistance<br />
                        House Doctor<br />
                    </div>
                    <div align="justify">
                        <span class="style4"><strong>Meeting Rooms & Banquet Facilities include:  </strong></span>Make business a pleasure in any of the five meeting rooms, comfortably seating 20 to 650 person’s auditorium style or 35 to 1000 persons for cocktails/receptions. 
                    </div>
                    <div>
                        <span class="style4"><strong>Recreation at Taj Residency, Bangalore  </strong></span>Guests can take advantage of our recreation activities for irresistible fun and relaxation.<br>
                    </div>
                     <div>
                        <span class="style4"><strong>Fitness and fun include: </strong></span>
                        Bookshop<br />
                        Fitness centre<br />
                        Swimming pool<br />
                        Jiva Spa<br />
                    </div>
                     <div>
                        <span class="style4"><strong>Recreation outside the hotel: </strong></span>Bangalore Walks<br>
City Heritage walks<br>
Jindal (Ayurvedic Treatment Center)<br>
Night Clubs/Discotheque<br>
Pubs<br>
Puttaparthi<br>
Shopping Arcade<br>
                    </div>
                     <div>
                     <span class="style44">&nbsp;</span>
                     </div>
                </div>
                </div>
                <div class="section" align="left" style="">
                  
                </div>
                <div class="spacer">
                </div>
                <div class="wrapperline">
                    <div class="boxinside">
                         <a href="http://www.tajhotels.com/TICsignup" target="_blank"><img src="images/img1.jpg" alt="" /></a> </div>
                    <div class="boxinside2">
                        <img src="images/img2.jpg" alt="" /></div>
                </div>
            </div>
            <div id="footer">
                <!-- #include file = "footer.htm" -->  
            </div>
        </div>

        <script src="js/jquery-1.9.1.min.js" type="text/javascript"></script> 
        
        <script type="text/javascript" src="js/jquery.jscrollpane.min.js"></script>
        
        <script type="text/javascript" src="js/jquery.mousewheel.js"></script>
        
        
        <script type="text/javascript">

            $(function() {

                $(".quick_links").click(function() {

                    $('.srv_links').slideToggle();

                })


            });
            
            $(function()
            {
	            $('.services').jScrollPane();
            });

        
        </script>
        
       
	<script type="text/javascript">
	    (function($) {

	        $(window).load(function() {

	        $(".services").mCustomScrollbar({
	                
	            });
	        });
	    })(jQuery);
	</script>
    </form>
