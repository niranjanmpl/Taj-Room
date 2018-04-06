Imports PMSPkgV1_0
Imports security
Imports System.Threading
Public Class CreateMemberPassword
    Inherits System.Web.UI.Page
    Dim strcmd As String
    Protected mainHTMLBody As New HtmlGenericControl
    Protected WithEvents DrpPlans As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents txtPassword As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtConfirmPassword As System.Web.UI.WebControls.TextBox

    Dim pgCookie As New CCookies
    Dim UniqueLogid As String = ""
    Dim devType As DEVICETYPE
    Dim userContext As UserContext
    Dim UsrCred As Hashtable = Nothing
    Dim UID As String
    Dim NSEID As String
    Dim encrypt As New Datasealing
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private CouponVersion As String
    Private sql_query As String
    Public url As String
    Dim planId As String
    Dim T_devreachmsg As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            'Put user code to initialize the page here
            Dim objSysConfig As New CSysConfig
            Me.PageTitle.InnerText = objSysConfig.GetConfig("HotelName")

            Dim commonFun As PMSCommonFun
            Dim userContext As UserContext

            commonFun = PMSCommonFun.getInstance
            url = Request.QueryString("encry")

            '----------------------START Get the PMSName and Version form "Config" Table --------------------------------------
            commonFun = PMSCommonFun.getInstance
            PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName1")))
            PMSVersion = objSysConfig.GetConfig("PMSVersion1")
            '----------------------END Get the PMSName and Version form "Config" Table --------------------------------------


            UID = commonFun.DecrptQueryString("UI", url)
            NSEID = encrypt.GetEncryptedData(UID)
            '-------- START Generate the Unique Id ----------
            UniqueLogid = commonFun.DecrptQueryString("logid", url)
            '-------- END Generate the Unique Id ----------

            If Not IsPostBack() Then


                Dim ObjElog As LoggerService
                '------------------- START De-Serialization --------------------------
                Dim UsrCred1 As Hashtable = Nothing
                userContext = New UserContext("", "")
                UsrCred1 = userContext.DeSerialize(UniqueLogid)
                '------------------- END De-Serialization --------------------------

                If IsNothing(UsrCred1) Then

                    ObjElog = LoggerService.gtInstance
                    ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in CreatePassword Page" & vbCrLf)
                    Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
                End If


                Dim IsAvilSec As Boolean
                Dim objLogInOut As LogInOutService
                objLogInOut = LogInOutService.getInstance

                If PMSNAMES.AMADEUSM = PMSName Then
                    IsAvilSec = objLogInOut.IsRegisteredUser_Member_PopUP(UsrCred1.Item("roomNo"), UsrCred1.Item("guestname"))
                Else
                    IsAvilSec = objLogInOut.IsRegisteredUser_RNLN_PopUp(UsrCred1.Item("roomNo"), UsrCred1.Item("guestname"))
                End If

                If IsAvilSec Then
                    hdpopupconfirm.Value = "2"
                    '  ClientScript.RegisterStartupScript([GetType](), "id", "start()", False)
                End If

                Session("guestid") = UsrCred1.Item("grcid")


                ObjElog = LoggerService.gtInstance
                If UsrCred1.Item("roomNo") <> "" And UsrCred1.Item("machineId") <> "" Then
                    ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: CreateMemberPassword Page -- Member Id:" & UsrCred1.Item("roomNo") & " -- MAC:" & UsrCred1.Item("machineId"))
                Else
                    ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: CreatePassword Page")
                End If

            End If


        Catch ex As ThreadAbortException

        Catch ex As Exception
            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.writeExceptionLogFile("Ex-CreateMemberPassword", ex)

            Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")

        End Try
    End Sub

    Private Sub login(ByRef userCrdential As UserContext)
        Dim AAA As AAAService
        Dim output As String = ""
        Dim ProcessOutput As String = ""
        Dim errInfo As String = ""

        AAA = AAAService.getInstance

        Dim ObjElog As LoggerService
        ObjElog = LoggerService.gtInstance

        Try

            output = AAA.AAA(userCrdential)
        Catch ex As Exception

            errInfo = "Error Message : " & ex.Message & vbCrLf & "Error Source : " & ex.Source & "  AAA-Exception" & vbCrLf & "Error Description : " & ex.StackTrace & vbCrLf & "Error Date: " & Now & "" & vbCrLf
            'check the InnerException
            While Not IsNothing(ex.InnerException)
                errInfo = errInfo & "-----The following InnerException reported: " + ex.InnerException.ToString() & vbCrLf
                ex = ex.InnerException
            End While
            ObjElog.write2LogFile("AAA-Exception", errInfo)

        End Try

        ObjElog = LoggerService.gtInstance
        ObjElog.write2LogFile(userCrdential.userId, Now() & " -- PMS Response: " & output)


        '---------------- START Here added the uniquelog id TO URL --------------
        Dim T_Url As String
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        T_Url = commonFun.DecrptQueryString_AllURL(url)
        T_Url = T_Url & "&logid=" & UniqueLogid

        T_Url = commonFun.EncrptQueryString_StringURL(T_Url)
        '---------------- END Here added the uniquelog id TO URL --------------


        Dim objSysConfig As New CSysConfig

        If UCase(output) = "SUCCESS" Then

            '---------------- START serialization ---------------
            If userCrdential.Serialize(UniqueLogid) Then
                Response.Redirect("UserInfo.aspx?encry=" & T_Url)
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
            End If
            '---------------- END serialization ---------------

        ElseIf UCase(output) = "COOKIE" Then
            pgCookie.ResetCookie(HttpContext.Current.Response)
            Response.Redirect("MemberLogin.aspx?encry=" & url)

            '--- After re-set the password.
        ElseIf UCase(userCrdential.item("tempdevreachmsg")) = "PREMIUM FREE OVER" Or UCase(userCrdential.item("tempdevreachmsg")) = "FREE OVER" Or UCase(userCrdential.item("tempdevreachmsg")) = "MAXDEVREACHED" Then


            If UCase(output) = "PREMIUM FREE OVER" Or UCase(output) = "MAXDEVREACHED" Then
                If userCrdential.Serialize(UniqueLogid) Then

                    Dim objPlan As New CPlan
                    If objPlan.IsDispPlanAftDevRec(userCrdential.item("planid")) = True Then

                        ObjElog = LoggerService.gtInstance
                        ObjElog.write2LogFile(userCrdential.roomNo, Now() & " -- Planid :" & userCrdential.item("planid") & " -- Enabled 'IsDispPlanAftDevRec' -- Redirect to PremiumOverPayAll Page" & vbCrLf)

                        Response.Redirect("PremiumOverPayAll.aspx?encry=" & T_Url)
                    Else
                        Response.Redirect("PlanSelectionExtraDev.aspx?encry=" & T_Url)
                    End If

                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If

            ElseIf UCase(userCrdential.item("tempdevreachmsg")) = "FREE OVER" Then

                If userCrdential.Serialize(UniqueLogid) Then
                    Response.Redirect("PlanSelectionFreeOverPayAll.aspx?encry=" & T_Url)
                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If

            End If

        ElseIf UCase(output) = "PLEASE SELECT A PLAN" Then

            Dim objLogInOut As LogInOutService
            objLogInOut = LogInOutService.getInstance
            If objLogInOut.IsUsedPerdayFreePlan(userCrdential) Then ' If Perday access avil go to new login, otherwise, premium plan

                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(userCrdential.roomNo, Now() & " -- Free available for a day. Display free and pay plans" & vbCrLf)

                If userCrdential.Serialize(UniqueLogid) Then
                    Response.Redirect("PlanSelectionPayAll.aspx?encry=" & T_Url)
                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If

            Else

                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(userCrdential.roomNo, Now() & " -- Free over for a day. redirect to PremiumOverPayAll" & vbCrLf)

                If userCrdential.Serialize(UniqueLogid) Then
                    Response.Redirect("PremiumOverPayAll.aspx?encry=" & T_Url)
                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If

            End If


        Else '------------Goes to Error page

            pgCookie.ResetCookie(HttpContext.Current.Response)


            If output = "" Or output = Nothing Then
                output = errInfo
            End If

            Dim objLogInOut As LogInOutService
            objLogInOut = LogInOutService.getInstance
            objLogInOut.loginfail(userCrdential, output)

            If UCase(output) = "INVALID CREDENTIAL" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=memberlogin&ErrorCode=114")

            ElseIf UCase(output) = "ROOM BLOCKED" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=memberlogin&ErrorCode=11")

            ElseIf UCase(output) = "CHECKOUT" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=memberlogin&ErrorCode=12")
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=memberlogin&ErrorCode=10")
            End If

        End If

    End Sub

    Private Sub MesgBox(ByVal sMessage As String)
        Dim msgedtble As String = sMessage.Replace("\", "\\")
        msgedtble = msgedtble.Replace(vbNewLine, "\n")
        Page.ClientScript.RegisterStartupScript(Me.GetType, "myScripts", "<script language='javascript'>alert('" & msgedtble & "');</script>")

    End Sub

    Protected Sub imgbtnlogin_Click(sender As Object, e As EventArgs) Handles imgbtnlogin.Click
        Dim Pwd As String = txtPassword.Text.Trim
        Dim ConfirmPwd As String = txtConfirmPassword.Text.Trim
        
        If Pwd.Length <= 7 Then
            MesgBox("Password should be minimum 8 characters long")
            txtPassword.Focus()
            Exit Sub
        End If

        If Pwd <> ConfirmPwd Then
            MesgBox("Confirm password not macth")
            txtConfirmPassword.Focus()
            Exit Sub
        End If

        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance

        userContext = New UserContext("", "")
        UsrCred = userContext.DeSerialize(UniqueLogid)

        If IsNothing(UsrCred) Then

            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in CreatePassword button click" & vbCrLf)

            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
        End If

        If UsrCred.Item("roomNo") <> "" And UsrCred.Item("guestname") <> "" Then

            Dim objLogInOut As LogInOutService
            objLogInOut = LogInOutService.getInstance


            If objLogInOut.CreateSecurePassword_Member(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), txtPassword.Text.Trim(), UsrCred.Item("machineId"), UID) Then

                Dim userCrdential As New UserContext(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), UsrCred.Item("selectedPlanId"), PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                userCrdential.item("usertype") = EUSERTYPE.ROOM
                userCrdential.item("grcid") = UsrCred.Item("grcid")
                userCrdential.item("guestposition") = UsrCred.Item("guestposition")

                userCrdential.item("charge") = "YES"

                userCrdential.item("errorcode") = UsrCred.Item("errorcode")
                userCrdential.item("errormsg") = UsrCred.Item("errormsg")
                userCrdential.item("guestposition") = UsrCred.Item("guestposition")
                userCrdential.item("devreachmsg") = UsrCred.Item("devreachmsg")
                userCrdential.item("tempdevreachmsg") = UsrCred.Item("tempdevreachmsg")
                userCrdential.item("loginfrom") = "createpassword"
                userCrdential.item("nseid") = UID
                userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

                login(userCrdential)

            Else
                Dim ObjElog As LoggerService
                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(UsrCred.Item("roomNo"), vbCrLf & Now() & " -- Already created Password.(Create Password)." & vbCrLf)
                HttpContext.Current.Response.Redirect("UserError.aspx?encry=" & url & "&findurl=MemberLogin&ErrorCode=81")
            End If

        
        End If
    End Sub
End Class