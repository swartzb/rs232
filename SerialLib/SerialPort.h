#pragma once

#include <windows.h>

class SerialPort
{
public:
	SerialPort(const WCHAR *portName);
	~SerialPort();
	HRESULT Open();
	HRESULT Close();

private:
	WCHAR *m_PortName;
	HANDLE m_handle;
};

