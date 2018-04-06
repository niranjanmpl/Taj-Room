<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CouponOTP.aspx.vb" Inherits="mpl.Taj.CouponOTP" %>

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
                    Log In:</div>
                <div class="terms_section">
                    Please do read and accept the <a target="terms" href="terms.html" class="fancybox fancybox.iframe">
                        terms and conditions </a>before logging on to this service.
                    <label for="checkbox-1">
                        <input type="checkbox" name="chkagree" id="chkagree">
                        <span class="font_set2">I Agree </span>
                    </label>
                </div>
                <div style="padding: 20px 0 0 20px; line-height: 20px; width: 100%; color: #58595b;
                    background: #fff; font-size: 14px;" class="form_fields" id="usersection" runat="server">
                    <div class="field_Section frm_info">
                        <div>
                            <label>Access Code</label>
                            <asp:TextBox ID="txtroomno" runat="server" CssClass="txt_box" Enabled="true" Text=""></asp:TextBox>
                           
                                 <asp:Label ID="LblError" runat="server" Visible="false" Text="" Style="color: Red"></asp:Label>  
                        </div>
                        <div>
                            <label>Mobile Number</label>
                            <asp:TextBox ID="txtlastname" runat="server" CssClass="txt_box1" Enabled="true" Text=""></asp:TextBox>                           
                                <asp:Label ID="LblError2" runat="server" Visible="false" Text="" Style="color: Red"></asp:Label> 
                               
                               
                        </div>
                        
                    </div>
                    <div class="button_Section btn_info1">
                         <asp:Button ID="imgbtnlogin" runat="server" Text="Send OTP" CssClass="btn_style btn_info "
                            BorderWidth="0" OnClientClick="javascript:return validate(frmlogin);" />
                        <input value="0" type="hidden" name="hdbutclick" />
                    </div>
                    <br />
                </div>
                <%--<div id="mobileno" style="padding: 20px 0 0 20px; line-height: 20px; width: 100%; color: #58595b; background: #fff; font-size: 14px;" class="form_fields" runat="server">
                    <div>
                    <p style="font-size:14px; font-family:'Sun SemiBold';">Please Enter Your mobile number to send OTP</p><br />
                    <p>
                            <label>
                                OTP</label>
                            <asp:TextBox ID="mobno" runat="server"  CssClass="txt_box" ></asp:TextBox>
                        </p>
                         <div class="button_Section spacer1" >
                        <asp:Button ID="imgmoblogin" runat="server" Text="Login" CssClass="btn_style" BorderWidth="0" />&nbsp;
                        
                        <input value="0" type="hidden" name="hdbutclick" />
                    </div><br />
                </div>
                </div>--%>
                <div id="OTP" style="padding: 20px 0 0 20px; line-height: 20px; width: 100%; color: #58595b;
                    background: #fff; font-size: 14px;" class="form_fields" runat="server" visible="false">
                    <div>
                        <%-- <p style="font-size:14px; font-family:'Sun SemiBold';"> please enter the OTP which has been sent to your registered mobile.</p><br />
                    <p style="font-size:14px; font-family:'Sun SemiBold';">OTP is Valid only for 15 minutes.</p><br />--%>
                        <div>
                            <label>
                                Enter OTP</label>
                            <asp:TextBox ID="txtotp" runat="server" CssClass="txt_box"></asp:TextBox>
                          
                                <asp:Label ID="LblError1" runat="server" Visible="false" Text="" Style="color: Red" class="LblError1"></asp:Label> 
                        </div>
                        <div class="button_Section spacer1">
                            <label id="btn_spacer1">
                            </label>
                            <asp:Button ID="imgotplogin" runat="server" Text="Login" CssClass="btn_style" Enabled="false"
                                OnClientClick="javascript:return validateotp(frmlogin);" BorderWidth="0" />&nbsp;
                            <asp:Button ID="BtnResend" runat="server" Text="Resend OTP" CssClass="btn_style" Enabled="false"
                                BorderWidth="0" />
                            <input value="0" type="hidden" name="hdbutclick1" />
                        </div>
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



        $(window).load(function () {

            $('.fancybox').fancybox({
                width: 500,
                height: 200,
                autoCenter: false

            });

            $('#imgbtnlogin').click(function (e) {



                var x = isMobileNo();

                if (x === false) {
                    //alert(x);
                    e.preventDefault();
                }


            });

            function isMobileNo() {



                var ValidChars = "0123456789";

                var Char;
                var char1;
                //alert(str.length);
                var str = document.getElementById("txtlastname").value;

                char1 = str.charAt(0)

                //alert(char1);
                for (i = 0; i < str.length; i++) {
                    Char = str.charAt(i);
                    if (ValidChars.indexOf(Char) == -1) {
                        alert('Dear Guest, Mobile No. is invalid.');
                        document.getElementById("txtlastname").focus();
                        return false;
                        break;
                    }
                }

                if (char1 == '0') {

                    alert('Dear Guest, ZERO not allowed in front of mobile no.');
                    document.getElementById("txtlastname").focus();
                    return false;
                    
                    
                }

                if (char1 == '+') {

                    alert('Dear Guest, + not allowed in front of mobile no.');
                    document.getElementById("txtlastname").focus();
                    return false;
                    
                }


                if (str.length != 10) {
                    alert('Dear Guest, Mobile no. should be 10 digits.');
                    document.getElementById("txtlastname").focus();
                    return false;
                    
                }





                return true;
            }



            $('#btn_close').click(function () {

                $("html, body").animate({ scrollTop: $(document).height() }, 1000);

                $('body').css('boder', 'solid 1px red');

            });











        });
	
    </script>

    <script type="text/javascript">


        $(function () {

            var max_width1 = $(window).width();

            if (max_width1 > 480) {
          
                if ($("#LblError2").is(":visible") == true) {

                    $('.btn_info ').css("margin-bottom" ,"25px");

                }else{
                    $('.btn_info ').css("margin-bottom", "8px");

                }
                 

            }

        });
    
    </script>
    <script language="javascript" type="text/javascript">


        function validate(frm) {
            if (parseInt(frm.hdbutclick.value) != 0)
                return false
            if (BoxesChecked(frm, "chkagree") <= 0) { alert('Dear Guest, Please Accept Terms and Conditions'); return false; }

            else if (isEmptyWithText(frm.txtroomno, 'Dear Guest, Please enter your Access Code.')) { return false; }
            else if (OnlyText(frm.txtroomno, "Access Code")) { return false; }

            else if (isEmptyWithText(frm.txtlastname, 'Dear Guest, Please enter your Mobile Number.')) { return false; }
            else if (isOnlyNumbers(frm.txtlastname, "Mobile Number")) { return false; }

            frm.hdbutclick.value = 1;



        }

        function validateotp(frm) {
            if (parseInt(frm.hdbutclick1.value) != 0)
                return false
            if (BoxesChecked(frm, "chkagree") <= 0) { alert('Dear Guest, Please Accept Terms and Conditions'); return false; }

            else if (isEmptyWithText(frm.txtotp, 'Dear Guest, Please enter OTP.')) { return false; }
            else if (isOnlyNumbers(frm.txtotp, "OTP")) { return false; }

            frm.hdbutclick1.value = 1;

        }

        function btn_fade() {

            var section = document.querySelector(".field_Section");

            section.style.opacity = 0.5;
            document.getElementById("chkagree").checked = true;


        }

        function OnlyText(str, Label) {
            var ValidChars = "abcdefghijklmnopqrstuvwxyzABCEDFGHIJKLMNOPQRSTUVWXYZ";
            var Char;
            for (i = 0; i < str.value.length; i++) {
                Char = str.value.charAt(i);
                if (ValidChars.indexOf(Char) == -1) {
                    alert('Dear Guest, ' + Label + ' should contain only alphabet characters');
                    str.focus();
                    return true;
                }
            }
            return false;
        }

        function isOnlyNumbers(str, Label) {
            var ValidChars = "0123456789";
            var Char;
            for (i = 0; i < str.value.length; i++) {
                Char = str.value.charAt(i);
                if (ValidChars.indexOf(Char) == -1) {
                    alert('Dear Guest, ' + Label + ' should contain only numbers');
                    str.focus();
                    return true;
                }
            }
            return false;
        }

        function isEmptyWithText(str, Label) {
            if (isWhitespace1(str, Label)) { return true; }
            if ((str.value == null) || (str.value == 'Room number') || (str.value == 'Password') || (str.value == 'Last Name') || (str.value == 'Confirm Password') || (str.value.length == 0)) {
                // alert(Label + ' can\'t be empty or with whitespaces alone');    
                alert(Label);
                str.select(); str.focus();
                return true;
            }
            return false;
        }

        function isWhitespace1(str, label) {
            var spaces = " \n\t\r"
            var i;
            for (i = 0; i < str.value.length; ++i) {
                if (spaces.indexOf(str.value.charAt(i)) == -1) { return false; }
            }
            //alert(label + " can't be empty");
            alert(label);
            str.select(); str.focus();
            return true;
        }

        			  
    </script>
    <%--<script src="js/img_setting.js?&src=<%= Session("imgsrc")%>" type="text/javascript" id="imgsrc"></script>--%>
    <script src="js/img_setting.js" type="text/javascript" id="Script1"></script>
    </form>
</body>
</html>
