#define _CRT_SECURE_NO_WARNINGS
#include "FileReader.h"
#include <fstream>

void ReadTextFile()
{
	FileReader 	sfText;

	std::string	szLine;// = new char[100];
	std::string xorKey = "0123456789ABCDEF0123456789ABCDEF";
	std::string word = "";
	
	bool		bReturn = false;
	char pathToFile[101];
	char wordTmp[101];

	printf("->> Path to file: ");
	scanf("%100s", &pathToFile);
	std::string path = ((std::string)pathToFile).c_str();
	
	int count = 0;

	if (sfText.Open(path))
	{
		printf("->> Word: ");
		scanf("%100s", &wordTmp);
		
		sfText.word = ((std::string)wordTmp).c_str();
		sfText.key = xorKey;

		printf("...in processing...:\n");

		while (sfText.GetNextLine() != 0)
		{
			sfText.FilterWithoutXOR();
		}

		printf("<<- Count: %d\n", sfText.m_wordsCount);
		sfText.Close();
	}
	else
	{
		printf("\n<<- Invalid file path/name!\n");
	}
}


int main()
{
	std::string str;
	char tmp[101];

	while (true)
	{
		printf("\nAre you want to filter?(input 1 to YES, 0(or other symbol) for NO)\n");

		scanf("%100s", tmp);
		std::string str = tmp;

		if (str != "1")
			break;
		else
		{
			ReadTextFile();
		}
	}

	system("pause");
	return 0;
}