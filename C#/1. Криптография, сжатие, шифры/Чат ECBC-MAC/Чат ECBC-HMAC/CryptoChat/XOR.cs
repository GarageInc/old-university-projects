using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoChat
{
    class XOR
    {
        public int[] backtextmass;
        public int[] textmass;

        public int[] keymass;

        public XOR(int textlength)
        {
            // Расширим наш ключ
            textmass = new int[textlength];
            backtextmass = new int[textlength];
            keymass = new int[textlength];

            Random r = new Random();
            for (int i = 0; i < textlength;)
            {
                int res = r.Next(0, textlength + 1);
                bool notExist = true;
                for (int j = 0; j < textlength; j++)
                {
                    if (res == textmass[j])
                    {
                        notExist = false;
                        break;
                    }
                }
                if (notExist)
                {
                    textmass[i] = res;
                    backtextmass[res - 1] = i;
                    i++;
                }
            }

            for (int i = 0; i < textlength;)
            {
                int res = r.Next(0, textlength + 1);
                bool notExist = true;
                for (int j = 0; j < textlength; j++)
                {
                    if (res == keymass[j])
                    {
                        notExist = false;
                        break;
                    }
                }
                if (notExist)
                {
                    keymass[i] = res;
                    i++;
                }
            }
        }

        // Принимает текст и ключ. XOR - основная операция - поступают блоки не меньше 8 байт
        public byte[] EncodeVernamCipher(byte[] text, byte[] key)
        {
            // Сделаем ключ длиной как текст(для XORа)
            byte[] newKey = new byte[this.textmass.Length];
            for (int i = 0, j = 0; i < text.Length; i++)
            {
                newKey[keymass[i] - 1] = key[j];
                if (j == key.Length - 1)
                    j = 0;
                else
                    j++;
            }

            // XOR ключа и текста
            byte[] result = new byte[text.Length];
            for (long i = 0, j = 0; i < text.Length; i++)
            {
                result[i] = (byte)(text[i] ^ newKey[j++]);
            }

            // Перемешиваем новый текст
            byte[] newResult = new byte[result.Length];
            for (int i = 0; i < textmass.Length; i++)
            {
                newResult[textmass[i] - 1] = result[i];
            }

            return newResult;
        }

        // Принимает текст и ключ. XOR - основная операция - поступают блоки не меньше 8 байт
        public byte[] DecodeVernamCipher(byte[] text, byte[] key)
        {
            // Сделаем ключ длиной как текст(для XORа)
            byte[] newKey = new byte[this.textmass.Length];
            for (int i = 0, j = 0; i < text.Length; i++)
            {
                newKey[keymass[i] - 1] = key[j];
                if (j == key.Length - 1)
                    j = 0;
                else
                    j++;
            }

            // Проведем обратную перестановку
            byte[] newText = new byte[text.Length];
            for (int i = 0; i < textmass.Length; i++)
            {
                newText[backtextmass[i]] = text[i];
            }

            // XOR текста
            byte[] result = new byte[text.Length];
            for (long i = 0, j = 0; i < text.Length; i++)
            {
                result[i] = (byte)(newText[i] ^ newKey[j++]);
            }

            // Возвращаем результат
            return result;
        }
    }
}
