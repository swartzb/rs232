#include "SerialPort.h"
#include <assert.h>
#include <tchar.h>

SerialPort::SerialPort(const TCHAR *portName)
	: m_handle(INVALID_HANDLE_VALUE)
{
	assert(portName);

	size_t len = _tcslen(portName);
	m_PortName = new TCHAR[len + 1];
	_tcsncpy(m_PortName, portName, len + 1);
}


SerialPort::~SerialPort()
{
	delete[] m_PortName;
}
