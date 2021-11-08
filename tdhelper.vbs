Option Explicit
On Error Goto 0

' ��� ������ ������� ��������� ������ ������ ��� ����������

' ���������� ���������� ��� �������� log-������
const LOG_PATH = ".\logs"


' ===== History
' 24.09.2009, ������ 1.0, ��� "�������� ����������"
'
' �����������:
' - ���������� ��������� �������� ��� ����������� ������� � �������� ��������:
'   - ��������� ���, � �.�. ����, � ����� ������ �� �������� �������
'   - �������� ���, � �.�. ����������
'   - ������ ���
'   - ���������� (� ��������� �� ������������������ ������������ �����������)
'   - �������������
' - ��������� �������� ������������ � ���� �������
'
' 25.09.2009, ������ 1.1, ��� "�������� ����������"
'
' �����������:
' - ���������� ������������ ���� ��� ��������� OCSP-������� � ��������������
'   ��������� 4.5.0 � ����� ������ ������
'
' 14.05.2012, ������ 1.2, ��� "�������� ����������"
'
' �����������:
' - �������������� �������� ���������� �������� (���� ��������� ������ �����
'   � ������ ���, ��� ������� �� �������� ������� "������������� �������")

' 28.10.2014, ������ 1.3, ��� "�������� ����������"
'
' �����������:
' - ��������� �������� �� ������� ����� (�.�. �������� ������)
' - ��������� ����������� ��������� �������� ��� ��������� ������
' - ��� ����������� ��������� ���� ������� (�� �������� �������� ���� � ����������� *.sig.sig...)
' - ��������� ����������� ��������� ������� (�������� ���� �� �������� � �������� ������� � ������ ����������� �������,
' ��� ���������� ����� �������, ��� ���������� �������� �� ������� �����������/����������� � ����������� �� ��������� �������� � �����)

' 15.12.2014, ������ 1.4, ��� "�������� ����������"
'
' �����������:
' - ��������� �������� ��������� (��� ������� �� �������, ���� ����������� ������� ����� � �������� ��������� � �� ��������/��������� �����,
'�� �� �������� ��������� ���������� �������� ������)

' TODO:
' - �������� ����� ���������� (���������� ������)
' - �������� ������ ������ �� "this.vbs operation --option1 value1 --option2 value2 ..."
'   ��� operation ��������� �� Sign[ature], Verify[Signatures], Drop[Signatures],
'   TakeOffSign[atures], Encrypt, Decrypt
' - ���������� ����� ������ ����� �������������

' ===== Constants

' enum PROFILESTORETYPE
Const REGISTRY_STORE = 0
Const XML_STORE = 1 ' since TD 3.3

' enum DATA_TYPE
Const DT_EMPTY_SIGNED_DATA = -2 ' since TD 4.2
Const DT_AUTO_DETECT = -1
Const DT_PLAIN_DATA = 0
Const DT_SIGNED_DATA = 2
Const DT_ENVELOPED_DATA = 3

' enum FORMAT 
Const UNKNOWN_TYPE = -1 ' since TD 4.5
Const BASE64_TYPE = 0 ' is equal to PROFILEEXITFORMAT::BASE64
Const DER_TYPE = 1 ' is equal to PROFILEEXITFORMAT::DER
Const XML_TYPE = 2 ' since 3.3
Const HEX_TYPE = 3 ' since 4.3
Const BINARY_TYPE = 1 ' is equal to DER_TYPE ' since TD 4.4
Const UNICODE_STRING_TYPE = 4 ' since TD 4.4

' enum POLICY_TYPE
Const POLICY_TYPE_NONE = 0
Const POLICY_TYPE_SIGNATURE = 1
Const POLICY_TYPE_ENCRYPT = 2

' enum SIGNATURE_TYPE
Const SIGNATURE_TYPE_UNKNOWN = 0            ' // ����������� ��� �������
Const SIGNATURE_TYPE_BASIC = 1              ' // ������� ������� (�� ������ � CAdES-BES)
' // CAdES types
' //SIGNATURE_TYPE_CADES_BES = 2         // CAdES Basic Electronic Signature
' //SIGNATURE_TYPE_CADES_EPES = 4        // CAdES Explicit Policy Electronic Signatures
' //SIGNATURE_TYPE_CADES_T = 8           // CAdES with Time
' //SIGNATURE_TYPE_CADES_C = 16          // CAdES with Complete validation data references
' //SIGNATURE_TYPE_CADES_X_LONG = 32     // Extended validation data: Long validation data
' //SIGNATURE_TYPE_CADES_X_TYPE_1 = 64   // Extended validation data: Type 1
' //SIGNATURE_TYPE_CADES_X_TYPE_2 = 128  // Extended validation data: Type 2
Const SIGNATURE_TYPE_CADES_X_LONG_TYPE_1 = 96 '//= SIGNATURE_TYPE_CADES_X_LONG + SIGNATURE_TYPE_CADES_X_TYPE_1

