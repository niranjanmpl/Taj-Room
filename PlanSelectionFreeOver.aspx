<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PlanSelectionFreeOver.aspx.vb"  Inherits="mpl.Taj.PlanSelectionFreeOver" %> 

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
    <link href="fonts/Sun.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="frmlogin" onsubmit="javascript:return validate(this);" method="post" name="frmlogin"
    runat="server">
    <div align="center" id="wrapper">
        <div id="header">
            <!-- #include file = "header.htm" -->
        </div>
        <div id="main" class="main_bg">
            
            <div id="nav" align="left">
                <div class="login_section">
                    Available Plans</div>
                <div style="padding: 3px 0 0 20px; line-height: 18px; color: #58595b;  "
                    class="plan_info font_set plan_height2  ">
                    <p>
                        Dear Guest,<br>
                        <br>
                        You have now used the permitted number of devices in the standard tier plan. For
                        additional device please select an additional plan.</p>
                    <p>
                        <input id="checkbox" value="NO" checked type="radio" name="rdopremium">
                        <label for="checkbox-1" class="plan_details">
                            <u><b>Premium Plan</b></u>
                            <asp:Label ID="lblplanname" runat="server" Text=""></asp:Label></label></p>
                    <p class="plan_note">
                        Bandwidth to support video streaming. unlimited email access, etc. with up to
                        <asp:Label ID="lblpremdev" runat="server" Text=""></asp:Label>
                        personal devices at a time, any additional devices will be chargeable. <span style="font-family:'Sun semibold'; font-size:16px;">Taxes Extra
                            as Applicable</span></p>
                    <p style="display: none;">
                        <asp:HyperLink ID="HyperLink2" Target="_new" runat="server" NavigateUrl="AutoUpgradePopUp.aspx"
                            class="fancybox fancybox.iframe" title="">HyperLink</asp:HyperLink>
                    </p>
                    <p class="btn_pos">
                    
                    <asp:Button ID="imgbtnlogin" runat="server" Text="Login" CssClass="btn_style" />
                    
                     </asp:ImageButton><input type="hidden" value="0" name="hdbutclick">
                        <input id="hdrdostatus" type="hidden" name="hdrdostatus" runat="server">
                        <input id="hdpopup" type="hidden" name="hdpopup" runat="server">
                        <input id="hdpopupconfirm" type="hidden" name="hdpopupconfirm" runat="server">
                    </p>
                </div>
                
                <div class="call_info height_setter2">
                    <p>
                        Dear Guest, <br /> Welcome to Taj Conect, an online facility which allows you to navigate
                        through our innumerable hotel service. For your convenience, you can connect to
                        our wireless network from anywhere in this hotel, and access the internet on multiple
                        device by selecting the respective plan on the given. For any further assistance,
                      <span>Please call us at Extension 63</span> 
                    </p>
                </div>
            </div>
            <div class="section" align="left" style="">
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
