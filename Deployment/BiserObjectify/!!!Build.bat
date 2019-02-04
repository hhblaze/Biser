"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\msbuild.exe" "%~dp0..\..\BiserObjectify\BiserObjectify.csproj" /t:rebuild /p:Configuration=Release
rem "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\msbuild.exe" "%~dp0..\Biser\BiserObjectify\BiserObjectify.csproj" /t:rebuild /p:Configuration=Release-NET47
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\msbuild.exe" "%~dp0..\..\BiserObjectify_Standard\BiserObjectify_Standard.csproj" /t:rebuild /p:Configuration=Release

nuget.exe pack "%~dp0!!!BiserObjectify.nuspec" -BasePath "%~dp0..\.." -OutputDirectory "%~dp0\"