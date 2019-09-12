@echo off
CLS

REM https://stackoverflow.com/questions/5669765/build-visual-studio-project-through-the-command-line
REM you'll have to find the "latest" version of where msbuild.exe resides on your machine.. here are some popular versions/locations
REM set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v2.0.50727
REM set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v3.5
REM set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
REM set msBuildDir=C:\Program Files (x86)\MSBuild\12.0\Bin
REM set msBuildDir=C:\Program Files (x86)\MSBuild\14.0\Bin
set msBuildDir=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\bin

REM change into script storage directory
cd %~dp0

rem ** Remove old binaries
rd /s /q .\Javi.ExplorerTreeView\bin\Release

rem ** Restore nuget packages
nuget restore Javi.ExplorerTreeView.sln

rem ** Build the solution
call "%msBuildDir%\msbuild.exe" Javi.ExplorerTreeView.sln /m /p:Configuration=Release "/p:Platform=Any CPU" /t:Clean,Build 
@IF %ERRORLEVEL% NEQ 0 GOTO err

@exit /B 0

:err
@PAUSE
@exit /B 1