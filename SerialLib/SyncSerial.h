#pragma once

#include <windows.h>

extern "C" __declspec(dllexport)
DWORD __stdcall OpenSerialPort(HANDLE *h, LPCWSTR portName);

extern "C" __declspec(dllexport)
DWORD __stdcall CloseSerialPort(HANDLE h);

extern "C" __declspec(dllexport)
DWORD __stdcall ConfigSerialPort(HANDLE h, DWORD dwBaudRate, DWORD dwTimeOutInSec);

extern "C" __declspec(dllexport)
DWORD __stdcall FlushSerialPort(HANDLE h);

extern "C" __declspec(dllexport)
DWORD __stdcall WriteSerialPort(HANDLE h, const CHAR *pszBuf, DWORD dwSize);

extern "C" __declspec(dllexport)
DWORD __stdcall ReadSerialPort(HANDLE h, char * const pszBuf, DWORD bufSize, DWORD *dwSize);
