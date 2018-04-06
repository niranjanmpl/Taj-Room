Imports PMSPkgV1_0

Public Class Block
    Inherits System.Web.UI.Page

    Protected WithEvents lblstandev As Global.System.Web.UI.WebControls.Label

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1))
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()

        'Dim objSysConfig As New CSysConfig
        'Me.PageTitle.InnerText = objSysConfig.GetConfig("HotelName")

        'Dim standardplan As String = ""
        ''--------- START get standard plan device limit from plans table ---------------
        'standardplan = objSysConfig.GetConfig("FreePlanId")

        'If standardplan <> "" Or standardplan <> Nothing Then
        '    Dim objPlan As New CPlan
        '    objPlan.getPlaninfo(standardplan, "", "")
        '    lblstandev.Text = objPlan.TotalConCurrentDev
        '    ' lblfreeplanname.Text = objPlan.PlanDescription
        'End If

        '--------- END get standard plan device limit from plans table ----------------


    End Sub

    Protected Sub imgbtnlogin_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnlogin.Click

        Dim ObjElog As LoggerService
        ObjElog = LoggerService.gtInstance
        ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Click the Upgrade button in BLOCK page")

        HttpContext.Current.Response.Redirect("http://www.upgrademe.in")
    End Sub
End Class