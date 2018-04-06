<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CreatePassword.aspx.vb" Inherits="mpl.Taj.CreatePassword" %>

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
<body onload="start()">
    <form id="frmlogin" onsubmit="javascript:return validate(this);" method="post" name="frmlogin"
    runat="server">
    <div align="center" id="wrapper">
        <div id="header" align="left">
            <!-- #include file = "header.htm" -->
        </div>
        <div id="main" class="main_bg">
            <div id="nav" align="left">
                <div class="login_section">
                    Create Secure Password</div>
                <div style="padding: 0px 0 0 20px; line-height: 18px; color: #58595b;" class="plan_info plan_height3 font_set form_fields">
                    <p class="font_set2">
                        Dear Guest, for security reason we recommend you to change your password.</p>
                 
                    <p class="font_set2">
                        Please change your login password:</p>
                    <p>
                        (The password should be minimum 8 characters long)</p>
                    <p>
                        <br>
                        <label>
                            Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" class="txt_box"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            Confirm Password</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" class="txt_box1"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                        </label>
                        <asp:Button ID="imgbtnlogin" runat="server" OnClientClick="javascript:return validate(this);" Text="Login" CssClass="btn_style" />
                        <input value="0" type="hidden" name="hdbutclick">
                    </p>
                </div>
                <div class="call_info height_setter">
                    <p>
                        Dear Guest,
                        <br />
                        Welcome to Taj Conect, an online facility which allows you to navigate through our
                        innumerable hotel services. For your convenience, you can connect to our wireless
                        network from anywhere in this hotel, and access the internet on multiple device
                        by selecting the respective plan on the given. For any further assistance, <span>Please
                            call us at Extension 63</span>
                    </p>
                </div>
            </div>
            <p style="display: none;">
                <asp:HyperLink ID="HyperLink2" Target="_new" runat="server" NavigateUrl="popupTIC.aspx"
                    class="fancybox fancybox.iframe" title="">HyperLink</asp:HyperLink>
            </p>
            <p style="display: none;">
                <asp:HyperLink ID="HyperLink1" Target="_new" runat="server" NavigateUrl="popupTajWebsite.aspx"
                    class="fancybox fancybox.iframe" title="">HyperLink</asp:HyperLink>
            </p>
            <input id="Hidden1" type="hidden" name="hdpopup" runat="server">
            <input id="Hidden2" type="hidden" name="hdpopupconfirm" runat="server">
            <p style="display: none;">
                <asp:HyperLink ID="HyperLink3" Target="_new" runat="server" NavigateUrl="ResetPopUp.aspx"
                    class="fancybox fancybox.iframe" title="">HyperLink</asp:HyperLink>
            </p>
            <input id="hdpopup" type="hidden" name="hdpopup" runat="server">
            <input id="hdpopupconfirm" type="hidden" name="hdpopupconfirm" runat="server">
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

        });
	
    </script>

    <script language="javascript" type="text/javascript">


        function validate(frm) {
            if (parseInt(frm.hdbutclick.value) != 0)
                return false

            else if (isEmptyWithText(frm.txtPassword, 'Please enter your Password.')) { return false; }
            else if (isEmptyWithText(frm.txtConfirmPassword, 'Please enter your Confirm Password.')) { return false; }

            frm.hdpopupconfirm.value = 2;
                if (frm.hdpopup.value != 20) {
                    start();
                    return false;
                }
            }

            frm.hdbutclick.value = 1
        }

        			  
    </script>
 

    <script language="javascript" type="text/javascript">



        function start() {


            var frm = document.frmlogin;

            if (frm.hdpopupconfirm.value == 2) {
                document.getElementById('HyperLink2').click();
            }

            if (frm.hdpopupconfirm.value == 5) {
                document.getElementById('HyperLink1').click();
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
