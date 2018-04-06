<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ResetPopUpAfterReset.aspx.vb" Inherits="mpl.Taj.ResetPopUpAfterReset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Document</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="fonts/Sun.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        *
        {
            margin: 0px;
            padding: 0px;
        }
        #app_wrapper
        {
            
            width: 400px;
            
            margin: 0 auto;
            border: solid 0px #eee;
        }
        #app_wrapper .logo
        {
            margin-top: 30px;
        }
        .button1
        {
            background: #856d42 url(images/input_bg.jpg) left  center;
             background-size:100% 100%;
            color: #fff;
            border: solid 1px #856d42 ;
            padding: 7px 20px;
             font-family: 'Sun semibold', Verdana;
            font-size: 16px;
            transition: all 0.3s ease;
            width: 200px;
            -webkit-transition: all 0.3s ease;
            -moz-transition: all 0.3s ease;
            -o-transition: all 0.3s ease;
            display: block;border-radius:10px;
            -ms-transition: all 0.3s ease;
            transition: all 0.3s ease;
        }
        /*.button1:hover
        {
            background: #7b7b7b;
        }*/
        .button2
        {
            background: #fff;
            color: #000;
            border: solid 1px #000;
            padding: 8px 20px;
          font-family: 'Sun semibold', Verdana;
            font-size: 16px;
            transition: all 0.3s ease;
            width: 200px;
            -webkit-transition: all 0.3s ease;
            -moz-transition: all 0.3s ease;
            -o-transition: all 0.3s ease;
            display: block;
            -ms-transition: all 0.3s ease;
            transition: all 0.3s ease; border-radius:10px;
            margin-top: 5px;
        }
        .button2:hover
        {
            background: #7b7b7b;
            color: #fff;
        }
        .msg_info
        {
            width: 400px;
            font-size: 18px; 
            margin-top: 20px;
            margin-bottom: 20px;
        }
        .msg_info h1
        {
             font-family: 'Sun', Verdana;
            font-size: 18px; padding:0px; margin:0px;
            margin-bottom: 10px; 
            text-shadow: 1px 1px 1px #eee;
            font-weight: bold;
        }
        .msg_info p
        {
            font-size:18px;
             font-family:'Sun';
            margin-bottom: 20px;
            text-shadow: 1px 1px 1px #eee;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="app_wrapper">
        <div class="msg_info">
            <h1>
                Dear Guest,</h1>
            <p style="text-align: justify;">
                Dear Guest, We are pleased to confirm the reset of your Internet password. Your
                password has been reset by the onsite Internet Cyber support team based on the verification
                of the details provided. Please note that all existing passwords from your devices
                already logged into the internet services have been changed. You will need to be
                re-entered for all devices which are logged-in for accessing internet through this
                service Should you require any assistance please contact our internet services desk
                on extension “62” and our team would be we would be delighted to provide personalized
                service. Thank you, Internet Services Team.
            </p>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnConfirm" class="button1"
                OnClientClick="return OpenYes()" runat="server" Text="Confirm" />&nbsp;&nbsp;
            <asp:Button ID="btncancel" class="button1" OnClientClick="return OpenNo()" runat="server"
                Text="Cancel" />
            <input id="hdchild" type="hidden" value="test" name="hdchild" runat="server"  />
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
        </div>
    </div>

    <script language="javascript" type="text/javascript">


        function OpenYes() {

            var phd = parent.document.getElementById("hdpopup");
            //alert(phd.value());
            phd.value = "20";
            parent.window.proceed();

            parent.jQuery.fancybox.close();

        }


        function OpenNo() {

            var phd = parent.document.getElementById("hdpopup");
            //alert(phd.value());
            phd.value = "10";
            parent.window.Cancel();

            parent.jQuery.fancybox.close();

        }

    </script>

    </form>
</body>
</html>
