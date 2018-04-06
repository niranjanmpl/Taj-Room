<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MemberLogin.aspx.vb" Inherits="mpl.Taj.MemberLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
                    Log In:</div>
                <div class="terms_section">
                    Please do read and accept the <a target="terms" href="terms.html" class="fancybox fancybox.iframe">
                        terms and conditions </a>before logging on to this service.
                    <label for="checkbox-1">
                        <input type="checkbox" name="chkagree" id="chkagree">
                        <span class="font_set2">I Agree </span>
                    </label>
                </div>
                <div style="padding: 20px 0 0 20px; line-height: 36px; width: 100%; color: #58595b;
                    background: #fff; font-size: 14px;" class="form_fields">
                    <div class="field_Section">
                        <p>
                            <label>
                                Member ID:</label>
                            <asp:TextBox ID="txtroomno" runat="server" class="txt_box"></asp:TextBox>
                        </p>
                        <p>
                            <label>
                                Password:</label>
                            <asp:TextBox ID="txtlastname" runat="server" class="txt_box1" TextMode="Password"></asp:TextBox>
                        </p>
                    </div>
                    <div class="button_Section">
                        <asp:Button ID="imgbtnlogin" runat="server" Text="Login" CssClass="btn_style" BorderWidth="0" />
                        <input value="0" type="hidden" name="hdbutclick" />
                    </div><br />
                   
                   
                    
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

            else if (isEmptyWithText(frm.txtroomno, 'Please enter your MemberId.')) { return false; }
            else if (isAlphaNumeric(frm.txtroomno, "MemberId")) { return false; }

            else if (isEmptyWithText(frm.txtlastname, 'Please enter your Password.')) { return false; }
            else if (isAlphaNumeric(frm.txtlastname, "password")) { return false; }

            frm.hdbutclick.value = 1
        }

        			  
    </script>
    <script src="js/img_setting.js" type="text/javascript"></script>
    </form>
</body>
</html>

