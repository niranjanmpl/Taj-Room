Imports PMSPkgV1_0
Public Class Feedback
    Inherits System.Web.UI.Page

    Dim userContext As UserContext
    Dim UsrCred As Hashtable = Nothing
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Dim url As String
    Protected pageTitle As New HtmlGenericControl
    Dim UniqueLogid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSysConfig As New CSysConfig
        Me.pageTitle.InnerText = objSysConfig.GetConfig("HotelName")

        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        url = Request.QueryString("encry")

        UniqueLogid = commonFun.DecrptQueryString("logid", url)

        If Not IsPostBack() Then
            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: Feedback Page -- Logid: " & UniqueLogid & vbCrLf)
        End If

        If UniqueLogid = "" Or UniqueLogid = Nothing Then
            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance

            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Logid missed" & vbCrLf)
        End If

    End Sub

    Private Sub MesgBox(ByVal sMessage As String)
        Dim msgedtble As String = sMessage.Replace("\", "\\")
        msgedtble = msgedtble.Replace(vbNewLine, "\n")
        Page.ClientScript.RegisterStartupScript(Me.GetType, "myScripts", "<script language='javascript'>alert('" & msgedtble & "');</script>")

    End Sub


    Protected Sub imgbtnsubmit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnsubmit.Click
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        Dim objSysConfig As New CSysConfig

        PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
        PMSVersion = objSysConfig.GetConfig("PMSVersion")

        '------------------- START De-Serialization --------------------------
        userContext = New UserContext("", "")
        UsrCred = userContext.DeSerialize(UniqueLogid)
        '------------------- END De-Serialization --------------------------

        If IsNothing(UsrCred) Then
            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in Feedback page" & vbCrLf)
            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
        End If

        Dim GuestId As String = UsrCred.Item("grcid")

        userContext = New UserContext(UsrCred.Item("roomNo"), UsrCred.Item("guestname"), PMSName, PMSVersion, "", UsrCred.Item("machineId"))

        userContext.item("inetservice") = Request.Form("optintservice")
        userContext.item("vpn") = Request.Form("optvpn")
        userContext.item("suggestion") = Request.Form("txtsuggestion")
        userContext.item("grcid") = GuestId

        Dim ObjLog As LogInOutService
        ObjLog = LogInOutService.getInstance


        If Request.Form("optintservice") = Nothing Then
            Dim ErrMsg As String = ObjLog.GetErrorDesc("16")
            MesgBox(ErrMsg)
        Else

            If Request.Form("optvpn") = Nothing Then
                userContext.item("vpn") = "NS"
            End If

            ObjLog = LogInOutService.getInstance
            ObjLog.AddFeedback_Taj(userContext)


            Dim objMail As MailService
            objMail = MailService.getInstance
            objMail.SendAdminMail(userContext, ErrTypes.FB)

            Dim ErrMsg As String = ObjLog.GetErrorDesc("15")
            ' MesgBox(ErrMsg)
            Response.Redirect("userinfo.aspx?encry=" & url)
        End If


    End Sub

    Protected Sub imgbtnclose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnclose.Click
        'MesgBox("Feedback not submitted")
        'imgbtnclose.Attributes.Add("onclick", "return closewindow();")
    End Sub

End Class
