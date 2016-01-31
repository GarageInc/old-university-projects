using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaffmaneCode_GUI
{
    class LZW
    {
        Dictionary<string, int> _myDictionary;
        int _countCode;
        List<int> _arrayResult;

        public LZW()
        {
            _myDictionary = new Dictionary<string, int>();
            _arrayResult = new List<int>();
            _countCode = 0;
        }

        // Формирование словаря символов - разобъем слово на буквы
        public void FormationDictionary(string source) // формирование словаря
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (!_myDictionary.ContainsKey(source[i].ToString()))
                {
                    _myDictionary.Add(source[i].ToString(), i);
                }
            }
            _countCode = _myDictionary.Count();
        }

        // Функция зашифровки сообщения
        public void Code(string message, ref string txtResultCrypt)
        {
            // Снова формируем словарь
            this.FormationDictionary(message);
            var bufferSequenceLetters = "";
            var codeOutput = 0;

            foreach (string convertLetter in message.Select(letter => letter.ToString()))
            {
                bufferSequenceLetters += convertLetter; // добавление символа к последовательности

                int value;
                if (_myDictionary.TryGetValue(bufferSequenceLetters, out value)) // ключ найден
                {
                    codeOutput = _myDictionary[bufferSequenceLetters]; // сохранение кода символа
                }
                else // ключ не найден
                {
                    _countCode++; // хранение значения кода для последующих последовательностей
                    _myDictionary.Add(bufferSequenceLetters, _countCode); // добавление послежовательности в словарь
                    _arrayResult.Add(codeOutput); //добавление результата

                    // вывод на форму
                    txtResultCrypt += codeOutput + " ";
                    
                    bufferSequenceLetters = convertLetter;  // присваиваем текущий символ
                    codeOutput = _myDictionary[bufferSequenceLetters];
                }
            }
            _arrayResult.Add(_myDictionary[bufferSequenceLetters]); // добавляем последнюю последовательность

            // вывод на форму
            txtResultCrypt += codeOutput.ToString();
        }

        // Декодирование сообщения
        public void Decode(string source,ref string txtOutput)
        {
            _myDictionary.Clear();
            _countCode = 256;

            FormationDictionary(source); // формирование первых символов

            var message = "";
            var currentLetter = "";

            foreach (var code in _arrayResult)
            {
                var key = GetKeyByValue(code); // узнать ключ по значению
                message += key;

                if (key != null)
                {
                    if (code > 256) // формируем последовательность символов для добавления в словарь
                        currentLetter += key[0].ToString();
                    else
                        currentLetter += key;

                    int value;
                    if (!_myDictionary.TryGetValue(currentLetter, out value)) // если значения нет в словаре, то добавляем его
                    {
                        _countCode++;
                        _myDictionary.Add(currentLetter, _countCode);

                        currentLetter = key;
                    }
                }
                else // если кода нет в словаре, то берём текущий символ и прибавляем к нему первый байт
                {
                    _countCode++;
                    currentLetter += currentLetter[0];
                    message += currentLetter;

                    _myDictionary.Add(currentLetter, _countCode);
                }
            }

            txtOutput += message;
        }

        // Функция получения ключа по значению.
        public string GetKeyByValue(int value)
        {
            return (from record in _myDictionary where record.Value.Equals(value) select record.Key).FirstOrDefault();
        }

        // Печать словаря
        public string GetString()
        {
            string result = "";
            foreach (var i in _myDictionary)
                result += (i.Key + ":" + i.Value + "    ");
            return result;
        }
    }
}
