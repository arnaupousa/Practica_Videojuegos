@echo off
chcp 65001 >nul
title Piedra, Papel o Tijeras

:: 1. Comprobar si dotnet está en el PATH
where dotnet >nul 2>nul
if %errorlevel% == 0 (
    echo Iniciando con dotnet...
    dotnet run
    goto fin
)

:: 2. Si no está dotnet en el PATH, compilar con el compilador csc de Visual Studio
set CSC="C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\Roslyn\csc.exe"
if exist %CSC% (
    echo Compilando juego con Visual Studio...
    %CSC% -nologo -out:PiedraPapelTijera.exe Program.cs Jugador.cs Partida.cs Estadisticas.cs
    if %errorlevel% == 0 (
        cls
        PiedraPapelTijera.exe
        del PiedraPapelTijera.exe >nul 2>nul
        goto fin
    )
)

echo [Error] No se ha encontrado dotnet ni el compilador de Visual Studio.
echo Por favor, abre el proyecto desde Visual Studio o instala el SDK de .NET.
pause

:fin
