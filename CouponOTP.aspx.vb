Imports PMSPkgV1_0
Imports security
Imports System.Threading
Imports System.Security
Public Class CouponOTP
    Inherits System.Web.UI.Page
    Dim pgCookie As New CCookies
    Dim UniqueLogid As String = ""
    Dim devType As DEVICETYPE
    Dim otptext As String = ""
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private CouponVersion As String
    Private sql_query As String
    Public url As String
    Dim planId As Long
    Dim UI, UURL, MAC, SC, OS, RN As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSysConfig As New CSysConfig
        Me.PageTitle.InnerText = objSysConfig.GetConfig("HotelName")
        Dim objElog As LoggerService = LoggerService.gtInstance

        Dim commonFun As PMSCommonFun
        Dim userContext As UserContext
        Dim userCredential As UserCredential

        Dim NSEID As String
        Dim encrypt As New Datasealing

        commonFun = PMSCommonFun.getInstance
        url = Request.QueryString("encry")


        Dim objPlan As New CPlan
        Dim currency As String = ""
        Dim getplanid As String = ""

        '----------------------START PMS Config --------------------------------------
        commonFun = PMSCommonFun.getInstance
        PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
        PMSVersion = objSysConfig.GetConfig("PMSVersion")
        CouponVersion = objSysConfig.GetConfig("CouponVersion")
        currency = objSysConfig.GetConfig("Currency")
        '----------------------END PMS Config ----------------------------------------

        Dim UI, UURL, SC, OS As String


        UI = commonFun.DecrptQueryString("UI", url)
        UURL = commonFun.DecrptQueryString("UURL", url)
        MAC = commonFun.DecrptQueryString("MA", url)
        SC = commonFun.DecrptQueryString("SC", url)
        OS = commonFun.DecrptQueryString("OS", url)
        RN = commonFun.DecrptQueryString("RN", url)

        '-------- START Generate the Unique Id ----------
        UniqueLogid = commonFun.GenerateUniqueId(MAC)

        '-------- END Generate the Unique Id ----------



        Dim IsEnableMacBasedLogin As String = objSysConfig.GetConfig("IsEnableMACBasedLogin")        '---mac based login
        If UCase(IsEnableMacBasedLogin) = "YES" Then
            'userContext = New UserContext("", "")
            'UsrCred = userContext.DeSerialize(UniqueLogid)

            '-------- START MAC based login ----------
            If MAC <> "" Then

                MAC = MAC.Replace(":", "")

                Dim objLogInOut As LogInOutService
                objLogInOut = LogInOutService.getInstance()
                If objLogInOut.CheckRTAvilOrNot_MAC_SQL_AccessCode(MAC) Then         ' ------------- Check RT avil or not for AccessCode



                    Dim Ds As DataSet
                    Dim username, password, plan As String

                    Ds = objLogInOut.FetchLoginCred_AccessCode_SQL(MAC)    'mac based authentication

                    If Ds.Tables(0).Rows.Count > 0 Then

                        username = Ds.Tables(0).Rows(0).Item("CouponUserId")
                        'password = username
                        password = Ds.Tables(0).Rows(0).Item("LoginMobile")
                        planId = objSysConfig.GetConfig("OTPPlanID").Trim()




                        If username <> "" And password <> "" Then

                            Dim userCrdential As New UserContext(username, password, PMSNAMES.UNKNOWN, CouponVersion, "", "", "", "", HttpContext.Current.Request)
                            userCrdential.item("usertype") = EUSERTYPE.NR
                            userCrdential.item("device") = "1"
                            userCrdential.item("vlanid") = RN
                            userCrdential.item("logunorpwd") = "pwd"
                            userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))


                            objElog.write2LogFile(username, Now() & " -- MAC based Login -- MAC:" & MAC)

                            login(userCrdential)
                            Exit Sub
                        Else
                            objElog.write2LogFile(username, Now() & " -- DB Credentials empty")
                        End If


                    End If



                End If

            End If
        End If

        LblError.Text = ""
        LblError1.Text = ""
        LblError2.Text = ""
        If Not IsPostBack() Then

            Dim All_URL As String = commonFun.DecrptQueryString_AllURL(url)

            devType = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

            'Dim ObjElog As LoggerService
            'ObjElog = LoggerService.gtInstance
            objElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: CouponOTP page" & vbCrLf _
                                                & "----------------------- Nomadix I/P: " & All_URL & vbCrLf _
                                                & "----------------------- MAC: " & MAC & vbCrLf _
                                                & "----------------------- Device Type: " & devType.ToString() & vbCrLf _
                                                & "----------------------- LogId: " & UniqueLogid & vbCrLf _
                                                & "----------------------- Idendified String: " & Request.ServerVariables("HTTP_USER_AGENT"))



            If UniqueLogid = "" Or UniqueLogid = Nothing Then

                objElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Logid missed" & vbCrLf)
                Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
            End If

            If MAC = "" Then Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
            '------------------- Begin Chkeck Nomadix ID (NASID) '------------------

            Dim chknseid As String = objSysConfig.GetConfig("License_ID")
            NSEID = encrypt.GetEncryptedData(UI)
            If chknseid <> NSEID Then
                Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=8")
            End If
            '------------------- End Chkeck Nomadix ID (NASID) '-------------------

            Dim objGuestSrv As GuestService

            If pgCookie.ReadCookie(Request) Then

                If pgCookie.expiryTime > Now() And pgCookie.userType = EUSERTYPE.COUPON Then
                    objGuestSrv = GuestService.getInstance
                    userCredential = objGuestSrv.getGuestInfo(pgCookie)
                    getplanid = objGuestSrv.GetPlanid(pgCookie.grcId)
                    userContext = New UserContext(userCredential, PMSNAMES.UNKNOWN, CouponVersion, getplanid, "", "", HttpContext.Current.Request)
                    userContext.item("usertype") = EUSERTYPE.COUPON
                    userContext.item("serviceaccess") = pgCookie.serviceAccess
                    userContext.item("cookiemac") = pgCookie.mac
                    userContext.item("loginbycookie") = True
                    userContext.item("device") = "1"
                    userContext.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))

                    objElog = LoggerService.gtInstance
                    objElog.write2LogFile(userCredential.usrId, Now() & "----- Found cookies" & vbCrLf _
                                 & "------------------------- Password: " & userCredential.passwd & vbCrLf _
                                 & "-------------------------User Id: " & userCredential.usrId & vbCrLf _
                                 & "------------------------- cookie MAC: " & pgCookie.mac)
                    login(userContext)

                End If
            End If
            '------------------- End Cookies '-------------------

        End If
    End Sub

    Protected Sub imgbtnlogin_Click(sender As Object, e As EventArgs) Handles imgbtnlogin.Click
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        Dim ObjElog As LoggerService
        Dim TempErrCode As String = ""
        Dim TempErrMsg As String = ""
        Dim objSysConfig As New CSysConfig
        Dim objGuest As GuestService
        objGuest = GuestService.getInstance

        planId = objSysConfig.GetConfig("OTPPlanID").Trim()
        Try
            Dim p As PushSMSService = PushSMSService.getInstance()
            If txtroomno.Text <> "" And txtlastname.Text <> "" Then
                'If BlockMobile(txtroomno.Text) = True Then
                '    LblError.Visible = True
                '    LblError.Text = "Your Mobile Number is Blocked"
                '    Return
                'End If

                If commonFun.ValidateCoupon(UCase(txtroomno.Text)) = False Then
                    TempErrCode = "13"

                ElseIf commonFun.ValidateMobileNo(UCase(txtlastname.Text)) = False Then
                    TempErrCode = "14"

                End If
                If TempErrCode = "13" Or TempErrCode = "14" Then
                    ObjElog = LoggerService.gtInstance
                    ObjElog.write2LogFile(txtroomno.Text, Now() & " Access Code: " & txtroomno.Text & "--  Mobile Number: " & txtlastname.Text)
                    Response.Redirect("UserError.aspx?encry=" & url & "&findurl=nrlogin&ErrorCode=" & TempErrCode)
                Else
                    Dim userCrdential As New UserContext(txtroomno.Text, txtlastname.Text, PMSNAMES.UNKNOWN, CouponVersion, "", "", "", "", HttpContext.Current.Request)
                    userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))
                    userCrdential.item("device") = "1"
                    userCrdential.item("logunorpwd") = "pwd"
                    userCrdential.item("usertype") = EUSERTYPE.NR
                    'userCrdential.item("outputbtn") = True
                    Dim Access As New CouponsV002
                    Dim output As Boolean
                    output = Access.doAuth(userCrdential)                   'authenticate access code
                    If UCase(output) <> True Then
                        LblError.Visible = True
                        LblError.Text = "Invalid Access Code"
                        Dim userCrdential1 As New UserContext(txtroomno.Text, txtlastname.Text, PMSNAMES.UNKNOWN, CouponVersion, "", "", "", "", HttpContext.Current.Request)
                        userCrdential1.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))
                        userCrdential1.item("device") = "1"
                        userCrdential1.item("logunorpwd") = "pwd"
                        userCrdential1.item("usertype") = EUSERTYPE.NR
                        Dim objLogInOut As LogInOutService
                        objLogInOut = LogInOutService.getInstance
                        objLogInOut.loginfail(userCrdential1, LblError.Text)
                    Else
                        Dim MaxDevLimit As String
                        Dim MaxMACLimit As String
                        Dim objPlan As New CPlan
                        Dim objLogInOut As LogInOutService = LogInOutService.getInstance

                        Dim i As Integer = isNewMobileRegister(txtroomno.Text, txtlastname.Text)
                        Dim remainingtime As Long = Access.getBalaceInet(userCrdential)


                        If remainingtime > 0 Then               'check device limit when time remaining for plan
                            objPlan.getPlaninfo(planId, "", "")
                            MaxDevLimit = objPlan.TotalConCurrentDev
                            MaxMACLimit = MaxDevLimit

                            Dim query As String = "select top 1 billid from bill where billgrcid='" & userCrdential.item("grcid") & "' and billmobile='" & userCrdential.password & "' order by billid desc"
                            Dim billid As String = ""
                            Dim objdbase As DbaseService = DbaseService.getInstance
                            Dim ds As DataSet = objdbase.DsWithoutUpdate(query)
                            ds = objdbase.DsWithoutUpdate(query)
                            If (ds.Tables(0).Rows.Count > 0) Then
                                billid = ds.Tables(0).Rows(0).Item("billid").ToString()
                            End If

                            If billid <> "" Then           'check device limit reached based on billid
                                If objLogInOut.IsReachedMaxDev(billid, MaxDevLimit) Then
                                    ObjElog = LoggerService.gtInstance
                                    ObjElog.write2LogFile(userCrdential.roomNo, Now() & " **** Device Limit Reached, MAC : " & userCrdential.machineId & vbCrLf)
                                    Response.Redirect("UserError.aspx?encry=" & url & "&findurl=nrlogin&ErrorCode=17")
                                End If
                            End If
                        End If

                        
                        otptext = GenerateOtp()
                        If otptext <> "" Then

                            Dim j As Integer = OTPRegister(txtroomno.Text, txtlastname.Text, otptext)   'insert to otpdetails table

                            If j > 0 Then      'if otp generated is inserted


                                Dim objlog As LoggerService = LoggerService.gtInstance

                                Dim IsEnableOTP As String = UCase(objSysConfig.GetConfig("IsEnableOTP"))      'send otp or not

                                Dim val As String = ""
                                Try

                                    Dim objdbase As DbaseService = DbaseService.getInstance
                                    Dim sql_query As String = "select top 1 OTP from OTPDetails where MacAddr ='" & MAC & "' and AccessCode='" & txtroomno.Text & "' and MobileNo='" & txtlastname.Text & "'  and OTPExpiryTime>getdate() order by OTPTime desc"
                                    Dim ds As DataSet = objdbase.DsWithoutUpdate(sql_query)
                                    If (ds.Tables(0).Rows.Count > 0) Then
                                        val = ds.Tables(0).Rows(0).Item("OTP").ToString()
                                    Else
                                        val = otptext
                                    End If


                                Catch ex As Exception
                                    objlog = LoggerService.gtInstance
                                    objlog.writeExceptionLogFile("Exc_sendOTP", ex)
                                    val = ""
                                End Try
                                If UCase(IsEnableOTP) = "YES" Then
                                    p.SendSMSToMobile_ValueFirst(txtlastname.Text, val)
                                Else
                                    objlog.write2LogFile("OTP", Now() & val & vbCrLf)
                                End If
                                objlog.write2LogFile(txtroomno.Text, Now() & " SMS Sent to " & txtlastname.Text & vbCrLf)


                                imgotplogin.Enabled = True
                                BtnResend.Enabled = True
                                LblError.Visible = False
                                imgbtnlogin.Enabled = False
                                'redirect to next page with encrypted data
                                OTP.Visible = True
                                imgbtnlogin.Visible = False
                                txtroomno.ReadOnly = True
                                txtlastname.ReadOnly = True
                                ScriptManager.RegisterStartupScript(Me.Page, Page.[GetType](), "text", "btn_fade()", True)

                            Else
                                LblError.Visible = True
                                LblError.Text = "Technical Error Occurred"

                            End If
                        Else
                            LblError.Visible = True
                            LblError.Text = "Technical Error Occurred"

                        End If

                    End If    'above this
                End If




            Else
                Dim userCrdential As New UserContext(txtroomno.Text, txtlastname.Text, PMSNAMES.UNKNOWN, CouponVersion, "", "", "", "", HttpContext.Current.Request)
                userCrdential.item("usertype") = EUSERTYPE.NR
                LblError.Visible = True
                LblError.Text = "Invalid Access Code"
                Dim objLogInOut As LogInOutService
                objLogInOut = LogInOutService.getInstance
                objLogInOut.loginfail(userCrdential, LblError.Text)
            End If
        Catch ex As Exception

        End Try
        

        'usersection.Disabled = True
    End Sub

    Protected Sub imgotplogin_Click(sender As Object, e As EventArgs) Handles imgotplogin.Click
        Dim ObjElog As LoggerService
        Dim TempErrCode As String = ""
        Dim TempErrMsg As String = ""
        Dim objSysConfig As New CSysConfig
        Dim objGuest As GuestService
        objGuest = GuestService.getInstance

        planId = objSysConfig.GetConfig("OTPPlanID").Trim()

        If txtotp.Text <> "" Then
            'PanelPlan.Visible = True
            'login_details.Visible = True

            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance

            If commonFun.ValidateRoomNo(UCase(txtotp.Text)) = False Then
                TempErrCode = "13"

                'ElseIf commonFun.ValidateRoomNo(UCase(txtotp.Text)) = False Then
                '    TempErrCode = "14"

            End If



            If TempErrCode = "13" Then

                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(txtroomno.Text, Now() & " -- Entered OTP: " & txtotp.Text)
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=nrlogin&ErrorCode=" & TempErrCode)
            Else
                Dim i As String = ValidateOTP(txtotp.Text)
                If i <> "" Then          'if mobile number exists send otp
                    ObjElog = LoggerService.gtInstance
                    ObjElog.write2LogFile(txtroomno.Text, Now() & " -- Entered valid OTP: " & txtotp.Text & vbCrLf)
                    'Dim userCrdential As New UserContext(txtroomno.Text, txtroomno.Text, planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                    Dim userCrdential As New UserContext(txtroomno.Text, txtlastname.Text, PMSNAMES.UNKNOWN, CouponVersion, "", "", "", "", HttpContext.Current.Request)
                    userCrdential.item("usertype") = EUSERTYPE.NR
                    userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))
                    userCrdential.item("device") = "1"
                    userCrdential.item("vlanid") = RN
                    userCrdential.item("charge") = "NO"
                    userCrdential.item("loginfrom") = "OTP"
                    userCrdential.item("logunorpwd") = "pwd"
                    'userCrdential.item("mailid") = txtOTP.Text
                    login(userCrdential)


                Else
                    ObjElog = LoggerService.gtInstance
                    ObjElog.write2LogFile(txtroomno.Text, Now() & " -- Entered InValid OTP: " & txtotp.Text & vbCrLf)
                    LblError1.Visible = True
                    LblError1.Text = "Invalid  OTP"
                    'Dim userCrdential As UserContext
                    'userCrdential = New UserContext(txtroomno.Text, txtroomno.Text, planId, PMSName, PMSVersion, "", "", HttpContext.Current.Request)
                    Dim userCrdential As New UserContext(txtroomno.Text, txtotp.Text, PMSNAMES.UNKNOWN, CouponVersion, "", "", "", "", HttpContext.Current.Request)
                    userCrdential.item("usertype") = EUSERTYPE.NR
                    userCrdential.item("device") = "1"
                    userCrdential.item("devicetype") = commonFun.FindDeviceType(Request.ServerVariables("HTTP_USER_AGENT"))
                    Dim objLogInOut As LogInOutService
                    objLogInOut = LogInOutService.getInstance
                    objLogInOut.loginfail(userCrdential, LblError1.Text)
                    ScriptManager.RegisterStartupScript(Me.Page, Page.[GetType](), "text", "btn_fade()", True)
                End If



            End If


        Else
            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile(txtroomno.Text, Now() & " --  Please enter the login credential")
            MesgBox("Dear Guest, Please enter the login credential")
        End If
    End Sub
    Private Sub MesgBox(ByVal sMessage As String)
        Dim msgedtble As String = sMessage.Replace("\", "\\")
        msgedtble = msgedtble.Replace(vbNewLine, "\n")
        Page.ClientScript.RegisterStartupScript(Me.GetType, "myScripts", "<script language='javascript'>alert('" & msgedtble & "');</script>")

    End Sub

    Private Sub login(ByRef userCrdential As UserContext)

        Dim AAA As AAAService
        Dim output As String = ""
        Dim ProcessOutput As String = ""

        AAA = AAAService.getInstance

        Dim ObjElog As LoggerService
        ObjElog = LoggerService.gtInstance

        Try

            output = AAA.AAA(userCrdential)
            
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


        '---------------- START Here added the uniquelog id TO URL --------------
        Dim T_Url As String
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        T_Url = commonFun.DecrptQueryString_AllURL(url)
        T_Url = T_Url & "&logid=" & UniqueLogid

        T_Url = commonFun.EncrptQueryString_StringURL(T_Url)
        '---------------- END Here added the uniquelog id TO URL --------------


        If UCase(ProcessOutput) = "ERROR" Then
            Response.Redirect("UserError.aspx?encry=" & url & "&findurl=nrlogin&ErrorCode=5")

        ElseIf UCase(output) = "SUCCESS" Then

            '---------------- START serialization ---------------
            If userCrdential.Serialize(UniqueLogid) Then
                Response.Redirect("UserInfo.aspx?encry=" & T_Url)
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&finderror=nrlogin&ErrorCode=10")
            End If
            '---------------- END serialization ---------------

        ElseIf UCase(output) = "COOKIE" Then
            pgCookie.ResetCookie(HttpContext.Current.Response)
            Response.Redirect("Mifilogin.aspx?encry=" & url)

        Else '''''Goes to Error page
            pgCookie.ResetCookie(HttpContext.Current.Response)

            If UCase(output) = "INVALID_USER" Then

                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=nrlogin&ErrorCode=4")

            ElseIf UCase(output) = "COUPON EXPIRED" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=nrlogin&ErrorCode=23")

            ElseIf output = "Invalid Roomno and Lastname , please call reception for Roomno and lastname" Or output = "INVALID CREDENTIAL" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=mifilogin&ErrorCode=4")

            ElseIf UCase(output) = "PLEASE SELECT A PLAN" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=nrlogin&ErrorCode=25")

            ElseIf UCase(output) = "MAXMACREACHED" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=nrlogin&ErrorCode=22")

            ElseIf UCase(output) = "MAXDEVREACHED" Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=mifilogin&ErrorCode=17")

            ElseIf output = "Invalid User Id / Password.Try again." Then
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=nrlogin&ErrorCode=21")

            ElseIf UCase(output) = "ROOM BLOCKED" Then

                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=nrlogin&ErrorCode=11")
            Else
                Response.Redirect("UserError.aspx?encry=" & url & "&findurl=nrlogin&ErrorCode=5")
            End If

        End If

    End Sub
    Public Function ValidateOTP(ByVal OTP As String) As String
        Dim val As String = ""
        Try
            Dim objdbase As DbaseService = DbaseService.getInstance
            Dim sql_query As String = "select OTP from OTPDetails where OTP ='" & OTP & "' and OTPExpiryTime>getdate()"
            Dim ds As DataSet = objdbase.DsWithoutUpdate(sql_query)
            If (ds.Tables(0).Rows.Count > 0) Then
                val = ds.Tables(0).Rows(0).Item("OTP").ToString()
            End If


        Catch ex As Exception
            Dim objlog As LoggerService = LoggerService.gtInstance
            objlog.writeExceptionLogFile("Exc_ValidateOTP", ex)
            val = ""
        End Try
        Return val
    End Function
    Private Function GenerateOtp() As String
        Try


            Dim lenthofpass As Integer = 6
            Dim allowedChars As String = ""
            'allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,"
            'allowedChars = "A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z,"
            allowedChars += "0,1,2,3,4,5,6,7,8,9"
            Dim sep As Char() = {","c}
            Dim arr As String() = allowedChars.Split(sep)
            Dim passwordString As String = ""
            Dim temp As String = ""
            Dim rand As New Random()
            For i As Integer = 0 To lenthofpass - 1
                temp = arr(rand.[Next](0, arr.Length))
                passwordString += temp
            Next
            'Dear Guest, Your internet access code is XXXXX Thank You
            'Return "Dear Guest, Your internet access code is " & passwordString & " Thank You"
            Return passwordString
        Catch ex As Exception
            Dim objlog As LoggerService = LoggerService.gtInstance
            objlog.writeExceptionLogFile("Exc_GenerateOtp mobile", ex)

            Return ""

        End Try
    End Function
    Private Function isNewMobileRegister(ByVal AccessCode As String, ByVal MobileNo As String) As Integer
        Dim i As Integer = 0
        Dim objSysConfig As New CSysConfig
        Try

            'Dim objSysConfig As New CSysConfig
            Dim Expiry As String = Trim(objSysConfig.GetConfig("OTPExpiry"))

            'Dim sqlQuery As String = "if (select CouponMobileNo from Coupons where CouponUserId='" & AccessCode & "')  is null update coupons set CouponMobileNo='" & MobileNo & "' where CouponUserId='" & AccessCode & "'"
            'Dim dbService As DbaseService = DbaseService.getInstance()
            'i = dbService.insertUpdateDelete(sqlQuery)

            Dim objdbase As DbaseService = DbaseService.getInstance
            Dim sql_query As String = "select * from OTPDetails where MobileNo ='" & MobileNo & "' and AccessCode='" & AccessCode & "'"
            Dim ds As DataSet = objdbase.DsWithoutUpdate(sql_query)
            If (ds.Tables(0).Rows.Count > 0) Then
                i = 1
            Else
                i = 0
            End If

        Catch ex As Exception

            Dim objlog As LoggerService = LoggerService.gtInstance
            objlog.writeExceptionLogFile("Exc_NewMobileRegister", ex)
            i = 0
            'Throw New Exception("Exception in New Mobile Registration", ex)
        End Try
        Return i
    End Function
    Private Function OTPRegister(ByVal AccessCode As String, ByVal MobileNo As String, ByVal OTP As String) As Integer
        Dim i As Integer = 0
        Dim objSysConfig As New CSysConfig
        Try

            'Dim objSysConfig As New CSysConfig
            Dim Expiry As String = Trim(objSysConfig.GetConfig("OTPExpiry"))

            Dim sqlQuery As String = "if not exists (select * from OTPDetails where AccessCode='" & AccessCode & "' and MobileNo='" & MobileNo & "' and Macaddr='" & MAC & "' and OTPExpiryTime>getdate()) INSERT INTO OTPDetails (AccessCode, MacAddr,MobileNo,OTPTime,OTP,OTPExpiryTime) Values ('" & AccessCode & "','" & MAC & "','" & MobileNo & "',getdate(),'" & OTP & "',dateadd(ss," & Expiry & ",getdate()))"
            
            Dim dbService As DbaseService = DbaseService.getInstance()
            dbService.insertUpdateDelete(sqlQuery)
            i = 1
        Catch ex As Exception

            Dim objlog As LoggerService = LoggerService.gtInstance
            objlog.writeExceptionLogFile("Exc_OTPRegister", ex)
            i = 0
            'Throw New Exception("Exception in New Mobile Registration", ex)
        End Try
        Return i
    End Function

    Protected Sub BtnResend_Click(sender As Object, e As EventArgs) Handles BtnResend.Click
        Try

            'insert to otpgenerated table
            If txtlastname.Text <> "" Then
                'Dim i As Integer = InsertOtpGenerated(hidOtp.Value, HidEmp.Value, HidMob.Value)
                'If i > 0 Then
                'if inserted
                'send otp
                Dim val As String = ""
                Try

                    Dim objdbase As DbaseService = DbaseService.getInstance
                    Dim sql_query As String = "select top 1 OTP from OTPDetails where MacAddr ='" & MAC & "' and AccessCode='" & txtroomno.Text & "' and MobileNo='" & txtlastname.Text & "'  and OTPExpiryTime>getdate() order by OTPTime desc"
                    Dim ds As DataSet = objdbase.DsWithoutUpdate(sql_query)
                    If (ds.Tables(0).Rows.Count > 0) Then
                        val = ds.Tables(0).Rows(0).Item("OTP").ToString()
                    Else
                        val = ""
                    End If


                Catch ex As Exception
                    Dim objlog As LoggerService = LoggerService.gtInstance
                    objlog.writeExceptionLogFile("Exc_ResendOTP", ex)
                    Val = ""
                End Try
                If val <> "" Then
                    Dim p As PushSMSService = PushSMSService.getInstance()
                    p.SendSMSToMobile_ValueFirst(txtlastname.Text, val)

                    Dim objLog As LoggerService = LoggerService.gtInstance()
                    objLog.write2LogFile(txtroomno.Text, Now() & " SMS resent to " & txtlastname.Text & vbCrLf)
                Else
                    LblError1.Visible = True
                    LblError1.Text = "Technical Error Occurred."
                End If
            Else
                LblError1.Visible = True
                LblError1.Text = "Please Provide Mobile No "
            End If

            ScriptManager.RegisterStartupScript(Me.Page, Page.[GetType](), "text", "btn_fade()", True)


        Catch ex As Exception
            Dim objlog As LoggerService = LoggerService.gtInstance
            objlog.writeExceptionLogFile("Exc_BtnResend_Click otpvalidation", ex)
        End Try
    End Sub

End Class