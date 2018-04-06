<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Usererror.aspx.vb" Inherits="mpl.Taj.Usererror" %>

<!DOCTYPE html>

<html>
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
<body onload="javascript:document.frmlogin.txtroomno.focus();">
    <form id="frmlogin" onsubmit="javascript:return validate(this);" method="post" name="frmlogin"
    runat="server">
    <div align="center" id="wrapper">
        <div id="header" align="left">
            <!-- #include file = "header.htm" -->
        </div>
        <div id="main" class="main_bg">
            <div id="nav" align="left">
                <div class="login_section">
                    Information</div>
                <div style="padding: 20px 0 0 20px; line-height: 18px; color: #58595b;" class="plan_info  font_set plan_height2 form_fields">
                    <b>
                        <p id="msgPara" runat="server" class="font_set2">
                        </p>
                    </b>
                    <br>
                    <br>
                    <td align="center">
                    
<asp:Button  ID="imbbtnback" runat="server" Text="Back" CssClass="btn_style" />

                     
                    </td>
                </div>
                <div class="call_info">
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
            <div class="spacer">
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
    <script src="js/img_setting.js" type="text/javascript" id="Script1"></script>
    </form>
</body>
</html>
