Imports PMSPkgV1_0
Public Class SndRevBytes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here

        Dim roomNo, MACaddress As String
        roomNo = Request("RN")
        MACaddress = Request("MA")

        Dim userContext As New UserContext(roomNo, MACaddress)
        Dim gateWayQryResult As NdxQueryGatewayResults
        Dim gateWay As IGatewayService
        Dim gateWayFact As GatewayServiceFactory


        gateWayFact = GatewayServiceFactory.getInstance
        gateWay = gateWayFact.getGatewayService("FIDELIO", "2.3.2")
        gateWayQryResult = gateWay.query(userContext)
        Response.Write(gateWayQryResult.ndxDateVolume)
        Response.End()
    End Sub

End Class