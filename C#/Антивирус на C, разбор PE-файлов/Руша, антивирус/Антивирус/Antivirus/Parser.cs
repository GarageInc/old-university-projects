
using System;

namespace Antivirus
{
    using System.Collections.Generic;
    using System.IO;

    //Класс в котором из файла выдираются области кода
    public class Parser
    {
        //сработал ли парсер
        public bool doneRenamed = false;

        // обертка для нашего бинарного файла,которая обеспечивает прямой доступ
        private FileStream _nameOfFile;

        // Все сегменты из файла
        public List<SegmentCode> AllSegments = new List<SegmentCode>();

        //количество сегментов
        private long _numberOfSegments;

        //класс для работы с областями
        public Parser(string f)
        {
            _nameOfFile = File.OpenRead(f);
        }

        //попытка парсинга
        public virtual long doIt()
        {
            long ans = 0;
            if (this.doneRenamed)
            {
                return 2;
            }
            //реализация парсинга
            var ch1 = (char)_nameOfFile.ReadByte();
            var ch2 = (char)_nameOfFile.ReadByte();

            if (!(ch1 == 'M' && ch2 == 'Z'))
            {
                return 1; //если не дос заголовок,то это не ПЕ файл
            }
            
            _nameOfFile.Seek(0x3c,SeekOrigin.Begin); //Если это ПЕ файл то по этому адресу должно лежать смещение
            // ReSharper disable once InconsistentNaming
            long offsetTOPEFile = ReadDWord(); //считываем смещение
            _nameOfFile.Seek(offsetTOPEFile, SeekOrigin.Begin); //перемещаемся к заголовку ПЕ файла
            ch1 = (char)_nameOfFile.ReadByte();
            ch2 = (char)_nameOfFile.ReadByte();

            if (!(ch1 == 'P' && ch2 == 'E'))
            {
                return 1; //последняя поверка на ПЕ
            }

            _nameOfFile.Seek(offsetTOPEFile + 6, SeekOrigin.Begin);
            _numberOfSegments = ReadWord();
            _nameOfFile.Seek(offsetTOPEFile + 248, SeekOrigin.Begin);

            // Считываем все сегменты файла
            for (long i = 0; i < _numberOfSegments; i++)
            {
                ReadSegment(_nameOfFile.Name);
            }

            // Закрываем поток, который считывал этот файл
            Close();
            doneRenamed = true;
            return ans;
        }
        
        
        // Чтение сегмента
        private void ReadSegment(string f)
        {
            string name = "";

            // Считывается имя файла
            for (long i = 0; i < 8; i++)
            {
                var ch = (char) _nameOfFile.ReadByte();
                if (ch > 21 && ch < 128)
                {
                    name = name + ch;
                }
            }

            ReadDWord(); //DWORD VirtualSize;
            ReadDWord(); //VirtualAdress;

            long length = ReadDWord(); //   DWORD SizeOfRawData;
            long pointer = ReadDWord(); // DWORD pointerToRawData;

            ReadDWord(); //DWORD pointerToRelocations;
            ReadDWord(); //DWORD pointerToLinenumbers;
            ReadWord(); //WORD NumberOfRelocations;
            ReadWord(); //WORD NumberOfLinenumbers;

            long x = ReadDWord(); //DWORD Characteristics;

            List<long?> temp = new List<long?>();

            while (x > 0)
            {
                temp.Add(x % 16);
                x = x / 16;
            }

            if (temp.Count < 8 || temp[1] != 2)
            {
                return;
            }

            AllSegments.Add(new SegmentCode(length, pointer, f));
        }

        //функция чтения WORD(unsigned short)
        public virtual long ReadWord()
        {
            byte[] mas = new byte[2];
            _nameOfFile.Read(mas, 0, 2);
            return Read(ref mas, 2);
        }

        //функция чтения DWORD(unsigned long)
        public virtual long ReadDWord()
        {
            byte[] mas = new byte[4];
            _nameOfFile.Read(mas, 0, 4);
            return Read(ref mas, 4);
        }

        //функция используеая для работы двух верхних функций
        private static long Read(ref byte[] mas, long numberOfByte)
        {
            long ans = 0;
            for (long i = 0; i < numberOfByte; i++)
            {
                for (long j = 0; j < 8; j++)
                {
                    //System.ouprlong(Math.abs(codeBytes[i] % 2));
                    ans += (long)Math.Pow(2, j + i * 8) * (Math.Abs(mas[i] % 2));
                    mas[i] >>= 1;
                }
                //System.ouprlong(" ");
            }
            return ans;
        }
        
        // Фукнция закрытия потока
        public void Close()
        {
            if (this._nameOfFile != null)
                this._nameOfFile.Close();
        }
    }
}
