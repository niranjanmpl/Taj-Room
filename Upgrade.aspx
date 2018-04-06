<%@ Page Language="vb" AutoEventWireup="false" aspcompat="true" Codebehind="Upgrade.aspx.vb" Inherits="PlanUpgrade.Upgrade" %>
<!DOCTYPE html>
<html>
<head>
    <title id="PageTitle" runat="server"></title>
   <link href="taj.css" rel="stylesheet" type="text/css" />
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <LINK href="images/favicon.ico" type="image/x-icon" rel="shortcut icon">
	<script language="JavaScript" src="validation.js"></script>
    <link href="popup/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    
  
</head>
<body>
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
                    Login</div>
                <div style="padding: 20px 0 0 20px; line-height: 18px; color: #58595b; font-size: 14px;">
                   <strong>Please do read </strong>  and accept the <a target="terms" href="terms.html" class="fancybox fancybox.iframe"><font color="#cc3300">
                        Terms and Conditions</font></a> <br /> before logging on to this service
                    <br>
                    <br>
                    <label for="checkbox-1">
                        <input type="checkbox" name="chkagree" id="chkagree">
                        <span style="font-weight:bold;"> I Agree </span></label>
                    <br>
                    <br>
                   
                    <asp:textbox id="txtroomno" runat="server" class="txt_box" placeholder="Room Number"></asp:textbox>
                    <br>
                                       
                    <asp:textbox id="txtlastname" runat="server" placeholder="Last Name" class="txt_box"></asp:textbox>
                    <br>
                    <br>
                   
                     <asp:imagebutton id="imgbtnlogin" runat="server" ImageUrl="images/button.jpg" Width="55" Height="22" BorderWidth="0"></asp:imagebutton><input value="0" type="hidden" name="hdbutclick">
                </div>
            </div>
             
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

        			<script language="javascript" type="text/javascript">


        			    function validate(frm) {
        			        if (parseInt(frm.hdbutclick.value) != 0)
        			            return false
        			        if (BoxesChecked(frm, "chkagree") <= 0) { alert('Accept Terms and Conditions'); return false; }

        			        else if (isEmpty(frm.txtroomno, 'Please enter your room number.') || isOnlyNumbers(frm.txtroomno, "Room number")) { return false; }
        			        else if (isEmpty(frm.txtlastname, 'Please enter your lastname.') || OnlyText(frm.txtlastname, "Last name")) { return false; }


        			        frm.hdbutclick.value = 1
        			    }

        			  
			</script>

            
<script language="javascript" type="text/javascript">

    $(function () {

        var txt = 'Room number';
        if ($('#txtroomno').val() == '') {
            $('#txtroomno').val(txt);
            $('#txtroomno').css("color", "#e1e1e1")
            // alert('');
        }

        $('#txtroomno').blur(function () {
            if ($(this).val() == '') {
                $(this).val(txt);
                $('#txtroomno').css("color", "#e1e1e1")
            }
        }).focus(function () {
            if ($(this).val() == txt) {
                $(this).val('');
                $('#txtroomno').css("color", "#000")
            }
        });

        var txt1 = 'Last Name';
        if ($('#txtlastname').val() == '') {
            $('#txtlastname').val(txt1);
            $('#txtlastname').css("color", "#e1e1e1")
            // alert('');
        }

        $('#txtlastname').blur(function () {
            if ($(this).val() == '') {
                $(this).val(txt1);
                $('#txtlastname').css("color", "#e1e1e1")
            }
        }).focus(function () {
            if ($(this).val() == txt1) {
                $(this).val('');
                $('#txtlastname').css("color", "#000")
            }
        });

    });
</script>

    </form>
</body>
</html>
