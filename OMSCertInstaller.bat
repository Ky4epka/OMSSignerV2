set "mount_disk=P:"
set "mount_path=C:\OMSCert"
set "data_dir=%~dp0OMSCert"

@echo d | xcopy "%data_dir%" "%mount_path%" /s /h /r /c /y /e /f /q /i
subst "%mount_disk%" /D
subst %mount_disk% "%mount_path%"

schtasks /CREATE /F /TN "OMSCertMount" /XML "C:\OMSSigner\OMSCertMount.xml" 

pause