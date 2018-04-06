Imports PMSPkgV1_0
Public Class Process
    Inherits System.Web.UI.Page
    Dim TempdevType, RN, MAC, plan As String
    Dim Room, Name, OS As String

    Public url As String
    Dim LoginFrom As String
    Protected WithEvents hdtype As System.Web.UI.HtmlControls.HtmlInputHidden
    Dim UniqueLogid As String
    Dim userContext As UserContext
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Put user code to initialize the page here
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        url = Request.QueryString("encry")

        UniqueLogid = commonFun.DecrptQueryString("logid", url)

        If UniqueLogid = "" Or UniqueLogid = Nothing Then

            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Logid missed in PlanSelection" & vbCrLf)
            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
        End If

        LoginFrom = Request.QueryString("ProcessType")

        '------------------- START De-Serialization --------------------------
        Dim UsrCred As Hashtable = Nothing
        UserContext = New UserContext("", "")
        UsrCred = userContext.DeSerialize(UniqueLogid)

        If IsNothing(UsrCred) Then

            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- DeSerialize is EMPTY in PlanSelection button click" & vbCrLf)
            Response.Redirect("UserError.aspx?finderror=ERROR&ErrorCode=9")
        End If
        '------------------- END De-Serialization --------------------------


        plan = Request.QueryString("plan")

        If plan = Nothing Or plan = "-1" Or plan = "0" Then

            Dim ObjLog As LoggerService
            ObjLog = LoggerService.gtInstance

            ObjLog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: Process" & vbCrLf _
                                                         & "----------------------- PlanId is Empty" & vbCrLf _
                                                         & "----------------------- User I/P: Room No: " & Room & " -- Last Name: " & Name & " -- VLAN ID: " & RN & " -- MAC: " & MAC & vbCrLf)

            Response.Redirect("UserError.aspx?encry=" & url & "&Msg=Sorry for the inconvenience please call our Cyber Support")
        End If

        Dim GuestId As String = UsrCred.Item("grcid")
        If GuestId <> "" Or GuestId <> Nothing Then
            Room = UsrCred.Item("roomNo")
            Name = UsrCred.Item("guestname")
            MAC = UsrCred.Item("machineId")

            Dim ObjLog As LoggerService
            ObjLog = LoggerService.gtInstance

            ObjLog.write2LogFile("LoginFlow", vbCrLf & Now() & " -- Landed Page: Process" & vbCrLf _
                                                         & "----------------------- User I/P: Room No: " & Room & " -- Last Name: " & Name & " -- VLAN ID: " & RN & " -- MAC: " & MAC & vbCrLf)


            If UCase(LoginFrom) = "MIFILOGIN" Then
                ObjLog.write2LogFile(Room, vbCrLf & Now() & " -- Process Page To Miiflogin page -- MAC: " & MAC)
                hdurl.Value = OS
            Else
                ObjLog.write2LogFile(Room, vbCrLf & Now() & " -- Process Page to PlanSelection Page -- MAC: " & MAC)
                hdurl.Value = "PlanSelection.aspx?encry=" & url & "&ProcessType=process&Plan=" & plan
            End If
        Else
            Response.Redirect("UserError.aspx?encry=" & url & "&Msg=Sorry for the inconvenience please call our Cyber Support")
        End If
    End Sub

End Class