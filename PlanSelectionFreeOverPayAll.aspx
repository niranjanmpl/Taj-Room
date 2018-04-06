<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PlanSelectionFreeOverPayAll.aspx.vb" Inherits="mpl.Taj.PlanSelectionFreeOverPayAll" %> 

<!DOCTYPE html>
<html>
<head>
    <title id="PageTitle" runat="server"></title>
    <link href="taj.css" rel="stylesheet" type="text/css" />
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <link href="images/favicon.ico" type="image/x-icon" rel="shortcut icon">

    <script language="JavaScript" src="validation.js"></script>

    <link type="text/css" href="jquery.jscrollpane.css" rel="stylesheet" media="all" />
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
                    Available Plans</div>
                <div style="padding: 3px 0 0 20px; line-height: 18px; color: #58595b;" class="plan_info font_set plan_height3 ">
                    <p>
                        Dear Guest,<br />
                        <br>
                        You have reached the maximum devices permitted under your existing plan. Please
                        upgrade your plan to access additional devices.
                    </p>
                    <p>
                        <input id="radio" value="NO" checked type="radio" name="rdoyesorno">
                        <input id="hdlsgstatus" type="hidden" name="hdlsgstatus" runat="server">
                        <label for="checkbox-1" class="plan_details">
                            <u><b>Premium Plan</b></u>
                            <asp:Label ID="lblplanname" runat="server" Text=""></asp:Label></label></p>
                    <p class="plan_note">
                        Bandwidth to support video streaming, unlimited email access, etc.,
                        <!--with up to
                        <asp:Label ID="lblpremdev" runat="server" Text=""></asp:Label>
                        personal devices,-->
                        any additional devices will be chargeable, with taxes extra as applicable.</p>
                    <div id="codevisible5" class="premium_sln">
                        <asp:DropDownList ID="cmbplans" runat="server" CssClass="select_style">
                            <asp:ListItem Value="-1" Selected="True">Select a Plan</asp:ListItem>
                        </asp:DropDownList>
                        <span class="style1" id="planinfospan"></span><span id="divlsg" class="days_sln"
                            style="display: none"><span>No.Of Days :</span>
                            <asp:TextBox ID="txtLongStay" Width="30" Height="15" runat="server" MaxLength="2"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtAmount" Width="70" Height="15" Enabled="false" runat="server"
                                MaxLength="2"></asp:TextBox>&nbsp;+ Taxes </span>
                    </div>
                    <p style="display: none;">
                        <asp:HyperLink ID="HyperLink2" Target="_new" runat="server" NavigateUrl="AutoUpgradePopUp.aspx"
                            class="fancybox fancybox.iframe" title="">HyperLink</asp:HyperLink>
                    </p>
                    <p class="btn_pos">
                        <%--<asp:ImageButton ID="imgbtnlogin" runat="server" ImageUrl="images/button.jpg" Width="55"
                            Height="22" BorderWidth="0"></asp:ImageButton>--%>
                        <asp:Button ID="imgbtnlogin" runat="server" Text="Login" CssClass="btn_style" />
                        <input type="hidden" value="0" name="hdbutclick">
                        <input id="hdrdostatus" type="hidden" name="hdrdostatus" runat="server">
                        <input id="hdpopup" type="hidden" name="hdpopup" runat="server">
                        <input id="hdpopupconfirm" type="hidden" name="hdpopupconfirm" runat="server">
                    </p>
                    <p>
                        &nbsp;</p>
                </div>
                <div class="call_info height_setter">
                    <p>
                        Dear Guest,
                        <br />
                        Welcome to Taj Conect, an online facility which allows you to navigate through our
                        innumerable hotel service. For your convenience, you can connect to our wireless
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

    <script type="text/javascript" src="js/jquery.jscrollpane.min.js"></script>

    <script type="text/javascript" src="js/jquery.mousewheel.js"></script>

    <script type="text/javascript">

        $(function () {

            $(".quick_links").click(function () {

                $('.srv_links').slideToggle();

            })


        });

        $(function () {
            $('.plan_info').jScrollPane();
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


            $('#btn_close').click(function () {

                $("html, body").animate({ scrollTop: $(document).height() }, 1000);

                $('body').css('boder', 'solid 1px red');

            });



        });
	
    </script>

    <script language="javascript" type="text/javascript">


        function validateLsg(frm) {

            var frm = document.frmlogin;
            var tmpdays = frm.txtLongStay.value;

            if (isEmpty(frm.txtLongStay, "No of days")) { return false; }
            if (isNumeric(frm.txtLongStay, "No of days")) { return false; }

            if (tmpdays < 7) {
                alert('Dear Guest, Days should be at least 7 days');
                return false;
            }
            if (tmpdays > 60) {
                alert('Dear Guest, Sorry, Your no.of days exceeds more than 60');
                return false;
            }


        }

        function validate(frm) {

            var T_lsg;
            T_lsg = frm.hdlsgstatus.value;


            if (parseInt(frm.hdbutclick.value) != 0)
                return false;

            else {


                if (frm.hdrdostatus.value != 1) {
                    if (frm.cmbplans.value == "-1") { alert('Please select a Plan'); return false; }

                    frm.hdpopupconfirm.value = 2

                    if (T_lsg == "0") {


                        var tmpdays = document.frmlogin.txtLongStay.value;

                        if (isEmpty(document.frmlogin.txtLongStay, "Enter No of days")) { return false; }
                        if (isNumeric(document.frmlogin.txtLongStay, "No of days")) { return false; }
                        if (tmpdays < 7) {
                            alert('Dear Guest, Days should be at least 7 days');
                            return false;
                        }
                        if (tmpdays > 60) {
                            alert('Dear Guest, Sorry, Your no.of days exceeds more than 60');
                            return false;
                        }

                    }

                    if (frm.hdpopup.value != 20) {
                        //alert('adssss');
                        start();
                        return false;
                    }
                }

                frm.hdbutclick.value = 1;
            }
        }

        			  
    </script>

    <script language="javascript" type="text/javascript">
    
  function amountNewRate(e)
{
var amt;
var days;
var tmpamt;
var tmpstax;
var tmpltax;
var tamptot;
days = e;


var frm = document.frmlogin;

 if (days >= 7 && days <= 15)
{
	
	tmpamt = frm.txtAmount.value = round((<%=LsgPlanAmount%>) * days);
       
    discount = (tmpamt * <%=Discountlst15%> /100)
    tamptot = parseFloat(tmpamt) - parseFloat(discount)
    frm.txtAmount.value = round(tamptot);
	
}
else if (days > 15 && days <= 60)
{
	 tmpamt = frm.txtAmount.value = round((<%=LsgPlanAmount%>) * days);
        
    discount = (tmpamt * <%=Discountgrt15%> /100)
    tamptot = parseFloat(tmpamt) - parseFloat(discount)
    frm.txtAmount.value = round(tamptot);
		
	
}

else
{
frm.txtAmount.value = 0

}
}


