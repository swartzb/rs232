#pragma once

#include "SyncSerialComm.h"

extern "C" __declspec(dllexport)
CSyncSerialComm * __stdcall SerialPortInit(const WCHAR *portName);

extern "C" __declspec(dllexport)
void __stdcall SerialPortDestroy(CSyncSerialComm *sp);

extern "C" __declspec(dllexport)
HRESULT __stdcall SerialPortOpen(CSyncSerialComm *sp);

extern "C" __declspec(dllexport)
HRESULT __stdcall SerialPortClose(CSyncSerialComm *sp);
