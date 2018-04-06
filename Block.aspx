<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Block.aspx.vb" Inherits="mpl.Taj.Block" %> 
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <link href="taj.css" rel="stylesheet" type="text/css" />
    <link href="fonts/Sun.css" rel="stylesheet" type="text/css" />
    <title id="PageTitle" runat="server">Taj Hotels and Resorts</title>
    <link href="images/favicon.ico" type="image/x-icon" rel="shortcut icon">
    <meta http-equiv="cache-control" content="no-cache">
    <title>Blocking Test</title>

    <script type="text/javascript">
<!--
        function reloadIt() {
            var clocktime = new Date();
            var utchours = clocktime.getUTCHours();
            var utcminutes = clocktime.getUTCMinutes();
            var utcseconds = clocktime.getUTCSeconds();
            var utcyear = clocktime.getUTCFullYear();
            var utcmonth = clocktime.getUTCMonth() + 1;
            var utcday = clocktime.getUTCDate();

            if (utchours < 10) { utchours = "0" + utchours }
            if (utcminutes < 10) { utcminutes = "0" + utcminutes }
            if (utcseconds < 10) { utcseconds = "0" + utcseconds }
            if (utcmonth < 10) { utcmonth = "0" + utcmonth }
            if (utcday < 10) { utcday = "0" + utcday }

            var utctime = utcyear + utcmonth + utcday;
            utctime += utchours + utcminutes + utcseconds;
            x = utctime

            isNew = self.location.href
            if (!isNew.match('#', 'x')) {
                self.location.replace(isNew + '#' + x)
            }
        }
 
//-->
    </script>

</head>
<body onload="reloadIt()">
    <form id="frmlogin" method="post" name="frmlogin" runat="server">
    <div align="center" id="wrapper">
        <div id="header" align="left">
            <!-- #include file = "header.htm" -->
        </div>
        <div id="main" class="main_bg">
            <div id="nav" align="left">
                <div class="login_section">
                    Available Plans</div>
                <div style="padding: 0px 28px 0 20px; line-height: 20px; color: #58595b;" class="plan_info font_set plan_height2">
                    <p style="margin-top: 0px;">
                        <strong>We regret to inform that this website cannot be accessed under the current plan.
                            Please upgrade to premium plan in order to browse this website.</strong></p>
                    <p>
                        The standard plan is suitable for basic e-mail and simple browsing for up to 3
                        <!--<asp:Label ID="lblstandev"
                            runat="server" Text=""></asp:Label> -->
                        devices. However, It has certain restrictions on voice commands and other high bandwidth
                        usage applications.</p>
                    <p>
                        <asp:Button ID="imgbtnlogin" runat="server" Text="Upgrade to Premium Plan" CssClass="btn_style" />
                        <%--<asp:ImageButton  runat="server" BorderWidth="0" Width="218" Height="22" ImageUrl="images/button-up.jpg">
                    </asp:ImageButton>--%><input type="hidden" value="0" name="hdbutclick">
                    </p>
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

    </form>
</body>
</html>
