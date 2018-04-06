Imports PMSPkgV1_0
Imports security
Imports System.Threading
Imports System.Security
'Imports MODI

Public Class Mifilogin
    Inherits System.Web.UI.Page

    Protected mainHTMLBody As New HtmlGenericControl
    Protected WithEvents txtroomno As System.Web.UI.WebControls.TextBox
    Protected pageTitle As New HtmlGenericControl
    Protected WithEvents btnlogin As System.Web.UI.WebControls.ImageButton
    Dim pgCookie As New CCookies
    Dim devType As DEVICETYPE

    Private planId As Long
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private CouponVersion As String
    Public url, FullURl As String
    Private accesstype As Integer
    Dim Recordno As Integer
    Dim MAC, OS, RN As String
    Dim UniqueLogid As String
    Public imgsrc As String
    Public hdrsrc As String
    Public txtsrc As String
    Public ocrtext As String = "None"
    Public filepath As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Dim objSysConfig As New CSysConfig
            Me.pageTitle.InnerText = objSysConfig.GetConfig("HotelName")

            Dim commonFun As PMSCommonFun
            Dim userContext As UserContext
            Dim userCredential As UserCredential

            Dim NSEID As String
            Dim encrypt As New Datasealing
            Dim getplanid As String

            commonFun = PMSCommonFun.getInstance
            url = Request.QueryString("encry")

            '---------------------- START PMS Config --------------------------------------
            commonFun = PMSCommonFun.getInstance
            PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
            PMSVersion = objSysConfig.GetConfig("PMSVersion")
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
            FullURl = commonFun.DecrptString(url)
            '-------- START Generate the Unique Id ----------
            UniqueLogid = commonFun.GenerateUniqueId(MAC)
            '-------- END Generate the Unique Id ----------

            ''-----------------Load Image from database-------------------------

            'Try

            '    Dim Img As String = ""

            '    If objSysConfig.GetConfig("ImgUrl") Is Nothing Then
            '        imgsrc = "None"
            '    Else
            '        Img = objSysConfig.GetConfig("ImgUrl").Trim()

            '        If Img <> "" Then
            '            'filepath = Server.MapPath("~/" & Img)
            '            'Dim extractText As String = Me.ExtractTextFromImage(filepath)

            '            'If extractText <> "" Then
            '            '    ocrtext = "some text"
            '            'End If
            '            imgsrc = Img
            '        Else
            '            imgsrc = "None"
            '        End If

            '    End If

            '    Dim HdrTxt As String = ""

            '    If objSysConfig.GetConfig("HdrTxt") Is Nothing Then
            '        hdrsrc = "None"
            '    Else
            '        HdrTxt = objSysConfig.GetConfig("HdrTxt").Trim()
            '        If HdrTxt <> "" Then
            '            hdrsrc = HdrTxt
            '        Else
            '            hdrsrc = "None"
            '        End If
            '    End If

            '    Dim Txt As String = ""

            '    If objSysConfig.GetConfig("InfoTxt") Is Nothing Then
            '        txtsrc = "None"
            '    Else
            '        Txt = objSysConfig.GetConfig("InfoTxt").Trim()
            '        If Txt <> "" Then
            '            txtsrc = Txt
            '        Else
            '            txtsrc = "None"
            '        End If
            '    End If


            '    'Session("imgsrc") = imgsrc & "?header=" & hdrsrc & "?text=" & txtsrc & "?ocrtext=" & ocrtext
            '    Session("imgsrc") = imgsrc & "?header=" & hdrsrc & "?text=" & txtsrc
            'Catch ex As Exception
            '    Dim log As LoggerService = LoggerService.gtInstance
            '    log.writeExceptionLogFile("Ex-ImgLoad", ex)
            'End Try

            ''---------------------------End Load Image----------------------------------------

            If Not IsPostBack() Then

                Dim All_URL As String = commonFun.DecrptQueryString_AllURL(url)

                devType = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

                Dim ObjElog As LoggerService
                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: Mifilogin page" & vbCrLf _
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

                '--------------------- Begin Chkeck Nomadix ID (NASID) ---------------------
                NSEID = UI
                Dim chknseid As String = objSysConfig.GetConfig("License_ID")
                NSEID = encrypt.GetEncryptedData(NSEID)
                If chknseid <> NSEID Then
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

            Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=10")

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
        T_Url = T_Url & "&logid=" & UniqueLogid & "&imgsrc=" & imgsrc

        T_Url = commonFun.EncrptQueryString_StringURL(T_Url)
        '---------------- END Here added the uniquelog id TO URL --------------

        Dim objSysConfig As New CSysConfig

        Dim objLogInOut As LogInOutService
        objLogInOut = LogInOutService.getInstance


        Dim IsEnableMultiplePlan As String = UCase(objSysConfig.GetConfig("IsEnableMultiplePayPlan"))

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

         

            Dim TajTIC As String = Trim(objSysConfig.GetConfig("TICLevel"))
            Dim TajwebsiteGuest As String = Trim(objSysConfig.GetConfig("BookedViaTajSite"))

            If objLogInOut.IsTICLevel(TajTIC, userCrdential.item("compcode")) Then
                If userCrdential.Serialize(UniqueLogid) Then
                    Response.Redirect("PlanSelectionPayAllTIC.aspx?encry=" & T_Url)
                End If
            ElseIf (UCase(userCrdential.item("compcode")) = UCase(TajwebsiteGuest)) Then
                If userCrdential.Serialize(UniqueLogid) Then
                    Session("tajgues") = "5"
                    Response.Redirect("PlanSelectionPayAllTaj.aspx?encry=" & T_Url)
                End If
            Else
                If userCrdential.Serialize(UniqueLogid) Then
                    Response.Redirect("PlanSelectionPayAll.aspx?encry=" & T_Url)
                End If
            End If


        ElseIf UCase(output) = "ADDITIONALDEV" Then
            Dim IsAvilSec As Boolean

            '  Dim objLogInOut As LogInOutService
            objLogInOut = LogInOutService.getInstance

            If PMSNAMES.AMADEUS = PMSName Then
                IsAvilSec = objLogInOut.IsRegisteredUser_RNLN_FIAS(userCrdential.roomNo, userCrdential.guestName)
            Else
                IsAvilSec = objLogInOut.IsRegisteredUser_RNLN(userCrdential.roomNo, userCrdential.guestName)
            End If

            If IsAvilSec = False Then


                Dim TajwebsiteGuest As String = Trim(objSysConfig.GetConfig("BookedViaTajSite"))
                Dim TajTIC As String = Trim(objSysConfig.GetConfig("TICLevel"))

                If (UCase(userCrdential.item("compcode")) = UCase(TajwebsiteGuest)) Or objLogInOut.IsTICLevel(TajTIC, userCrdential.item("compcode")) Then

                    If objLogInOut.IsResetPwd_RNLN_FIAS(userCrdential.roomNo, userCrdential.guestName) Then
                        If userCrdential.Serialize(UniqueLogid) Then
                            Response.Redirect("CreatePasswordAfterReset.aspx?encry=" & T_Url)
                        End If
                    Else
                        If userCrdential.Serialize(UniqueLogid) Then
                            Response.Redirect("CreatePassword.aspx?encry=" & T_Url)
                        End If
                    End If

                Else
                    If objLogInOut.IsResetPwd_RNLN_FIAS(userCrdential.roomNo, userCrdential.guestName) Then
                        If userCrdential.Serialize(UniqueLogid) Then
                            Response.Redirect("CreatePasswordAfterReset.aspx?encry=" & T_Url)
                        End If
                    Else
                        If userCrdential.Serialize(UniqueLogid) Then
                            Response.Redirect("CreatePasswordPopUp.aspx?encry=" & T_Url)
                        End If
                    End If


                End If


            Else
                If userCrdential.Serialize(UniqueLogid) Then
                    Response.Redirect("CheckPassword.aspx?encry=" & T_Url)
                End If

            End If
        ElseIf UCase(output) = "PREMIUM FREE OVER" Or UCase(output) = "FREE OVER" Or UCase(output) = "MAXDEVREACHED" Then

            If userCrdential.item("serviceaccess") = SERVICETYPE.FREE Then
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=mifilogin&ErrorCode=17")
            End If

            Dim TajTIC As String = Trim(objSysConfig.GetConfig("TICLevel"))

            'Dim objLogInOut As LogInOutService
            objLogInOut = LogInOutService.getInstance

            If objLogInOut.IsUsedPerdayFreePlan_Difffree(userCrdential) Then ' If Perday access avil go to new login, otherwise, premium plan

                userCrdential.item("devreachmsg") = "MAXDEVREACHED"

                If userCrdential.Serialize(UniqueLogid) Then

                    If objLogInOut.IsTICLevel(TajTIC, userCrdential.item("compcode")) Then

                        Response.Redirect("PlanSelectionPayAllTIC.aspx?encry=" & T_Url)

                    Else
                        If IsEnableMultiplePlan = "YES" Then
                            Response.Redirect("PlanSelectionPayAll.aspx?encry=" & T_Url)
                        Else
                            Response.Redirect("PlanSelection.aspx?encry=" & T_Url)
                        End If
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

            'Dim objLogInOut As LogInOutService
            objLogInOut = LogInOutService.getInstance
            objLogInOut.loginfail(userCrdential, output)

            If UCase(output) = "INVALID CREDENTIAL" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=mifilogin&ErrorCode=4")

            ElseIf UCase(output) = "ROOM BLOCKED" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=mifilogin&ErrorCode=11")

            ElseIf UCase(output) = "CHECKOUT" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=mifilogin&ErrorCode=12")
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=mifilogin&ErrorCode=10")
            End If
        End If

    End Sub

    'Protected Sub imgbtnlogin_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnlogin.Click

    '    Dim objSysConfig As New CSysConfig

    '    If txtroomno.Text <> "" And txtlastname.Text <> "" Then

    '        Dim ObjElog As LoggerService
    '        Dim TempErrCode As String = ""
    '        Dim TempErrMsg As String = ""

    '        Dim commonFun As PMSCommonFun
    '        commonFun = PMSCommonFun.getInstance

    '        If commonFun.ValidateRoomNo(UCase(txtroomno.Text)) = False Then
    '            TempErrCode = "13"
    '            TempErrMsg = "Enter numeric values in Room Number"

    '        ElseIf commonFun.ValidateLastName(UCase(txtlastname.Text)) = False Then
    '            TempErrCode = "14"
    '            TempErrMsg = "Enter alphabets values in Last name"
    '        End If

    '        If TempErrCode = "13" Or TempErrCode = "14" Then
    '            ObjElog = LoggerService.gtInstance
    '            ObjElog.write2LogFile(txtroomno.Text, Now() & " --  Room Number: " & txtroomno.Text & " -- Last Name: " & txtlastname.Text)

    '            Dim userCrdential As UserContext
    '            userCrdential = New UserContext(txtroomno.Text, txtlastname.Text, planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
    '            userCrdential.item("usertype") = EUSERTYPE.ROOM
    '            userCrdential.item("device") = "1"
    '            userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

    '            Dim objLogInOut As LogInOutService
    '            objLogInOut = LogInOutService.getInstance
    '            objLogInOut.loginfail(userCrdential, TempErrMsg)

    '            Response.Redirect("UserError.aspx?encry=" & url & "&findurl=mifilogin&ErrorCode=" & TempErrCode)

    '        Else

    '            ObjElog = LoggerService.gtInstance
    '            ObjElog.write2LogFile(txtroomno.Text, Now() & " --  Entered Room Number as : " & txtroomno.Text & " -- Entered Last Name as : " & txtlastname.Text & vbCrLf)

    '            Dim userCrdential As New UserContext(txtroomno.Text, txtlastname.Text, planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
    '            userCrdential.item("usertype") = EUSERTYPE.ROOM
    '            userCrdential.item("device") = "1"
    '            userCrdential.item("loginfrom") = "mifi"
    '            userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))
    '            login(userCrdential)

    '        End If

    '    End If
    'End Sub

    Protected Sub imgbtnlogin_Click(sender As Object, e As EventArgs) Handles imgbtnlogin.Click
        Dim objSysConfig As New CSysConfig

        If txtroomno.Text <> "" And txtlastname.Text <> "" Then

            Dim ObjElog As LoggerService
            Dim TempErrCode As String = ""
            Dim TempErrMsg As String = ""

            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance

            If commonFun.ValidateRoomNo(UCase(txtroomno.Text)) = False Then
                TempErrCode = "13"
                TempErrMsg = "Enter numeric values in Room Number"

            ElseIf commonFun.ValidateLastName(UCase(txtlastname.Text)) = False Then
                TempErrCode = "14"
                TempErrMsg = "Enter alphabets values in Last name"
            End If

            If TempErrCode = "13" Or TempErrCode = "14" Then
                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(txtroomno.Text, Now() & " --  Room Number: " & txtroomno.Text & " -- Last Name: " & txtlastname.Text)

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
                ObjElog.write2LogFile(txtroomno.Text, Now() & " --  Entered Room Number as : " & txtroomno.Text & " -- Entered Last Name as : " & txtlastname.Text & vbCrLf)

                Dim userCrdential As New UserContext(txtroomno.Text, txtlastname.Text, planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                userCrdential.item("usertype") = EUSERTYPE.ROOM
                userCrdential.item("device") = "1"
                userCrdential.item("loginfrom") = "mifi"
                userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))
                login(userCrdential)

            End If

        End If

    End Sub

    'Private Function ExtractTextFromImage(filePath As String) As String
    '    Dim modiDocument As New Document()
    '    modiDocument.Create(filePath)
    '    modiDocument.OCR(MiLANGUAGES.miLANG_ENGLISH)
    '    Dim modiImage As MODI.Image = TryCast(modiDocument.Images(0), MODI.Image)
    '    Dim extractedText As String = modiImage.Layout.Text
    '    modiDocument.Close()
    '    Return extractedText
    'End Function
End Class