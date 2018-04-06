<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Process.aspx.vb" Inherits="mpl.Taj.Process" %>

<!DOCTYPE html>
<html>
<head>
    <title id="PageTitle" runat="server"></title> 
  
   <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
     <link href="taj.css" rel="stylesheet" type="text/css" />
    <LINK href="images/favicon.ico" type="image/x-icon" rel="shortcut icon">
	<script language="JavaScript" src="validation.js"></script>
    <link href="popup/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    
    
</head>
<body  onLoad="start()">
<form id="frmprocess" method="post" name="frmlogin" runat="server">
    <div align="center" id="wrapper">
        <div id="header" align="left">
              <!-- #include file = "header.htm" -->
        </div>
        
        <div id="main" class="main_bg">
            
            <div id="nav" align="left">
                <div class="login_section">
                    Please wait</div>
                <div style="padding: 20px 0 0 20px; line-height: 18px; color: #58595b; " class="plan_info font_set plan_height2 ">
                   <span>Dear Guest,</span> <br /><br />
                   Your data is on process, please wait.

                  <p> <div class="processtime" id="CH_dtimer1_digits"></div></p>
                   <p><input id="cur_clock" type="hidden" value="clock" name="cur_clock"> <input id="hdurl" type="hidden" name="hdurl" runat="server"></p>  
                   
                </div>
                <div class="call_info">
                    <p>
                        Dear Guest,
                        <br />
                        Welcome to Taj Conect, an online facility which allows you to navigate through our
                        innumerable hotel service. For your convenience, you can connect to our wireless
                        network from anywhere in this hotel, and access the internet on multiple device
                        by selecting the respective plan on the given. For any further assistance, <span>Please
                            call us at Extension 63</span>
                    </p>
                </div>
            </div>
            
             
            <div class="section" align="left" style="">
                  <!-- #include file = "right.htm" -->
            </div>
            <div class="spacer"></div>
            
        </div>
        <div id="footer">
           <!-- #include file = "footer.htm" -->  
        </div>
    </div>
    

    <script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        $(function () {

            $(".quick_links").click(function () {

                $('.srv_links').slideToggle();

            })


        });

        
    </script>
        	       

        <script src="popup/jquery.fancybox.js" type="text/javascript"></script>

        <script type="text/javascript">
            $(function () {


                $('.fancybox').fancybox({
                    width: 500,
                    height: 200,
                    autoCenter: false

                });


                $('#btn_close').click(function () {

                    $("html, body").animate({ scrollTop: $(document).height() }, 1000);

                    $('body').css('boder', 'solid 1px red');

                });



            });
	
        </script>

        <script type="text/javascript">
            var time, timerID, clockticks, balsecs
            var hh = 0;
            var mm = 0;
            var ss = 0;

            function start() {

                time = 10;
                showtime()
            }
            function showtime() {
                clockticks = time
                hh = 0
                mm = 0

                ss = Math.floor(clockticks)

                if (time > 0) {

                    document.getElementById("CH_dtimer1_digits").innerHTML = ss + " Sec(s)"
                    timerID = setTimeout("showtime()", 1000)
                    time--;
                }
                if (time <= 0) {
                    clearTimeout(timerID)
                    var frm = document.frmprocess
                    var strurl = frm.hdurl.value;

                    window.location.href = strurl
                }

            }


		</script>

        		
    </form>
</body>
</html>
