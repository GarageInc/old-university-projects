// FileReader.cpp: implementation of the FileReader class.
#include "FileReader.h"

//
//#ifdef _DEBUG
//#undef THIS_FILE
//static char THIS_FILE[]=__FILE__;
//#define new DEBUG_NEW
//#endif

// Constructor
FileReader::FileReader( int nBufSize )
{
	m_dwRead = nBufSize;
	m_nBufferSize = nBufSize;
	m_pBuffer = new char[ nBufSize ];
	szLine = new char[ nBufSize ];

	m_dwIndex = 0;
	m_dwLine = 0;

	m_wordsCount = 0;

	word = "";
	word_j = 0;
	key = "";
}

// Деструктор
FileReader::~FileReader()
{
	delete m_pBuffer;
}

bool FileReader::Open( std::string szFile )
{
	m_fFile.open( szFile );

	if ( !m_fFile.is_open() )
	{
		return false;
	}

	return true;
}


void FileReader::Close()
{
	m_fFile.close();
	m_dwIndex = 0;
	m_dwLine = 0;
}


int FileReader::GetNextLine( int lineSize )
{
	int iLineSize = m_nBufferSize;

	if  ( lineSize > 0 ) {

		iLineSize = lineSize;
		szLine = new char[ iLineSize ];// dog-nail
	}// pass

	bool	bStop = false;
	int		nOut;

	nOut = 0;

	while (!bStop)
	{
		if (!m_dwLine || m_dwIndex == m_dwRead)
		{
			m_fFile.seekg(0, m_fFile._Seekcur);
			m_fFile.read(m_pBuffer, m_nBufferSize);
			m_dwRead = m_fFile.gcount();

			m_dwIndex = 0;
			if (m_dwRead == 0)
			{
				bStop = true;
				if (nOut>0)
				{
					szLine[nOut++] = (char)0;
					return m_dwLine;
				}
				else return m_dwLine = 0; // nix gelezen
			}
			else
			{
				if (m_dwRead != (int)m_nBufferSize)
					bStop = true;	// END-OF-FILE
			}
		}
		for (; m_dwIndex < m_dwRead; m_dwIndex++)
		{
			if ((nOut + 1) == iLineSize)
			{
				m_dwLine++;
				return m_dwLine;
			}

			/*
			switch (m_pBuffer[m_dwIndex])
			{
				case 0x0d:// \r - возврат каретки End of Line encountered
				case 0x0a:// \n
					if ((m_dwIndex + 1) < m_dwRead) // Check we're not on end of m_pBuffer ???
						if (m_pBuffer[m_dwIndex + 1] == '\n' || m_pBuffer[m_dwIndex + 1] == '\r')
						{
							if (nOut == 0)
								m_dwLine++;
							m_dwIndex++;
						}
					if (nOut != 0)
					{
						szLine[nOut++] = '\0';
						m_dwLine++;

						return m_dwLine;
					}
					break;
				default: {
					//chTemp[nOut++] = m_pBuffer[m_dwIndex];
					szLine[nOut++] = m_pBuffer[m_dwIndex];
			}
			}*/

			szLine[nOut++] = m_pBuffer[ m_dwIndex ];
		}
	}

	if (nOut > 0)
	{
		szLine[nOut++] = '\0';
		return m_dwLine;
	}
	return m_dwLine = 0; //nix gelezen
}

void FileReader::FilterWithXOR()
{
	int i = 0;
	int k = 0;

	while (szLine[i]) {

		// если чтение слова не оборвалось по-середине. Но всякое бывает - чтение кусочка файла может завершиться на середине требуемого слова или слова, на него похожего

		if (szLine[i] && szLine[i] ^ key[k] != word[word_j]) {

			word_j = 0;
		}

		if (word_j == 0) {

			while (szLine[i] && szLine[i] ^ key[k] != word[word_j]) {
				i++;
				k++;
			}
		}

		while (szLine[i] && szLine[i] ^ key[k] == word[word_j] && word[word_j]) {
			i++;
			k++;

			word_j++;
		}

		if ( !word[word_j] ) {
			m_wordsCount++;

			word_j = 0;
		}// pass
	}
}

void FileReader::FilterWithoutXOR()
{
	int i = 0;
	int k = 0;
	
	while ( szLine[ i ] ) {
		
		// если чтение слова не оборвалось по-середине. Но всякое бывает - чтение кусочка файла может завершиться на середине требуемого слова или слова, на него похожего
		
		if (szLine[i] && szLine[i] != word[word_j]) {

			word_j = 0;
		}

		if ( word_j == 0 ) {

			while (szLine[i] && szLine[i] != word[word_j]) {
				i++;
				k++;
			}
		}

		printf("%d\n", word_j);
		while (szLine[i] && szLine[i] == word[word_j] && word[word_j]) {
			i++;
			k++;

			word_j++;
		}
		
		if ( !word[word_j] ) {
			m_wordsCount++;

			word_j = 0;
		}// pass
	}

}