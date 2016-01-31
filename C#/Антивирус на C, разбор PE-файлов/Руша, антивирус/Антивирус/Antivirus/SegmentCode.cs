using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Antivirus
{
    public class SegmentCode
    {
        //байт массив сегмента кода
        public byte[] CodeBytes;

        //размер сегмента кода
        private readonly long _sizeofSegment;
        private readonly long _pointer;
        private readonly FileStream _nameOfFile;

        //конструктор сегмента кода от размера его,смещение, FileStream
        //и название сегмента
        public SegmentCode(long length, long pointer, string f)
        {
            this._sizeofSegment = length;
            this._pointer = pointer;
            this._nameOfFile = File.OpenRead(f);


            // Функция считывания сегмента кода
            // Становимся на начало считывания
            _nameOfFile.Seek(_pointer, SeekOrigin.Begin);

            // Заводим массив
            CodeBytes = new byte[_sizeofSegment];

            // Считываем
            // БЫЛО: nameOfFile.read(codeBytes);
            //_nameOfFile.Read(CodeBytes, (int)_pointer, (int)_sizeofSegment);
            _nameOfFile.Read(CodeBytes, 0, (int)_sizeofSegment);

            _nameOfFile.Close();
        }

        public SegmentCode(byte[] codeBytes)
        {
            this.CodeBytes = codeBytes;
            this._sizeofSegment = codeBytes.Length;
        }

        // Получили сигнатуру вируса
        public virtual bool Find(SegmentCode virusSegment)
        {
            bool ans = false;
            try
            {
                // Пытаемся найти ошибку
                ans = VirusLibrary.KMP(virusSegment.CodeBytes, CodeBytes);// ВОТ ТУТ БЫЛА ЛОГИЧЕСКАЯ ОШИБКА!
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            return ans;
        }

        public void Close()
        {
            this._nameOfFile.Close();
        }
        
    }
}

