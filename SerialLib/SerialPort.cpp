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

//////////////////////////////////////////////////////////////////////
// Name: Open
// Version: 1.0
// Return: HRESULT
// Comment: This function is used open a connection with a serial port.
// Uses non-overlapped i/o, and allows for reading & writing to the 
// port.
//////////////////////////////////////////////////////////////////////

HRESULT SerialPort::Open()
{
	HRESULT hResult;

	m_handle = CreateFile(m_PortName, /* Port Name */
		GENERIC_READ | GENERIC_WRITE, /* Desired Access */
		0, /* Shared Mode */
		NULL, /* Security */
		OPEN_EXISTING, /* Creation Disposition */
		0,
		NULL); /* Non Overlapped */

	if (m_handle == INVALID_HANDLE_VALUE)
	{
		unsigned long error = ::GetLastError();
		hResult = E_FAIL;
	}

	else
		hResult = S_OK;

	return hResult;
}

//////////////////////////////////////////////////////////////////////
// Name: Close
// Version: 1.0
// Return: HRESULT
// Comment: This function is used to close the serial port connection
// Note: This function is called with the destructor
//////////////////////////////////////////////////////////////////////

HRESULT SerialPort::Close()
{
	if (m_handle != INVALID_HANDLE_VALUE)
	{
		CloseHandle(m_handle);
		m_handle = INVALID_HANDLE_VALUE;
	}

	return S_OK;
}

