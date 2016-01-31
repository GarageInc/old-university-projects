// Интерфейс
#include<afx.h>
#if !defined(AFX_STRINGFILE_H__A6247296_4E48_11D2_BF32_0040333952B6__INCLUDED_)
#define AFX_STRINGFILE_H__A6247296_4E48_11D2_BF32_0040333952B6__INCLUDED_

// Оптимальный считываем раздел
#define SFBUF_SIZE	2048

#if _MSC_VER > 1000
#pragma once
#endif 


class CLogReader
{
public:
	CLogReader(int nBufSize = SFBUF_SIZE);
	virtual ~CLogReader();
	bool Open(LPCTSTR szFile, CFileException *feError=NULL);
	void Close();

	DWORD GetNextLine(LPSTR szLine,int iLineSize);
	DWORD GetNextLine(CString &szLine);

	bool SetFilter(CString filter);// Установка фильтра
	bool FilterLine(CString szLine);// Фильтрование
	bool SubFilterLineFunction(CString szLine, int i);// Выделил часть кода, дабы читалось легче

protected:
	int m_nBufferSize;
	DWORD	m_nMaxSize;
	DWORD	m_dwRead;
	DWORD	m_dwLine;
	DWORD	m_dwMasterIndex;
	DWORD	m_dwIndex;
	int		m_nSectionCount;
	BYTE	*m_pBuffer;
	CFile	m_fFile;
	char	firstFilter;
	char	secondFilter;
	CString filterBody;
};

#endif // !defined(AFX_STRINGFILE_H__A6247296_4E48_11D2_BF32_0040333952B6__INCLUDED_)
