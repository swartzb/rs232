#include "SyncSerial.h"
#include <assert.h>
#include <stdio.h>

DWORD __stdcall OpenSerialPort(HANDLE *h, LPCWSTR portName)
{
	DWORD error;

	*h = CreateFile(portName, /* Port Name */
		GENERIC_READ | GENERIC_WRITE, /* Desired Access */
		0, /* Shared Mode */
		NULL, /* Security */
		OPEN_EXISTING, /* Creation Disposition */
		0,
		NULL); /* Non Overlapped */

	if (*h == INVALID_HANDLE_VALUE)
	{
		error = ::GetLastError();
	}
	else
	{
		error = ERROR_SUCCESS;
	}

	return error;
}

DWORD __stdcall CloseSerialPort(HANDLE h)
{
	DWORD error;

	if (h != INVALID_HANDLE_VALUE)
	{
		if (CloseHandle(h))
		{
			h = INVALID_HANDLE_VALUE;
			error = ERROR_SUCCESS;
		}
		else
		{
			error = ::GetLastError();
		}
	}
	else
	{
		error = ERROR_INVALID_HANDLE;
	}

	return error;
}

DWORD __stdcall ConfigSerialPort(HANDLE h, DWORD dwBaudRate, DWORD dwTimeOutInSec)
{
	if (!SetupComm(h, 1024, 1024))
	{
		return ::GetLastError();
	}

	DCB dcbConfig;

	if (GetCommState(h, &dcbConfig)) /* Configuring Serial Port Settings */
	{
		dcbConfig.BaudRate = dwBaudRate;
		dcbConfig.ByteSize = 8;
		dcbConfig.Parity = NOPARITY;
		dcbConfig.StopBits = ONESTOPBIT;
		dcbConfig.fBinary = TRUE;
		dcbConfig.fParity = TRUE;
	}
	else
	{
		return ::GetLastError();
	}

	if (!SetCommState(h, &dcbConfig))
	{
		return ::GetLastError();
	}

	COMMTIMEOUTS commTimeout;

	if (GetCommTimeouts(h, &commTimeout)) /* Configuring Read & Write Time Outs */
	{
		commTimeout.ReadIntervalTimeout = 1000 * dwTimeOutInSec;
		commTimeout.ReadTotalTimeoutConstant = 1000 * dwTimeOutInSec;
		commTimeout.ReadTotalTimeoutMultiplier = 0;
		commTimeout.WriteTotalTimeoutConstant = 1000 * dwTimeOutInSec;
		commTimeout.WriteTotalTimeoutMultiplier = 0;
	}
	else
	{
		return ::GetLastError();
	}

	if (SetCommTimeouts(h, &commTimeout))
	{
		return ERROR_SUCCESS;
	}
	else
	{
		return ::GetLastError();
	}
}

DWORD __stdcall FlushSerialPort(HANDLE h)
{
	DWORD error = ERROR_SUCCESS;

	if (PurgeComm(h, PURGE_TXABORT | PURGE_RXABORT | PURGE_TXCLEAR | PURGE_RXCLEAR))
	{
		error = ERROR_SUCCESS;
	}
	else
	{
		error = ::GetLastError();
	}

	return error;
}

DWORD __stdcall WriteSerialPort(HANDLE h, const CHAR *pszBuf, DWORD dwSize)
{
	DWORD error = ERROR_SUCCESS;

	assert(pszBuf);

	unsigned long dwNumberOfBytesSent = 0;

	while (dwNumberOfBytesSent < dwSize)
	{
		unsigned long dwNumberOfBytesWritten;

		if (WriteFile(h, &pszBuf[dwNumberOfBytesSent], 1, &dwNumberOfBytesWritten, NULL) != 0)
		{
			if (dwNumberOfBytesWritten > 0)
			{
				++dwNumberOfBytesSent;
			}
			else
			{
				error = ::GetLastError();
				break;
			}
		}

		else
		{
			error = ::GetLastError();
			break;
		}
	}

	return error;
}

DWORD __stdcall ReadSerialPort(HANDLE h, char * const pszBuf, DWORD bufSize, DWORD *dwSize, DWORD *dwEventMask)
{
	DWORD error = ERROR_SUCCESS;

	if (!SetCommMask(h, EV_BREAK | EV_ERR | EV_RXCHAR)) /* Setting Event Type */
	{
		return ::GetLastError();
	}

	if (WaitCommEvent(h, dwEventMask, NULL)) /* Waiting For Event to Occur */
	{
		if (*dwEventMask & EV_BREAK)
		{
			_snprintf_s(pszBuf, bufSize, _TRUNCATE, "BREAK received");
			return error;
		}
		else if (*dwEventMask & EV_ERR)
		{
			_snprintf_s(pszBuf, bufSize, _TRUNCATE, "Line status error occurred");
			return error;
		}
		else if (*dwEventMask & EV_RXCHAR)
		{
			char szBuf;
			DWORD dwIncommingReadSize;
			*dwSize = 0;
			unsigned int ndx = 0;

			do
			{
				if (ReadFile(h, &szBuf, 1, &dwIncommingReadSize, NULL) != 0)
				{
					if (dwIncommingReadSize > 0)
					{
						if (szBuf == '\r')
						{
							break;
						}
						*dwSize += dwIncommingReadSize;
						if (ndx < bufSize - 1)
						{
							pszBuf[ndx] = szBuf;
							ndx += dwIncommingReadSize;
						}
					}
				}
				else
				{
					error = ::GetLastError();
					break;
				}

			} while (dwIncommingReadSize > 0);

			pszBuf[ndx] = '\0';

			return error;
		}
		else
		{
			_snprintf_s(pszBuf, bufSize, _TRUNCATE, "unknown COMM event");
			return error;
		}
	}
	else
	{
		return ::GetLastError();
	}
}