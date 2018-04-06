Imports PMSPkgV1_0
Public Class GetPlaninfo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Dim planId, strhtml As String
        'Dim totalamount As Double
        'Dim servicetax As Double
        'Dim luxurytax As Double
        'Dim currency As String

        Dim objPlan As New CPlan
        Dim objSysConfig As New CSysConfig

        planId = Request.QueryString("planId")
        'Dim Stax, Ltax, TaxDisplay As String


        Dim LSGPlanId As String = objSysConfig.GetConfig("LongStayPlanId")

        If UCase(planId) = "NONE" Then
            'strhtml = "<table border='0' cellpadding='0' cellspacing='0' width='60%'>"
            'strhtml = strhtml & "<tr><td height='22' colspan='3' align='center' valign='middle'><strong>&nbsp;</strong></td></tr>"
            'strhtml = strhtml & "<tr><td height='19' colspan='3' align='left'><span class='textbodyA'>&nbsp;</span></td></tr>"
            'strhtml = strhtml & "<tr><td height='19' colspan='3' align='left' class='textbodyA'>&nbsp;</td></tr>"
            'strhtml = strhtml & "<tr><td height='25' colspan='3' align='left' class='textbodyA'>&nbsp;</td></tr>"
            'strhtml = strhtml & "</table>"
            Response.Write(" ")

        ElseIf planId = LSGPlanId Then

            'strhtml = "<table class='TextClr' border='0' cellpadding='0' cellspacing='0' width='100%' height='60%'>"
            ''strhtml = strhtml & "<tr><td><asp:label id='lblLongStay' runat='server' CssClass='style1' Font-Bold='True' Width='212px'>Please enter the number of days WIFI access is required</asp:label></td>"
            ''strhtml = strhtml & "<td><table><tr><td><INPUT style='WIDTH: 96px; HEIGHT: 22px' size='10' id='txtLongStay' onkeyup ='amount(this.value);' type='text' name='txtLongStay' runat='server'></td>"
            ' ''strhtml = strhtml & "<td><asp:regularexpressionvalidator id='RegularExpressionValidator1' runat='server' CssClass='style1' Width='154px' ControlToValidate='txtLongStay' ValidationExpression='^[0-9]*$' ErrorMessage='Numbers Only'></asp:regularexpressionvalidator></td>"
            ''strhtml = strhtml & "</tr></table></td></tr>"
            ''strhtml = strhtml & "<TR><TD style='HEIGHT: 1px'><asp:label id='Label1' runat='server' CssClass='style1' Font-Bold='True' Width='208px'>Plan Amount INR</asp:label></TD>"
            ''strhtml = strhtml & "<TD class='style1'><INPUT style='WIDTH: 96px; HEIGHT: 22px' readOnly type='text' size='10' name='txtAmount'disabled></TD></TR></table>"
            ' ''strhtml = strhtml & "<tr><td style='HEIGHT: 3px'></td><td style='HEIGHT: 3px'><INPUT class='style1' id='btnSubmit' type='button' value='Submit' name='Submit' runat='server'></td></tr>"
            'strhtml = strhtml & "<tr><td>&nbsp;</td><td align='center' colspan='4' class='style4'>&nbsp;Plan : Long Stay</td></tr>"
            'strhtml = strhtml & "<tr><td>&nbsp;</td><td></td><td colspan='3' class='style4' >" & "For the guest who needs more than 7 days of access" & "</td></tr></table>"
            Response.Write("")

        ElseIf objPlan.getPlaninfo(planId, "", "") Then


            Response.Write(objPlan.PlanDescription)

            'currency = objSysConfig.GetConfig("Currency")

            'Stax = objSysConfig.GetConfig("ServiceTax")
            'Ltax = objSysConfig.GetConfig("LuxuaryTax")


            'TaxDisplay = objSysConfig.GetConfig("IsEnableTaxDisplay")

            'If UCase(TaxDisplay) = "NO" Then
            '    totalamount = CDbl(objPlan.planAmount)
            'Else
            '    servicetax = CDbl(objPlan.planAmount) * CDbl(Stax) / 100
            '    luxurytax = CDbl(objPlan.planAmount) * CDbl(Ltax) / 100
            '    totalamount = CDbl(objPlan.planAmount) + servicetax + luxurytax
            'End If

            'If UCase(TaxDisplay) = "NO" Then
            '    strhtml = "<table border='0' class='TextClr' cellpadding='0'  cellspacing='0'  width='60%'>"
            '    'strhtml = strhtml & "<tr><td width='11%' height='19' align='left'>&nbsp;</td><td width='26%'>&nbsp;</td><td width='8%'>&nbsp; </td><td width='55%' align='left' class='style3' valign='middle'><strong>Plan " & objPlan.planName & "</strong></td></tr>"
            '    strhtml = strhtml & "<tr><td align='center'>&nbsp;</td><td height='19' align='center'><span class='style1'>Duration</span></td><td align='center'class='style1'>:</td><td align='left' class='style1'>" & objPlan.planTimeinHMS & " </td></tr>"
            '    strhtml = strhtml & "<tr><td align='center'>&nbsp;</td><td height='19' align='center' class='style1'>Validity</td><td align='center' class='style1'>:</td><td align='left' class='style1'>" & objPlan.planValidityinHMS & " </td></tr>"
            '    strhtml = strhtml & "<tr><td align='center'>&nbsp;</td><td height='19' align='center' class='style1'>Amount</td><td align='center' class='style1'>:</td><td align='left'><table height='19' border='0' cellpadding='0' cellspacing='0'><tr><td align='center' class='style1'>" & objPlan.planAmount & " &nbsp;</td><td align='left' class='style1'>" & currency & " </td></tr></table></td></tr>"
            '    strhtml = strhtml & "</table>"
            '    Response.Write(strhtml)
            'Else
            '    strhtml = "<table border='0' class='TextClr' cellpadding='0'  cellspacing='0' width='60%'>"
            '    'strhtml = strhtml & "<tr><td width='11%' height='19' align='left'>&nbsp;</td><td width='26%'>&nbsp;</td><td width='8%'>&nbsp; </td><td width='55%' align='left' class='style3' valign='middle'><strong>Plan " & objPlan.planName & "</strong></td></tr>"
            '    strhtml = strhtml & "<tr><td align='left'>&nbsp;</td><td height='19' align='left'><span class='style1'>Duration</span></td><td align='center'class='style1'>:</td><td align='left' class='style1'>" & objPlan.planTimeinHMS & " </td></tr>"
            '    strhtml = strhtml & "<tr><td align='left'>&nbsp;</td><td height='19' align='left' class='style1'>Validity</td><td align='center' class='style1'>:</td><td align='left' class='style1'>" & objPlan.planValidityinHMS & " </td></tr>"
            '    strhtml = strhtml & "<tr><td align='left'>&nbsp;</td><td height='19' align='left' class='style1'>Amount</td><td align='center' class='style1'>:</td><td align='left' class='style1'>" & objPlan.planAmount & " </td></tr>"
            '    strhtml = strhtml & "<tr><td align='left'>&nbsp;</td><td height='19' align='left' class='style1'>Service Tax</td><td align='center' class='style1'>:</td><td align='left' class='style1'>" & Stax & " </td></tr>"
            '    strhtml = strhtml & "<tr><td align='left'>&nbsp;</td><td height='19' align='left' class='style1'>Luxury Tax</td><td align='center' class='style1'>:</td><td align='left' class='style1'>" & Ltax & " </td></tr>"
            '    strhtml = strhtml & "<tr><td align='left'>&nbsp;</td><td height='19' align='left' class='style1'>Total Amount</td><td align='center' class='style1'>:</td><td align='left'><table height='19' border='0' cellpadding='0' cellspacing='0'><tr><td align='center' class='style1'>" & totalamount & " &nbsp;</td><td align='left' class='style1'>" & currency & " </td></tr></table></td></tr>"
            '    strhtml = strhtml & "</table>"
            '    Response.Write(strhtml)
            'End If

        Else
            'strhtml = "<table border='0' cellpadding='0' cellspacing='0' width='60%'>"
            'strhtml = strhtml & "<tr><td height='22' colspan='3' align='center' valign='middle'><strong>&nbsp;</strong></td></tr>"
            'strhtml = strhtml & "<tr><td height='19' colspan='3' align='left'><span class='textbodyA'>&nbsp;</span></td></tr>"
            'strhtml = strhtml & "<tr><td height='19' colspan='3' align='left' class='textbodyA'>&nbsp;</td></tr>"
            'strhtml = strhtml & "<tr><td height='25' colspan='3' align='left' class='textbodyA'>&nbsp;</td></tr>"
            'strhtml = strhtml & "</table>"
            Response.Write("")
        End If
        Response.End()
    End Sub

End Class