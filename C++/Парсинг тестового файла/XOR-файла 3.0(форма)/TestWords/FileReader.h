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

class FileReader
{
public:
	FileReader(int nBufSize = SFBUF_SIZE);
	virtual ~FileReader();
	
	bool Open(std::string szFile);
	void Close();

	int GetNextLine( int lineSize = 0 );
	void parseSymbol( char symbol );

	void FilterWithXOR();
	void FilterWithoutXOR();

	int	m_wordsCount;
	std::string word;

	int word_j;

	std::string key;

	char* szLine;
protected:
	int m_nBufferSize;
	int	m_dwRead;
	int	m_dwLine;
	int	m_dwIndex;
	int	m_nSectionCount;

	int nOut;

	int tmp_counter;

	char *m_pBuffer;
	
	std::ifstream	m_fFile;

	bool bStop;
};

#endif // !defined(AFX_STRINGFILE_H__A6247296_4E48_11D2_BF32_0040333952B6__INCLUDED_)
