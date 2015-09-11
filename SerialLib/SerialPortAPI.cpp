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

DWORD __stdcall SerialPortOpen(CSyncSerialComm *sp)
{
	return sp->Open();
}

DWORD __stdcall SerialPortClose(CSyncSerialComm *sp)
{
	return sp->Close();
}

DWORD __stdcall SerialPortConfig(CSyncSerialComm *sp, DWORD dwBaudRate, DWORD dwTimeOutInSec)
{
	return sp->ConfigPort(dwBaudRate, dwTimeOutInSec);
}

DWORD __stdcall SerialPortWrite(CSyncSerialComm *sp, const WCHAR *pszBuf, DWORD dwSize)
{
	return sp->Write(pszBuf, dwSize);
}

DWORD __stdcall SerialPortRead(CSyncSerialComm *sp, char * const pszBuf, DWORD *dwSize)
{
	return sp->Read(pszBuf, dwSize);
}
