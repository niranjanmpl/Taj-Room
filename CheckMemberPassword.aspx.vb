Imports PMSPkgV1_0
Imports security
Imports System.Threading
Public Class CheckMemberPassword
    Inherits System.Web.UI.Page

    Protected mainHTMLBody As New HtmlGenericControl
    Protected WithEvents DrpPlans As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtConfirmPassword As System.Web.UI.WebControls.TextBox

    Dim pgCookie As New CCookies
    Dim UniqueLogid As String = ""
    Dim devType As DEVICETYPE
    Dim userContext As UserContext
    Dim UsrCred As Hashtable = Nothing

    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private CouponVersion As String
    Private sql_query As String
    Public url As String
    Dim planId As Long

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
                    ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in CheckPassword Page" & vbCrLf)
                    Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
                End If


                ObjElog = LoggerService.gtInstance
                If UsrCred1.Item("roomNo") <> "" And UsrCred1.Item("machineId") <> "" Then
                    ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: CheckMemberPassword Page -- Member ID:" & UsrCred1.Item("roomNo") & " -- MAC:" & UsrCred1.Item("machineId"))
                Else
                    ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: CheckMemberPassword Page")
                End If

            End If


        Catch ex As ThreadAbortException

        Catch ex As Exception
            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.writeExceptionLogFile("Ex-CheckMemberPassword", ex)

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
            Response.Redirect("MifiLogin.aspx?encry=" & url)

        ElseIf UCase(output) = "PLEASE SELECT A PLAN" Then

            If userCrdential.Serialize(UniqueLogid) Then

                Response.Redirect("PlanSelectionPayAll.aspx?encry=" & T_Url)

            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
            End If


        ElseIf UCase(output) = "PREMIUM FREE OVER" Or UCase(output) = "FREE OVER" Or UCase(output) = "MAXDEVREACHED" Then


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

            ElseIf UCase(output) = "FREE OVER" Then

                If userCrdential.Serialize(UniqueLogid) Then
                    Response.Redirect("PlanSelectionFreeOverPayAll.aspx?encry=" & T_Url)
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
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=couponlogin&ErrorCode=4")

            ElseIf UCase(output) = "ROOM BLOCKED" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=couponlogin&ErrorCode=11")

            ElseIf UCase(output) = "CHECKOUT" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=couponlogin&ErrorCode=12")
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=couponlogin&ErrorCode=10")
            End If
        End If

    End Sub

    Protected Sub imgbtnlogin_Click(sender As Object, e As EventArgs) Handles imgbtnlogin.Click
        Dim Pwd As String = txtPassword.Text.Trim

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

            Dim IsValidSecurePwd As Boolean = False
            IsValidSecurePwd = objLogInOut.CheckSecurePassword_Member(UsrCred.Item("grcid"), txtPassword.Text.Trim())


            If IsValidSecurePwd Then
                Dim userCrdential As New UserContext(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), UsrCred.Item("selectedPlanId"), PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                userCrdential.item("usertype") = EUSERTYPE.ROOM
                userCrdential.item("grcid") = UsrCred.Item("grcid")
                userCrdential.item("guestposition") = UsrCred.Item("guestposition")

                userCrdential.item("charge") = "YES"

                userCrdential.item("errorcode") = UsrCred.Item("errorcode")
                userCrdential.item("errormsg") = UsrCred.Item("errormsg")
                userCrdential.item("guestposition") = UsrCred.Item("guestposition")
                '   userCrdential.item("devreachmsg") = UsrCred.Item("devreachmsg")
                userCrdential.item("loginfrom") = "checkpassword"
                userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

                login(userCrdential)

            Else

                Dim ObjElog As LoggerService
                Dim userCrdential1 As UserContext


                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(UsrCred.Item("roomNo"), Now() & " --  Entered Secure Password as : " & Pwd & vbCrLf)


                userCrdential1 = New UserContext(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                userCrdential1.item("usertype") = EUSERTYPE.ROOM
                userCrdential1.item("device") = "1"
                userCrdential1.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

                objLogInOut = LogInOutService.getInstance
                objLogInOut.loginfail(userCrdential1, "Invalid secure password")

                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(UsrCred.Item("roomNo"), vbCrLf & Now() & " -- Invalid secure password." & vbCrLf)
                HttpContext.Current.Response.Redirect("UserError.aspx?encry=" & url & "&findurl=checkpassword&ErrorCode=32")
            End If

        End If
    End Sub

End Class