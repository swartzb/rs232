#pragma once

#include <windows.h>

class SerialPort
{
public:
	SerialPort(const WCHAR *portName);
	~SerialPort();

private:
	WCHAR *m_PortName;
	HANDLE m_handle;
};

