<%@ Page Language="vb" AutoEventWireup="false" AspCompat="true" CodeBehind="PlanUpgrade.aspx.vb" Inherits="PlanUpgrade.PlanUpgrade" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title id="PageTitle" runat="server"></title>
    <link href="taj.css" rel="stylesheet" type="text/css" />
     <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <link href="images/favicon.ico" type="image/x-icon" rel="shortcut icon">

    <script language="JavaScript" src="validation.js"></script>

    <link href="popup/jquery.fancybox.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmlogin" onsubmit="javascript:return validate(this);" method="post" name="frmlogin"
    runat="server">
    <div align="center" id="wrapper">
        <div id="header">
            <!-- #include file = "header.htm" -->
        </div>
        <div id="main">
            <div class="quick_links">
                <span>Menu</span>
            </div>
            <div class="srv_links">
                <!-- #include file = "left.htm" -->
            </div>
            <div id="nav" align="left">
                <div class="login_section">
                    Available Plans</div>
                <div style="padding: 3px 0 0 20px; line-height: 18px; color: #58595b; font-size: 14px;"
                    class="plan_info">
                    <br />
                    <input id="radio" value="NO" CHECKED type="radio" name="rdoyesorno">
                    <label for="checkbox" class="plan_details">
                        <u><b>Premium Plan</b></u>
                        <asp:Label ID="lblplanname" runat="server" Text=""></asp:Label></label></p>
                    <p class="plan_note">
                        Bandwidth to support video streaming, unlimited e-mail access, etc., with up to
                        <asp:Label ID="lblpremdev" runat="server" Text=""></asp:Label>
                        personal devices at a time, any additional devices will be chargeable. <b>Taxes Extra
                            as Applicable</b></p>


                     <p style="display:none;">
                         <asp:HyperLink ID="HyperLink2" Target="_new" runat="server" NavigateUrl="PlanConfirmation.aspx" class="fancybox fancybox.iframe" title="">HyperLink</asp:HyperLink>
                     </p>

                    <p class="btn_pos">
                        <asp:ImageButton ID="imgbtnlogin" runat="server" ImageUrl="images/button.jpg" Width="55"
                            Height="22" BorderWidth="0"></asp:ImageButton><input type="hidden" value="0" name="hdbutclick">
                        <input id="hdrdostatus" type="hidden" name="hdrdostatus" runat="server">
                         <input id="hdpopup" type="hidden" name="hdpopup" runat="server">
                    <input id="hdpopupconfirm" type="hidden" name="hdpopupconfirm" runat="server">
                    </p>
                </div>
            </div>
             <div class="section" align="left" style="">
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
                    width: 300,
                    height: 300,
                    autoCenter: false,
                    scrolling: 'no',
                    closeBtn: false, // hide close button
                    'iframe': { 'scrolling': 'no' }

                });


                $('#btn_close').click(function () {

                    $("html, body").animate({ scrollTop: $(document).height() }, 1000);

                    $('body').css('boder', 'solid 1px red');

                });



            });
	
        </script>

    <script language="javascript" type="text/javascript">

          function validate(frm) {


              if (parseInt(frm.hdbutclick.value) != 0)
                  return false;

              else {

                  var premium = document.getElementById("checkbox").checked;

                  if (premium == false) { alert('Please select a Plan'); return false; }

                  frm.hdpopupconfirm.value = 2;

                  if (frm.hdpopup.value != 20) {
                      //alert('adssss');
                      start();
                      return false;
                  }

                  frm.hdbutclick.value = 1
              }
          }
                    		              			  
    </script>

      <script language="javascript" type="text/javascript">



          function start() {

              var frm = document.frmlogin;

              if (frm.hdpopupconfirm.value == 2) {
                  document.getElementById('HyperLink2').click();
              }

          }

          function Cancel() {

              // visiblerdoPopUp();
          }

          function proceed() {

              document.getElementById('imgbtnlogin').click();
          }
  	


    </script>

    </form>
</body>
</html>
