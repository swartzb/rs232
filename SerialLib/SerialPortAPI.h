#pragma once

#include "SerialPort.h"
#include <tchar.h>

__declspec(dllexport)
SerialPort *SerialPortInit(const TCHAR *portName);

__declspec(dllexport)
void SerialPortDestroy(SerialPort *sp);
