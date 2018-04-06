Imports PMSPkgV1_0
Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Dim url, eurl As String
        Dim commonFun As PMSCommonFun
        Dim objSysConfig As New CSysConfig
        commonFun = PMSCommonFun.getInstance

        url = commonFun.EncrptQueryString(Request)

        Response.Redirect("mifilogin.aspx?encry=" & url)
    End Sub

End Class