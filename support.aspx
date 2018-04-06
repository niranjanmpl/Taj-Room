<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="support.aspx.vb" Inherits="mpl.Taj.support" %>

<!DOCTYPE html>
<html>
<head>
   <title id="PageTitle" runat="server"></title>
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <link href="taj.css" rel="stylesheet" type="text/css" />
   <link type="text/css" href="jquery.jscrollpane.css" rel="stylesheet" media="all" />
</head>
<body>
    <form>
    <div align="center" id="wrapper">
        <div id="header" align="left">
           <!-- #include file = "header.htm" -->
        </div>
        <div id="main">
            <div class="quick_links">
                <span>Menu</span>
            </div>
            <div class="srv_links highlight">
               <!-- #include file = "left_Support.htm" -->
            </div>
            <div id="nav" align="left" class="support_section" >
                <div class="login_section srv_hdr">
                    Support</div>
                <div class="services">
                    <div id="top">
                    </div>
                    <div align="justify">
                        <span class="style6"><strong>Dear Guest,</strong><br>
                        </span><span class="featuretext">Please find answers to Frequently Asked Questions below.
                            We hope you find these useful. We provide 24 x 7 help desk assistance at this hotel.
                            To access the helpdesk, contact the help desk &nbsp;at any time.</span><br>
                    </div>
                     <ul>
                        <li><a href="#2">VPN FAQS </a></li>
                        <li><a href="#4">Internet Speed FAQS </a></li>
                        <li><a href="#7">If you have e-mail receiving problems </a></li>
                        <li><a href="#8">If you have e-mail sending problems </a></li>
                        <li><a href="#9">If you have web browsing problems</a></li>
                        <li><a href="#10">If your laptop has a problem and you need help </a></li>
                        <li><a href="#11">Other Problems</a></li>
                    </ul>
                    <a href="#11">Other Problems</a>
                    <div>
                        <p class="featuretext" align="justify">
                            <span class="style4"><strong><a id="2" name="2"></a><strong>VPN FAQs</strong></strong></span>
                            <br>
                            This hotel is tested and certified for Cisco VPN, Microsoft VPN, Intel VPN and Checkpoint
                            VPN. Also this network is capable of supporting more than one user from the same
                            organization connecting to the same VPN server&nbsp;
                            <br>
                            <br>
                            Please note that some VPN connections do not allow general browsing once VPN is
                            connected. This may be restricted by your VPN administrator and the hotel may not
                            be able to do anything about it. If you need assistance on VPN, please contact our
                            contact the help desk or contact your organization’s VPN administrator</p>
                        <br />
                    </div>
                    <span class="style4"><strong>Room Plans</strong></span>
                    <p id="pRoomPlans" runat="server">
                        &nbsp;</p>
                    <div class="top_nav">
                        <a href="#top">TOP</a>
                    </div>
                    <div>
                        <p class="featuretext" align="justify">
                            <span class="style4"><strong><a id="4" name="4"></a><strong>Internet Speed FAQs</strong></strong></span><br>
                            This hotel provides
                            <%--<%=TotBandWidth%>--%>
                            Mbps premium bandwidth and a redundant back-up line. If you find the speed slow,
                            this could be due to the site or server you are trying to access or due to bottlenecks
                            in the internet. If you continue to face a problem, please contact the help desk
                            for assistance.
                        </p>
                    </div>
                    <p class="featuretext" align="justify">
                        <span class="style4"><strong><a id="7" name="7"></a><strong>If you have e-mail receiving
                            problems</strong></strong></span><br>
                        1. Make sure you are connected to our network and you have logged in by entering
                        your room number and lastname / firstname.
                    </p>
                    <p class="featuretext" align="justify">
                        2. If you still have a problem please contact the help desk.
                    </p>
                    <p class="featuretext" align="justify">
                        3. If you are using VPN, please go through the VPN portion of this support document
                        and check if any of the tips help you
                        <br>
                    </p>
                    <br>
                    <p align="justify" class="featuretext">
                        <span class="style4"><strong><a name="8" id="8"></a><strong>If you have e-mail sending
                            problems</strong></strong></span><br>
                        If you have SMTP authentication enabled in your laptop e-mail software, you may
                        be prompted to enter a user name and lastname / firstname. to send e-mail. In this
                        case, you will need to disable SMTP authentication. The process for doing this for
                        popular e-mail software is given below. Please click on the software that you are
                        using
                    </p>
                    <p align="justify" class="style4">
                        <a href="javascript:outlook();">Microsoft Outlook</a>
                    </p>
                    <p align="justify" class="style4">
                        <a href="javascript:outlookexp();">Microsoft Outlook Express </a>
                    </p>
                    <p align="justify" class="style4">
                        <a href="Javascript:eudora();">Eudora </a>
                    </p>
                    <div class="top_nav">
                        <a href="#top">TOP</a>
                    </div>
                    <p align="justify" class="featuretext">
                        If you still have difficulty, please call our help desk at Extension. <strong><strong>
                        </strong></strong><strong><strong></strong></strong> 
                        <p align="justify" class="featuretext">
                            <span class="style4"><strong><strong><a name="9" id="9"></a>If you have web browsing
                                problems </strong></strong></span>
                            <br>
                            1. Make sure you are connected to our network and you have logged in by entering
                            your room number and lastname / firstname.<br>
                            <br>
                            2. If you have done the above and still not able to browse, please select another
                            website so that you can verify that the problem is not with the particular site
                            you are visiting. For example, try browsing www.google.com.
                            <br>
                            <br>
                            3. If you still have a problem please contact the help desk.
                        </p>
                        <p align="justify" class="featuretext">
                            4. If you are using VPN, please go through the VPN portion of this support document
                            and check if any of the tips help you
                        </p>
                        <br>
                        <p align="justify" class="featuretext">
                            <span class="style4"><strong><a name="10" id="10"></a><strong>If you have a laptop problem
                                and need help</strong></strong></span>
                            <br>
                            We have Internet help desk staff 24 x 7 at the hotel. They can certainly assist
                            you in trying to resolve your problem.please contact the help desk.
                        </p>
                        <br>
                        <p align="justify" class="featuretext">
                            <strong class="style4"><a name="11" id="11"></a>Other Problems</strong>
                            <br>
                            Please call our 24 x 7 contact the help desk.
                        </p>
                        <div class="top_nav">
                            <a href="#top">TOP</a>
                        </div>
                        <div style="height:20px;">
                        
                        </div>
                </div>
            </div>
            <div class="section" align="left" style="">
               
            </div>
            <div class="spacer">
            </div>
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

    <script type="text/javascript" src="js/jquery.jscrollpane.min.js"></script>

    <script type="text/javascript" src="js/jquery.mousewheel.js"></script>

    <script type="text/javascript">

        $(function() {

            $(".quick_links").click(function() {

                $('.srv_links').slideToggle();

            })
            
              $(function()
            {
	            $('.services').jScrollPane({hijackInternalLinks: true});
            });


        });

        
    </script>

    <script language="javascript">
        function plandetails() {
            var frm = document.frmlogin;
            var XMLObj = null
            if (window.ActiveXObject)
                XMLObj = new ActiveXObject("Microsoft.XMLHTTP");
            else if (window.XMLHttpRequest)
                XMLObj = new XMLHttpRequest();
            GetURL = "Plandetails.aspx";
            document.getElementById("planinfospan").innerHTML = "Loading... Please Wait.";
            XMLObj.open("GET", GetURL, true);
            XMLObj.onreadystatechange = function () {
                if (XMLObj.readyState == 4) {
                    document.getElementById("planinfospan").innerHTML = XMLObj.responseText
                }
            };
            XMLObj.send(null);
        }
        function outlook() {
            window.open('outlook.html');
        }
        function outlookexp() {
            window.open('outlook_express.html');
        }
        function eudora() {
            window.open('http://www.eudora.com/techsupport/kb/2363hq.html');
        }
		</script>

    </form>
</body>
</html>
