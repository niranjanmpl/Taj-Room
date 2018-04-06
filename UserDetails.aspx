<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserDetails.aspx.vb" Inherits="mpl.Taj.UserDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Document</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <script language="JavaScript" src="validation.js"></script>
    <link href="fonts/Sun.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        *
        {
            margin: 0px;
            padding: 0px;
        }
        #app_wrapper
        {
            width: 100%;
            margin: 0 auto;
        }
        #app_wrapper .logo
        {
            margin-top: 30px;
        }
        .button1
        {
            background: #856d42 url(images/input_bg.jpg) left center;
            background-size: 100% 100%;
            color: #fff;
            border: solid 1px #856d42;
            padding: 7px 20px;
            font-family: 'Sun semibold' , Verdana;
            font-size: 16px;
            transition: all 0.3s ease;
            width: 200px;
            -webkit-transition: all 0.3s ease;
            -moz-transition: all 0.3s ease;
            -o-transition: all 0.3s ease;
            display: block;
            border-radius: 10px;
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
            font-family: 'Sun semibold' , Verdana;
            font-size: 16px;
            transition: all 0.3s ease;
            width: 200px;
            -webkit-transition: all 0.3s ease;
            -moz-transition: all 0.3s ease;
            -o-transition: all 0.3s ease;
            display: block;
            -ms-transition: all 0.3s ease;
            transition: all 0.3s ease;
            border-radius: 10px;
            margin-top: 10px;
        }
        .button2:hover
        {
            background: #7b7b7b;
            color: #fff;
        }
        .msg_info
        {
            width: 100%;
            font-size: 16px;
            margin-top: 0px;
            margin-bottom: 20px;
        }
        .msg_info h1
        {
            font-family: 'Sun semibold' , Verdana;
            padding: 0px;
            margin: 0px;
            margin-bottom: 10px;
            margin-top: 0px !important;
            text-shadow: 1px 1px 1px #eee;
            font-size: 16px;
        }
        .msg_info p
        {
            font-size: 16px;
            font-family: 'Sun';
            margin-bottom: 10px;
            text-shadow: 1px 1px 1px #eee;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="app_wrapper">
        <div class="msg_info">
            <h1>
                <asp:Label ID="lblHeading" runat="server" Text="Label"></asp:Label></h1>
            <p>
                Email Id<br />
                <asp:TextBox ID="txtEmail" runat="server" Width="218px"></asp:TextBox><br />
                Ex:info@gmail.com
            </p>
            <input id="hdstatus" type="hidden" value="31" name="hdstatus" runat="server">
            <p id="textCCode" runat="server">
                CountryCode<br />
                <asp:DropDownList ID="drpcountrycode" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                MobileNo.<br />
                <asp:TextBox ID="txtMobileNo" runat="server" Width="218px"></asp:TextBox><br />
                Ex:1234567890
            </p>
            <input id="hdguestid" type="hidden" value="" name="hdguestid" runat="server" />
            <label for="checkbox-1">
                <asp:CheckBox ID="CheckBoxlike" Checked="true" runat="server" />
                <span style="font-family: 'Sun semibold', Verdana;">I would like to receive communication
                    from Taj Hotels Resorts & Palaces </span>
            </label>
            <br />
            <label for="checkbox-1">
                <asp:CheckBox ID="CheckBoxIAgree" Checked="true" runat="server" />
                <span style="font-family: 'Sun semibold', Verdana;">I agree with the terms and conditions
                </span>
            </label>
            <br>
            <asp:Button ID="btnyes" runat="server" class="button1" OnClientClick="return validate()"
                Text="Continue" />
            <asp:Button ID="btnyesAvilData" runat="server" class="button1" OnClientClick="return validateAvilData()"
                Text="Continue" />
            <br />
            <asp:Button ID="btnyesUpdate" runat="server" class="button1" OnClientClick="return validateAvilDataUpdate()"
                Text="Update" />
            <input value="0" type="hidden" id="hdbutclick" name="hdbutclick" runat="server" />
            <input id="hdchild" type="hidden" value="test" name="hdchild" runat="server" />
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        function validate() {
            var hdflag = document.getElementById('hdbutclick').value;

            document.getElementById('hdstatus').value = 31;

            if (hdflag != 0)
                return false

            else {
                if (isEmpty(document.getElementById('txtEmail').value, 'Please enter your Email id') || isEmail(document.getElementById('txtEmail').value, "Email id")) { return false; }
                var CCode = document.getElementById('drpcountrycode').value;
                if (CCode.trim() == '+91') {
                    if (isEmpty(document.getElementById('txtMobileNo').value, 'Mobile No.') || isMobileNo(document.getElementById('txtMobileNo').value, "")) { return false; }
                }
                else {
                    if (isEmpty(document.getElementById('txtMobileNo').value, 'Please enter your Mobile No.') || isMobileNoInt(document.getElementById('txtMobileNo').value, "Mobile No")) { return false; }
                }
            }
            //OpenYes();
            document.getElementById('hdbutclick').value = 1;

        }


        function validateAvilData() {
            var hdflag = document.getElementById('hdbutclick').value;

            document.getElementById('hdstatus').value = 31;

            if (hdflag != 0)
                return false

            else {
                if (isEmpty(document.getElementById('txtEmail').value, 'Please enter your Email id') || isEmail(document.getElementById('txtEmail').value, "Email id")) { return false; }

            }
            OpenYes();
            document.getElementById('hdbutclick').value = 1;

        }

        function validateAvilDataUpdate() {
            var hdflag = document.getElementById('hdbutclick').value;

            if (hdflag != 0)
                return false

            else {
                if (isEmpty(document.getElementById('txtEmail').value, 'Please enter your Email id') || isEmail(document.getElementById('txtEmail').value, "Email id")) { return false; }

            }
            OpenNo();
            // document.getElementById('hdbutclick').value = 1;
            document.getElementById('hdstatus').value = 30;
            document.getElementById('hdbutclick').value = 0;
        }

        function isEmpty(str, Label) {
            //if (str.value == null) { alert(str.length) }
            if ((str == null) || (str.length == 0)) {
                alert(Label + ' can\'t be empty');
                str.select(); str.focus();
                return true;

            }
            return false;

        }




        function isMobileNo(str, Label) {
            var ValidChars = "+-0123456789";
            var Char;
            var char1;
            //alert(str.length);
            char1 = str.charAt(0);
            //alert(char1);
            for (i = 0; i < str.length; i++) {
                Char = str.charAt(i);
                if (ValidChars.indexOf(Char) == -1) {
                    alert(Label + 'Mobile No. invalid.');
                    str.focus();
                    return true;
                }
            }

            if (char1 == '0') {

                alert(Label + 'ZERO not allowed in front of mobile no.');
                str.focus();
                return true;
            }

            if (char1 == '+') {

                alert(Label + '+ not allowed in front of mobile no.');
                str.focus();
                return true;
            }


            //               if (str.length != 10) {
            //                   alert(Label + 'Mobile no. should be 10 digits.');
            //                   str.focus();
            //                   return true;
            //               }

            return false;
        }


        function isMobileNoInt(str, Label) {
            var ValidChars = "0123456789";
            var Char;
            var char1;

            char1 = str.charAt(0);

            if (char1 == '0') {

                alert(Label + ' filed ZERO not allowed.');
                str.focus();
                return true;
            }

            if (char1 == '+') {

                alert(Label + ' field + not allowed.');
                str.focus();
                return true;
            }

            if (str.length > 15) {
                alert(Label + ' should be 5 to 15 digits.');
                str.focus();
                return true;
            }

            if (str.length < 5) {
                alert(Label + ' should be 5 to 15 digits.');
                str.focus();
                return true;
            }


            for (i = 0; i < str.length; i++) {
                Char = str.charAt(i);
                if (ValidChars.indexOf(Char) == -1) {
                    alert(Label + ' invalid.');
                    str.focus();
                    return true;
                }
            }
            return false;
        }

        function isEmail(str) {
            var s = str;
            var i = 1, Length = s.length, result;
            while ((i < Length) && (s.charAt(i) != '@')) i++;
            if ((i == Length) || (s.charAt(i) != '@')) {
                alert("Email address don\'t have the character @ after the login name.");
                str.focus();
                return true;
            }
            i += 2;
            while ((i < Length) && (s.charAt(i) != '.')) i++;
            if ((i == Length) || (s.charAt(i) != '.')) {
                alert("Email address don\'t have the character . after the domain name.");
                str.focus();
                return true;
            }
            if (i + 1 >= Length) {
                alert("Email address should have atleast one character after . ");
                str.focus();
                return true;
            }
            return false;
        }


    </script>
    <script language="javascript" type="text/javascript">



        function OpenYes() {


            var phd = parent.document.getElementById("hdpopup");

            //alert(phd.value());
            phd.value = "20";
            parent.window.proceed();
            parent.jQuery.fancybox.close();



        }

        function OpenYesUpdate() {


            var phd = parent.document.getElementById("hdpopup");

            //alert(phd.value());
            phd.value = "50";
            parent.window.proceed();
            parent.jQuery.fancybox.close();



        }


        function OpenNo() {

            var phd = parent.document.getElementById("hdpopup");
            //alert(phd.value());
            phd.value = "10";
            //  parent.window.Cancel();

            // parent.jQuery.fancybox.close();

        }

    </script>
    </form>
</body>
</html>
