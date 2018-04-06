<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Userinfo.aspx.vb" Inherits="mpl.Taj.Userinfo" %>

<!DOCTYPE html>

<html>
<head>
    <title id="PageTitle" runat="server"></title>
    <link href="taj.css" rel="stylesheet" type="text/css" />
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <link href="fonts/Sun.css" rel="stylesheet" type="text/css" />
</head>
<body onload="start(); ">
    <form id="frmPostlogin" name="frmPostlogin" method="post" runat="server">
    <div align="center" id="wrapper">
        <div id="header" align="left">
            <!-- #include file = "header.htm" -->
        </div>
        <div id="main" class="main_bg">
            <div id="nav" align="left">
                <div class="login_section">
                    Connection Status</div>
                <input id="hdRoomNo" type="hidden" name="hdRoomNo" runat="server">
                <input id="hdgrcid" type="hidden" name="hdgrcid" runat="server">
                <input id="hdMAC" type="hidden" name="hdMAC" runat="server">
                <input id="hdRegPg" type="hidden" name="hdRegPg" runat="server">
                <input id="hdLN" type="hidden" name="hdLN" runat="server">
                <input id="hdplanid" type="hidden" name="hdplanid" runat="server">
                <input id="hdclickpostUp" type="hidden" name="hdclickpostUp" runat="server">
                <input id="hdlogintime" type="hidden" name="hdlogintime" runat="server">
                <div style="padding: 3px 0 0 20px; line-height: 18px; color: #58595b;" id="con_status"
                    class="plan_info font_set plan_height3  ">
                    <p>
                        <b><asp:Label ID="msg"   runat="server" Text=""></asp:Label></p></b>
                    <p>
                         <b>Connected Balance Time Left:</b>  <span style="color: #F0710F" id="CH_dtimer1_digits">
                        </span>
                    </p>
                    <p>
                         <b>Bytes Send & Received:</b>  <span style="color: #F0710F" id="totbytes">
                        </span>
                    </p>
                    <p>
                        <div id="fb" runat="server" style="">
                            <asp:HyperLink ID="Hyperfeedback2" runat="server" Style="font-family:'Sun SemiBold'; font-size:18px;
                                color: #58595b; ">Feedback</asp:HyperLink>
                            <asp:Label ID="lblfeedback" runat="server"> &nbsp;We value your feedback. Help us improve our services</asp:Label>
                            &nbsp;<asp:HyperLink ID="Hyperfeedback1" runat="server" Target="printer" Style="font-family:'Sun SemiBold'; font-size:18px;
                                color: #58595b; ">click here.</asp:HyperLink>
                        </div>
                    </p>
                    <p>
                        <asp:HyperLink ID="Hlinkprinter2" runat="server" Target="printer" Style="line-height: 18px;
                            font-weight: bold; color: #58595b; font-size: 14px;"> Printer</asp:HyperLink>
                        <asp:Label ID="lblprinter" runat="server">To print from your room or 
										laptop, </asp:Label>
                        <asp:HyperLink ID="Hlinkprinter1" runat="server" Target="printer" Style="color: #F0710F;
                            font-size: 14px;">click here.</asp:HyperLink>
                    </p>
                    <p style="display: none;">
                        <asp:HyperLink ID="HyperLink2" Target="_new" runat="server" NavigateUrl="AutoUpgradePopUp.aspx"
                            class="fancybox fancybox.iframe" title="">HyperLink</asp:HyperLink>
                    </p>
                    <input id="hdpopup" type="hidden" name="hdpopup" runat="server">
                    <input id="hdpopupconfirm" type="hidden" name="hdpopupconfirm" runat="server">
                    <p>
                        <asp:LinkButton ID="linkUpgrade" runat="server"  Style="font-family:'Sun SemiBold'; font-size:20px;  
                                color: #58595b; ">UPGRADE TO PREMIUM PLAN</asp:LinkButton></p>
                    <p>
                        <asp:HyperLink ID="Hlinkvirus2" runat="server" Target="virus" Style="line-height: 18px;
                            font-weight: bold; color: #58595b; font-size: 14px; font-weight: bold">Virus Advisory</asp:HyperLink>
                        &nbsp;
                        <asp:HyperLink ID="Hlinkvirus1" runat="server" Target="virus" Style="color: #F0710F;
                            font-size: 14px;">Click here if you wish on-line
                        <%--<br>--%>
                        scanning of your computer</asp:HyperLink></p>
                </div>
                <div class="call_info height_setter">
                    <p>
                        Dear Guest,
                        <br />
                        Welcome to Taj Connect, an online facility which allows you to navigate through our
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
                width: 300,
                height: 300,
                autoCenter: false,
                scrolling: 'no',
                closeBtn: false, // hide close button
                'iframe': { 'scrolling': 'no' }

            });

        });
	
    </script>

    <script language="javascript">
			var time, timerID, clockticks,balsecs
            var day = 0;
			var hh = 0;
			var mm = 0;
			var ss = 0;
			function stop (){}
				
	
			function start(){
						
				var frm = document.frmPostlogin
				var strurl = frm.hdRegPg.value; 
			    //window.open(strurl);
				myWindow = window.open(strurl, "newwindow") 

				GetByteCount();
				time = <%=Plantime%> - 1 
				showtime()
			}
	
			function showtime(){
				clockticks = time ;
			//Remaining hrs alert
			if(clockticks == 300){
			var t1 = new Date();
				alert("You Have 5 minutes Left");
			var t2 = new Date();
				rt = Math.round((t2 - t1)/1000);
				if (time > rt){
					time = time - rt;
					clockticks = time;
				}
				else
					time = 0;	
				}
				//get bytes count from nomadix

				if(clockticks%60 == 0)
					GetByteCount()

                                        
                  //Calculate days     
	            if (clockticks >= 86400)
	            {
                    day = clockticks/86400
		            balsecs= clockticks%86400
		            day= Math.floor(day)
		            clockticks = balsecs	
                
	            } 
				//Calculate hours
				if (clockticks >= 3600){
					hh = clockticks/3600
					balsecs= clockticks%3600
					hh= Math.floor(hh)
					clockticks = balsecs	
				}      
				else{hh=0	}
				//Calculate Minutes
				if(clockticks >= 60){
					mm = clockticks/60
					balsecs = clockticks%60
					mm = Math.floor(mm)
					clockticks = balsecs
				}
				else {mm=0 }
				//Calculate Seconds
				ss = Math.floor(clockticks)
			
				 if (day >= 1)
                 {
    	            document.getElementById("CH_dtimer1_digits").innerHTML = day + " Days: " +hh + " Hrs: " + mm + " Min: " + ss + " Sec"
                  }
	            else if(time > 0) 
	            {
    	            document.getElementById("CH_dtimer1_digits").innerHTML = hh + " Hrs: " + mm + " Min: " + ss + " Sec"
                }
	            else
                {
		            document.getElementById("CH_dtimer1_digits").innerHTML ="You have been logged out or your Account Expired"
                }
	
	timerID = setTimeout("showtime()",1000)
	time--;	
			}
		
		function createRequestObject() {
			var request_o; //declare the variable to hold the object.
			
			if (window.XMLHttpRequest){
				// If IE7, Mozilla, Safari, etc: Use native object
				request_o = new XMLHttpRequest()
			}
			else{
				if (window.ActiveXObject){
				// ...otherwise, use the ActiveX control for IE5.x and IE6
				request_o = new ActiveXObject("Microsoft.XMLHTTP");
				}
			}		
			return request_o; //return the object
		}
			//Bytes count program
			function GetByteCount(){
				var sd_data, URL, XMLObj
				var frm = document.frmPostlogin
				XMLObj = createRequestObject();
				/*if (window.ActiveXObject)
					XMLObj = new ActiveXObject("Microsoft.XMLHTTP");
				else if (window.XMLHttpRequest)
					XMLObj = new XMLHttpRequest();*/
				sd_data = "MA=" + frm.hdMAC.value + "&RN=" + frm.hdRoomNo.value;
				URL = "SndRevBytes.aspx?" + sd_data
				XMLObj.open("GET",URL,true);
				XMLObj.onreadystatechange = function() 
				{
					document.getElementById("totbytes").innerHTML = "Loading...";
					if (XMLObj.readyState == 4)
						document.getElementById("totbytes").innerHTML = XMLObj.responseText;
				}				
				XMLObj.send(null);
			}
    </script>
    <script src="js/img_setting.js" type="text/javascript" id="Script1"></script>
    </form>
</body>
</html>
