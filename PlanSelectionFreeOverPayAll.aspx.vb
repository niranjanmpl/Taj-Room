Imports PMSPkgV1_0
Imports security

Public Class PlanSelectionFreeOverPayAll
    Inherits System.Web.UI.Page
    Protected mainHTMLBody As New HtmlGenericControl
    Dim pgCookie As New CCookies
    Protected WithEvents btnlogin As System.Web.UI.WebControls.ImageButton
    Public url As String
    Private planId As Long
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private CouponVersion As String
    Dim getplanid As String
    Dim userContext As UserContext
    Dim UsrCred As Hashtable = Nothing
    Dim IsUsedFree As Boolean = False
    Dim MAC, OS As String
    Dim ProcessType As String
    Dim UniqueLogid As String
    Dim premiumplan As String = ""
    Dim standardplan As String = ""
    Public LSGPlanId As String = "-1"
    Public LsgPlanAmount As Double
    Public Discountlst15, Discountgrt15 As Double
    Dim Currency, noofdays As String
    Public Stax, Ltax As Double
    Public Ratelst7, Ratelst30, Rategrt30 As Double
    Dim redirect_url As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here

        Dim objSysConfig As New CSysConfig
        Me.PageTitle.InnerText = objSysConfig.GetConfig("HotelName")

        Dim objPlan As New CPlan
        Dim allPlans As DataSet
        Dim commonFun As PMSCommonFun
        Dim encrypt As New Datasealing

        Dim planName, planamount As String

        commonFun = PMSCommonFun.getInstance
        url = Request.QueryString("encry")

        cmbplans.Attributes.Add("onChange", "displayplandetails(this.value,'plan')")

        '----------------------START PMS Config --------------------------------------
        commonFun = PMSCommonFun.getInstance
        PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
        PMSVersion = objSysConfig.GetConfig("PMSVersion")
        '----------------------END PMS Config ----------------------------------------

        UniqueLogid = commonFun.DecrptQueryString("logid", url)

        If UniqueLogid = "" Or UniqueLogid = Nothing Then

            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Logid missed" & vbCrLf)
            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
        End If


        Dim IsEnableLongStay As String = objSysConfig.GetConfig("IsEnableLongStay")
        Dim IsLSGNewRate As String = objSysConfig.GetConfig("IsLSGNewRate")
        LSGPlanId = objSysConfig.GetConfig("LongStayPlanId")

        If UCase(IsEnableLongStay) = "YES" Then
            '
            Dim T_LsgPlanId As String = objSysConfig.GetConfig("LongStayPlanId")
            '--------- START get LSG Amount ---------------

            If T_LsgPlanId <> "" Or T_LsgPlanId <> Nothing Then
                objPlan.getPlaninfo(T_LsgPlanId, "", "")
                LsgPlanAmount = objPlan.planAmount

                If LsgPlanAmount = "0.0" Then
                    Dim ObjElog As LoggerService
                    ObjElog = LoggerService.gtInstance
                    ObjElog.write2LogFile("LongStayPlan_Error", vbCrLf & Now() & " --  Long Stay Plan Amount is ZERO" & vbCrLf)
                    Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
                End If
            Else
                Dim ObjElog As LoggerService
                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile("LongStayPlan_Error", vbCrLf & Now() & " -- Long Stay Planid Not Configured" & vbCrLf)
                Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
            End If
            '--------- START  get LSG Amount ---------------

            noofdays = objSysConfig.GetConfig("Lonstaylimit")

            If UCase(IsLSGNewRate) = "YES" Then
                Discountlst15 = objSysConfig.GetConfig("Discount<15")
                Discountgrt15 = objSysConfig.GetConfig("Discount>15")
            Else
                Ratelst7 = objSysConfig.GetConfig("Amount<7days")
                Ratelst30 = objSysConfig.GetConfig("Amount<30days")
                Rategrt30 = objSysConfig.GetConfig("Amount>30days")

                Stax = objSysConfig.GetConfig("ServiceTax")
                Ltax = objSysConfig.GetConfig("LuxuaryTax")
            End If

        End If

        If UCase(IsLSGNewRate) = "YES" Then
            txtLongStay.Attributes.Add("onkeyup", "amountNewRate(this.value);")
        Else
            txtLongStay.Attributes.Add("onkeyup", "amount(this.value);")
        End If

        If Not IsPostBack() Then



            '--------- START get standard plan device limit from plans table ---------------
            'standardplan = objSysConfig.GetConfig("FreePlanId")

            'If standardplan <> "" Or standardplan <> Nothing Then
            '    Dim objPlan As New CPlan
            '    objPlan.getPlaninfo(standardplan, "", "")
            '    lblstandev.Text = objPlan.TotalConCurrentDev
            'End If

            '--------- END get standard plan device limit from plans table ----------------

            '--------- START get premium plan device limit from plans table ---------------
            premiumplan = objSysConfig.GetConfig("PremiumFreePlanId")

            If premiumplan <> "" Or premiumplan <> Nothing Then
                objPlan.getPlaninfo(premiumplan, "", "")
                lblpremdev.Text = objPlan.TotalConCurrentDev
                'lblplanname.Text = objPlan.PlanDescription
            End If

            '--------- END get premium plan device limit from plans table -----------------

            Dim ObjElog As LoggerService
            '------------------- START De-Serialization --------------------------
            Dim UsrCred1 As Hashtable = Nothing
            userContext = New UserContext("", "")
            UsrCred1 = userContext.DeSerialize(UniqueLogid)
            '------------------- END De-Serialization --------------------------

            If IsNothing(UsrCred1) Then

                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in PlanSelectionFreeOverPayAll Page" & vbCrLf)
                Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
            End If


            ObjElog = LoggerService.gtInstance
            If UsrCred1.Item("roomNo") <> "" And UsrCred1.Item("machineId") <> "" Then
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: PlanSelectionFreeOverPayAll Page -- Room No:" & UsrCred1.Item("roomNo") & " -- MAC:" & UsrCred1.Item("machineId"))
            Else
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: PlanSelectionFreeOverPayAll Page")
            End If

            '---------------------------------------------------- END If request come from process page ---------------

            allPlans = objPlan.getAllPlans(PLANTYPES.ROOM, PLANSTATUS.ACTIVEONLY)

            For indx = 0 To allPlans.Tables(0).Rows.Count - 1
                planName = allPlans.Tables(0).Rows(indx).Item("PlanName")
                planId = allPlans.Tables(0).Rows(indx).Item("PlanId")
                planamount = allPlans.Tables(0).Rows(indx).Item("PlanAmount")
                cmbplans.Items.Add(New ListItem(planName, planId))
            Next

            '------------- START Enable Long stay Plan ----------------

            If UCase(IsEnableLongStay) = "YES" Then
                cmbplans.Items.Add(New ListItem("Long Stay", LSGPlanId))
            End If
            '------------- END Enable Long stay Plan ----------------

            cmbplans.SelectedIndex = 0
            planId = -1
        Else
            If cmbplans.SelectedValue = "" Then
                planId = -1
            Else
                If hdrdostatus.Value = "1" Then
                    planId = objSysConfig.GetConfig("FreePlanId")
                Else
                    planId = cmbplans.SelectedValue
                End If

            End If
        End If

        mainHTMLBody.Attributes.Add("onLoad", "displayplandetails('" & planId & "', 'body')")

    End Sub

    Private Sub login(ByRef userCrdential As UserContext)
        Dim AAA As AAAService
        Dim output As String = ""
        Dim ProcessOutput As String = ""

        AAA = AAAService.getInstance

        Dim ObjElog As LoggerService
        ObjElog = LoggerService.gtInstance

        Try

            '----------- START Avoid parallel login for restrict double posting ------------------------
            Dim FlagErr As String = "-1"
            Dim ProcessObj As ProcessService

            ProcessObj = ProcessService.gtInstance
            FlagErr = ProcessObj.AvoidParallelProcess(userCrdential.roomNo, userCrdential.guestName, userCrdential.machineId)
            '----------- END Avoid parallel login for restrict double posting --------------------------

            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile(userCrdential.userId, Now() & " -- Process Return code: " & FlagErr & " -- MAC: " & userCrdential.machineId)

            If FlagErr = "0" Then

                output = AAA.AAA(userCrdential)

                ProcessObj = ProcessService.gtInstance
                ProcessObj.UpdateProcessTbl(userCrdential.roomNo, userCrdential.guestName, userCrdential.machineId)

            ElseIf FlagErr = "2" Then
                ProcessOutput = "MIFILOGIN"

            ElseIf FlagErr = "3" Then
                ProcessOutput = "PROCESS"

            Else
                ProcessOutput = "ERROR"
                ProcessObj = ProcessService.gtInstance
                ProcessObj.UpdateProcessTbl(userCrdential.roomNo, userCrdential.guestName, userCrdential.machineId)
            End If


        Catch ex As Exception
            Dim errInfo As String

            errInfo = "Error Message : " & ex.Message & vbCrLf & "Error Source : " & ex.Source & "  AAA-Exception" & vbCrLf & "Error Description : " & ex.StackTrace & vbCrLf & "Error Date: " & Now & "" & vbCrLf
            'check the InnerException
            While Not IsNothing(ex.InnerException)
                errInfo = errInfo & "-----The following InnerException reported: " + ex.InnerException.ToString() & vbCrLf
                ex = ex.InnerException
            End While
            ObjElog.write2LogFile("AAA-Exception", errInfo)

            Dim ProcessObj As ProcessService
            ProcessObj = ProcessService.gtInstance
            ProcessObj.UpdateProcessTbl(userCrdential.roomNo, userCrdential.guestName, userCrdential.machineId)
            ProcessOutput = "ERROR"
        End Try

        ObjElog = LoggerService.gtInstance
        ObjElog.write2LogFile(userCrdential.userId, Now() & " -- PMS Response: " & output)


        Dim objSysConfig As New CSysConfig
        Dim IsEnableMultiplePlan As String = UCase(objSysConfig.GetConfig("IsEnableMultiplePayPlan"))

        If UCase(ProcessOutput) = "ERROR" Then

            Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")

        ElseIf UCase(ProcessOutput) = "MIFILOGIN" Then
            HttpContext.Current.Response.Redirect("Process.aspx?encry=" & url & "&ProcessType=Mifilogin")

        ElseIf UCase(ProcessOutput) = "PROCESS" Then

            '---------------- START serialization ---------------
            If userCrdential.Serialize(UniqueLogid) Then
                Response.Redirect("Process.aspx?encry=" & url & "&ProcessType=PlanSelection&Plan=" & planId)
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
            End If
            '---------------- END serialization ---------------

        ElseIf UCase(output) = "SUCCESS" Then

            ''---------------- START Here added the uniquelog id TO URL --------------
            'Dim T_Url As String
            'Dim commonFun As PMSCommonFun
            'commonFun = PMSCommonFun.getInstance
            'T_Url = commonFun.DecrptQueryString_AllURL(url)
            'T_Url = T_Url & "&LoginFrom=AutoUpgrade"

            'T_Url = commonFun.EncrptQueryString_StringURL(T_Url)
            ''---------------- END Here added the uniquelog id TO URL --------------

            ''----------- START Auto Upgrade ----------------------------
            'Dim objLogInOut As LogInOutService
            'objLogInOut = LogInOutService.getInstance
            'objLogInOut.AutoUpgrade(userCrdential)
            ''----------- END Auto Upgrade ----------------------------

            '---------------- START serialization ---------------
            If userCrdential.Serialize(UniqueLogid) Then
                Response.Redirect("UserInfo.aspx?encry=" & url)
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
            End If
            '---------------- END serialization ---------------

        ElseIf UCase(output) = "PREMIUM FREE OVER" Then
            If userCrdential.Serialize(UniqueLogid) Then

                If IsEnableMultiplePlan = "YES" Then
                    Response.Redirect("PremiumOverPayAll.aspx?encry=" & url)
                Else
                    Response.Redirect("PremiumOver.aspx?encry=" & url)
                End If

            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
            End If

        ElseIf UCase(output) = "MAXDEVREACHED" Then 'Maximum device limit reached, Show PlanSelection and call new login
            If userCrdential.Serialize(UniqueLogid) Then

                If IsEnableMultiplePlan = "YES" Then
                    Response.Redirect("PremiumOverPayAll.aspx?encry=" & url)
                Else
                    Response.Redirect("PremiumOver.aspx?encry=" & url)
                End If
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
            End If


        ElseIf UCase(output) = "FREE OVER" Then
            If userCrdential.Serialize(UniqueLogid) Then

                If IsEnableMultiplePlan = "YES" Then
                    Response.Redirect("PlanSelectionFreeOverPayAll.aspx?encry=" & url)
                Else
                    Response.Redirect("PlanSelectionFreeOver.aspx?encry=" & url)
                End If
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
            End If

        ElseIf UCase(output) = "PLEASE SELECT A PLAN" Then
            pgCookie.ResetCookie(HttpContext.Current.Response)
            Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=25")


        ElseIf UCase(output) = "COOKIE" Then
            pgCookie.ResetCookie(HttpContext.Current.Response)
            Response.Redirect("mifilogin.aspx?encry=" & url)

        Else
            pgCookie.ResetCookie(HttpContext.Current.Response)
            Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
        End If

    End Sub


    'Protected Sub imgbtnlogin_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnlogin.Click


    '    Dim objSysConfig As New CSysConfig
    '    Dim IsEnableMultiplePlan As String = UCase(objSysConfig.GetConfig("IsEnableMultiplePayPlan"))

    '    Dim Days As Integer = 0
    '    Dim day As String = ""
    '    Dim Rate As Double = 0.0
    '    Dim Discount As Double = 0.0
    '    Dim totamt As Double = 0.0

    '    LSGPlanId = objSysConfig.GetConfig("LongStayPlanId")
    '    Dim IsLSGNewRate As String = objSysConfig.GetConfig("IsLSGNewRate")

    '    '--------- START get LSG Plan --------------------------------------------------
    '    If planId.ToString = LSGPlanId Then

    '        Dim ObjElog As LoggerService
    '        Dim T_LsgPlanId As String = objSysConfig.GetConfig("LongStayPlanId")

    '        Dim objPlan As New CPlan
    '        If T_LsgPlanId <> "" Or T_LsgPlanId <> Nothing Then
    '            objPlan.getPlaninfo(T_LsgPlanId, "", "")
    '            LsgPlanAmount = objPlan.planAmount

    '            If LsgPlanAmount = "0.0" Then
    '                ObjElog = LoggerService.gtInstance
    '                ObjElog.write2LogFile("LongStayPlan_Error", vbCrLf & Now() & " -- Long Stay Planid Not Configured" & vbCrLf)
    '                Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
    '            End If
    '        Else
    '            ObjElog = LoggerService.gtInstance
    '            ObjElog.write2LogFile("LongStayPlan_Error", vbCrLf & Now() & " -- Long Stay Planid Not Configured" & vbCrLf)
    '            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
    '        End If


    '        day = txtLongStay.Text

    '        If noofdays <> 0 Or noofdays <> Nothing Then
    '            If Convert.ToInt32(day) > Convert.ToInt32(noofdays) Then
    '                Response.Redirect("usererror.aspx?encry=" & url & "&Errid=111&errcode=&logintype=mifilongstay&days=" & noofdays)
    '                Exit Sub
    '            End If

    '            If day = " " Then
    '                Response.Redirect("usererror.aspx?encry=" & url & "&Errid=108&errcode=&errmsg=&logintype=mifilongstay")
    '                Exit Sub
    '            End If
    '        End If

    '        Days = day

    '        If Days < 7 Then
    '            Response.Redirect("usererror.aspx?encry=" & url & "&Errid=108&errcode=&errmsg=&logintype=mifilongstay")
    '            Exit Sub
    '        End If

    '        If UCase(IsLSGNewRate) = "YES" Then
    '            If Days >= 7 And Days <= 15 Then


    '                Rate = FormatNumber(Days * LsgPlanAmount, 2, , , TriState.False)
    '                Discount = Rate * Discountlst15 / 100

    '                totamt = Rate - Discount
    '                totamt = FormatAmount(totamt)
    '            ElseIf Days >= 15 And Days < 30 Then


    '                Rate = FormatNumber(Days * LsgPlanAmount, 2, , , TriState.False)
    '                Discount = Rate * Discountgrt15 / 100

    '                totamt = Rate - Discount
    '                totamt = FormatAmount(totamt)
    '            ElseIf Days >= 30 Then

    '                Rate = FormatNumber(Days * LsgPlanAmount, 2, , , TriState.False)
    '                Discount = Rate * Discountgrt15 / 100

    '                totamt = Rate - Discount
    '                totamt = FormatAmount(totamt)

    '            End If

    '        Else

    '            Dim amt As Integer

    '              If Days >= 7 And Days < 15 Then
    '                amt = objSysConfig.GetConfig("Amount<7days")
    '                totamt = FormatNumber(Days * amt / 7, 2, , , TriState.False)

    '            ElseIf Days >= 15 And Days < 30 Then
    '                amt = objSysConfig.GetConfig("Amount<30days")
    '                totamt = FormatNumber(Days * amt / 15, 2, , , TriState.False)

    '            ElseIf Days >= 30 Then
    '                amt = objSysConfig.GetConfig("Amount>30days")
    '                totamt = FormatNumber(Days * amt / 30, 2, , , TriState.False)
    '            End If

    '        End If



    '        Dim commonFun As PMSCommonFun
    '        commonFun = PMSCommonFun.getInstance
    '        Dim UsrCred As Hashtable = Nothing
    '        userContext = New UserContext("", "")
    '        UsrCred = userContext.DeSerialize(UniqueLogid)

    '        '----------------------START PMS Config --------------------------------------
    '        commonFun = PMSCommonFun.getInstance
    '        PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
    '        PMSVersion = objSysConfig.GetConfig("PMSVersion")
    '        Currency = objSysConfig.GetConfig("Currency")
    '        Dim LSGPlanId As Long = objSysConfig.GetConfig("LongStayPlanId")
    '        '----------------------END PMS Config ----------------------------------------

    '        If UCase(IsLSGNewRate) = "YES" Then

    '            ObjElog = LoggerService.gtInstance
    '            ObjElog.write2LogFile(UsrCred.Item("roomNo"), Now() & " -- Selected days: " & Days & vbCrLf _
    '                                                    & "------------------------- Amount: " & Rate _
    '                                                    & "------------------------- Discount: " & Discount _
    '                                                    & "------------------------- TotAmt: " & totamt & vbCrLf)
    '        Else

    '            ObjElog = LoggerService.gtInstance
    '            ObjElog.write2LogFile(UsrCred.Item("roomNo"), Now() & " -- Selected days: " & Days & vbCrLf _
    '                                                   & "----------------------- Amount: " & totamt & vbCrLf)
    '        End If

    '        If UsrCred.Item("roomNo") <> "" And UsrCred.Item("guestname") <> "" Then
    '            Dim userCrdential As New UserContext(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), LSGPlanId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
    '            userCrdential.item("usertype") = EUSERTYPE.ROOM
    '            userCrdential.item("device") = "1"
    '            userCrdential.item("noofdays") = day
    '            userCrdential.item("billamount") = totamt
    '            userCrdential.item("vlanid") = UsrCred.Item("vlanid")
    '            userCrdential.item("charge") = "YES"

    '            userCrdential.item("grcid") = UsrCred.Item("grcid")

    '            userCrdential.item("errorcode") = UsrCred.Item("errorcode")
    '            userCrdential.item("errormsg") = UsrCred.Item("errormsg")
    '            userCrdential.item("guestposition") = UsrCred.Item("guestposition")
    '            userCrdential.item("devreachmsg") = UsrCred.Item("devreachmsg")

    '            If UsrCred.Item("devreachmsg") = "MAXDEVREACHED" Then
    '                userCrdential.item("charge") = "NO"
    '            End If

    '            userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

    '            Dim IsAvilSec As Boolean

    '            Dim objLogInOut As LogInOutService
    '            objLogInOut = LogInOutService.getInstance

    '            If PMSNAMES.AMADEUS = PMSName Then
    '                IsAvilSec = objLogInOut.IsRegisteredUser_RNLN_FIAS(userCrdential.roomNo, userCrdential.guestName)
    '            Else
    '                IsAvilSec = objLogInOut.IsRegisteredUser_RNLN(userCrdential.roomNo, userCrdential.guestName)
    '            End If


    '            Dim T_Url As String
    '            commonFun = PMSCommonFun.getInstance
    '            T_Url = commonFun.DecrptQueryString_AllURL(url)
    '            T_Url = T_Url & "&LoginFrom=AutoUpgrade"

    '            T_Url = commonFun.EncrptQueryString_StringURL(T_Url)

    '            If IsAvilSec = False Then

    '                Dim TajwebsiteGuest As String = Trim(objSysConfig.GetConfig("BookedViaTajSite"))
    '                Dim TajTIC As String = Trim(objSysConfig.GetConfig("TICLevel"))

    '                If (UCase(userCrdential.item("compcode")) = UCase(TajwebsiteGuest)) Or objLogInOut.IsTICLevel(TajTIC, userCrdential.item("compcode")) Then
    '                    If userCrdential.Serialize(UniqueLogid) Then
    '                        Response.Redirect("CreatePassword.aspx?encry=" & T_Url)
    '                    End If

    '                Else
    '                    If userCrdential.Serialize(UniqueLogid) Then
    '                        Response.Redirect("CreatePasswordPopUp.aspx?encry=" & T_Url)
    '                    End If
    '                End If

    '            Else
    '                If userCrdential.Serialize(UniqueLogid) Then
    '                    Response.Redirect("CheckPassword.aspx?encry=" & T_Url)
    '                End If

    '            End If
    '            'login(userCrdential)
    '        End If
    '        '--------- END get LSG Plan --------------------------------------------------
    '    Else

    '        Dim commonFun As PMSCommonFun
    '        commonFun = PMSCommonFun.getInstance

    '        userContext = New UserContext("", "")
    '        UsrCred = userContext.DeSerialize(UniqueLogid)

    '        If IsNothing(UsrCred) Then

    '            Dim ObjElog As LoggerService
    '            ObjElog = LoggerService.gtInstance
    '            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in PlanSelectionFreeOverPayAll button click" & vbCrLf)
    '            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
    '        End If

    '        If UsrCred.Item("roomNo") <> "" And UsrCred.Item("guestname") <> "" Then
    '            Dim userCrdential As New UserContext(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
    '            userCrdential.item("usertype") = EUSERTYPE.ROOM
    '            userCrdential.item("device") = "1"
    '            userCrdential.item("charge") = "YES"
    '            userCrdential.item("extradevofday") = "1"

    '            userCrdential.item("grcid") = UsrCred.Item("grcid")

    '            userCrdential.item("errorcode") = UsrCred.Item("errorcode")
    '            userCrdential.item("errormsg") = UsrCred.Item("errormsg")
    '            userCrdential.item("guestposition") = UsrCred.Item("guestposition")
    '            userCrdential.item("devreachmsg") = UsrCred.Item("devreachmsg")

    '            If UsrCred.Item("devreachmsg") = "MAXDEVREACHED" Then
    '                userCrdential.item("charge") = "NO"
    '            End If

    '            userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

    '            Dim IsAvilSec As Boolean

    '            Dim objLogInOut As LogInOutService
    '            objLogInOut = LogInOutService.getInstance

    '            If PMSNAMES.AMADEUS = PMSName Then
    '                IsAvilSec = objLogInOut.IsRegisteredUser_RNLN_FIAS(userCrdential.roomNo, userCrdential.guestName)
    '            Else
    '                IsAvilSec = objLogInOut.IsRegisteredUser_RNLN(userCrdential.roomNo, userCrdential.guestName)
    '            End If

    '            Dim T_Url As String
    '            commonFun = PMSCommonFun.getInstance
    '            T_Url = commonFun.DecrptQueryString_AllURL(url)
    '            T_Url = T_Url & "&LoginFrom=AutoUpgrade"

    '            T_Url = commonFun.EncrptQueryString_StringURL(T_Url)

    '            If IsAvilSec = False Then

    '                Dim TajwebsiteGuest As String = Trim(objSysConfig.GetConfig("BookedViaTajSite"))
    '                Dim TajTIC As String = Trim(objSysConfig.GetConfig("TICLevel"))

    '                If (UCase(userCrdential.item("compcode")) = UCase(TajwebsiteGuest)) Or (UCase(userCrdential.item("compcode")) = UCase(TajTIC)) Then
    '                    If userCrdential.Serialize(UniqueLogid) Then
    '                        Response.Redirect("CreatePassword.aspx?encry=" & T_Url)
    '                    End If

    '                Else
    '                    If userCrdential.Serialize(UniqueLogid) Then
    '                        Response.Redirect("CreatePasswordPopUp.aspx?encry=" & T_Url)
    '                    End If
    '                End If

    '            Else
    '                If userCrdential.Serialize(UniqueLogid) Then
    '                    Response.Redirect("CheckPassword.aspx?encry=" & T_Url)
    '                End If

    '            End If

    '            ' login(userCrdential)
    '        End If


    '    End If

    'End Sub

    Function FormatAmount(ByVal Amount As Double) As String
        Return CStr(FormatNumber(Amount, 0, , , TriState.False))
    End Function

    Protected Sub imgbtnlogin_Click(sender As Object, e As EventArgs) Handles imgbtnlogin.Click
        Dim objSysConfig As New CSysConfig
        Dim IsEnableMultiplePlan As String = UCase(objSysConfig.GetConfig("IsEnableMultiplePayPlan"))

        Dim Days As Integer = 0
        Dim day As String = ""
        Dim Rate As Double = 0.0
        Dim Discount As Double = 0.0
        Dim totamt As Double = 0.0

        LSGPlanId = objSysConfig.GetConfig("LongStayPlanId")
        Dim IsLSGNewRate As String = objSysConfig.GetConfig("IsLSGNewRate")

        '--------- START get LSG Plan --------------------------------------------------
        If planId.ToString = LSGPlanId Then

            Dim ObjElog As LoggerService
            Dim T_LsgPlanId As String = objSysConfig.GetConfig("LongStayPlanId")

            Dim objPlan As New CPlan
            If T_LsgPlanId <> "" Or T_LsgPlanId <> Nothing Then
                objPlan.getPlaninfo(T_LsgPlanId, "", "")
                LsgPlanAmount = objPlan.planAmount

                If LsgPlanAmount = "0.0" Then
                    ObjElog = LoggerService.gtInstance
                    ObjElog.write2LogFile("LongStayPlan_Error", vbCrLf & Now() & " -- Long Stay Planid Not Configured" & vbCrLf)
                    Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
                End If
            Else
                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile("LongStayPlan_Error", vbCrLf & Now() & " -- Long Stay Planid Not Configured" & vbCrLf)
                Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
            End If


            day = txtLongStay.Text

            If noofdays <> 0 Or noofdays <> Nothing Then
                If Convert.ToInt32(day) > Convert.ToInt32(noofdays) Then
                    Response.Redirect("usererror.aspx?encry=" & url & "&Errid=111&errcode=&logintype=mifilongstay&days=" & noofdays)
                    Exit Sub
                End If

                If day = " " Then
                    Response.Redirect("usererror.aspx?encry=" & url & "&Errid=108&errcode=&errmsg=&logintype=mifilongstay")
                    Exit Sub
                End If
            End If

            Days = day

            If Days < 7 Then
                Response.Redirect("usererror.aspx?encry=" & url & "&Errid=108&errcode=&errmsg=&logintype=mifilongstay")
                Exit Sub
            End If

            If UCase(IsLSGNewRate) = "YES" Then
                If Days >= 7 And Days <= 15 Then


                    Rate = FormatNumber(Days * LsgPlanAmount, 2, , , TriState.False)
                    Discount = Rate * Discountlst15 / 100

                    totamt = Rate - Discount
                    totamt = FormatAmount(totamt)
                ElseIf Days >= 15 And Days < 30 Then


                    Rate = FormatNumber(Days * LsgPlanAmount, 2, , , TriState.False)
                    Discount = Rate * Discountgrt15 / 100

                    totamt = Rate - Discount
                    totamt = FormatAmount(totamt)
                ElseIf Days >= 30 Then

                    Rate = FormatNumber(Days * LsgPlanAmount, 2, , , TriState.False)
                    Discount = Rate * Discountgrt15 / 100

                    totamt = Rate - Discount
                    totamt = FormatAmount(totamt)

                End If

            Else

                Dim amt As Integer

                If Days >= 7 And Days < 15 Then
                    amt = objSysConfig.GetConfig("Amount<7days")
                    totamt = FormatNumber(Days * amt / 7, 2, , , TriState.False)

                ElseIf Days >= 15 And Days < 30 Then
                    amt = objSysConfig.GetConfig("Amount<30days")
                    totamt = FormatNumber(Days * amt / 15, 2, , , TriState.False)

                ElseIf Days >= 30 Then
                    amt = objSysConfig.GetConfig("Amount>30days")
                    totamt = FormatNumber(Days * amt / 30, 2, , , TriState.False)
                End If

            End If



            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            Dim UsrCred As Hashtable = Nothing
            userContext = New UserContext("", "")
            UsrCred = userContext.DeSerialize(UniqueLogid)

            '----------------------START PMS Config --------------------------------------
            commonFun = PMSCommonFun.getInstance
            PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
            PMSVersion = objSysConfig.GetConfig("PMSVersion")
            Currency = objSysConfig.GetConfig("Currency")
            Dim LSGPlanId As Long = objSysConfig.GetConfig("LongStayPlanId")
            '----------------------END PMS Config ----------------------------------------

            If UCase(IsLSGNewRate) = "YES" Then

                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(UsrCred.Item("roomNo"), Now() & " -- Selected days: " & Days & vbCrLf _
                                                        & "------------------------- Amount: " & Rate _
                                                        & "------------------------- Discount: " & Discount _
                                                        & "------------------------- TotAmt: " & totamt & vbCrLf)
            Else

                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(UsrCred.Item("roomNo"), Now() & " -- Selected days: " & Days & vbCrLf _
                                                       & "----------------------- Amount: " & totamt & vbCrLf)
            End If

            If UsrCred.Item("roomNo") <> "" And UsrCred.Item("guestname") <> "" Then
                Dim userCrdential As New UserContext(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), LSGPlanId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                userCrdential.item("usertype") = EUSERTYPE.ROOM
                userCrdential.item("device") = "1"
                userCrdential.item("noofdays") = day
                userCrdential.item("billamount") = totamt
                userCrdential.item("vlanid") = UsrCred.Item("vlanid")
                userCrdential.item("charge") = "YES"

                userCrdential.item("grcid") = UsrCred.Item("grcid")

                userCrdential.item("errorcode") = UsrCred.Item("errorcode")
                userCrdential.item("errormsg") = UsrCred.Item("errormsg")
                userCrdential.item("guestposition") = UsrCred.Item("guestposition")
                userCrdential.item("devreachmsg") = UsrCred.Item("devreachmsg")

                If UsrCred.Item("devreachmsg") = "MAXDEVREACHED" Then
                    userCrdential.item("charge") = "NO"
                End If

                userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

                Dim IsAvilSec As Boolean

                Dim objLogInOut As LogInOutService
                objLogInOut = LogInOutService.getInstance

                If PMSNAMES.AMADEUS = PMSName Then
                    IsAvilSec = objLogInOut.IsRegisteredUser_RNLN_FIAS(userCrdential.roomNo, userCrdential.guestName)
                Else
                    IsAvilSec = objLogInOut.IsRegisteredUser_RNLN(userCrdential.roomNo, userCrdential.guestName)
                End If


                Dim T_Url As String
                commonFun = PMSCommonFun.getInstance
                T_Url = commonFun.DecrptQueryString_AllURL(url)
                T_Url = T_Url & "&LoginFrom=AutoUpgrade"

                T_Url = commonFun.EncrptQueryString_StringURL(T_Url)

                If IsAvilSec = False Then

                    Dim TajwebsiteGuest As String = Trim(objSysConfig.GetConfig("BookedViaTajSite"))
                    Dim TajTIC As String = Trim(objSysConfig.GetConfig("TICLevel"))

                    If (UCase(userCrdential.item("compcode")) = UCase(TajwebsiteGuest)) Or objLogInOut.IsTICLevel(TajTIC, userCrdential.item("compcode")) Then
                        If userCrdential.Serialize(UniqueLogid) Then
                            Response.Redirect("CreatePassword.aspx?encry=" & T_Url)
                        End If

                    Else
                        If userCrdential.Serialize(UniqueLogid) Then
                            Response.Redirect("CreatePasswordPopUp.aspx?encry=" & T_Url)
                        End If
                    End If

                Else
                    If userCrdential.Serialize(UniqueLogid) Then
                        Response.Redirect("CheckPassword.aspx?encry=" & T_Url)
                    End If

                End If
                'login(userCrdential)
            End If
            '--------- END get LSG Plan --------------------------------------------------
        Else

            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance

            userContext = New UserContext("", "")
            UsrCred = userContext.DeSerialize(UniqueLogid)

            If IsNothing(UsrCred) Then

                Dim ObjElog As LoggerService
                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in PlanSelectionFreeOverPayAll button click" & vbCrLf)
                Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
            End If

            If UsrCred.Item("roomNo") <> "" And UsrCred.Item("guestname") <> "" Then
                Dim userCrdential As New UserContext(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                userCrdential.item("usertype") = EUSERTYPE.ROOM
                userCrdential.item("device") = "1"
                userCrdential.item("charge") = "YES"
                userCrdential.item("extradevofday") = "1"

                userCrdential.item("grcid") = UsrCred.Item("grcid")

                userCrdential.item("errorcode") = UsrCred.Item("errorcode")
                userCrdential.item("errormsg") = UsrCred.Item("errormsg")
                userCrdential.item("guestposition") = UsrCred.Item("guestposition")
                userCrdential.item("devreachmsg") = UsrCred.Item("devreachmsg")

                If UsrCred.Item("devreachmsg") = "MAXDEVREACHED" Then
                    userCrdential.item("charge") = "NO"
                End If

                userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

                Dim IsAvilSec As Boolean

                Dim objLogInOut As LogInOutService
                objLogInOut = LogInOutService.getInstance

                If PMSNAMES.AMADEUS = PMSName Then
                    IsAvilSec = objLogInOut.IsRegisteredUser_RNLN_FIAS(userCrdential.roomNo, userCrdential.guestName)
                Else
                    IsAvilSec = objLogInOut.IsRegisteredUser_RNLN(userCrdential.roomNo, userCrdential.guestName)
                End If

                Dim T_Url As String
                commonFun = PMSCommonFun.getInstance
                T_Url = commonFun.DecrptQueryString_AllURL(url)
                T_Url = T_Url & "&LoginFrom=AutoUpgrade"

                T_Url = commonFun.EncrptQueryString_StringURL(T_Url)

                If IsAvilSec = False Then

                    Dim TajwebsiteGuest As String = Trim(objSysConfig.GetConfig("BookedViaTajSite"))
                    Dim TajTIC As String = Trim(objSysConfig.GetConfig("TICLevel"))

                    If (UCase(userCrdential.item("compcode")) = UCase(TajwebsiteGuest)) Or (UCase(userCrdential.item("compcode")) = UCase(TajTIC)) Then
                        If userCrdential.Serialize(UniqueLogid) Then
                            Response.Redirect("CreatePassword.aspx?encry=" & T_Url)
                        End If

                    Else
                        If userCrdential.Serialize(UniqueLogid) Then
                            Response.Redirect("CreatePasswordPopUp.aspx?encry=" & T_Url)
                        End If
                    End If

                Else
                    If userCrdential.Serialize(UniqueLogid) Then
                        Response.Redirect("CheckPassword.aspx?encry=" & T_Url)
                    End If

                End If

                ' login(userCrdential)
            End If


        End If

    End Sub
End Class