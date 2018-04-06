Imports System.Net
Imports System.Net.Cache
Imports System.IO
Imports System.Xml
Imports PMSPkgV1_0

Public Structure SMSResponse
    Dim SMSSentStatus As SMSSENTSTAUS
    Dim SMSGUID As String
    Dim SMSErrorCode As String
    Dim SMSErrorDesc As String
End Structure

Public Enum SMSSENTSTAUS
    SUCCESS
    FAILURE
End Enum
Public Class PushSMSService
    Private Shared gtPushSMSServiceInst As PushSMSService = Nothing

    Dim SMSIPAddr As String
    Dim SMSUsrName As String
    Dim SMSPwd As String
    Dim GSMID As String

    Private Sub New()
        getSMSConfig()
    End Sub

    Public Shared Function getInstance() As PushSMSService
        If gtPushSMSServiceInst Is Nothing Then gtPushSMSServiceInst = New PushSMSService
        Return gtPushSMSServiceInst
    End Function

    Public Sub ChangeInstConfig()
        getSMSConfig()
    End Sub

    Private Sub getSMSConfig()
        'Dim objSysConfig As New CSysConfig

        'SMSIPAddr = Trim(objSysConfig.GetConfig("SMSIP"))
        'SMSUsrName = Trim(objSysConfig.GetConfig("SMSUserName"))
        'SMSPwd = Trim(objSysConfig.GetConfig("SMSPassword"))
        'GSMID = Trim(objSysConfig.GetConfig("GSMID"))

        SMSIPAddr = "api.myvaluefirst.com/psms/servlet/psms.Eservice2"
        SMSUsrName = "microsensenpr"
        SMSPwd = "micxml55"
        GSMID = "MSENSE"
    End Sub



    Public Function SendSMSToMobile_ValueFirst(ByVal MobileNo As String, ByVal SMSMessage As String) As SMSResponse

        Dim tmp_SMSResponse As SMSResponse

        tmp_SMSResponse.SMSSentStatus = SMSSENTSTAUS.FAILURE
        tmp_SMSResponse.SMSGUID = "-1"
        tmp_SMSResponse.SMSErrorCode = ""
        tmp_SMSResponse.SMSErrorDesc = ""

        Dim SendSMSURL As String = ""
        Dim ObjElog As LoggerService

        Dim SMSSendTime As Date = Now()

        Try

            SendSMSURL = "http://" & SMSIPAddr
            SMSMessage = "Dear Guest, Your internet access code is " & SMSMessage & " Thank You"
            Dim RequestURL As String
            RequestURL = SendSMSURL.Trim() & "?data=<?xml version=""1.0"" encoding=""ISO-8859-1""?>"
            RequestURL = RequestURL & "<!DOCTYPE MESSAGE SYSTEM ""http://127.0.0.1/psms/dtd/message.dtd"" >"
            RequestURL = RequestURL & "<MESSAGE>"
            RequestURL = RequestURL & "<USER USERNAME=""" & SMSUsrName & """ PASSWORD=""" & SMSPwd & """/>"
            RequestURL = RequestURL & "<SMS UDH=""0"" CODING=""1"" TEXT=""" & SMSMessage & """ PROPERTY="""" ID=""1"" DLR=""1"" VALIDITY=""0"">"
            RequestURL = RequestURL & "<ADDRESS FROM=""" & GSMID & """ TO=""" & MobileNo & """ SEQ=""10"" />"
            RequestURL = RequestURL & "</SMS></MESSAGE>"
            RequestURL = RequestURL & "&action=send"

            Dim httpReq As HttpWebRequest = DirectCast(WebRequest.Create(RequestURL), HttpWebRequest)
            httpReq.CachePolicy = New HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore)
            httpReq.KeepAlive = False
            httpReq.Timeout = 20 * 1000
            Dim httpRes As HttpWebResponse = DirectCast(httpReq.GetResponse(), HttpWebResponse)
            Dim responseFromServer As String

            If httpRes.StatusCode = HttpStatusCode.OK Then
                Dim dataStream As Stream = httpRes.GetResponseStream()
                Dim reader As New StreamReader(dataStream)
                responseFromServer = reader.ReadToEnd()
                reader.Close()

                tmp_SMSResponse = SMSresParser(responseFromServer)

                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(MobileNo, Now() & " --- SMS GUID: " & tmp_SMSResponse.SMSGUID & vbCrLf _
                                    & " ------------------------ SMS ErrorCode :" & tmp_SMSResponse.SMSErrorCode.ToString() & vbCrLf _
                                    & " ------------------------ SMS ErrorDesc :" & tmp_SMSResponse.SMSErrorDesc.ToString() & vbCrLf _
                                    & " ------------------------ SMS STATUS :" & tmp_SMSResponse.SMSSentStatus.ToString())


                Return tmp_SMSResponse

            Else
                tmp_SMSResponse.SMSSentStatus = SMSSENTSTAUS.FAILURE
                tmp_SMSResponse.SMSGUID = "-1"
                tmp_SMSResponse.SMSErrorCode = "-1"
                tmp_SMSResponse.SMSErrorDesc = "SMS_GWERR_" & httpRes.StatusCode.ToString

                ObjElog = LoggerService.gtInstance
                ObjElog.write2LogFile(MobileNo, Now() & " --- SMS API Service is not working -- HTTP Error Code: " & httpRes.StatusCode.ToString & vbCrLf)

                Return tmp_SMSResponse
            End If

        Catch EX As Exception

            ObjElog = LoggerService.gtInstance
            ObjElog.write2LogFile(MobileNo, Now() & " --- " & EX.Message.ToString())

            tmp_SMSResponse.SMSSentStatus = SMSSENTSTAUS.FAILURE
            tmp_SMSResponse.SMSGUID = "-1"
            tmp_SMSResponse.SMSErrorCode = "-1"
            tmp_SMSResponse.SMSErrorDesc = "SMS_EX_" & EX.Message.ToString()
            Return tmp_SMSResponse
        End Try
    End Function

    Private Function SMSresParser(ByVal smsresp As String) As SMSResponse

        Dim tmp_SMSResponse As SMSResponse

        tmp_SMSResponse.SMSSentStatus = SMSSENTSTAUS.FAILURE
        tmp_SMSResponse.SMSGUID = "-1"
        tmp_SMSResponse.SMSErrorDesc = ""
        tmp_SMSResponse.SMSErrorCode = ""

        Dim smsres As New XmlDocument

        If IsNothing(smsres) Then
            tmp_SMSResponse.SMSSentStatus = SMSSENTSTAUS.FAILURE
            tmp_SMSResponse.SMSErrorCode = "ERROR_NORESPONSE"
            Return tmp_SMSResponse
        End If

        smsres.LoadXml(smsresp)


        Dim GUID, errCode, errDesc As String
        errCode = "" : GUID = "" : errDesc = ""

        Dim _element As XmlElement
        _element = smsres.SelectSingleNode("/MESSAGEACK/GUID")
        If Not IsNothing(_element) Then
            GUID = _element.GetAttribute("GUID")
        End If

        _element = smsres.SelectSingleNode("/MESSAGEACK/GUID/ERROR")
        If Not IsNothing(_element) Then
            errCode = _element.GetAttribute("CODE")
        End If

        _element = smsres.SelectSingleNode("/MESSAGEACK/Err")
        If Not IsNothing(_element) Then
            errCode = _element.GetAttribute("Code")
            errDesc = _element.GetAttribute("Desc")
        End If


        If errCode <> "" Then
            tmp_SMSResponse.SMSSentStatus = SMSSENTSTAUS.FAILURE
            tmp_SMSResponse.SMSErrorCode = errCode
            tmp_SMSResponse.SMSErrorDesc = errDesc
            Return tmp_SMSResponse

        Else
            tmp_SMSResponse.SMSSentStatus = SMSSENTSTAUS.SUCCESS
            tmp_SMSResponse.SMSErrorCode = ""
            tmp_SMSResponse.SMSGUID = GUID
            Return tmp_SMSResponse
        End If

    End Function
End Class
