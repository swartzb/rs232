#include "SerialPortAPI.h"

SerialPort * __stdcall SerialPortInit(const TCHAR *portName)
{
	SerialPort *sp = new SerialPort(portName);
	return sp;
}

void __stdcall SerialPortDestroy(SerialPort *sp)
{
	delete sp;
}
