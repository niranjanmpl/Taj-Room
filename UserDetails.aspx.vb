Imports PMSPkgV1_0
Public Class UserDetails
    Inherits System.Web.UI.Page
    Public IsAvil As Integer = 0
    Dim obj As GuestService

    Protected WithEvents hdstatus As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try




            If Not IsPostBack() Then

                Dim objPlan As New CPlan
                obj = GuestService.getInstance
                Dim obj1 As New Mifilogin
                ''--------------------- START Bind Country code -------------
                'Dim CName, Ccode As String
                'Dim Ds As DataSet
                'Ds = objPlan.getCountryCode()
                'For indx = 0 To Ds.Tables(0).Rows.Count - 1
                '    CName = Ds.Tables(0).Rows(indx).Item("CName")
                '    Ccode = Ds.Tables(0).Rows(indx).Item("CCode")
                '    CName = CName.Trim() & " (" & Ccode.Trim() & ")"
                '    drpcountrycode.Items.Add(New ListItem(CName, Ccode))

                '    If UCase(CName) = "INDIA (+91)" Then
                '        drpcountrycode.SelectedIndex = indx
                '    End If

                'Next


                ''--------------------- END Bind Country code -------------
                Dim GuestID As String = Session("guestid")

                If GuestID <> "" Then
                    Dim ds1 As New DataSet
                    ds1 = obj.CheckEmail(GuestID)

                    Dim cfa2 As String = "NULL"

                    
                    If IsDBNull(ds1.Tables(0).Rows(0).Item("GuestCFA2")) Or IsDBNull(ds1.Tables(0).Rows(0).Item("GuestCFA3")) Then

                        'If cfa2 = "NULL" Or cfa2 = "NULL" Then
                        If IsDBNull(ds1.Tables(0).Rows(0).Item("GuestCFA2")) Then
                            txtEmail.Text = ""
                            txtMobileNo.Text = ds1.Tables(0).Rows(0).Item("GuestCFA3").ToString()

                        End If
                        If IsDBNull(ds1.Tables(0).Rows(0).Item("GuestCFA3")) Then
                            txtEmail.Text = ds1.Tables(0).Rows(0).Item("GuestCFA2").ToString()
                            txtMobileNo.Text = ""

                        End If

                        hdguestid.Value = ds1.Tables(0).Rows(0).Item("GuestId")
                        lblHeading.Text = "Dear Guest, Please provide the following information for us to communicate and provide you with our best services."
                        btnyes.Text = "Save and continue"
                        'txtEmail.Text = ""
                        'txtMobileNo.Text = ""

                        txtEmail.ReadOnly = False
                        txtMobileNo.ReadOnly = False
                        drpcountrycode.Visible = True
                        textCCode.Visible = True

                        btnyesAvilData.Visible = False
                        btnyesUpdate.Visible = False

                        txtEmail.BackColor = Drawing.Color.White
                        txtMobileNo.BackColor = Drawing.Color.White

                    ElseIf ds1.Tables(0).Rows(0).Item("GuestCFA2") = "" Or ds1.Tables(0).Rows(0).Item("GuestCFA3") = "" Then

                        If ds1.Tables(0).Rows(0).Item("GuestCFA2") = "" Then
                            txtEmail.Text = ""
                            txtMobileNo.Text = ds1.Tables(0).Rows(0).Item("GuestCFA3").ToString()

                        End If
                        If ds1.Tables(0).Rows(0).Item("GuestCFA3") = "" Then
                            txtEmail.Text = ds1.Tables(0).Rows(0).Item("GuestCFA2").ToString()
                            txtMobileNo.Text = ""

                        End If

                        hdguestid.Value = ds1.Tables(0).Rows(0).Item("GuestId")
                        lblHeading.Text = "Dear Guest, Please provide the following information for us to communicate and provide you with our best services."
                        btnyes.Text = "Save and continue"
                        'txtEmail.Text = ""
                        'txtMobileNo.Text = ""

                        txtEmail.ReadOnly = False
                        txtMobileNo.ReadOnly = False
                        drpcountrycode.Visible = True
                        textCCode.Visible = True

                        btnyesAvilData.Visible = False
                        btnyesUpdate.Visible = False

                        txtEmail.BackColor = Drawing.Color.White
                        txtMobileNo.BackColor = Drawing.Color.White

                    Else
                        hdguestid.Value = ds1.Tables(0).Rows(0).Item("GuestId")
                        txtEmail.Text = ds1.Tables(0).Rows(0).Item("GuestCFA2")
                        drpcountrycode.Text = ds1.Tables(0).Rows(0).Item("GuestCFA7")
                        drpcountrycode.Visible = True
                        txtMobileNo.Text = ds1.Tables(0).Rows(0).Item("GuestCFA3")
                        'cfa3 = ds1.Tables(0).Rows(0).Item("GuestCFA3").ToString()
                        'If (cfa3 = "") Then
                        'cfa3 = ""
                        'End If
                        'txtMobileNo.Text = cfa3 '
                        btnyes.Text = "Save and continue"
                        btnyes.Visible = True
                        btnyesAvilData.Visible = False
                        btnyesUpdate.Visible = False

                        textCCode.Visible = True
                        txtEmail.ReadOnly = False
                        txtMobileNo.ReadOnly = False

                        txtEmail.BackColor = Drawing.Color.GhostWhite
                        ' txtEmail.BorderStyle = BorderStyle.None

                        txtMobileNo.BackColor = Drawing.Color.GhostWhite


                        lblHeading.Text = "Dear Guest, Please re-confirm the information below to communicate and provide you with our best services."
                    End If
                    
                End If

                '--------------------- START Bind Country code -------------
                Dim CName, Ccode As String
                Dim Ds As DataSet
                Ds = objPlan.getCountryCode()
                For indx = 0 To Ds.Tables(0).Rows.Count - 1
                    CName = Ds.Tables(0).Rows(indx).Item("CName")
                    Ccode = Ds.Tables(0).Rows(indx).Item("CCode").ToString().Trim()
                    CName = CName.Trim() & " (" & Ccode.Trim() & ")"
                    drpcountrycode.Items.Add(New ListItem(CName, Ccode))

                    If UCase(CName) = "INDIA (+91)" Then
                        drpcountrycode.SelectedIndex = indx
                    End If
                  

                Next


                Dim x As Integer = 5
                While (x > 1)
                    Dim mobtemp As String = txtMobileNo.Text.Substring(0, x)
                    If txtMobileNo.Text.Contains(mobtemp) Then
                        Dim it As ListItem = drpcountrycode.Items.FindByValue(mobtemp)
                        If Not it Is Nothing Then
                            txtMobileNo.Text = txtMobileNo.Text.Replace(mobtemp, "")
                            drpcountrycode.SelectedValue = mobtemp
                        End If
                    End If
                    x = x - 1
                End While
                '--------------------- END Bind Country code -------------

            Else

                Dim clickupdate As String = hdstatus.Value

                If clickupdate = "30" Then

                    Dim objPlan As New CPlan
                    obj = GuestService.getInstance
                    Dim obj1 As New Mifilogin
                    '--------------------- START Bind Country code -------------
                    Dim CName, Ccode As String
                    Dim Ds As DataSet
                    Ds = objPlan.getCountryCode()
                    For indx = 0 To Ds.Tables(0).Rows.Count - 1
                        CName = Ds.Tables(0).Rows(indx).Item("CName")
                        Ccode = Ds.Tables(0).Rows(indx).Item("CCode").ToString().Trim()
                        CName = CName.Trim() & " (" & Ccode.Trim() & ")"
                        drpcountrycode.Items.Add(New ListItem(CName, Ccode))

                        If UCase(CName) = "INDIA (+91)" Then
                            drpcountrycode.SelectedIndex = indx
                        End If

                    Next


                    '--------------------- END Bind Country code -------------
                    Dim GuestID As String = Session("guestid")

                    If GuestID <> "" Then

                        hdguestid.Value = GuestID
                        lblHeading.Text = "Dear Guest, Please provide the following information for us to communicate and provide you with our best services."
                        btnyes.Text = "Save and continue"
                        txtEmail.Text = ""
                        txtMobileNo.Text = ""

                        btnyes.Visible = True
                        btnyesAvilData.Visible = False
                        btnyesUpdate.Visible = False

                        drpcountrycode.Visible = True
                        txtEmail.ReadOnly = False
                        txtMobileNo.ReadOnly = False
                        textCCode.Visible = True

                        txtEmail.BackColor = Drawing.Color.White
                        txtMobileNo.BackColor = Drawing.Color.White

                    End If

                End If

            End If

        Catch ex As Exception
            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.writeExceptionLogFile("Ex-UsrDetails", ex)
        End Try


    End Sub

    Protected Sub btnyes_Click(sender As Object, e As EventArgs) Handles btnyes.Click, btnyesAvilData.Click, btnyesUpdate.Click

        Dim guestid As String = hdguestid.Value

        Try

            Dim Ilike As Boolean = CheckBoxlike.Checked
            Dim IAgree As Boolean = CheckBoxIAgree.Checked

            Dim T_CCode As String = drpcountrycode.SelectedValue
            obj = GuestService.getInstance
            If txtEmail.Text <> "" And txtMobileNo.Text <> "" Then
                Dim ObjElog As LoggerService
                ObjElog = LoggerService.gtInstance

                'update emailid and mobile number for guest
                obj.UpdateEmailAndMobNo(guestid, txtEmail.Text, txtMobileNo.Text, T_CCode, IAgree, Ilike)

                Dim loge As LoggerService = LoggerService.gtInstance
                loge.write2LogFile("UserDetails update", vbCrLf & Now() & " -- Landed Page: UserDetails" & vbCrLf _
                                                          & "-----------------------Guest ID: " & guestid & vbCrLf _
                                                             & "-----------------------Email ID: " & txtEmail.Text & vbCrLf _
                                                                 & "----------------------- Mobile: " & txtMobileNo.Text & vbCrLf)


                ScriptManager.RegisterStartupScript(Me.Page, Page.[GetType](), "text", "OpenYes()", True)
            End If

        Catch ex As Exception
            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance
            ObjElog.writeExceptionLogFile("Ex-UsrDetails-btnClick", ex)
        End Try

    End Sub

    
End Class