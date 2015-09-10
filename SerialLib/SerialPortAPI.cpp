#include "SerialPortAPI.h"

CSyncSerialComm * __stdcall SerialPortInit(const WCHAR *portName)
{
	CSyncSerialComm *sp = new CSyncSerialComm(portName);
	return sp;
}

void __stdcall SerialPortDestroy(CSyncSerialComm *sp)
{
	delete sp;
}

HRESULT __stdcall SerialPortOpen(CSyncSerialComm *sp)
{
	return sp->Open();
}

HRESULT __stdcall SerialPortClose(CSyncSerialComm *sp)
{
	return sp->Close();
}
