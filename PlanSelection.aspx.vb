Imports PMSPkgV1_0
Imports security
Public Class PlanSelection
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
    Dim standardplan As String = ""
    Dim premiumplan As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here


        Dim objSysConfig As New CSysConfig
        Me.PageTitle.InnerText = objSysConfig.GetConfig("HotelName")


        Dim commonFun As PMSCommonFun
        Dim encrypt As New Datasealing

        commonFun = PMSCommonFun.getInstance
        url = Request.QueryString("encry")


        '----------------------START PMS Config --------------------------------------
        commonFun = PMSCommonFun.getInstance
        PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
        PMSVersion = objSysConfig.GetConfig("PMSVersion")
        '----------------------END PMS Config ----------------------------------------

        UniqueLogid = commonFun.DecrptQueryString("logid", url)

        If UniqueLogid = "" Or UniqueLogid = Nothing Then

            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Logid missed in PlanSelection" & vbCrLf)
            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
        End If



        If Not IsPostBack() Then

            Dim ObjElog As LoggerService
            '------------------- START De-Serialization --------------------------
            Dim UsrCred1 As Hashtable = Nothing
            userContext = New UserContext("", "")
            UsrCred1 = userContext.DeSerialize(UniqueLogid)
            '------------------- END De-Serialization --------------------------

            If IsNothing(UsrCred1) Then

                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in PlanSelection Page" & vbCrLf)
                Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
            End If


            ObjElog = LoggerService.gtInstance
            If UsrCred1.Item("roomNo") <> "" And UsrCred1.Item("machineId") <> "" Then
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: PlanSelection Page -- Room No:" & UsrCred1.Item("roomNo") & " -- MAC:" & UsrCred1.Item("machineId"))
            Else
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: PlanSelection Page")
            End If


            '--------- START get standard plan device limit from plans table ---------------
            standardplan = objSysConfig.GetConfig("FreePlanId")

            If standardplan <> "" Or standardplan <> Nothing Then
                Dim objPlan As New CPlan
                objPlan.getPlaninfo(standardplan, "", "")
                lblstandev.Text = objPlan.TotalConCurrentDev
                lblfreeplanname.Text = objPlan.PlanDescription
            End If

            '--------- END get standard plan device limit from plans table ----------------

            '--------- START get premium plan device limit from plans table ---------------
            premiumplan = objSysConfig.GetConfig("PremiumFreePlanId")

            If premiumplan <> "" Or premiumplan <> Nothing Then
                Dim objPlan As New CPlan
                objPlan.getPlaninfo(premiumplan, "", "")
                lblpremdev.Text = objPlan.TotalConCurrentDev
                lblplanname.Text = objPlan.PlanDescription
            End If

            '--------- END get premium plan device limit from plans table -----------------


            '---------------------------------------------------- START If request come from process page -------------
            ProcessType = Request.QueryString("ProcessType")
            If UCase(ProcessType) = UCase("PROCESS") Then
                Dim RoomNo, lastname, MAC As String

                '------------------- START De-Serialization --------------------------
                Dim UsrCred As Hashtable = Nothing
                userContext = New UserContext("", "")
                UsrCred = userContext.DeSerialize(UniqueLogid)
                '------------------- END De-Serialization ----------------------------

                If IsNothing(UsrCred) Then

                    ObjElog = LoggerService.gtInstance
                    ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in PlanSelection Process Page" & vbCrLf)
                    Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
                End If

                RoomNo = UsrCred.Item("roomNo")
                lastname = UsrCred.Item("guestname")
                MAC = UsrCred.Item("machineId")
                planId = Request.QueryString("plan")

                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(RoomNo, Now() & " -- START Login from Process" & vbCrLf _
                                                        & "----------------------- RoomNo: " & RoomNo & " -- Lastname: " & lastname & " -- MAC: " & MAC)



                If RoomNo <> "" And lastname <> "" And planId <> 0 Then
                    Dim userCrdential As New UserContext(RoomNo, lastname, planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                    userCrdential.item("usertype") = EUSERTYPE.ROOM
                    userCrdential.item("device") = "1"
                    userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))
                    login(userCrdential)
                Else
                    pgCookie.ResetCookie(HttpContext.Current.Response)
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If

            End If

            planId = -1
        Else
            If hdrdostatus.Value = "0" Then
                planId = objSysConfig.GetConfig("FreePlanId")
            ElseIf hdrdostatus.Value = "1" Then
                planId = objSysConfig.GetConfig("PremiumFreePlanId")
            Else
                planId = "-1"
            End If
        End If

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


      

        If UCase(ProcessOutput) = "ERROR" Then

            Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")

        ElseIf UCase(ProcessOutput) = "MIFILOGIN" Then
            HttpContext.Current.Response.Redirect("Process.aspx?encry=" & url & "&ProcessType=Mifilogin")

        ElseIf UCase(ProcessOutput) = "PROCESS" Then

            '---------------- START serialization ---------------
            If userCrdential.Serialize(UniqueLogid) Then
                HttpContext.Current.Response.Redirect("Process.aspx?encry=" & url & "&ProcessType=PlanSelection&Plan=" & planId)
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
            End If
            '---------------- END serialization ---------------

        ElseIf UCase(output) = "SUCCESS" Then
            '---------------- START serialization ---------------
            If userCrdential.Serialize(UniqueLogid) Then
                HttpContext.Current.Response.Redirect("UserInfo.aspx?encry=" & url)
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
            End If
            '---------------- END serialization ---------------


        ElseIf UCase(output) = "PREMIUM FREE OVER" Or UCase(output) = "FREE OVER" Or UCase(output) = "MAXDEVREACHED" Then

            Dim objSysConfig As New CSysConfig
            Dim IsEnableMultiplePlan As String = UCase(objSysConfig.GetConfig("IsEnableMultiplePayPlan"))

            '-------------- Here check Perday free allowed or not -----------
            Dim objLogInOut As LogInOutService
            objLogInOut = LogInOutService.getInstance

            If objLogInOut.IsUsedPerdayFreePlan_Difffree(userCrdential) Then ' If Perday access avil go to new login, otherwise, premium plan

                userCrdential.item("devreachmsg") = "MAXDEVREACHED"

                If userCrdential.Serialize(UniqueLogid) Then

                    If IsEnableMultiplePlan = "YES" Then
                        Response.Redirect("PlanSelectionPayAll.aspx?encry=" & url)
                    Else
                        Response.Redirect("PlanSelection.aspx?encry=" & url)
                    End If
                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If

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
            End If

        ElseIf UCase(output) = "COOKIE" Then
            pgCookie.ResetCookie(HttpContext.Current.Response)
            HttpContext.Current.Response.Redirect("mifilogin.aspx?encry=" & url)

        Else
            pgCookie.ResetCookie(HttpContext.Current.Response)
            Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
        End If

    End Sub

    'Protected Sub imgbtnlogin_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnlogin.Click

    '    Dim objSysConfig As New CSysConfig


    '    Dim commonFun As PMSCommonFun
    '    commonFun = PMSCommonFun.getInstance

    '    userContext = New UserContext("", "")
    '    UsrCred = userContext.DeSerialize(UniqueLogid)

    '    If IsNothing(UsrCred) Then

    '        Dim ObjElog As LoggerService
    '        ObjElog = LoggerService.gtInstance
    '        ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in PlanSelection button click" & vbCrLf)
    '        Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
    '    End If

    '    If UsrCred.Item("roomNo") <> "" And UsrCred.Item("guestname") <> "" Then

    '        Dim userCrdential As New UserContext(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
    '        userCrdential.item("usertype") = EUSERTYPE.ROOM
    '        userCrdential.item("device") = "1"
    '        userCrdential.item("charge") = "YES"

    '        userCrdential.item("errorcode") = UsrCred.Item("errorcode")
    '        userCrdential.item("errormsg") = UsrCred.Item("errormsg")
    '        userCrdential.item("guestposition") = UsrCred.Item("guestposition")
    '        userCrdential.item("devreachmsg") = UsrCred.Item("devreachmsg")

    '        If UsrCred.Item("devreachmsg") = "MAXDEVREACHED" Then
    '            userCrdential.item("charge") = "NO"
    '        End If

    '        userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))
    '        login(userCrdential)

    '    End If

    'End Sub

    Protected Sub imgbtnlogin_Click(sender As Object, e As EventArgs) Handles imgbtnlogin.Click
        Dim objSysConfig As New CSysConfig


        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance

        userContext = New UserContext("", "")
        UsrCred = userContext.DeSerialize(UniqueLogid)

        If IsNothing(UsrCred) Then

            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in PlanSelection button click" & vbCrLf)
            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
        End If

        If UsrCred.Item("roomNo") <> "" And UsrCred.Item("guestname") <> "" Then

            Dim userCrdential As New UserContext(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
            userCrdential.item("usertype") = EUSERTYPE.ROOM
            userCrdential.item("device") = "1"
            userCrdential.item("charge") = "YES"

            userCrdential.item("errorcode") = UsrCred.Item("errorcode")
            userCrdential.item("errormsg") = UsrCred.Item("errormsg")
            userCrdential.item("guestposition") = UsrCred.Item("guestposition")
            userCrdential.item("devreachmsg") = UsrCred.Item("devreachmsg")

            If UsrCred.Item("devreachmsg") = "MAXDEVREACHED" Then
                userCrdential.item("charge") = "NO"
            End If

            userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))
            login(userCrdential)

        End If

    End Sub
End Class