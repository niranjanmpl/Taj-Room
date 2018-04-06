<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PlanSelection.aspx.vb" Inherits="mpl.Taj.PlanSelection" %>

<!DOCTYPE html>
<html>
<head>
    <title id="PageTitle" runat="server"></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <link href="taj.css" rel="stylesheet" type="text/css" />
    <link href="images/favicon.ico" type="image/x-icon" rel="shortcut icon">

    <script language="JavaScript" src="validation.js"></script>

    <link href="popup/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="fonts/Sun.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmlogin" onsubmit="javascript:return validate(this);" method="post" name="frmlogin"
    runat="server">
    <div align="center" id="wrapper">
        <div id="header" align="left">
            <!-- #include file = "header.htm" -->
        </div>
        <div id="main" class="main_bg">
            <div id="nav" align="left">
                <div class="login_section">
                    Available Plans</div>
                <div style="padding: 3px 0 0 20px; line-height: 13px; color: #58595b;" class="plan_info plan_height3 font_set">
                    <p>
                        <input type="radio" id="rdoplans" name="plans" value="YES" />
                        <label for="checkbox-1" class="plan_details">
                            <b>Standard Plan</b>
                            <asp:Label ID="lblfreeplanname" runat="server" Text=""></asp:Label></label></p>
                    <p style="padding: 0 12px 0 0px;" class="plan_note">
                        This plan is suitable for basic e-mail and simple browsing for upto
                        <asp:Label ID="lblstandev" runat="server" Text=""></asp:Label>
                        personal devices. However, It has certain restrictions on voice commands and other
                        high bandwidth usage applications.</p>
                    <p>
                        <input type="radio" id="rdoplans1" name="plans" value="NO" />
                        <label for="checkbox-1" class="plan_details">
                            <b>Premium Plan</b>
                            <asp:Label ID="lblplanname" runat="server" Text=""></asp:Label></label></p>
                    <p style="padding: 0 12px 0 0px;" class="plan_note">
                        This plan comes equipped with the requisite bandwidth to support video streaming,
                        unlimited e-mail access, etc., for up to
                        <asp:Label ID="lblpremdev" runat="server" Text=""></asp:Label>
                        personal devices at a time. Additional devices will be chargeable, with Taxes Extra
                        as applicable.</p>
                    <p style="display: none;">
                        <asp:HyperLink ID="HyperLink2" Target="_new" runat="server" NavigateUrl="PlanConfirmation.aspx"
                            class="fancybox fancybox.iframe" title="">HyperLink</asp:HyperLink>
                    </p>
                    <p class="btn_pos">
                        <asp:Button ID="imgbtnlogin" runat="server" Text="Login" CssClass="btn_style" />
                        <input type="hidden" value="0" name="hdbutclick">
                        <input id="hdrdostatus" type="hidden" name="hdrdostatus" runat="server">
                        <input id="hdpopup" type="hidden" name="hdpopup" runat="server">
                        <input id="hdpopupconfirm" type="hidden" name="hdpopupconfirm" runat="server">
                    </p>
                </div>
                <div class="call_info height_setter">
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

                var free = document.getElementById("rdoplans").checked;
                var premium = document.getElementById("rdoplans1").checked;

                if (free) {
                    frm.hdrdostatus.value = 0;

                }

                if (premium) {
                    frm.hdrdostatus.value = 1;

                    frm.hdpopupconfirm.value = 2;

                    if (frm.hdpopup.value != 20) {
                        //    alert('adssss');
                        start();
                        return false;
                    }
                }


                if (free == false && premium == false) { alert('Please select a Plan'); return false; }




                frm.hdbutclick.value = 1;
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
