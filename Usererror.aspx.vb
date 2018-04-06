Imports PMSPkgV1_0
Imports System.Threading


Public Class Usererror
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents btnback As System.Web.UI.HtmlControls.HtmlInputButton


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Public url As String
    Public Errmessage As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            'Put user code to initialize the page here
            Dim objSysConfig As New CSysConfig
            Dim commonFun As PMSCommonFun
            Dim finderror, ExtNo As String
            Me.PageTitle.InnerText = objSysConfig.GetConfig("HotelName")

            commonFun = PMSCommonFun.getInstance
            url = commonFun.BrowserQueryString(Request, 2)

            finderror = Request.QueryString("finderror")
            ExtNo = objSysConfig.GetConfig("HlpDskExt")


            '----------- START Fetch the Error Message from DB -----------------
            Dim ErrMsg As String = ""
            If Request.QueryString("ErrorCode") <> "" Then
                Dim ObjLog As LogInOutService
                ObjLog = LogInOutService.getInstance
                ErrMsg = ObjLog.GetErrorDesc(Request.QueryString("ErrorCode"))
                If ErrMsg <> "0" Then
                    msgPara.InnerText = ErrMsg
                Else
                    msgPara.InnerText = "Dear Guest, Sorry for the inconvenience please call our Cyber Support at Ext." & ExtNo
                End If
            Else
                msgPara.InnerText = Request.QueryString("Msg")
            End If

            Dim ErrorCode As String = Request.QueryString("ErrorCode")
            If (ErrorCode = 1 Or ErrorCode = 2 Or ErrorCode = 5 Or ErrorCode = 6 Or ErrorCode = 7) Or (UCase(finderror) = "ERROR") Then
                imbbtnback.Visible = False
            Else
                imbbtnback.Visible = True
            End If

            If Not IsPostBack Then

                Dim ObjElog As LoggerService
                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: UserError Page" & vbCrLf _
                                            & "------------------------ Error Code: " & ErrorCode)
            End If

        Catch ex As ThreadAbortException

        Catch ex As Exception
            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.writeExceptionLogFile("Ex-UserError", ex)

        End Try

    End Sub

  
    'Protected Sub imbbtnback_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbbtnback.Click

    '    If Request.QueryString("findurl") = "checkpassword" Then
    '        Response.Redirect("CheckPassword.aspx?" & url)
    '    ElseIf Request.QueryString("findurl") = "checkpwdupgrade" Then
    '        Response.Redirect("CheckPwdUpgrade.aspx?" & url)
    '    ElseIf Request.QueryString("LoginFrom") = "Upgrade" Then
    '        Response.Redirect("Upgrade.aspx?" & url)

    '    Else
    '        Dim ErrorCode As String = Request.QueryString("ErrorCode")
    '        If ErrorCode = "2" Then ''If guest not even login but try to upgrade, means..redirect to some URL
    '            Response.Redirect("http://www.yahoo.com")
    '        Else
    '            Response.Redirect("mifilogin.aspx?" & url)
    '        End If
    '    End If


    'End Sub

    Protected Sub imbbtnback_Click(sender As Object, e As EventArgs) Handles imbbtnback.Click

        If Request.QueryString("findurl") = "checkpassword" Then
            Response.Redirect("CheckPassword.aspx?" & url)
        ElseIf Request.QueryString("findurl") = "checkpwdupgrade" Then
            Response.Redirect("CheckPwdUpgrade.aspx?" & url)
        ElseIf Request.QueryString("LoginFrom") = "Upgrade" Then
            Response.Redirect("Upgrade.aspx?" & url)
        ElseIf Request.QueryString("findurl") = "memberlogin" Then
            Response.Redirect("MemberLogin.aspx?" & url)
        ElseIf Request.QueryString("findurl") = "nrlogin" Then
            Response.Redirect("CouponOTP.aspx?" & url)

        Else
            Dim ErrorCode As String = Request.QueryString("ErrorCode")
            If ErrorCode = "2" Then ''If guest not even login but try to upgrade, means..redirect to some URL
                Response.Redirect("http://www.yahoo.com")
            Else
                Response.Redirect("mifilogin.aspx?" & url)
            End If
        End If

    End Sub
End Class