' enum VERIFYFLAG
Const VF_CERT_AND_SIGN_OLD = 0
Const VF_SIGN_ONLY = 1
Const VF_CERT_ONLY = 2 ' // only using with VF_SIGN_ONLY
Const VF_CERT_AND_SIGN = 3
Const VF_TSP_ONLY = 4 ' // only using with VF_SIGN_ONLY
Const VF_SIGN_AND_TSP = 5
'Const VF_SIGN_AND_CERT_AND_TSP = 6
Const VF_ALL_POSSIBLE = -1

' enum VERIFYSTATUS
Const VS_CORRECT = 1                             ' // �������� ������ �������
Const VS_UNSUFFICIENT_INFO = 2                   ' // ������������ ������ (���������, ����� ��� ��� ��� �������� ����������� ������������ ��)
Const VS_UNCORRECT = 3                           ' // ��� �����������, ��� ��� ��� �� ���������
'
Const VS_INVALID_CERTIFICATE_BLOB = 4            ' // �� ������ �������� �����������
Const VS_CERTIFICATE_TIME_EXPIRIED = 5           ' // ���� ���� �������� �����������
Const VS_CERTIFICATE_NO_CHAIN = 6                ' // ���������� ��������� ������� (���� ������������) ��� �����������
Const VS_CRL_UPDATING_ERROR = 7                  ' // ��������� ������ ��� ���������� ���
Const VS_LOCAL_CRL_NOT_FOUND = 8                 ' // �� ������ ��� � ��������� ���������
Const VS_CRL_TIME_EXPIRIED = 9                   ' // ��� ������, ������ �� ��������� � ����������
Const VS_CERTIFICATE_IN_CRL = 10                 ' // ���������� ���������� � ���
Const VS_CERTIFICATE_IN_LOCAL_CRL = 11           ' // ���������� ���������� � ���, ���������� ��� �� ���� �����������
Const VS_CERTIFICATE_CORRECT_BY_LOCAL_CRL = 12   ' // ���������� ������������, �� ���������� ��� �� ���� �����������
Const VS_CERTIFICATE_USING_RESTRICTED = 13       ' // ���������� �������� ��� ������������� (�����������, ��������� � ��.)
Const VS_NOT_APPLICABLE_FOR_SPECIFIED_USAGE = 13 ' // �� �������� ��� ��������� ������� ������������� (��� ���)
Const VS_CERTIFICATE_RESTRICTED_BY_LICENSE = 14  ' // ����������� �������� �� ������������� ������� �����������
Const VS_REVOCATION_STATUS_UNKNOWN = 15          ' // RP ���������� ������ UNKNOWN (�������� ��� ��� ���-������)
Const VS_REVOCATION_OCSP_ERROR = 16              ' // �� �����-���� �������� �� ������� ��������� ������ ����������� �� OCSP
Const VS_CADES_ATTRIBUTES_NOT_VERIFIED = 17      ' // ��� ���������, �� �������������� �������� ���� �� ����������� ��� ��������
Const VS_CHAIN_UNCORRECT_BY_SPECIFIED_CTLS = 18  ' // ������������ ���� ������������ �� ������������� (�� ����� ��) ���
Const VS_CTL_IS_NOT_SIGNED = 19                  ' // ��� �� �������� (��� ����������� - ���� �������� �������� �� ���, ��� ��� - ��� ������� ����� ��������� ������� ���)
Const VS_CTL_TIME_EXPIRIED = 20                  ' // ����� �������� ��� ������� (��� ��� �� ��������)

' enum WIZARD_TYPE
Const SIGN_WIZARD_TYPE = 1
Const ADD_SIGN_WIZARD_TYPE = 2
Const COSIGN_WIZARD_TYPE = 4
Const CONNECT_KEYCARRIER_WIZARD_TYPE = 8 ' since TD 3.2
Const ENCRYPT_WIZARD_TYPE = 64
Const DECRYPT_WIZARD_TYPE = 1024
Const VERIFY_SIGNATURE_WIZARD_TYPE = 2048
Const DECRYPT_VERIFY_SIGNATURE_WIZARD_TYPE = 4096
Const DROP_SIGNATURE_WIZARD_TYPE = 8192
Const VIEW_DOCUMENT_WIZARD_TYPE = 16384
Const OPEN_MANAGERS_WIZARD_TYPE = 32768
'//COSIGN_WIZARD_TYPE_WITH_FILES = 32768
Const ADD_SIGN_WIZARD_TYPE_WITH_FILES = 65536


' self constants
Const STATUS_UNKNOWN = -1
Const STATUS_OK = 0
Const STATUS_WARNING = 1
Const STATUS_BAD = 2
Const STATUS_INVALID = 3
Const STATUS_NO_DOC = 4


' ===== global variables
Dim g_iOperationMode : g_iOperationMode = ""
Dim g_sSignExt : g_sSignExt = ""
Dim g_sEncrExt : g_sEncrExt = ""

