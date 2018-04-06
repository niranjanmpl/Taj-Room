<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CheckPassword.aspx.vb" Inherits="mpl.Taj.CheckPassword" %>

<!DOCTYPE html>
<html>
<head>
    <title id="PageTitle" runat="server"></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <link href="taj.css" rel="stylesheet" type="text/css" />
    <link href="images/favicon.ico" type="image/x-icon" rel="shortcut icon">
    <link href="fonts/Sun.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" src="validation.js"></script>

    <link href="popup/jquery.fancybox.css" rel="stylesheet" type="text/css" />
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
                    Secure Password</div>
                <div style="padding: 20px 0 0 20px; line-height: 18px; color: #58595b; font-size: 14px;"
                    class="plan_info plan_height form_fields ">
                    <p class="font_set2">
                        Dear Guest, for security reason we recommend you to enter your password.</p>
                    <br>
                    <p>
                        <label>
                            Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" Cssclass="txt_style"></asp:TextBox>
                        <br />
                    </p>
                    <p>
                        <label>
                        </label>
                        <asp:Button ID="imgbtnlogin" runat="server" Text="Login" CssClass="btn_style" />
                        <input value="0" type="hidden" name="hdbutclick">
                    </p>
                    <p style="margin-top: 14px;">
                        &nbsp;
                    </p>
                </div>
                <div class="call_info">
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

            else if (isEmptyWithText(frm.txtPassword, 'Please enter your Password.')) { return false; }

            frm.hdbutclick.value = 1
        }

        			  
    </script>

    <script type="text/javascript">

        $(function () {

            var txt = 'Password';
            if ($('.txt_box').val() == '') {
                $('.txt_box').val(txt);
                $('.txt_box').css("color", "#e1e1e1")
                // alert('');
            }

            $('.txt_box').blur(function () {
                if ($(this).val() == '') {
                    $(this).val(txt);
                    $('.txt_box').css("color", "#e1e1e1")
                }
            }).focus(function () {
                if ($(this).val() == txt) {
                    $(this).val('');
                    $('.txt_box').css("color", "#fff")
                }
            });

            var txt1 = 'Confirm Password';

            //alert($('.pwd').val());
            if ($('.txt_box1').val() == '') {
                $('.txt_box1').val(txt1);
                $('.txt_box1').css("color", "#e1e1e1")

            }

            $('.txt_box1').blur(function () {
                if ($(this).val() == '') {
                    $(this).val(txt1);
                    $('.txt_box1').css("color", "#e1e1e1")
                }
            }).focus(function () {
                if ($(this).val() == txt1) {
                    $(this).val('');
                    $('.txt_box1').css("color", "#fff")
                }
            });

        });

    </script>

    </form>
</body>
</html>
