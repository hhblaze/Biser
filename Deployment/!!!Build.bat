"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\msbuild.exe" "%~dp0..\Biser\Biser.csproj" /t:rebuild /p:Configuration=Release
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\msbuild.exe" "%~dp0..\Biser\Biser.csproj" /t:rebuild /p:Configuration=Release-NET47
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\msbuild.exe" "%~dp0..\Biser_Standard\Biser_Standard.csproj" /t:rebuild /p:Configuration=Release

nuget.exe pack "%~dp0!!!Biser.nuspec" -BasePath "%~dp0.." -OutputDirectory "%~dp0..\Deployment"