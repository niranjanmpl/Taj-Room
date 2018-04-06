<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="selectaccess.aspx.vb"
    Inherits="mpl.Taj.selectaccess" %>

<!DOCTYPE html>
<html>
<head>
    <title id="PageTitle" runat="server"></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <link href="taj.css" rel="stylesheet" type="text/css" />
    <link href="images/favicon.ico" type="image/x-icon" rel="shortcut icon">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Expires" content="-1">
    <script language="JavaScript" src="validation.js"></script>
    <link href="popup/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="fonts/Sun.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmlogin" method="post" name="frmlogin" runat="server">
    <div align="center" id="wrapper">
        <div id="header" align="left">
            <!-- #include file = "header.htm" -->
        </div>
        <div id="main" class="main_bg">
            <div id="nav" align="left">
                <div class="login_section">
                    Select Access:</div>
                <div id="OTP" style="padding: 0px 0 0 20px; line-height: 20px; width: 100%; color: #58595b;
                    background: #fff; font-size: 14px;" class="form_fields" runat="server" visible="true">
                    <%-- <p style="font-size:14px; font-family:'Sun SemiBold';"> please enter the OTP which has been sent to your registered mobile.</p><br />
                    <p style="font-size:14px; font-family:'Sun SemiBold';">OTP is Valid only for 15 minutes.</p><br />--%>
                   <div class="hsia_info">
                        <span style="font-weight: bold;">Highspeed Paid Access<br />
                            <asp:Button ID="btnUpgrade" runat="server" Text="Upgrade" CssClass="btn_style" BorderWidth="0" style="margin-top:10px" /></span>
                    </div>
                    <div class="comp_access">
                        <span style="font-weight: bold;">Complimentary Access<br />
                            <asp:Button ID="btnContinue" runat="server" Text="Continue" CssClass="btn_style" style="margin-top:10px" BorderWidth="0" /></span><br />
                        <input value="0" type="hidden" name="hdbutclick" />
                        <br />
                    </div>
                </div>
                <div class="call_info">
                    <p>
                        Dear Guest,
                        <br />
                        Welcome to Taj Connect, an online facility which allows you to navigate through
                        our innumerable hotel services. For your convenience, you can connect to our wireless
                        network from anywhere in this hotel, and access the internet on multiple device
                        by selecting the respective plan on the given. For any further assistance, <span>Please
                            call us at Extension 63</span>
                    </p>
                </div>
            </div>
            <div class="section" align="left" style="">
            </div>
            <div class="spacer">
            </div>
        </div>
        <div id="footer">
            <!-- #include file = "footer.htm" -->
        </div>
    </div>
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
	
    </script>
    <script language="javascript" type="text/javascript">


        function validate(frm) {
            if (parseInt(frm.hdbutclick.value) != 0)
                return false
            if (BoxesChecked(frm, "chkagree") <= 0) { alert('Accept Terms and Conditions'); return false; }

            else if (isEmptyWithText(frm.txtroomno, 'Please enter your UserID.')) { return false; }
            else if (OnlyText(frm.txtroomno, "UserID")) { return false; }

            else if (isEmptyWithText(frm.txtlastname, 'Please enter your password.')) { return false; }
            else if (OnlyText(frm.txtlastname, "password")) { return false; }

            frm.hdbutclick.value = 1
        }

        			  
    </script>
    <%--<script src="js/img_setting.js?&src=<%= Session("imgsrc")%>" type="text/javascript" id="imgsrc"></script>--%>
    <script src="js/img_setting.js" type="text/javascript" id="Script1"></script>
    </form>
</body>
</html>
