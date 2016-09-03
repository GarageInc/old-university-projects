#include "CLogReader.h"

CLogReader::CLogReader(int nBufSize)
{
	m_dwRead = nBufSize;
	m_nBufferSize = nBufSize;
	m_pBuffer = new char[nBufSize];
	m_dwIndex = 0;
	m_dwLine = 0;

	firstFilter = '-';
	secondFilter = '-';
}

CLogReader::~CLogReader()
{
	delete m_pBuffer;
	delete szLine;
}

bool CLogReader::Open(char* szFile)
{
	m_fFile.open(szFile);

	if (!m_fFile.is_open())
	{
		return false;
	}
		
	return true;
}


void CLogReader::Close()
{
	m_fFile.close();
	m_dwIndex = 0;
	m_dwLine = 0;
	firstFilter = '-';
	secondFilter = '-';
}

int CLogReader::GetNextLine()
{
	int iLineSize = m_nBufferSize;

	szLine = new char[ iLineSize];

	bool	bStop = false;

	nOut = 0;
	
	while(!bStop)
	{
		if(!m_dwLine || m_dwIndex==m_dwRead)
		{
			m_fFile.seekg(0, m_fFile._Seekcur);
			m_fFile.read(m_pBuffer,m_nBufferSize);//read			
			m_dwRead= m_fFile.gcount();
			

			m_dwIndex = 0;
			if(m_dwRead == 0)
			{
				bStop = true; 
				if(nOut>0)
				{
					szLine[nOut++] = (char) 0;
					return m_dwLine;	
				}
				else return m_dwLine = 0; 
			}
			else
			{
				if(m_dwRead != (int) m_nBufferSize)
					bStop = true;	//END-OF-FILE
			}
		}
		for(;m_dwIndex < m_dwRead; m_dwIndex++)
		{
			if((nOut+1) == iLineSize)
			{
				m_dwLine++;
				return m_dwLine;
			}
			switch(m_pBuffer[m_dwIndex])
			{
				case 0x0d://End of Line 
				case 0x0a:
					if((m_dwIndex+1) < m_dwRead) // Check we're not on end of m_pBuffer
						if(m_pBuffer[m_dwIndex+1] == '\n' || m_pBuffer[m_dwIndex+1] == '\r')
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
				default: szLine[nOut++] = m_pBuffer[m_dwIndex];
			}
			//szLine[nOut++] = m_pBuffer[m_dwIndex];
		}
	}
	if(nOut>0)
	{
		szLine[nOut++] = '\0';
		return m_dwLine;	
	}
	return m_dwLine = 0; //nix gelezen
}

// Валидация
// правильные маски: abc, *abc, ?abc, abc*, abc?, *abc*, ?abc?, *abc?, ?abc*. 
// Причем количество символов может быть абсолютно любым
// Фильтр типа abc???? - означает, что должны быть отобраны строки, начинающиеся с abc и заказнчивающиеся любым дополнительным символом(условие неисключаемое)
bool CLogReader::SetFilter(char* filter)
{
	filterBody = "";

	int i = 0;
	
		// Getting of first symbol
		// first part
		if ( filter[ i] && filter[ i] == '*')
		{
			firstFilter = '*';
			// body of filter
			while ( filter[ i] && filter[ i] == '*')
				i++;
		}
		else if ( filter[ i] && filter[ i] == '?')
		{
			firstFilter = '?';
			while ( filter[ i] && filter[ i] == '?')
				i++;
		}
		
		if ( i >= strlen(filter))
			return false;

		// main body
		if ( filter[ i] && filter[ i] != '*' && filter[ i] != '?')
		{
			while ( filter[ i] && filter[ i] != '*' && filter[ i] != '?')
			{
				filterBody += filter[ i];
				i++;
			}
		}
		
		// second part
		if( filter[ i] && filter[ i] == '*')
		{
			secondFilter = '*';

			while ( filter[ i] && filter[ i] == '*')
				i++;
		}
		else if ( filter[ i] && filter[ i] == '?')
		{
			secondFilter = '?';

			while ( filter[ i] && filter[ i] == '?')
				i++;
		}

		if ( i < nOut)
			return false;
		
		return true;
}

// Считаю, что * - любая последовательность символов(вплоть до пустой последовательности)
// Считаю, что ? - один единиченый символ
// При этом семантически * === ***..** и ? === ?????..?
bool CLogReader::FilterLine()
{
	int i = 0;

	// Если первая строка начинается с заданного символа:
	if ( firstFilter == '-')
	{
		// Начинается ли данная строка с заданного слова?
		while ( szLine[ i] && filterBody[ i] && filterBody[ i]==szLine[ i])
		{
			i++;
		}

		// Если нет - возвращаем НЕТ
		if ( i < strlen(filterBody))
			return false;
		// иначе, если слово прочитано, проверяем на второй фильтр(его наличие и если он есть - на его тип)
		else
		{
			return SubFilterLineFunction( szLine, i);
		}
	}
	// Если сначала идет произвольное количество символов
	else if ( firstFilter=='*')
	{
		// as cicle

		L1:
		// Ищем вхождение нашего слова
		int j = 0;
		while ( szLine[ i] && filterBody[j] && szLine[ i] != filterBody[j])
		{
			i++;
		}

		// Если мы где-то посреди строки нашли наше слово - то считываем его
		if ( i < nOut)
		{
			// Считываем, пока совпадают
			while (  szLine[ i] && filterBody[j] && szLine[ i] == filterBody[j])
			{
				i++; 
				j++;
			}
			// если полностью считали наше слово, то фильтруем, иначе - идем до конца
			if ( !filterBody[j])
			{
				return SubFilterLineFunction(szLine, i);
			}
			else
			{
				// WARNING!
				// По-хорошему сюда цикл нужно написать, но эта лапша код не портит.
				goto L1;
			}
		}
		else
		{
			return false;
		}
	}
	// Если вначале идет один любой символ
	else if(firstFilter='?')
	{
		// Начинаем считывание со второго символа
		i++;
		
		int j = 0;
		// Считываем слово, которое должно находиться в строке
		// если не находится - то извините, но фильтр не подходит
		while (szLine[ i] && filterBody[j] && szLine[ i] == filterBody[j])
		{
			i++;
			j++;
		}

		// если не дошли до конца тела фильтра - то увы
		if (!filterBody[j])
		{
			return SubFilterLineFunction(szLine, i);
		}
		else
		{
			return false;
		}
	}
	
	return true;
}


bool CLogReader::SubFilterLineFunction(char* szLine, int i)
{
	// если без фильтра - строки должны  совпадать по длине
	if ( secondFilter == '-')
	{
		// проверка на "abc", на совпадение
		if ( !szLine[ i])
			return true;
		else
			return false;
	}
	// abc тоже подходит под фильтрм abc****
	else if ( secondFilter == '*')
	{
		return true;
	}
	// проверим на одиночный символ после фильтруемого слова
	else if ( secondFilter == '?')
	{
		if ( i + 1 == strlen( szLine))
			return true;
		else
			return false;
	}
	else
	{
		return true;
	}
}