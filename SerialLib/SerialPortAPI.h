#pragma once

#include "SyncSerialComm.h"

extern "C" __declspec(dllexport)
CSyncSerialComm * __stdcall SerialPortInit(const WCHAR *portName);

extern "C" __declspec(dllexport)
void __stdcall SerialPortDestroy(CSyncSerialComm *sp);

extern "C" __declspec(dllexport)
DWORD __stdcall SerialPortOpen(CSyncSerialComm *sp);

extern "C" __declspec(dllexport)
DWORD __stdcall SerialPortClose(CSyncSerialComm *sp);

extern "C" __declspec(dllexport)
DWORD __stdcall SerialPortConfig(CSyncSerialComm *sp, DWORD dwBaudRate, DWORD dwTimeOutInSec);

extern "C" __declspec(dllexport)
DWORD __stdcall SerialPortWrite(CSyncSerialComm *sp, const CHAR *pszBuf, DWORD dwSize);

extern "C" __declspec(dllexport)
DWORD __stdcall SerialPortRead(CSyncSerialComm *sp, char * const pszBuf, DWORD bufSize, DWORD *dwSize);

extern "C" __declspec(dllexport)
DWORD __stdcall SerialPortFlush(CSyncSerialComm *sp);
