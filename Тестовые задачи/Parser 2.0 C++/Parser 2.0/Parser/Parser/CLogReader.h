// Интерфейс
#if !defined(AFX_STRINGFILE_H__A6247296_4E48_11D2_BF32_0040333952B6__INCLUDED_)
#define AFX_STRINGFILE_H__A6247296_4E48_11D2_BF32_0040333952B6__INCLUDED_

// Оптимальный считываемый раздел
#define SFBUF_SIZE	2048

#if _MSC_VER > 1000
#pragma once
#endif 

#include <fstream>

class CLogReader
{
public:
	CLogReader(int nBufSize = SFBUF_SIZE);
	virtual ~CLogReader();

	bool Open(char* szFile);
	void Close();

	int GetNextLine();

	bool SetFilter( char* filter);// Установка фильтра
	bool FilterLine();
	bool SubFilterLineFunction( char* szLine, int i);
	
	char* szLine;
	int		nOut;

protected:
	int m_nBufferSize;
	int	m_dwRead;
	int	m_dwLine;
	int	m_dwIndex;

	int		m_nSectionCount;
	char	*m_pBuffer;

	std::ifstream	m_fFile;

	char	firstFilter;
	char	secondFilter;

	char* filterBody;
};

#endif // !defined(AFX_STRINGFILE_H__A6247296_4E48_11D2_BF32_0040333952B6__INCLUDED_)
