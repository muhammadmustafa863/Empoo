@echo off
setlocal
set APPDIR=C:\MartPOS
if not exist %APPDIR% mkdir %APPDIR%
echo Copying files to %APPDIR%...
xcopy /E /I /Y . %APPDIR% >nul
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
  echo .NET runtime not found. Please install .NET 8 Runtime from Microsoft first.
  exit /b 1
)
cd /d %APPDIR%
if exist MartPOS.UI\MartPOS.UI.csproj (
  dotnet restore
)
echo Installation complete.
endlocal