Dim g_oProfile : Set g_oProfile = Nothing
Dim g_oProfileName : g_oProfileName = ""

Dim g_oPKCS7Message1 : Set g_oPKCS7Message1 = CreateObject("DigtCrypto.PKCS7Message")
Dim g_oPKCS7Message2 : Set g_oPKCS7Message2 = Nothing
Dim g_oPKCS7Message : Set g_oPKCS7Message = g_oPKCS7Message1
Dim g_oPKCS7MessageShared : Set g_oPKCS7MessageShared = g_oPKCS7Message1

Dim oFSOShared : Set oFSOShared = CreateObject("Scripting.FileSystemObject")

Dim oLogFile : Set oLogFile = Nothing
Dim sLogFileName : sLogFileName = ""

Dim g_sInputCharSet : g_sInputCharSet = ""
Dim g_sOutputCharSet : g_sOutputCharSet = ""


' ===== helpers
Function IsNothing( ByRef oObject )
	if Not IsObject(oObject) then
		IsNothing = True
	elseif TypeName(oObject) = "Nothing" then
		IsNothing = True
	else
		IsNothing = False
	end if
End function

Function TruncatePath( ByVal sFullname )
	TruncatePath = sFullname

	Dim iPos : iPos = InStrRev( sFullname, "\" )
	if IsNumeric(iPos) then
		if iPos > 0 then
			TruncatePath = Right( sFullname, Len(sFullname) - iPos )
		end if
	end if
End Function

Function TruncateFileExtension( ByVal sFullname )
	TruncateFileExtension = sFullname

	Dim sFilename : sFilename = TruncatePath(sFullname)
	Dim iPos : iPos = InStrRev( sFilename, "." )
	if IsNumeric(iPos) then
		if iPos > 0 then
			TruncateFileExtension = Left( sFullname, Len(sFullname) - Len(sFilename) + iPos - 1 )
		end if
	end if
End Function

Function InitLog()
	Dim curDate : curDate = Date()
	Dim curYear : curYear = Right(CStr(Year(curDate)), 2)
	Dim curMonth : curMonth = CStr(Month(curDate)) : curMonth = String(2-len(curMonth), "0") & curMonth
	Dim curDay : curDay = CStr(Day(curDate)) : curDay = String(2-len(curDay), "0") & curDay

	sLogFileName = oFSOShared.BuildPath(LOG_PATH, curYear & curMonth & curDay & ".log")

	if not oFSOShared.FolderExists( LOG_PATH ) then
		oFSOShared.CreateFolder( LOG_PATH )
	end if
	Set oLogFile = oFSOShared.OpenTextFile( sLogFileName, 8, true )

	Dim strInputData : strInputData = ""
	Dim sParams : sParams = ""
	Dim i, iCount : iCount = WScript.Arguments.Count
	For i = 0 to iCount - 1
		
		strInputData = WScript.Arguments.Item(i)
		strInputData = CheckInputData( strInputData )
		sParams = sParams & """" & strInputData & """ "
		'sParams = sParams & """" & WScript.Arguments.Item(i) & """ "
	Next

	oLogFile.WriteLine ""
	LogMsg( "������: """ & WScript.ScriptFullName & """" )
	LogMsg( "������� � �����������: " & sParams )
	LogMsg( "----------------------------------------------------------------------------" )
End Function

Function LogMsg( ByVal sMessage )
	oLogFile.WriteLine( CStr(Time()) & " " & Replace(sMessage, vbCrLf, " / ") )

	if InStr( WScript.FullName, "cscript.exe" ) > 0 then
		WScript.Echo sMessage
	end if
End Function

Function PrintError( ByVal sMessage, ByVal iExitCode ) ' if iExitCode == -1, then iExitCode set to Err.Number
	if -1 = iExitCode then
		iExitCode = Err.Number
	end if

	Dim sRes : sRes = "Error number: " & CStr(Err.Number) & ", Description: " & CStr(Err.Description)

	if 0 = iExitCode then
		sRes = sMessage
	elseif Len(sRes) > 0 then
		sRes = sMessage & vbCrLf & sRes
	end if

	LogMsg sRes
	'WScript.Echo sRes

	WScript.Quit iExitCode
End Function

Function PrintUsage( ByVal iExitCode )
	Dim sUsage : sUsage = _
		"�������� ������������� �������: " & vbCrLf & _
		WScript.ScriptName & " <��_������� ��� ��_����> <���_�������> <���_�������_���������> <��������>" & vbCrLf & _
		vbCrLf & _
		"�������� <��������> ����� ��������� ���������� ��������:" & vbCrLf & _
		"- S[ign] - ���������" & vbCrLf & _
		"- A[dd] - �������� ���������" & vbCrLf & _
		"- V[erifySignature] - ��������� �������" & vbCrLf & _
		"- T[akeOffSignature] - ����� �������" & vbCrLf & _
		"- E[ncrypt] - �����������" & vbCrLf & _
		"- D[ecrypt] - ������������"

	WScript.Echo sUsage
	WScript.Quit iExitCode
End Function

Function CachePinCode() ' implemented caching engine is applicable for CryptoPro CSP (only?)
	' caching of pin-code
	if IsNothing(g_oPKCS7Message2) then
'		g_oPKCS7Message.Profile = g_oProfile
'		g_oPKCS7Message.Import DT_PLAIN_DATA, "123"
'		g_oPKCS7Message.Sign
'		g_oPKCS7Message.Export DT_SIGNED_DATA, BASE64_TYPE

		Set g_oPKCS7Message2 = CreateObject("DigtCrypto.PKCS7Message")
		g_oPKCS7Message2.Profile = g_oPKCS7Message.Profile
		Set g_oPKCS7Message = g_oPKCS7Message2
	end if
End Function

Function ParseCryptoOperation( ByVal sOperation )

	if 0 = StrComp( Left(sOperation, 1), "s", vbTextCompare ) then
		ParseCryptoOperation = SIGN_WIZARD_TYPE
	elseif 0 = StrComp( Left(sOperation, 1), "t", vbTextCompare ) then
		ParseCryptoOperation = DROP_SIGNATURE_WIZARD_TYPE
	elseif 0 = StrComp( Left(sOperation, 1), "v", vbTextCompare ) then
		ParseCryptoOperation = VERIFY_SIGNATURE_WIZARD_TYPE
	elseif 0 = StrComp( Left(sOperation, 1), "e", vbTextCompare ) then
		ParseCryptoOperation = ENCRYPT_WIZARD_TYPE
	elseif 0 = StrComp( Left(sOperation, 1), "d", vbTextCompare ) then
		ParseCryptoOperation = DECRYPT_WIZARD_TYPE
	elseif 0 = StrComp( Left(sOperation, 1), "a", vbTextCompare ) then
		ParseCryptoOperation = ADD_SIGN_WIZARD_TYPE	
	else
		PrintError "������� ���������������� ��������: """ & sOperation & """", -1
	end if
End Function

Function LoadProfileByIdOrName( ByVal sProfileName )
	Dim oProfileStore : Set oProfileStore = CreateObject("DigtCrypto.ProfileStore")
	oProfileStore.Open REGISTRY_STORE, ""

	Dim oProfiles : Set oProfiles = oProfileStore.Store
	Dim oResProfile : Set oResProfile = oProfiles.Profile( sProfileName )

	if IsNothing(oResProfile) then
		Dim iCount : iCount = oProfiles.Count

		Dim oProfile, i
		For i = 0 to iCount - 1
			Set oProfile = oProfiles.Item(i)
			if oProfile.Name = sProfileName then
				Set oResProfile = oProfile
				exit for
			end if
		Next
	end if

	if not IsNothing(oResProfile) then
		if Len(oResProfile.TSPProfileID) > 0 then ' TD 4.5.0 and earlier requires to be set of TSP&OCSP profiles manually
			On Error Resume Next
			Dim oTSPProfile : Set oTSPProfile = oResProfile.TSPProfile ' TD 4.5.0 and earlier throws exeception if it's not set
			if Err.Number <> 0 then
				Set oTSPProfile = Nothing
			end if
			On Error Goto 0

			if IsNothing(oTSPProfile) then
				oResProfile.TSPProfile = oProfileStore.TSPProfileStore.Profile(oResProfile.TSPProfileID)
			end if
		end if
	end if

	Set LoadProfileByIdOrName = oResProfile
End Function

Function VerifyProfile( ByRef oProfile, ByVal iOperation )
	if SIGN_WIZARD_TYPE = iOperation or ADD_SIGN_WIZARD_TYPE = iOperation then
		if SIGNATURE_TYPE_BASIC <> oProfile.SignatureType then
			oProfile.VerifyCertByOCSP = False ' for TD 4.5.0 and earlier
		end if
	elseif ENCRYPT_WIZARD_TYPE = iOperation then
		' checking of recipient certificates
		Dim oRecipients : Set oRecipients = oProfile.Recipients
		Dim oCert, i, c : c = oRecipients.Count

		Dim iCurStatus, iResStatus : iResStatus = STATUS_UNKNOWN
		For i = 0 to c - 1
			Set oCert = oRecipients.Item(i)
			iCurStatus = VerifyCertificate( oCert, POLICY_TYPE_ENCRYPT )
			iCurStatus = SignStatusToSolidStatus( iCurStatus )

			if iResStatus < iCurStatus then
				iResStatus = iCurStatus
			end if
		Next

		if iResStatus >= STATUS_BAD then
			PrintError "��� ������� � ������ ��� ���������� ������������ ����������� ������������ ���������", -1
		end if
	end if
End Function

Function Convert( ByVal sInputData, ByVal sInputCharSet, ByVal sOutputCharSet )
	On Error Resume Next
	
	Dim strConverted
	Dim objStream : Set objStream = CreateObject("ADODB.Stream")
	
	objStream.Type = 2
	objStream.Mode = 3
	objStream.Open
	objStream.Charset = sInputCharSet
	objStream.WriteText sInputData
	objStream.Position = 0
	objStream.Charset = sOutputCharSet
	strConverted = objStream.ReadText
	objStream.Close
				
	Convert = strConverted
				
	On Error Goto 0
End Function

Function CheckInputData( ByVal sInputData )
	On Error Resume Next

	If g_sInputCharSet <> "" And g_sOutputCharSet <> "" Then 	
		sInputData = Convert( sInputData, g_sInputCharSet, g_sOutputCharSet )
		CheckInputData = sInputData
		Exit Function
	End If
	
	'�������� �� ������������� �������� ����� ��� �����
	If oFSOShared.FileExists( sInputData ) Or oFSOShared.FolderExists( sInputData ) Then
		CheckInputData = sInputData
		Exit Function
	End If	
	
	'�������� �� ������������� �������
	Dim objTempProfile : Set objTempProfile = CreateObject("DigtCrypto.Profile")
	Set objTempProfile = LoadProfileByIdOrName( sInputData )
	if Not IsNothing(objTempProfile) then
		CheckInputData = sInputData
		Exit Function
	end if
	
	'���� �������� �� ������������� ������, �� � ���������� �� ���������
	Dim iCounterInCoder, iCounterOutCoder, strConverted
	Dim arrPageCoding :	arrPageCoding = Array("cp866", "windows-1251", "UTF-8")	
		
	For iCounterInCoder = LBound(arrPageCoding) to UBound(arrPageCoding)
		For iCounterOutCoder = LBound(arrPageCoding) to UBound(arrPageCoding)
			strConverted = Convert( sInputData, arrPageCoding(iCounterInCoder), arrPageCoding(iCounterOutCoder) )
				
			'�������� �� ������������� �������� ����� ��� �����
			If oFSOShared.FileExists( strConverted ) Or oFSOShared.FolderExists( strConverted ) Then
				sInputData = strConverted
				g_sInputCharSet = arrPageCoding(iCounterInCoder)
				g_sOutputCharSet = arrPageCoding(iCounterOutCoder)
				CheckInputData = sInputData
				Exit Function
			End If	
				
			'�������� �� ������������� �������
			Set objTempProfile = Nothing
			Set objTempProfile = LoadProfileByIdOrName( strConverted )
							
			If objTempProfile.Name = "" Then 
				'MsgBox "Error, profile is not existed"
			Else
				sInputData = strConverted
				g_sInputCharSet = arrPageCoding(iCounterInCoder)
				g_sOutputCharSet = arrPageCoding(iCounterOutCoder)
				CheckInputData = sInputData
				Exit Function					
			End If
        Next		
	Next

	CheckInputData = sInputData

	On Error Goto 0
End Function


' ===== functions

' ---
Function SignFile( ByVal sInputFilename, ByVal sOutputFilename )
	if Right(sInputFilename, 4) = ".sig" or Right(sInputFilename, 4) = ".p7s" then
		LogMsg "���� """ & sInputFilename & """ �� ��� ��������, �.�. �� ��� �������� ������ �������."
		exit function
	end if

	g_oPKCS7Message.Load DT_PLAIN_DATA, sInputFilename
	g_oPKCS7Message.Sign
	g_oPKCS7Message.Save DT_SIGNED_DATA, g_oProfile.SignExitFormat, sOutputFilename

	LogMsg "���� """ & sInputFilename & """ �������� � �������� � """ & sOutputFilename & """"

	CachePinCode
End Function

' ---
Function AddSign( ByVal sInputFilename, ByVal sOutputFilename )
	On Error Resume Next
	
		If Right(sInputFilename, 4) = ".sig" Or Right(sInputFilename, 4) = ".p7s" Then
			g_oPKCS7Message.Load DT_SIGNED_DATA, sInputFilename
			
			'��� ��������: �������� ������ �������, ��������� ������������ ��� ���
			'� ��������� ����� ������ �� ����, ��� ����������� �� ������� 
			Dim oTmpPKCS7Message : Set oTmpPKCS7Message = g_oPKCS7Message
			Dim oTempProfile : Set oTempProfile = oTmpPKCS7Message.Profile
			Dim oSignatures : Set oSignatures = oTmpPKCS7Message.Signatures
			Dim bDetached : bDetached = oSignatures.Item(0).Detached
			Dim bAttached : bAttached = False 
		
			If bDetached <> oTempProfile.Detach Then
				oTempProfile.Detach = bDetached
				oTmpPKCS7Message.Profile = oTempProfile
			End If

			' ���� �������� �������� ��� ��������� �������
			If bDetached = True Then 
				bAttached = GetFilePathForDetachSign( sInputFilename, oSignatures )
			Else
				bAttached = True
			End If
		
			'��������� �������
			If bAttached = True And Err.Number = 0 Then 
				Dim iCountSignatures : iCountSignatures = oTmpPKCS7Message.Signatures.Count
				oTmpPKCS7Message.Sign
			Else
				LogMsg "��������� ������ ��� �������� ��������� ����� ��� �������� ��������� �������."
			End If
			
			if Err.Number = 0 then
				oTmpPKCS7Message.Save DT_SIGNED_DATA, g_oProfile.SignExitFormat, sOutputFilename
				LogMsg "� ���� """ & sInputFilename & """ ��������� ������� � �� �������� � """ & sOutputFilename & """"
			End If
		Else
			LogMsg "���������� �������� ������� � ���� """ & sInputFilename & ". ���� �� �������� ��������."
		End If
		
	On Error Goto 0	
End Function

Function GetFilePathForDetachSign( ByVal sInputFilename, ByVal oSignatures )
	Dim bAttached, sDocPath, arrStr, i, iCount : iCount = oSignatures.Count
	For i = 0 to iCount - 1
		sDocPath = ExtractDocumentFilename( oSignatures.Item(i) )
		if Len(sDocPath) > 0 then
			arrStr = Split(sInputFilename, "\", -1, vbTextCompare)
			arrStr(UBound(arrStr)) = sDocPath
			sDocPath = Join(arrStr, "\")

			bAttached = g_oPKCS7Message.Load( DT_SIGNED_DATA, sInputFilename, sDocPath )
			if (0 = Err.Number) and bAttached then
				exit for
			end if
		end if
	Next

	' ���� ���� ��������� �� ������, �� ������� �������� ���������� �� �������
	if not bAttached then ' Err.Number �� ��������� �� ������, ���� ���� ������
		sDocPath = Left(sInputFilename, Len(sInputFilename) - Len(g_oProfile.SignatureExtension(g_oProfile.SignExitFormat)) - 1)
		bAttached = g_oPKCS7Message.Load( DT_SIGNED_DATA, sInputFilename, sDocPath )
	end if

	' ���� ��� ��� �� ������, �� �������� ��������� ����������
	if not bAttached then ' Err.Number �� ��������� �� ������, ���� ���� ������
		arrStr = Split(sInputFilename, ".", -1, vbTextCompare)
		sDocPath = Left(sInputFilename, Len(sInputFilename) - Len(UBound(arrStr)) - 1)
		bAttached = g_oPKCS7Message.Load( DT_SIGNED_DATA, sInputFilename, sDocPath )
	end if
		
	GetFilePathForDetachSign = bAttached
End Function
' ---
Function TakeOffSignature( ByVal sInputFilename, ByVal sOutputFilename )
	On Error Resume Next

	Dim bAttached : bAttached = g_oPKCS7Message.Load( DT_SIGNED_DATA, sInputFilename )

	if Err.Number <> 0 then
		LogMsg "���� """ & sInputFilename & """ �� �������� ���!"
	elseif not bAttached then
		LogMsg "���� ��� """ & sInputFilename & """ �� �������� ������������ ���������!"
	else
		g_oPKCS7Message.Save DT_PLAIN_DATA, DER_TYPE, sOutputFilename

		LogMsg "�� ����� ��� """ & sInputFilename & """ �������� �������� �������� � """ & sOutputFilename & """"
	end if

	On Error Goto 0
End Function

' ---
Function SignStatusToSolidStatus( ByVal iSignStatus )
	Select Case iSignStatus

	Case VS_CORRECT SignStatusToSolidStatus = STATUS_OK
	Case VS_CERTIFICATE_CORRECT_BY_LOCAL_CRL SignStatusToSolidStatus = STATUS_OK

	Case VS_UNSUFFICIENT_INFO SignStatusToSolidStatus = STATUS_WARNING
	Case VS_CRL_UPDATING_ERROR SignStatusToSolidStatus = STATUS_WARNING
	Case VS_LOCAL_CRL_NOT_FOUND SignStatusToSolidStatus = STATUS_WARNING
	Case VS_CRL_TIME_EXPIRIED SignStatusToSolidStatus = STATUS_WARNING
	Case VS_REVOCATION_STATUS_UNKNOWN SignStatusToSolidStatus = STATUS_WARNING
	Case VS_REVOCATION_OCSP_ERROR SignStatusToSolidStatus = STATUS_WARNING
	Case VS_CTL_TIME_EXPIRIED SignStatusToSolidStatus = STATUS_WARNING

	Case Else SignStatusToSolidStatus = STATUS_BAD

	End Select
End Function

Function VerifyCertificate( ByRef oCert, ByVal iPolicyType )
	oCert.Profile = g_oProfile
	VerifyCertificate = oCert.IsValid( iPolicyType )
End Function

Function VerifySignatures( ByRef oSignatures, ByVal iLevel )
	Dim iResStatus : iResStatus = STATUS_UNKNOWN

	Dim oSignature, iCurStatus, i, iCount : iCount = oSignatures.Count
	For i = 0 to iCount - 1
		Set oSignature = oSignatures.Item(i)
		iCurStatus = oSignature.Verify( VF_ALL_POSSIBLE )
		iCurStatus = SignStatusToSolidStatus( iCurStatus )

		if iResStatus < iCurStatus then
			iResStatus = iCurStatus
		end if

		if SIGNATURE_TYPE_BASIC = oSignature.SignatureType then
			iCurStatus = VerifyCertificate( oSignature.Certificate, POLICY_TYPE_SIGNATURE )
			iCurStatus = SignStatusToSolidStatus( iCurStatus )

			if iResStatus < iCurStatus then
				iResStatus = iCurStatus
			end if
		end if

		if oSignature.Cosignature.Count > 0 then
			iCurStatus = VerifySignatures( oSignature.Cosignature, iLevel + 1 )
			iCurStatus = SignStatusToSolidStatus( iCurStatus )

			if iResStatus < iCurStatus then
				iResStatus = iCurStatus
			end if
		end if
	Next

	VerifySignatures = iResStatus
End Function

Function ExtractDocumentFilename( ByRef oSignature )
	ExtractDocumentFilename = ""

	Dim sPath : sPath = oSignature.Resource
	if Len(sPath) > 5 and StrComp(Left(sPath, 5), "file:", vbTextCompare) = 0 then
		sPath = Right(sPath, Len(sPath) - 5)

		' �������� ��� ������� � ����������� ��� �������������� ��������� �� �������������� �������
		Dim arrStr : arrStr = Split(sPath, "\", -1, vbTextCompare)
		sPath = arrStr(UBound(arrStr))
		arrStr = Split(sPath, "/", -1, vbTextCompare)
		sPath = arrStr(UBound(arrStr))

		ExtractDocumentFilename = sPath
	end if
End Function

Function VerifySignature( ByVal sInputFilename )
	Dim sStatus, iStatus : iStatus = STATUS_INVALID

	On Error Resume Next

	Dim bAttached : bAttached = g_oPKCS7Message.Load( DT_SIGNED_DATA, sInputFilename )

	if Err.Number <> 0 then
		LogMsg "������ �������� ����� """ & sInputFilename & """, ��������, �� �� �������� ���"
	elseif not bAttached then
		iStatus = STATUS_NO_DOC

		' ������� ����������, ���� ��� ���� ��������� �� ������ ���������� �� ���
		Dim oSignatures : Set oSignatures = g_oPKCS7Message.Signatures
		
		bAttached = GetFilePathForDetachSign( sInputFilename, oSignatures )
	end if

	if bAttached then
		iStatus = VerifySignatures( g_oPKCS7Message.Signatures, 0 )
	end if

	Select Case iStatus
	Case STATUS_OK sStatus = "�����"
	Case STATUS_WARNING sStatus = "��� ������� ������� � ����� ��� ���������� ���"
	Case STATUS_BAD sStatus = "���� ��� ��������� ��� ���������������!"
	Case STATUS_NO_DOC sStatus = "���� ��� �� �������� ������������ ��������� � ��� ������������ �� ���� �����������"
	Case Else sStatus = "��� �������� �������� ������ (��������, ���� �� �������� ���)"
	End Select

	LogMsg "���������� ������ ��� ����� """ & sInputFilename & """: " & sStatus

	On Error Goto 0
End Function

' ---
Function EncryptFile( ByVal sInputFilename, ByVal sOutputFilename )
	g_oPKCS7Message.Load DT_PLAIN_DATA, sInputFilename
	g_oPKCS7Message.Encrypt
	g_oPKCS7Message.Save DT_ENVELOPED_DATA, g_oProfile.EncryptExitFormat, sOutputFilename

	LogMsg "���� """ & sInputFilename & """ ���������� � �������� � """ & sOutputFilename & """"
End Function

' ---
Function DecryptFile( ByVal sInputFilename, ByVal sOutputFilename )
	On Error Resume Next

	g_oPKCS7Message.Load DT_ENVELOPED_DATA, sInputFilename

	if Err.Number <> 0 then
		LogMsg "���� """ & sInputFilename & """ �� �������� ����������� ������!"
	elseif IsNothing(g_oPKCS7Message.Decrypt) then
		LogMsg "���� """ & sInputFilename & """ �� ������� ������������!"
	else
		g_oPKCS7Message.Save DT_PLAIN_DATA, DER_TYPE, sOutputFilename

		LogMsg "���� """ & sInputFilename & """ ����������� � �������� � """ & sOutputFilename & """"

		CachePinCode
	end if

	On Error Goto 0
End Function

' ---
Function ProcessFile( ByVal sInputFilename, ByVal sOutputFilename )

	if SIGN_WIZARD_TYPE = g_iOperationMode then
		ProcessFile = SignFile( sInputFilename, sOutputFilename & "." & g_sSignExt )
	elseif DROP_SIGNATURE_WIZARD_TYPE = g_iOperationMode then
		ProcessFile = TakeOffSignature( sInputFilename, TruncateFileExtension(sOutputFilename) )
	elseif VERIFY_SIGNATURE_WIZARD_TYPE = g_iOperationMode then
		ProcessFile = VerifySignature( sInputFilename )
	elseif ENCRYPT_WIZARD_TYPE = g_iOperationMode then
		ProcessFile = EncryptFile( sInputFilename, sOutputFilename & "." & g_sEncrExt )
	elseif DECRYPT_WIZARD_TYPE = g_iOperationMode then
		ProcessFile = DecryptFile( sInputFilename, TruncateFileExtension(sOutputFilename) )
	elseif ADD_SIGN_WIZARD_TYPE = g_iOperationMode then
		ProcessFile = AddSign( sInputFilename, sOutputFilename )
	else
		PrintError "������� ���������������� ��������, id=" & CStr(g_iOperationMode), -1
	end if
End Function

' ---
Function ProcessFolder( ByVal sInputFolderOrFile, ByVal sOutputFolder )
	Dim oFile 
	Dim bIsFile : bIsFile = False
	Dim bIsFolder : bIsFolder = False
	
	If oFSOShared.FileExists( sInputFolderOrFile ) Then
		bIsFile = True
	End If

	If oFSOShared.FolderExists( sInputFolderOrFile ) Then
		bIsFolder = True
	End If

	If bIsFile = False And bIsFolder = False Then
		PrintError "�� ������ ������� ��� ���� '" & CStr(sInputFolderOrFile) & "'", -1
	End If

	if not oFSOShared.FolderExists( sOutputFolder ) then
		oFSOShared.CreateFolder( sOutputFolder )
	end if

	If bIsFile = True Then
		'��������� ��������� ���������� �����
		Set oFile = oFSOShared.GetFile( sInputFolderOrFile )
		if oFile.Size <> 0 then
			ProcessFile sInputFolderOrFile, sOutputFolder & "\" & oFile.Name
		else
			LogMsg "���������� �������� ��������� ��������. ���� """ & oFile.Name & """ - ������."
		end if
	Else
		'�������� �������� �����
		Dim oInputFolder : Set oInputFolder = oFSOShared.GetFolder( sInputFolderOrFile )

		' processing files
		Dim oFiles : Set oFiles = oInputFolder.Files
		For Each oFile in oFiles
			if oFile.Size <> 0 then
				ProcessFile sInputFolderOrFile & "\" & oFile.Name, sOutputFolder & "\" & oFile.Name
			else
				LogMsg "���������� �������� ��������� ��������. ���� """ & oFile.Name & """ - ������."
			end if
		Next

		' processing subfolders
		Dim oFolder, oFolders : Set oFolders = oInputFolder.SubFolders
		For Each oFolder in oFolders
			ProcessFolder sInputFolder & "\" & oFolder.Name, sOutputFolder & "\" & oFolder.Name
		Next
	End If
End Function

' ===== main part =====
if WScript.Arguments.Count < 4 then
	PrintUsage(1)
else
	InitLog
	
	g_iOperationMode = ParseCryptoOperation( WScript.Arguments(3) )

	Dim strProfileName : strProfileName = WScript.Arguments(2)
	g_oProfileName = strProfileName
	strProfileName = CheckInputData( strProfileName )
	
	Set g_oProfile = LoadProfileByIdOrName( strProfileName )
	'Set g_oProfile = LoadProfileByIdOrName( WScript.Arguments(2) )
	'g_oProfile.Display
	if IsNothing(g_oProfile) then
		PrintError "�� ������� ��������� ��������� """ & CStr(WScript.Arguments(2)) & """", -1
	end if

	VerifyProfile g_oProfile, g_iOperationMode

	g_oPKCS7Message.Profile = g_oProfile
	g_sSignExt = g_oProfile.SignatureExtension(g_oProfile.SignExitFormat) ' since TD 4.2
	g_sEncrExt = g_oProfile.EncryptedExtension(g_oProfile.EncryptExitFormat) ' since TD 4.2
	
	'�������� ������� ����� ��� �����
	Dim strInputFolderName : strInputFolderName = WScript.Arguments(0)
	strInputFolderName = CheckInputData( strInputFolderName )
	
	'�������� �������� �����
	Dim strOutputFolderName : strOutputFolderName = WScript.Arguments(1)
	strOutputFolderName = CheckInputData( strOutputFolderName )
	
	ProcessFolder strInputFolderName, strOutputFolderName
	'ProcessFolder WScript.Arguments(0), WScript.Arguments(1)

	LogMsg( "----------------------------------------------------------------------------" )
	PrintError "���������� ������� ���������, ������ � ����� """ & sLogFileName & """", 0
end if
