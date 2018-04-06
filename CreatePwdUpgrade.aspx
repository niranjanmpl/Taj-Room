<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CreatePwdUpgrade.aspx.vb" Inherits="PlanUpgrade.CreatePwdUpgrade" %>

<!DOCTYPE html>
<html>
<head>
    <title id="PageTitle" runat="server"></title> 
  
   <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
     <link href="taj.css" rel="stylesheet" type="text/css" />
    <LINK href="images/favicon.ico" type="image/x-icon" rel="shortcut icon">
	<script language="JavaScript" src="validation.js"></script>
    <link href="popup/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    
    
</head>
<body onLoad="start()">
<form id="frmlogin" onSubmit="javascript:return validate(this);" method="post" name="frmlogin" runat="server">
    <div align="center" id="wrapper">
        <div id="header" align="left">
              <!-- #include file = "header.htm" -->
        </div>
        
        <div id="main">
            <div class="quick_links">
              <span>Menu</span> 
            </div>
            <div class="srv_links highlight">
               <!-- #include file = "left.htm" -->
            </div>
            <div id="nav" align="left">
                <div class="login_section">
                    Create Secure Password</div>
                <div style="padding: 20px 0 0 20px; line-height: 18px; color: #58595b; font-size: 14px;">
                    
                    <p  style="font-weight:bold;">Dear Guest, for security reason we recommend you to change your password.</p>
                    <br />
                    <p  style="font-weight:bold;">Please change your login password:</p>
                    <p>(The password should be minimum 8 characters long)</p>
                    <br>

                    <asp:textbox id="txtPassword" runat="server" class="txt_box"></asp:textbox>
                    <br>
                                       
                    <asp:textbox id="txtConfirmPassword" runat="server" class="txt_box1"></asp:textbox>
                    <br>
                    <br>
                   
                     <asp:imagebutton id="imgbtnlogin" runat="server" ImageUrl="images/button.jpg" Width="55" Height="22" BorderWidth="0"></asp:imagebutton><input value="0" type="hidden" name="hdbutclick">
                </div>
            </div>

             <p style="display:none;">
                         <asp:HyperLink ID="HyperLink2" Target="_new" runat="server" NavigateUrl="ResetPopUp.aspx" class="fancybox fancybox.iframe" title="">HyperLink</asp:HyperLink>
            </p>

            <input id="hdpopup" type="hidden" name="hdpopup" runat="server">
             <input id="hdpopupconfirm" type="hidden" name="hdpopupconfirm" runat="server">
             
            <div class="section" align="left" style="">
                 
            </div>
            <div class="spacer"></div>
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
                    $('.txt_box').css("color", "#000")
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
                    $('.txt_box1').css("color", "#000")
                }
            });

        });

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

