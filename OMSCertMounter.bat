@echo off

set "mount_disk=P:"
set "mount_path=C:\OMSCert"

subst "%mount_disk%" /D
subst %mount_disk% "%mount_path%"

@echo on