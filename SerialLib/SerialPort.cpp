#include "SerialPort.h"
#include <assert.h>

SerialPort::SerialPort(const WCHAR *portName)
	: m_handle(INVALID_HANDLE_VALUE)
{
	assert(portName);

	size_t len = wcslen(portName);
	m_PortName = new WCHAR[len + 1];
	wcsncpy_s(m_PortName, len + 1, portName, len);
}


SerialPort::~SerialPort()
{
	delete[] m_PortName;
}
