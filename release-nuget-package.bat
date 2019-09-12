@echo off
cls

rem ** Build solution
echo Build solution
call releasebuild.bat

IF %ERRORLEVEL% NEQ 0 GOTO err

rem ** Specify used packages folder
nuget.exe config -Set repositoryPath=.\packages\

rem ** Create nuget package
echo Cleaning NuGet packages
IF EXIST *.nupkg (del *.nupkg)

echo Create Nuget package
nuget.exe pack -IncludeReferencedProjects -properties Configuration=Release
IF %ERRORLEVEL% NEQ 0 GOTO err

rem ** Push package to nuget
rem    uses API key previously added to local git config
echo Push Nuget package
nuget push .\*.nupkg -Source https://api.nuget.org/v3/index.json
IF %ERRORLEVEL% NEQ 0 GOTO err


pause
exit /B 0

:err
PAUSE
exit /B 1