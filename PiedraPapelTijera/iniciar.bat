@echo off
cd /d "%~dp0"
title Piedra, Papel o Tijeras

if not exist "PiedraPapelTijera.exe" (
    echo Compilando juego...
    if exist "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\Roslyn\csc.exe" (
        "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\Roslyn\csc.exe" -nologo -out:PiedraPapelTijera.exe Program.cs Jugador.cs Partida.cs Estadisticas.cs
    ) else (
        where dotnet >nul 2>nul
        if %errorlevel% == 0 (
            dotnet build -o .
        )
    )
)

if exist "PiedraPapelTijera.exe" (
    cls
    PiedraPapelTijera.exe
) else (
    echo [ERROR] No se pudo encontrar ni generar PiedraPapelTijera.exe.
    echo Asegurate de compilar el proyecto en Visual Studio.
)

pause
