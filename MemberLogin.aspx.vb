Imports PMSPkgV1_0
Imports security
Imports System.Threading
Imports System.Security
Public Class MemberLogin
    Inherits System.Web.UI.Page
    Protected mainHTMLBody As New HtmlGenericControl
    'Protected WithEvents txtroomno As System.Web.UI.WebControls.TextBox
    'Protected pageTitle As New HtmlGenericControl
    Protected WithEvents btnlogin As System.Web.UI.WebControls.ImageButton
    Dim pgCookie As New CCookies
    Dim devType As DEVICETYPE
    Dim UID As String
    Dim NSEID As String
    Dim encrypt As New Datasealing
    Private planId As String
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private CouponVersion As String
    Public url As String
    Private accesstype As Integer
    Dim Recordno As Integer
    Dim MAC, OS, RN As String
    Dim UniqueLogid As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim objSysConfig As New CSysConfig
            Me.PageTitle.InnerText = objSysConfig.GetConfig("HotelName")

            Dim commonFun As PMSCommonFun
            Dim userContext As UserContext
            Dim userCredential As UserCredential

            
            Dim getplanid As String

            commonFun = PMSCommonFun.getInstance
            url = Request.QueryString("encry")

            '---------------------- START PMS Config --------------------------------------
            commonFun = PMSCommonFun.getInstance
            PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName1")))
            PMSVersion = objSysConfig.GetConfig("PMSVersion1")
            ' CouponVersion = objSysConfig.GetConfig("CouponVersion")
            ' currency = objSysConfig.GetConfig("Currency")
            '---------------------- END PMS Config ----------------------------------------
            Dim UI, UURL, MAC, SC, OS As String

            UI = commonFun.DecrptQueryString("UI", url)
            UURL = commonFun.DecrptQueryString("UURL", url)
            MAC = commonFun.DecrptQueryString("MA", url)
            SC = commonFun.DecrptQueryString("SC", url)
            OS = commonFun.DecrptQueryString("OS", url)
            RN = commonFun.DecrptQueryString("RN", url)

            UID = commonFun.DecrptQueryString("UI", url)
            NSEID = encrypt.GetEncryptedData(UID)
            '-------- START Generate the Unique Id ----------
            UniqueLogid = commonFun.GenerateUniqueId(MAC)
            '-------- END Generate the Unique Id ----------


            If Not IsPostBack() Then

                Dim All_URL As String = commonFun.DecrptQueryString_AllURL(url)

                devType = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

                Dim ObjElog As LoggerService
                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: MemberLogin page" & vbCrLf _
                                                    & "----------------------- Nomadix I/P: " & All_URL & vbCrLf _
                                                    & "----------------------- MAC: " & MAC & vbCrLf _
                                                    & "----------------------- Device Type: " & devType.ToString() & vbCrLf _
                                                    & "----------------------- LogId: " & UniqueLogid & vbCrLf _
                                                    & "----------------------- Idendified String: " & Request.ServerVariables("HTTP_USER_AGENT"))

                If UniqueLogid = "" Or UniqueLogid = Nothing Then
                    Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
                    ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Logid missed" & vbCrLf)
                End If


                If MAC = "" Then Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
                '------------ START IF BC MAC Avil redirect to Business center login page --------------
                Dim objGuest As GuestService
                objGuest = GuestService.getInstance
                Dim BCURL As String = objGuest.FetchBCMachineOS(MAC)
                If BCURL <> "NA" Then
                    Response.Redirect(BCURL & "?encry=" & url)
                End If
                '------------ END IF BC MAC Avil redirect to Business center login page ----------------

                '--------------------- Begin Check Nomadix ID (NASID) ---------------------
                NSEID = UI
                'Dim chknseid As String = objSysConfig.GetConfig("License_ID")
                'NSEID = encrypt.GetEncryptedData(NSEID)

                Dim objdbase As DbaseService = DbaseService.getInstance
                Dim query As String = "select * from clients where hostid='" & NSEID & "'"
                Dim ds As DataSet = objdbase.DsWithoutUpdate(query)
                If ds.Tables(0).Rows.Count > 0 Then
                    'Dim usercredential1 As UserContext
                    'usercredential1 = New UserContext("", "")
                    'usercredential1.item("nasip") = ds.Tables(0).Rows(0).Item("hotelnasip")
                Else
                    'If chknseid <> NSEID Then
                    Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=8")
                End If
                '--------------------- End Chkeck Nomadix ID (NASID) ---------------------

                '--------------------- Begin Cookies ---------------------
                Dim objGuestSrv As GuestService
                If pgCookie.ReadCookie(Request) Then

                    If pgCookie.expiryTime > Now() And pgCookie.userType = EUSERTYPE.ROOM Then
                        objGuestSrv = GuestService.getInstance
                        userCredential = objGuestSrv.getGuestInfo(pgCookie.grcId)
                        getplanid = objGuestSrv.GetPlanid(pgCookie.grcId)
                        userContext = New UserContext(userCredential, PMSName, PMSVersion, getplanid, "", "", HttpContext.Current.Request)
                        userContext.item("usertype") = EUSERTYPE.ROOM
                        userContext.item("serviceaccess") = pgCookie.serviceAccess
                        userContext.item("loginbycookie") = True
                        userContext.item("loginfrom") = "mifi"
                        userContext.item("cookiemac") = pgCookie.mac
                        userContext.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

                        ObjElog.write2LogFile(userCredential.usrId, Now() & "----- Found cookies" & vbCrLf _
                                      & "------------------------- Last Name: " & userCredential.passwd & vbCrLf _
                                      & "------------------------- Room No: " & userCredential.usrId & vbCrLf _
                                      & "------------------------- cookie MAC: " & pgCookie.mac)

                        login(userContext)

                    ElseIf pgCookie.expiryTime > Now() And pgCookie.userType = EUSERTYPE.COUPON Then
                        objGuestSrv = GuestService.getInstance
                        userCredential = objGuestSrv.getGuestInfo(pgCookie)
                        getplanid = objGuestSrv.GetPlanid(pgCookie.grcId)
                        userContext = New UserContext(userCredential, PMSNAMES.UNKNOWN, CouponVersion, getplanid, "", "", HttpContext.Current.Request)
                        userContext.item("usertype") = EUSERTYPE.COUPON
                        userContext.item("loginbycookie") = True
                        login(userContext)
                    End If
                End If
                '--------------------- End Cookies ---------------------
            End If

        Catch ex As ThreadAbortException

        Catch ex As Exception
            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.writeExceptionLogFile("Ex-MifiLoginPage", ex)

            Response.Redirect("UserError.aspx?encry=" & url & "&finderror=memberlogin&ErrorCode=10")

        End Try


    End Sub

    Private Sub login(ByRef userCrdential As UserContext)

        Dim errInfo As String = ""
        Dim AAA As AAAService
        Dim output As String = ""

        AAA = AAAService.getInstance

        Try
            output = AAA.AAA(userCrdential)

        Catch ex As Exception

            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance

            errInfo = "Error Message : " & ex.Message & vbCrLf & "Error Source : " & ex.Source & "  AAA-Exception" & vbCrLf & "Error Description : " & ex.StackTrace & vbCrLf & "Error Date: " & Now & "" & vbCrLf
            'check the InnerException
            While Not IsNothing(ex.InnerException)
                errInfo = errInfo & "-----The following InnerException reported: " + ex.InnerException.ToString() & vbCrLf
                ex = ex.InnerException
            End While
            ObjElog.write2LogFile("AAA-Exception", errInfo)
        End Try

        '---------------- START Here added the uniquelog id TO URL --------------
        Dim T_Url As String
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        T_Url = commonFun.DecrptQueryString_AllURL(url)
        T_Url = T_Url & "&logid=" & UniqueLogid

        T_Url = commonFun.EncrptQueryString_StringURL(T_Url)
        '---------------- END Here added the uniquelog id TO URL --------------


        Dim objSysConfig As New CSysConfig
        Dim IsEnableSecureLogin As String = UCase(objSysConfig.GetConfig("IsEnableSecureLogin"))


        If UCase(IsEnableSecureLogin) = "YES" Then

            Dim objLogInOut As LogInOutService
            objLogInOut = LogInOutService.getInstance

            If UCase(output) = "ADDITIONALDEV" Then

                Dim IsAvilSec As Boolean

                If PMSNAMES.AMADEUSM = PMSName Then
                    IsAvilSec = objLogInOut.IsRegisteredUser_Member(userCrdential.roomNo, userCrdential.guestName)
                Else
                    IsAvilSec = objLogInOut.IsRegisteredUser_RNLN(userCrdential.roomNo, userCrdential.guestName)
                End If

                If IsAvilSec = False Then

                    If userCrdential.Serialize(UniqueLogid) Then
                        Response.Redirect("CreateMemberPassword.aspx?encry=" & T_Url)
                    End If

                Else
                    userCrdential.item("loginfrom") = "memberconnect"
                    output = AAA.AAA(userCrdential)                             'directly connect internet for user who have set password
                    If UCase(output) = "SUCCESS" Then
                        If userCrdential.Serialize(UniqueLogid) Then
                            Response.Redirect("userinfo.aspx?encry=" & T_Url)
                        End If
                    End If
                    'If userCrdential.Serialize(UniqueLogid) Then
                    '    Response.Redirect("checkMemberPassword.aspx?encry=" & T_Url)
                    'End If

                End If

            ElseIf UCase(output) = "CREATE SECURE PWD" Then

                Dim IsAvilSec As Boolean

                userCrdential.item("tempdevreachmsg") = output ' If re-set password

                If PMSNAMES.AMADEUSM = PMSName Then
                    IsAvilSec = objLogInOut.IsRegisteredUser_Member(userCrdential.roomNo, userCrdential.guestName)
                Else
                    IsAvilSec = objLogInOut.IsRegisteredUser_RNLN(userCrdential.roomNo, userCrdential.guestName)
                End If

                If IsAvilSec = False Then

                    If userCrdential.Serialize(UniqueLogid) Then
                        Response.Redirect("CreateMemberPassword.aspx?encry=" & T_Url)
                    End If

                Else
                    userCrdential.item("loginfrom") = "memberconnect"               'directly connect internet for user who have set password
                    output = AAA.AAA(userCrdential)
                    If UCase(output) = "SUCCESS" Then
                        If userCrdential.Serialize(UniqueLogid) Then
                            Response.Redirect("userinfo.aspx?encry=" & T_Url)
                        End If
                    End If


                End If

            ElseIf UCase(output) = "SUCCESS" Then
                Dim IsAvilSec As Boolean
                If PMSNAMES.AMADEUSM = PMSName Then
                    IsAvilSec = objLogInOut.IsRegisteredUser_Member(userCrdential.roomNo, userCrdential.guestName)
                Else
                    IsAvilSec = objLogInOut.IsRegisteredUser_RNLN(userCrdential.roomNo, userCrdential.guestName)
                End If

                If IsAvilSec = False Then

                    If userCrdential.Serialize(UniqueLogid) Then
                        Response.Redirect("CreateMemberPassword.aspx?encry=" & T_Url)
                    End If

                Else
                    userCrdential.item("loginfrom") = "memberconnect"               'directly connect internet for user who have set password
                    If userCrdential.Serialize(UniqueLogid) Then
                        Response.Redirect("userinfo.aspx?encry=" & T_Url)
                    End If



                End If

            ElseIf UCase(output) = "MAXDEVREACHED" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=memberlogin&ErrorCode=17")

            ElseIf UCase(output) = "COOKIE" Then
                pgCookie.ResetCookie(HttpContext.Current.Response)
                Response.Redirect("MemberLogin.aspx?encry=" & url)
            Else
                pgCookie.ResetCookie(HttpContext.Current.Response)

                If output = "" Or output = Nothing Then
                    output = errInfo
                End If

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

        Else


            If UCase(output) = "SUCCESS" Then

                '---------------- START serialization ---------------
                If userCrdential.Serialize(UniqueLogid) Then
                    Response.Redirect("UserInfo.aspx?encry=" & T_Url)
                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&finderror=memberlogin&ErrorCode=10")
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


                'Dim objSysConfig As New CSysConfig
                'Dim NotExtraDevPlanid As String = objSysConfig.GetConfig("24HoursPlanid")
                'Dim ObjElog As LoggerService

                'If userCrdential.item("planid") = NotExtraDevPlanid Then

                '    ObjElog = LoggerService.gtInstance
                '    ObjElog.write2LogFile(userCrdential.roomNo, Now() & " -- Planid :" & userCrdential.item("planid") & " -- Found 24 hours plan" & vbCrLf)

                '    Dim objLogInOut As LogInOutService
                '    objLogInOut = LogInOutService.getInstance
                '    If objLogInOut.IsUsedPerdayFreePlan(userCrdential) Then ' If Perday access avil go to new login, otherwise, premium plan

                '        ObjElog = LoggerService.gtInstance
                '        ObjElog.write2LogFile(userCrdential.roomNo, Now() & " -- Free available for a day. Display free and pay plans" & vbCrLf)

                '        If userCrdential.Serialize(UniqueLogid) Then
                '            Response.Redirect("PlanSelectionPayAll.aspx?encry=" & T_Url)
                '        Else
                '            Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                '        End If

                '    Else

                '        ObjElog = LoggerService.gtInstance
                '        ObjElog.write2LogFile(userCrdential.roomNo, Now() & " -- Free over for a day. redirect to PremiumOverPayAll" & vbCrLf)

                '        If userCrdential.Serialize(UniqueLogid) Then
                '            Response.Redirect("PremiumOverPayAll.aspx?encry=" & T_Url)
                '        Else
                '            Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                '        End If

                '    End If
                'End If

                'If objLogInOut.IsUsedPerdayFreePlan(userCrdential) Then ' If Perday access avil go to new login, otherwise, premium plan

                '    userCrdential.item("devreachmsg") = "MAXDEVREACHED"

                '    If userCrdential.Serialize(UniqueLogid) Then
                '        Response.Redirect("PlanSelectionPayAll.aspx?encry=" & T_Url)
                '    Else
                '        Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")
                '    End If

                'ElseIf UCase(output) = "PREMIUM FREE OVER" Or UCase(output) = "MAXDEVREACHED" Then


                '---- On Plan A (1 Day), after device limit reached, it should show all the plan (i.e. Plan A to D) and not the extra device.
                If UCase(output) = "PREMIUM FREE OVER" Or UCase(output) = "MAXDEVREACHED" Then
                    If userCrdential.Serialize(UniqueLogid) Then

                        Dim objPlan As New CPlan
                        If objPlan.IsDispPlanAftDevRec(userCrdential.item("planid")) = True Then

                            Dim ObjElog As LoggerService
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
                    Response.Redirect("UserError.aspx?encry=" & url & "&findurl=memberlogin&ErrorCode=114")

                ElseIf UCase(output) = "ROOM BLOCKED" Then
                    Response.Redirect("UserError.aspx?encry=" & url & "&findurl=memberlogin&ErrorCode=11")

                ElseIf UCase(output) = "CHECKOUT" Then
                    Response.Redirect("UserError.aspx?encry=" & url & "&findurl=memberlogin&ErrorCode=12")
                Else
                    Response.Redirect("UserError.aspx?encry=" & url & "&findurl=memberlogin&ErrorCode=10")
                End If
            End If


        End If



    End Sub
    Protected Sub imgbtnlogin_Click(sender As Object, e As EventArgs) Handles imgbtnlogin.Click

        Dim objSysConfig As New CSysConfig

        planId = objSysConfig.GetConfig("MembershipPlanId")
        If txtroomno.Text <> "" And txtlastname.Text <> "" Then
           
            If planId <> "" Then
                Dim ObjElog As LoggerService
                Dim TempErrCode As String = ""
                Dim TempErrMsg As String = ""

                Dim commonFun As PMSCommonFun
                commonFun = PMSCommonFun.getInstance

                If commonFun.ValidateSecurePassword(UCase(txtroomno.Text)) = False Then
                    TempErrCode = "13"
                    TempErrMsg = "Enter alphanumeric values in Member ID"

                ElseIf commonFun.ValidateSecurePassword(UCase(txtlastname.Text)) = False Then
                    TempErrCode = "14"
                    TempErrMsg = "Enter alphanumeric values in password"
                End If

                If TempErrCode = "13" Or TempErrCode = "14" Then
                    ObjElog = LoggerService.gtInstance
                    ObjElog.write2LogFile(txtroomno.Text, Now() & " --  Member Id: " & txtroomno.Text & " -- password: " & txtlastname.Text)

                    Dim userCrdential As UserContext
                    userCrdential = New UserContext(txtroomno.Text, txtlastname.Text, planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                    userCrdential.item("usertype") = EUSERTYPE.ROOM
                    userCrdential.item("device") = "1"
                    userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

                    Dim objLogInOut As LogInOutService
                    objLogInOut = LogInOutService.getInstance
                    objLogInOut.loginfail(userCrdential, TempErrMsg)

                    Response.Redirect("UserError.aspx?encry=" & url & "&findurl=mifilogin&ErrorCode=" & TempErrCode)

                Else

                    ObjElog = LoggerService.gtInstance
                    ObjElog.write2LogFile(txtroomno.Text, Now() & " --  Entered Member Id as : " & txtroomno.Text & " -- Entered password as : " & txtlastname.Text & vbCrLf)

                    Dim userCrdential As New UserContext(txtroomno.Text, txtlastname.Text, planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                    userCrdential.item("usertype") = EUSERTYPE.ROOM
                    userCrdential.item("device") = "1"
                    userCrdential.item("loginfrom") = "member"
                    userCrdential.item("nseid") = UID
                    userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))
                    login(userCrdential)
                End If
            Else
                Dim ObjElog As LoggerService = LoggerService.gtInstance
                ObjElog.write2LogFile(txtroomno.Text, Now() & " MembershipPlanId is empty in config table " & vbCrLf)
            End If
            

        End If
    End Sub

End Class