function amount(e)
{
var amt;
var days;
var tmpamt;
var tmpstax;
var tmpltax;
var tamptot;
days = e;


var frm = document.frmlogin;

 if (days >= 7 && days < 15)
{
	tmpamt =  round((<%=Ratelst7%>/7) * days);
	//tmpstax = (parseFloat(tmpamt) * parseFloat(<%=Stax%>)/100).toFixed(2);
	//tmpltax = (parseFloat(tmpamt) * parseFloat(<%=Ltax%>)/100).toFixed(2);
	//tamptot = parseFloat(tmpamt) + parseFloat(tmpstax) + parseFloat(tmpltax).toFixed(2);
	//tmpamt = parseFloat(tmpamt).toFixed(2)
	frm.txtAmount.value = tmpamt;
}
else if (days >= 15 && days < 30)
{
	tmpamt =  round((<%=Ratelst30%>/15) * days);
	//tmpstax = (parseFloat(tmpamt) * parseFloat(<%=Stax%>)/100).toFixed(2);
	//tmpltax = (parseFloat(tmpamt) * parseFloat(<%=Ltax%>)/100).toFixed(2);
	//tamptot = parseFloat(tmpamt) + parseFloat(tmpstax) + parseFloat(tmpltax).toFixed(2);
	//tmpamt = parseFloat(tmpamt).toFixed(2)
	frm.txtAmount.value = tmpamt;
}
else if(days >= 30 && days <= 60)
{
	tmpamt = round((<%=Rategrt30%>/30) * days);
	//tmpstax = (parseFloat(tmpamt) * parseFloat(<%=Stax%>)/100).toFixed(2);
	//tmpltax = (parseFloat(tmpamt) * parseFloat(<%=Ltax%>)/100).toFixed(2);
	//tamptot = parseFloat(tmpamt) + parseFloat(tmpstax) + parseFloat(tmpltax).toFixed(2);
	//tmpamt = parseFloat(tmpamt).toFixed(2)
	frm.txtAmount.value = tmpamt;
    
}

else
{
frm.txtAmount.value = 0

}
}
    </script>

    <script language="javascript" type="text/javascript">


        function displayplandetails(planId) {


             var frm = document.frmlogin;
            var s_planid;
            var lsgplanid;

            s_planid = planId
            lsgplanid = <%=LSGPlanId %>
           
             if (s_planid == lsgplanid)
             {
             
                  document.getElementById('divlsg').style.display = "block";
                  frm.hdlsgstatus.value = 0;
             }
             else
             {
                  document.getElementById('divlsg').style.display = "none";
                  frm.hdlsgstatus.value = 1;

             }
             
            sd_data = planId;
            var frm = document.frmlogin;
            var GetURL, selusrid
            var XMLObj = createRequestObject();
            GetURL = "GetPlaninfo.aspx?planId=" + sd_data;
            document.getElementById("planinfospan").innerHTML = "Loading... Please Wait.";


            XMLObj.open("GET", GetURL, true);

            XMLObj.onreadystatechange = function () {
                if (XMLObj.readyState == 4) {
                    document.getElementById("planinfospan").innerHTML = XMLObj.responseText
                }
            };
            XMLObj.send(null);
        }

        function createRequestObject() {
            var request_o; //declare the variable to hold the object.
            var browser = navigator.appName; //find the browser name
            if (browser == "Microsoft Internet Explorer") {
                /* Create the object using MSIE's method */
                request_o = new ActiveXObject("Microsoft.XMLHTTP");
            } else {
                /* Create the object using other browser's method */
                request_o = new XMLHttpRequest();
            }
            return request_o; //return the object
        }

        function displayplandetailsonload(plan) {

            alert('load')
            sd_data = plan;
            var frm = document.frmlogin;
            var sd_data, GetURL, selusrid
            var XMLObj = createRequestObject();
            GetURL = "GetPlaninfo.aspx?planid=" + sd_data;
            document.getElementById("planinfospan").innerHTML = "Loading... Please Wait.";
            /*if (window.ActiveXObject)
            XMLObj = new ActiveXObject("Microsoft.XMLHTTP");
            else
            if (window.XMLHttpRequest)
            XMLObj = new XMLHttpRequest();    	 */

            XMLObj.open("GET", GetURL, true);

            XMLObj.onreadystatechange = function () {
                if (XMLObj.readyState == 4) {
                    document.getElementById("planinfospan").innerHTML = XMLObj.responseText
                }
            };
            XMLObj.send(null);
        }

    </script>

    <script language="javascript" type="text/javascript">



        function start() {

            var frm = document.frmlogin;

            if (frm.hdpopupconfirm.value == 2) {
                document.getElementById('HyperLink2').click();
            }

        }

        function Cancel() {

            //  visiblerdoPopUp();
        }

        function proceed() {

            document.getElementById('imgbtnlogin').click();
        }
  	


    </script>

    </form>
</body>
</html> 