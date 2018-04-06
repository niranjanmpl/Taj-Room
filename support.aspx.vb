Imports PMSPkgV1_0
Public Class support
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmbplans As System.Web.UI.WebControls.DropDownList
    Protected WithEvents validtxtroomno As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents validtxtlastname As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents lbldisplans As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Public ExtensionNumber, currency, TotBandWidth As String
    Protected pNormalCouponPlans As New HtmlGenericControl
    Protected pBulkExtraPlans As New HtmlGenericControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Dim objSysConfig As New CSysConfig
        Me.PageTitle.InnerText = objSysConfig.GetConfig("HotelName")
        'lblProNEWS.Text = objSysConfig.GetConfig("PromotionNEWS")

        Dim objPlan As New CPlan
        Dim plans As DataSet
        Dim indx As Integer
        Dim planName, planAmount, opstr As String

        currency = objSysConfig.GetConfig("Currency")
        ExtensionNumber = objSysConfig.GetConfig("HlpDskExt")
        TotBandWidth = objSysConfig.GetConfig("HotelTotBandwidth")

        Dim FreePlan As String = objSysConfig.GetConfig("FreePlanId")
        Dim PremiumPlan As String = objSysConfig.GetConfig("PremiumFreePlanId")

        opstr = ""

        'plansFree = objPlan.getAllPlansOnlyFREE(PLANTYPES.FREE, PLANSTATUS.ACTIVEONLY)


        'Dim planbandwidth As String
        'For indx = 0 To plansFree.Tables(0).Rows.Count - 1

        '    Dim FreePlanid As String = plansFree.Tables(0).Rows(indx).Item("PlanId")
        '    If (FreePlanid = FreePlan) Or (FreePlanid = PremiumPlan) Then
        '        planName = plansFree.Tables(0).Rows(indx).Item("PlanName")
        '        planbandwidth = plansFree.Tables(0).Rows(indx).Item("PlanBandUp")
        '        '   opstr = opstr & planName & "&nbsp;" & planbandwidth & " Kbps &nbsp;" & "<br>"
        '        opstr = opstr & planName & "nbsp;" & "<br>"
        '    End If

        'Next
        opstr = opstr & "Standard Plan as follows, <br>"
        opstr = opstr & "&nbsp;&nbsp;&nbsp;&nbsp;Everyday 24 Hours"

        opstr = opstr & "<br><br>"

        opstr = opstr & "Premium Plan as follows, <br>"
        plans = objPlan.getAllPlans(PLANTYPES.ROOM, PLANSTATUS.ACTIVEONLY)

        For indx = 0 To plans.Tables(0).Rows.Count - 1
            planName = plans.Tables(0).Rows(indx).Item("PlanName")
            planAmount = plans.Tables(0).Rows(indx).Item("PlanAmount")
            'planbandwidth = plans.Tables(0).Rows(indx).Item("PlanBandUp")
            'opstr = opstr & planName & "&nbsp;" & currency & "&nbsp;" & planAmount & "&nbsp;" & planbandwidth & " Kbps &nbsp;" & "<br>"
            If planAmount <> "0" Then
                opstr = opstr & "&nbsp;&nbsp;&nbsp;&nbsp;" & planName & "&nbsp;" & currency & "&nbsp;" & planAmount & "&nbsp;" & "<br>"
            End If

        Next
        pRoomPlans.InnerHtml = opstr

        plans.Dispose()

        'plans = objPlan.getAllPlans(PLANTYPES.NORMALCOUPON, PLANSTATUS.ACTIVEONLY)

        'opstr = ""
        'For indx = 0 To plans.Tables(0).Rows.Count - 1
        '    planName = plans.Tables(0).Rows(indx).Item("PlanName")
        '    planAmount = plans.Tables(0).Rows(indx).Item("PlanAmount")
        '    opstr = opstr & planName & "&nbsp;" & currency & "&nbsp;" & planAmount & "<br>"
        'Next
        'pNormalCouponPlans.InnerHtml = opstr
    End Sub

End Class