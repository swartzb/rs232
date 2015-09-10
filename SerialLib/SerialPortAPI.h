#pragma once

#include "SerialPort.h"
#include <tchar.h>

extern "C" __declspec(dllexport)
SerialPort * __stdcall SerialPortInit(const TCHAR *portName);

extern "C" __declspec(dllexport)
void __stdcall SerialPortDestroy(SerialPort *sp);
