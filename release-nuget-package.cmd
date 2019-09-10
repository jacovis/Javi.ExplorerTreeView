rem from:
rem https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio

rem ** Remove old binaries
rd /s /q .\Javi.MediaInfo\bin\Release

rem ** Build package
dotnet restore
dotnet build -c Release .\Javi.MediaInfo\Javi.MediaInfo.csproj

rem ** Push package to nuget
rem    uses API key previously added to local git config
nuget push .\Javi.MediaInfo\bin\Release\*.nupkg -Source https://api.nuget.org/v3/index.json