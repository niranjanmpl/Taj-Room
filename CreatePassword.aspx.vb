Imports PMSPkgV1_0
Imports security
Imports System.Threading

Public Class CreatePassword
    Inherits System.Web.UI.Page

    Dim strcmd As String
    Protected mainHTMLBody As New HtmlGenericControl
    Protected WithEvents DrpPlans As System.Web.UI.WebControls.DropDownList

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
    Dim T_devreachmsg As String = ""
    Protected WithEvents txtPassword As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtConfirmPassword As Global.System.Web.UI.WebControls.TextBox


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
            PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
            PMSVersion = objSysConfig.GetConfig("PMSVersion")
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
                    ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in CreatePassword Page" & vbCrLf)
                    Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
                End If


                Dim IsAvilSec As Boolean
                Dim objLogInOut As LogInOutService
                objLogInOut = LogInOutService.getInstance

                If PMSNAMES.AMADEUS = PMSName Then
                    IsAvilSec = objLogInOut.IsRegisteredUser_RNLN_FIAS_PopUP(UsrCred1.Item("roomNo"), UsrCred1.Item("guestname"))
                Else
                    IsAvilSec = objLogInOut.IsRegisteredUser_RNLN_PopUp(UsrCred1.Item("roomNo"), UsrCred1.Item("guestname"))
                End If

                If IsAvilSec = False Then
                    hdpopupconfirm.Value = "200"
                    '  ClientScript.RegisterStartupScript([GetType](), "id", "start()", False)
                End If

                If hdpopupconfirm.Value = "200" Then
                    Try


                        If Session("tajgues").ToString() = "5" Then
                            hdpopupconfirm.Value = "5"
                        Else
                            hdpopupconfirm.Value = "2"
                        End If
                    Catch ex As Exception
                        hdpopupconfirm.Value = "2"
                    End Try



                End If



                ObjElog = LoggerService.gtInstance
                If UsrCred1.Item("roomNo") <> "" And UsrCred1.Item("machineId") <> "" Then
                    ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: CreatePassword Page -- Room No:" & UsrCred1.Item("roomNo") & " -- MAC:" & UsrCred1.Item("machineId"))
                Else
                    ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: CreatePassword Page")
                End If

            End If


        Catch ex As ThreadAbortException

        Catch ex As Exception
            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.writeExceptionLogFile("Ex-CreatePassword", ex)

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
        Dim IsEnableMultiplePlan As String = UCase(objSysConfig.GetConfig("IsEnableMultiplePayPlan"))
        Dim objLogInOut As LogInOutService
        objLogInOut = LogInOutService.getInstance

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

            '--- After re-set the password.
        ElseIf UCase(userCrdential.item("tempdevreachmsg")) = "PREMIUM FREE OVER" Or UCase(userCrdential.item("tempdevreachmsg")) = "FREE OVER" Or UCase(userCrdential.item("tempdevreachmsg")) = "MAXDEVREACHED" Then


            '-------------- Here check Perday free allowed or not -----------


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

            ElseIf UCase(userCrdential.item("tempdevreachmsg")) = "PREMIUM FREE OVER" Then
                If userCrdential.Serialize(UniqueLogid) Then

                    If IsEnableMultiplePlan = "YES" Then
                        Response.Redirect("PremiumOverPayAll.aspx?encry=" & url)
                    Else
                        Response.Redirect("PremiumOver.aspx?encry=" & url)
                    End If

                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If

            ElseIf UCase(userCrdential.item("tempdevreachmsg")) = "MAXDEVREACHED" Then 'Maximum device limit reached, Show PlanSelection and call new login
                If userCrdential.Serialize(UniqueLogid) Then

                    If IsEnableMultiplePlan = "YES" Then
                        Response.Redirect("PremiumOverPayAll.aspx?encry=" & url)
                    Else
                        Response.Redirect("PremiumOver.aspx?encry=" & url)
                    End If
                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If


            ElseIf UCase(userCrdential.item("tempdevreachmsg")) = "FREE OVER" Then
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

        ElseIf UCase(output) = "PLEASE SELECT A PLAN" Then

            If userCrdential.Serialize(UniqueLogid) Then

                If IsEnableMultiplePlan = "YES" Then
                    Response.Redirect("PlanSelectionPayAll.aspx?encry=" & T_Url)
                Else
                    Response.Redirect("PlanSelection.aspx?encry=" & T_Url)
                End If

            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
            End If


        ElseIf UCase(output) = "PREMIUM FREE OVER" Or UCase(output) = "FREE OVER" Or UCase(output) = "MAXDEVREACHED" Then




            If objLogInOut.IsUsedPerdayFreePlan_Difffree(userCrdential) Then ' If Perday access avil go to new login, otherwise, premium plan

                userCrdential.item("devreachmsg") = "MAXDEVREACHED"

                If userCrdential.Serialize(UniqueLogid) Then

                    If IsEnableMultiplePlan = "YES" Then
                        Response.Redirect("PlanSelectionPayAll.aspx?encry=" & T_Url)
                    Else
                        Response.Redirect("PlanSelection.aspx?encry=" & T_Url)
                    End If
                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If

            ElseIf UCase(output) = "PREMIUM FREE OVER" Then
                If userCrdential.Serialize(UniqueLogid) Then

                    If IsEnableMultiplePlan = "YES" Then
                        Response.Redirect("PremiumOverPayAll.aspx?encry=" & T_Url)
                    Else
                        Response.Redirect("PremiumOver.aspx?encry=" & T_Url)
                    End If

                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If

            ElseIf UCase(output) = "MAXDEVREACHED" Then 'Maximum device limit reached, Show PlanSelection and call new login
                If userCrdential.Serialize(UniqueLogid) Then

                    If IsEnableMultiplePlan = "YES" Then
                        Response.Redirect("PremiumOverPayAll.aspx?encry=" & T_Url)
                    Else
                        Response.Redirect("PremiumOver.aspx?encry=" & T_Url)
                    End If
                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If


            ElseIf UCase(output) = "FREE OVER" Then
                If userCrdential.Serialize(UniqueLogid) Then

                    If IsEnableMultiplePlan = "YES" Then
                        Response.Redirect("PlanSelectionFreeOverPayAll.aspx?encry=" & T_Url)
                    Else
                        Response.Redirect("PlanSelectionFreeOver.aspx?encry=" & T_Url)
                    End If
                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                End If
            End If

        Else '------------Goes to Error page

            pgCookie.ResetCookie(HttpContext.Current.Response)


            If output = "" Or output = Nothing Then
                output = errInfo
            End If


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

    Private Sub MesgBox(ByVal sMessage As String)
        Dim msgedtble As String = sMessage.Replace("\", "\\")
        msgedtble = msgedtble.Replace(vbNewLine, "\n")
        Page.ClientScript.RegisterStartupScript(Me.GetType, "myScripts", "<script language='javascript'>alert('" & msgedtble & "');</script>")

    End Sub


    Protected Sub imgbtnlogin_Click(sender As Object, e As EventArgs) Handles imgbtnlogin.Click
        Dim Pwd As String = txtPassword.Text.Trim
        Dim ConfirmPwd As String = txtConfirmPassword.Text.Trim

        If Pwd.Length <= 4 Then
            MesgBox("Password should be minimum 5 characters long")
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

            If objLogInOut.CreateSecurePassword_RNLN(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), txtPassword.Text.Trim()) Then

                Dim userCrdential As New UserContext(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), UsrCred.Item("selectedPlanId"), PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                userCrdential.item("usertype") = EUSERTYPE.ROOM
                userCrdential.item("grcid") = UsrCred.Item("grcid")
                userCrdential.item("guestposition") = UsrCred.Item("guestposition")

                userCrdential.item("charge") = "YES"
                userCrdential.item("grcid") = UsrCred.Item("grcid")
                userCrdential.item("isdevalreadycalc") = "YES" ' no need to find dev count, avoid display "dev reached message"

                userCrdential.item("errorcode") = UsrCred.Item("errorcode")
                userCrdential.item("errormsg") = UsrCred.Item("errormsg")
                userCrdential.item("guestposition") = UsrCred.Item("guestposition")
                userCrdential.item("devreachmsg") = UsrCred.Item("devreachmsg")
                userCrdential.item("tempdevreachmsg") = UsrCred.Item("tempdevreachmsg")

                userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))
                userCrdential.item("billamount") = UsrCred.Item("billamount")
                userCrdential.item("noofdays") = UsrCred.Item("noofdays")
                login(userCrdential)

            Else
                Dim ObjElog As LoggerService
                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(UsrCred.Item("roomNo"), vbCrLf & Now() & " -- Already created Password.(Create Password)." & vbCrLf)
                HttpContext.Current.Response.Redirect("UserError.aspx?encry=" & url & "&findurl=mifilogin&ErrorCode=81")
            End If

        End If
    End Sub
End Class