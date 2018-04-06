Imports PMSPkgV1_0
Public Class Userinfo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "
    Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents lbldispguestname As System.Web.UI.WebControls.Label
    Protected WithEvents HLinkvirus3 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents divlogout As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblogout As System.Web.UI.WebControls.LinkButton
    Protected WithEvents hdMAC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdRegPg As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdRoomNo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdLN As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdplanid As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdlogintime As System.Web.UI.HtmlControls.HtmlInputHidden


    Protected WithEvents Hlinkfeedback1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hlinkfeedback2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents HLinkvirus1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents HLinkvirus2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hlinkprinter1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hlinkprinter2 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents Hyperfeedback1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Hyperfeedback2 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents hdgrcid As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdusertype As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdmsg As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Label_logout As System.Web.UI.WebControls.Label
   
    Protected WithEvents lblfreemsg As System.Web.UI.WebControls.Label
#End Region

    Public ReqPage, Macadd, grcid, roomNo As String
    Public Plantime As Long
    Public usertype As Integer
    Public url As String
    Protected WithEvents lblProNEWS As WebControls.Label
    Protected pageTitle As New HtmlGenericControl
    Protected WithEvents ads As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents asd As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Paneltimer As System.Web.UI.WebControls.Panel
    Protected WithEvents CH_dtimer1_digits As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents totbytes As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents timedisplay As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents imgclock As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents bytesendreceived As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents ImgPromotion As System.Web.UI.WebControls.LinkButton
    Protected logoutoption As New HtmlGenericControl
    Dim UniqueLogid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Put user code to initialize the page here
        Dim objSysConfig As New CSysConfig
        Dim userContext As UserContext
        Dim gateWay As IGatewayService
        Dim gateWayFact As GatewayServiceFactory
        Dim gatWayQryResult As NdxQueryGatewayResults
        Dim pgCookie As New CCookies

        Dim LoginFrom, MA As String
        Me.pageTitle.InnerText = objSysConfig.GetConfig("HotelName")


        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance

        url = Request.QueryString("encry")
       

        UniqueLogid = commonFun.DecrptQueryString("logid", url)

        If UniqueLogid = "" Or UniqueLogid = Nothing Then

            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Logid missed" & vbCrLf)
            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
        End If

        '------------------- START De-Serialization --------------------------
        Dim UsrCred As Hashtable = Nothing
        userContext = New UserContext(hdRoomNo.Value, hdMAC.Value)
        UsrCred = userContext.DeSerialize(UniqueLogid)
        '------------------- END De-Serialization --------------------------

        If IsNothing(UsrCred) Then

            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in UserInfo" & vbCrLf)
            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
        End If


        LoginFrom = commonFun.DecrptQueryString("LoginFrom", url)
        MA = commonFun.DecrptQueryString("MA", url)


        If Not IsPostBack() Then

            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            If UsrCred.Item("roomNo") <> "" And UsrCred.Item("machineId") <> "" Then
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: PostLogin Page -- Room No:" & UsrCred.Item("roomNo") & " -- MAC:" & UsrCred.Item("machineId"))
            Else
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: PostLogin Page")
            End If


            If UsrCred.Item("machineId") = "" Then Response.Redirect("UserError.aspx?Msg=This Error has probably occurred because you directly accessed our login screen. Please use your browser to access any internet website, and if you are not already authenticated, you will be automatically be presented with a login screen.")

            Dim GuestId As String = UsrCred.Item("grcid")

            If GuestId <> "" Or GuestId <> Nothing Then

                If UsrCred.Item("logintime") = Nothing Then
                    UsrCred.Item("logintime") = Now()
                    UsrCred.Item("serviceaccess") = 1
                End If

                If Not GuestId <> "" Or GuestId <> Nothing Then pgCookie.SetCookieWithSerialize(UsrCred, Response)

                hdMAC.Value = UsrCred.Item("machineId")
                hdRegPg.Value = UsrCred.Item("requestedPage")
                hdRoomNo.Value = UsrCred.Item("roomNo")
                hdgrcid.Value = GuestId
                hdLN.Value = UsrCred.Item("guestname")
                hdplanid.Value = UsrCred.Item("selectedPlanId")
                hdlogintime.Value = UsrCred.Item("logintime")

                userContext = New UserContext(hdRoomNo.Value, hdMAC.Value)
                If UsrCred.Item("serviceaccess") = SERVICETYPE.FREE Then

                    'timedisplay.Visible = False
                    'imgclock.Visible = False
                    'bytesendreceived.Visible = False
                    'totbytes.Visible = False
                End If


                If UCase(LoginFrom) = "UPGRADE" Then
                    msg.Visible = True

                    Dim ObjLog As LogInOutService
                    ObjLog = LogInOutService.getInstance
                    Dim ErrMsg As String = ObjLog.GetErrorDesc("3")
                    If ErrMsg <> "0" Then
                        msg.Text = ErrMsg
                    End If
                ElseIf UCase(LoginFrom) = "AUTOUPGRADE" Then

                    hdpopupconfirm.Value = "2"

                End If


                '----------- START Display device Count order -------------------------------

                Dim objLogInOut As LogInOutService
                objLogInOut = LogInOutService.getInstance

                If objLogInOut.FindAlreadyUsedMAC_InfoPage(UsrCred.Item("billid"), UsrCred.Item("machineId")) Then

                    Dim T_MACCount As Integer = objLogInOut.FetchMACCount(UsrCred.Item("billid"))

                    Dim ObjLog As LogInOutService
                    ObjLog = LogInOutService.getInstance

                    msg.Text = ObjLog.GetErrorDesc("10" & T_MACCount)

                End If

                '----------- END Display device Count order -------------------------------

                Dim AllFreeplan As String = objSysConfig.GetConfig("AllFreePlanId")

                objLogInOut = LogInOutService.getInstance
                If objLogInOut.isFreePlan(AllFreeplan, UsrCred.Item("selectedPlanId")) Or objLogInOut.isFreePlan(AllFreeplan, UsrCred.Item("planid")) Then
                    linkUpgrade.Visible = True
                ElseIf hdplanid.Value = "YES" Then
                    linkUpgrade.Visible = True
                Else
                    linkUpgrade.Visible = False
                End If

                If UsrCred.Item("serviceaccess") = SERVICETYPE.FREE Then
                    linkUpgrade.Visible = False
                    'timedisplay.Visible = False
                    'imgclock.Visible = False
                    'bytesendreceived.Visible = False
                    'totbytes.Visible = False
                End If


                'Dim FreePlanId As String = objSysConfig.GetConfig("FreePlanId")
                'If (UsrCred.Item("selectedPlanId") = FreePlanId) Or (UsrCred.Item("planid") = FreePlanId) Then
                '    linkUpgrade.Visible = True
                'ElseIf hdplanid.Value = "YES" Then
                '    linkUpgrade.Visible = True
                'Else
                '    linkUpgrade.Visible = False
                'End If

                If UCase(LoginFrom) = "UPGRADE" Then
                    linkUpgrade.Visible = False
                End If


            Else
                hdRoomNo.Value = "___"
                hdRegPg.Value = Request("OS")
                hdMAC.Value = Request("MA")
                userContext = New UserContext(hdMAC.Value, hdMAC.Value)
            End If


            If UCase(objSysConfig.GetConfig("IsEnablePrinter")) = "YES" Then
                Hlinkprinter1.Visible = True
                Hlinkprinter2.Visible = True
                lblprinter.Visible = True
                Hlinkprinter1.NavigateUrl = objSysConfig.GetConfig("PrinterURL")
                Hlinkprinter2.NavigateUrl = objSysConfig.GetConfig("PrinterURL")
            Else
                Hlinkprinter1.Visible = False
                Hlinkprinter2.Visible = False
                lblprinter.Visible = False
            End If

            If UCase(objSysConfig.GetConfig("IsEnableFeedback")) = "YES" Then
                Hyperfeedback1.Visible = True
                Hyperfeedback2.Visible = True
                lblfeedback.Visible = True
                fb.Visible = True
                Hyperfeedback1.NavigateUrl = "feedback.aspx?encry=" & url
                Hyperfeedback2.NavigateUrl = "feedback.aspx?encry=" & url
            Else
                Hyperfeedback1.Visible = False
                Hyperfeedback2.Visible = False
                lblfeedback.Visible = False
                fb.Visible = False
            End If

            HLinkvirus1.NavigateUrl = objSysConfig.GetConfig("VirusAdvisory")
            HLinkvirus2.NavigateUrl = objSysConfig.GetConfig("VirusAdvisory")

        End If



        If UCase(LoginFrom) = "UPGRADE" Then

            Dim ObjLog As LogInOutService
            ObjLog = LogInOutService.getInstance
            Plantime = ObjLog.FetchRTForGuest(hdgrcid.Value)
            hdMAC.Value = MA

        Else

            'Dim FreePlanId As String = objSysConfig.GetConfig("FreePlanId")
            Dim AllFreeplan As String = objSysConfig.GetConfig("AllFreePlanId")

            Dim objLogInOut As LogInOutService
            objLogInOut = LogInOutService.getInstance
            If objLogInOut.isFreePlan(AllFreeplan, UsrCred.Item("selectedPlanId")) Or objLogInOut.isFreePlan(AllFreeplan, UsrCred.Item("planid")) Then

                Dim ObjPlan As New CPlan
                If UsrCred.Item("accessgrant") Is Nothing Then

                    '------------- START display the upto checkout time, NOT for 24 hours -----------------
                    Dim IsFindGuestNoOfStay As String = Trim(objSysConfig.GetConfig("IsFindGuestNoOfStay"))
                    If UCase(IsFindGuestNoOfStay) = "YES" Then

                        '  Dim objLogInOut As LogInOutService
                        objLogInOut = LogInOutService.getInstance

                        Plantime = objLogInOut.FetchLoginPlanduration(UsrCred.Item("billid"))

                    Else
                        Dim Planid As String = UsrCred.Item("selectedPlanId")

                        If Planid <> 0 Then
                            ObjPlan.getPlaninfo(Planid)
                            Plantime = ObjPlan.planTime
                        End If
                    End If
                    '------------- END display the upto checkout time, NOT for 24 hours -----------------


                Else
                    Plantime = UsrCred.Item("remainingtime")
                End If

            Else
                gateWayFact = GatewayServiceFactory.getInstance
                gateWay = gateWayFact.getGatewayService("FIDELIO", "2.3.2")

                gatWayQryResult = gateWay.query(userContext)
                If UCase(gatWayQryResult.gtStatus) = "ERROR" Then
                    Plantime = 0
                Else
                    Plantime = gatWayQryResult.ndxExpireTime
                End If
            End If

            If UsrCred.Item("usertype") = EUSERTYPE.NR Then
                msg.Text = "Successfully Connected!!!"
                Hlinkprinter1.Visible = False
                Hlinkprinter2.Visible = False
                lblprinter.Visible = False
                Hyperfeedback1.Visible = False
                Hyperfeedback2.Visible = False
                lblfeedback.Visible = False
                fb.Visible = False
                HLinkvirus1.Visible = False
                HLinkvirus2.Visible = False
            End If
        End If

    End Sub

    Protected Sub linkUpgrade_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkUpgrade.Click

        Dim PMSName As PMSNAMES
        Dim PMSVersion As String

        Dim objSysConfig As New CSysConfig
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
        PMSVersion = objSysConfig.GetConfig("PMSVersion")

        Dim ObjElog As LoggerService
        ObjElog = LoggerService.gtInstance

        ObjElog.write2LogFile(hdRoomNo.Value, vbCrLf & Now() & " -- Click the Upgrade button in postlogin page")

        If hdRoomNo.Value <> "" Or hdLN.Value <> "" Then
            Dim userCrdential As New UserContext(hdRoomNo.Value, hdLN.Value, hdplanid.Value, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
            userCrdential.item("usertype") = EUSERTYPE.ROOM
            userCrdential.item("device") = "1"
            userCrdential.item("upgrade") = "1"

            Dim ObjUpgrade As New PlanUpgrade.Upgrade
            ObjUpgrade.PlanUpgrade(userCrdential)
        Else
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Credential empty.")
        End If

    End Sub
   
End Class