// Интерфейс
#if !defined(AFX_STRINGFILE_H__A6247296_4E48_11D2_BF32_0040333952B6__INCLUDED_)
#define AFX_STRINGFILE_H__A6247296_4E48_11D2_BF32_0040333952B6__INCLUDED_

// Оптимальный считываем раздел
#define SFBUF_SIZE	2048

#if _MSC_VER > 1000
#pragma once
#endif 

#include <stdio.h>
#include <fstream>

class CLogReader
{
public:
	CLogReader(int nBufSize = SFBUF_SIZE);
	virtual ~CLogReader();
	bool Open(std::string szFile);
	void Close();

	int GetNextLine(char* szLine,int iLineSize);
	int GetNextLine(std::string * szLine);

	bool SetFilter(std::string filter);// Установка фильтра
	bool FilterLine(std::string szLine);// Фильтрование
	bool SubFilterLineFunction(std::string szLine, int i);// Выделил часть кода, дабы читалось легче

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
	std::string filterBody;
};

#endif // !defined(AFX_STRINGFILE_H__A6247296_4E48_11D2_BF32_0040333952B6__INCLUDED_)
