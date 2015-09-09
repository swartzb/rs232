#pragma once

#include <windows.h>

class SerialPort
{
public:
	SerialPort(const TCHAR *portName);
	~SerialPort();

private:
	TCHAR *m_PortName;
	HANDLE m_handle;
};

