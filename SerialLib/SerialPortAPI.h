#pragma once

#include "SerialPort.h"

extern "C" __declspec(dllexport)
SerialPort * __stdcall SerialPortInit(const WCHAR *portName);

extern "C" __declspec(dllexport)
void __stdcall SerialPortDestroy(SerialPort *sp);
