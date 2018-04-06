<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Feedback.aspx.vb" Inherits="mpl.Taj.Feedback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title id="PageTitle" runat="server"></title>
    <link href="taj.css" rel="stylesheet" type="text/css" />
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <LINK href="images/favicon.ico" type="image/x-icon" rel="shortcut icon">
	<script language="JavaScript" src="validation.js"></script>
    <link href="popup/jquery.fancybox.css" rel="stylesheet" type="text/css" />
 
</head>
<body>
    <form id="frmlogin" method="post" name="frmlogin"
    runat="server">
    <div id="main_wrapper">
        <div id="wrapper">
           
             <!-- #include file = "header.htm" -->
               
           
            <div class="center_contents">
                <h3>
                    Feedback</h3>
                <div style="margin-top: 1em;">
                    <span class="plan_txt">Rate our Internet Service</span>
                  <div id="Div2" class="plan_note">
                          </div>
                          
                  		    <table width="80%" border="0" align="left" class="TextClr" cellpadding="0" cellspacing="0">
						    <tr >
								<td width="22%" height="28" align="center">
									Excellent
								</td>
								<td width="24%" align="center">
									Very good
								</td>
								<td width="19%" align="center">
									Good
								</td>
								<td width="18%" align="center">
									Average
								</td>
								<td width="17%" align="center">
									Poor
								</td>
							</tr>
							<tr >
								<td height="33" align="center">
									<input name="optintservice" type="radio" value="Excellent">
								</td>
								<td height="33" align="center">
									<input name="optintservice" type="radio" value="Very Good">
								</td>
								<td height="33" align="center">
									<input name="optintservice" type="radio" value="Good">
								</td>
								<td height="33" align="center">
									<input name="optintservice" type="radio" value="Average">
								</td>
								<td height="33" align="center">
									<input name="optintservice" type="radio" value="Poor">
								</td>
							</tr>
						</table>
                        
                          <div id="Div1" class="plan_note">
                          </div>
                  <span class="plan_txt">Did you use a VPN connection? [If you are not sure, please do not make a selection]</span>
                 <div id="vpn1" class="plan_note">
                  <input name="optvpn" type="radio" value="Yes"> Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input name="optvpn" type="radio" value="No"> No </div>
                  
                 <span class="plan_txt">Please use the box below to enter your valuable comments</span>
                   <div id="sugg" class="plan_note">
                 <textarea name="txtsuggestion" cols="40" rows="5"></textarea></div>
                  <div id="divsugg" class="plan_note"> 
                      <asp:ImageButton ID="imgbtnsubmit" ImageUrl="images/submit.gif" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; 
                    <!-- <asp:ImageButton ID="imgbtnclose" ImageUrl="images/close.gif" OnClientClick="myClosure();" runat="server" /> -->
                      <input type="button" src="images/close.gif" onclick="myClosure();" class="backimg" />
                  </div>
            </div>
          
        </div>
          <div id="footer">      
              <!-- #include file = "footer.htm" -->           
            </div>
    </div>

    <script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>

    <script src="js/mobile.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function () {

            if (jQuery.browser.mobile) {

                if (screen.width > 480) {

                    function bannerheight() {

                        var banner_ht = $('#banner_txt').innerHeight();

                        $('#non_flash img').height(banner_ht);
                    }


                    $(window).resize(function () {

                        bannerheight();

                    });

                }

                $(".flash_content").css("display", "none");
                $(".flash_content2").css("display", "none");
                $("#non_flash").css("display", "block");


            }
            else {

                $(".flash_content").css("display", "block");
                $(".flash_content2").css("display", "block");
                $("#non_flash").css("display", "none");



            }
        }); 
    </script>

    <script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>

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


        function myClosure() {

            alert('Dear Guest, Please submit your feedback');
        }
	
    </script>

       </form>
</body>
</html>
