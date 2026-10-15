@echo off
chcp 65001 >nul
title Piedra, Papel, Tijera, Lagarto, Spock
cd /d "%~dp0"

if not exist "bin\Debug\PPT.exe" (
    echo Compilando el proyecto...
    "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" PPT.csproj /nologo /v:q
)

if exist "bin\Debug\PPT.exe" (
    "bin\Debug\PPT.exe"
) else (
    echo Error: No se pudo encontrar ni generar el ejecutable.
    pause
)
