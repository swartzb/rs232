#include "SerialPortAPI.h"

SerialPort *SerialPortInit(const TCHAR *portName)
{
	SerialPort *sp = new SerialPort(portName);
	return sp;
}

void SerialPortDestroy(SerialPort *sp)
{
	delete sp;
}
