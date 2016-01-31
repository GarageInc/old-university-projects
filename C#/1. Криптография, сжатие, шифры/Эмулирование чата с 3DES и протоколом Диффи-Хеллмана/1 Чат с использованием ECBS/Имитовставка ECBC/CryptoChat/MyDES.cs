using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CryptoChat.Properties;

namespace CryptoChat
{
    class My3DES
    {
        private static DefaultData defData;

        public My3DES()
        {
            defData = new DefaultData();
        }

        public byte[] Encryption(string text, string key_string)
        {
            int ln = 0;
            int step = 6;// По 6 символов(т.к. 6*8=48 бит) - берем 48 бит и расширяем до 64

            byte[] bytes = new byte[8 * (text.Length % step == 0 ? (text.Length / step) : (text.Length / step+1))];// То, что вернём

            int[,] key = Keys(key_string);

            int j = 0;
            while (ln < text.Length)
            {
                if (text.Length - ln < step)
                    step = text.Length - ln;

                // Откусываем по кусочку от строки, шифруем 64мя битами и возвращаем
                var block = EncryptionBlock(text.Substring(ln, step), key);// Возвращаются массив из 64 бит, т.е. 8 байт
                for (int i = 0; i <8; i++)
                {
                    bytes[8*j + i] = block[i];
                }
                j++;
                ln += step;
            }

            return bytes;
        }

        public byte[] EncryptionBlock(string str, int[,] key)
        {
            //Проверка на четность
            bool error = CheckKey(key);
            if (error)
            {
                MessageBox.Show("Ошибка!");
                return null;
            }

            byte[] unicodeBytes = Encoding.GetEncoding(1251).GetBytes(str);
            
            int[] bitMass = new int[64];
            int[] t_ip = new int[64];
            int[][] l = new int[17][];
            int[][] r = new int[17][];

            // Инициализируем массивы 17 на 32
            for (int i = 0; i < 17; i++)
            {
                l[i] = new int[32];
                r[i] = new int[32];
            }
            
            List<int> list = new List<int>();

            // Переводим в список байты нашей строки
            for (int j = unicodeBytes.Length - 1; j > -1; j--)
                for (int i = 0; i < 8; ++i)
                    list.Add((unicodeBytes[j] >> i) & 1);// Побитовый сдвиг на 2 в степени i

            list.Reverse();
            
            // Переводим в массив битов наш реверснутый результат
            for(int t=0; t<list.Count; t++)
            {
                bitMass[t] = list[t];
            }

            //IP
            for (int i = 0; i < defData.ip.Length; i++)
                t_ip[i] = bitMass[defData.ip[i] - 1];// Создаем массив из перемешанных битов

            //L[0]R[0]
            for (int i = 0; i < t_ip.Length; i++)// 
            {
                if (i < 32) 
                    l[0][i] = t_ip[i];
                else 
                    r[0][i - 32] = t_ip[i];
            }


            int[] ff_f = new int[32];
            int[] keys_t = new int[key.GetLength(1)];

            //16
            for (int i = 1; i < l.Length; i++)
            {
                l[i] = r[i - 1];
                for (int j = 0; j < keys_t.Length; j++)
                    keys_t[j] = key[i - 1, j];

                ff_f = ff(r[i - 1], keys_t);
                for (int j = 0; j < ff_f.Length; j++)
                    r[i][j] = l[i - 1][j] ^ ff_f[j];
            }

            int[] liri = new int[64];
            for (int i = 0; i < 64; i++)
            {
                if (i < 32) liri[i] = l[l.Length - 1][i];
                else liri[i] = r[r.Length - 1][i - 32];
            }

            int[] _out = new int[64];
            for (int i = 0; i < 64; i++)
                _out[i] = liri[defData._ip[i] - 1];

            // Переводим в биты
            BitArray bites = new BitArray(64);
            for (var i=0; i<64; i++)
            {
                bites[i] = _out[i] == 1 ? true : false;
            }
            // Переводим в байты
            byte[] bytes = new byte[Convert.ToInt32(Math.Ceiling(bites.Count / 8.0))];
            bites.CopyTo(bytes, 0);

            return bytes;
        }


        // Расшифровка полного текста
        public byte[] Decryption(string text, string key_string)
        {
            int[,] key = this.Keys(key_string);
         
            // Сначала текст конвертируем в байты
            byte[] bytes = Encoding.Default.GetBytes(text);
            // Байты конвертируем в биты
            BitArray b = new BitArray(bytes);
            // Потом биты в массив int
            var encryptArr = new int[b.Count];
            var index = 0;
            for (; index < b.Count; index++)
            {
                encryptArr[index] = b[index] ? 1 : 0;
            }

            var n = encryptArr.Length / 64;// Для получения блоков по 64 бита
            
            byte[] outBytes = new byte[n*6];
            int j = 0;
            for (int i = 0; i < n; i++)
            {
                // Получаем дешифрованные блоки по 6 байт
                var block = DecryptionBlock(encryptArr.Skip(i * 64).Take(64).ToArray(), key);

                for (int x = 0; x < 6; x++)
                {
                    outBytes[j*6+x] = block[x];
                }
                j++;
            }

            // Возвращаем байты
            return outBytes;
        }


        // Расшифровка блок 
        public byte[] DecryptionBlock(int[] arr, int[,] key)
        {
            //Проверка на четность
            bool error = CheckKey(key);
            if (error)
            {
                MessageBox.Show("Ошибка");
                return null;
            }

            // Создаем массив битов и обратного перевода
            int[] bitMass = new int[64];
            int[] t_ip = new int[64];
            int[] liri = new int[64];
            int[] ff_f = new int[32];
            int[][] l = new int[17][];
            int[][] r = new int[17][];
            int[] keys_t = new int[key.GetLength(1)];
            List<int> list = new List<int>();

            for (int i = 0; i < 17; i++)
            {
                l[i] = new int[32];
                r[i] = new int[32];
            }

            for (int i = 0; i < 64; i++)
                liri[defData._ip[i] - 1] = arr[i];

            for (int i = 0; i < 64; i++)
            {
                if (i < 32) l[l.Length - 1][i] = liri[i];
                else r[r.Length - 1][i - 32] = liri[i];
            }

            for (int i = l.Length - 1; i > 0; i--)
            {
                r[i - 1] = l[i];

                for (int j = 0; j < keys_t.Length; j++)
                    keys_t[j] = key[i - 1, j];
                ff_f = ff(r[i - 1], keys_t);

                for (int j = 0; j < ff_f.Length; j++)
                    l[i - 1][j] = r[i][j] ^ ff_f[j];
            }

            for (int i = 0; i < t_ip.Length; i++)
            {
                if (i < 32) t_ip[i] = l[0][i];
                else t_ip[i] = r[0][i - 32];
            }

            for (int i = 0; i < defData.ip.Length; i++)
                bitMass[defData.ip[i] - 1] = t_ip[i];

            for (int i = 0; i < bitMass.Length - 16; i++)
            {
                list.Add(bitMass[i]);
            }

            // Возвращаем 8 расшифрованных байт
            byte[] bytes = new byte[list.Count / 8];

            for (int i = 0; i < bytes.Length; i++)
            {
                StringBuilder t = new StringBuilder();
                // Добавляем в строку
                for (int j = 0; j < 8; j++)
                {
                    t.Append(list[i * 8 + j].ToString());
                }
                bytes[i] = Convert.ToByte(Convert.ToInt32(t.ToString(), 2));
            }

            return bytes;
        }

        static public int[] ff(int[] mass, int[] key)
        {
            int[] fe = new int[defData.e.Length];
            int[] b0 = new int[defData.e.Length];
            int[,] b1 = new int[8, 6];
            int[] b2 = new int[8];
            String b3 = "";
            int[] b4;
            int[] bp = new int[32];

            for (int i = 0; i < defData.e.Length; i++)
                fe[i] = mass[defData.e[i] - 1];

            for (int i = 0; i < defData.e.Length; i++)
                b0[i] = fe[i] ^ key[i];

            int iStr = 0, iStb = 0;
            for (int i = 0; i < defData.e.Length; i++, iStb++)
            {
                if (iStb == 6)
                {
                    iStr++;
                    iStb = 0;
                }
                b1[iStr, iStb] = b0[i];
            }

            int[] lines = new int[8];
            int[] columen = new int[8];
            for (int i = 0; i < b1.GetLength(0); i++)
                for (int j = 0, t = 1, tt = 0; j < b1.GetLength(1); j++, t--, tt--)
                {
                    if (j < 2) { lines[i] += b1[i, j] * (int)Math.Pow(2, t); tt = 4; }
                    else columen[i] += b1[i, j] * (int)Math.Pow(2, tt);
                }

            for (int i = 0; i < b2.GetLength(0); i++)
            {
                b2[i] = defData.sm[16 * (4 * i + lines[i]) + columen[i]];
                b3 += Convert.ToString(b2[i], 2).PadLeft(4, '0');
            }
            b4 = b3.Select(ch => int.Parse(ch.ToString())).ToArray();

            for (int i = 0; i < bp.Length; i++)
                bp[i] = b4[defData.p[i] - 1];
            return bp;
        }

        public int[,] Keys(string key_string)
        {
            string str = key_string;
            str = (str.Length > 8) ? str.Substring(0, 8) : str.Substring(0, str.Length);
            
            byte[] unicodeBytes = Encoding.Unicode.GetBytes(str);
            byte[] asciiBytes = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, unicodeBytes);

            int[] bitMass = new int[64];
            int[] pc = { 57,49,41,33,25,17,9,1,58,50,42,34,26,18,10,2,59,51,43,35,27,19,11,3,60,52,44,36,63,55,47,39,31,23,15,
                               7,62,54,46,38,30,22,14,6,61,53,45,37,29,21,13,5,28,20,12,4};
            int[] pc1 = { 14,17,11,24,1,5,3,28,15,6,21,10,23,19,12,4,26,8,16,7,27,20,13,2,41,52,31,37,47,55,30,40,51,45,33,48,
                              44,49,39,56,34,53,46,42,50,36,29,32 };
            int[] bitMass2 = new int[pc.Length];
            int[] c0 = new int[pc.Length / 2];
            int[] d0 = new int[pc.Length / 2];
            int[] shift = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
            int[,] c = new int[shift.Length, pc.Length / 2];
            int[,] d = new int[shift.Length, pc.Length / 2];
            int[,] cd = new int[shift.Length, pc.Length];
            int[,] key = new int[shift.Length, pc1.Length];

            List<int> list = new List<int>();
            for (int j = asciiBytes.Length - 1; j > -1; j--)
                for (int i = 0; i < 8; ++i)
                    list.Add((asciiBytes[j] >> i) & 1);
            list.Reverse();
            int t = 0;
            foreach (var item in list)
            {
                bitMass[t] = item;
                t++;
            }

            for (int i = 0; i < pc.Length; i++)
                bitMass2[i] = bitMass[pc[i] - 1];

            for (int i = 0; i < bitMass2.Length; i++)
            {
                if (i < 28) c0[i] = bitMass2[i];
                else d0[i - 28] = bitMass2[i];
            }

            for (int tt = 0; tt < shift.Length; tt++)
            {
                for (int j = 0; j < shift[tt]; ++j)
                {
                    int temp = c0[0];
                    for (int i = 0; i < c0.Length - 1; i++)
                        c0[i] = c0[i + 1];
                    c0[c0.Length - 1] = temp;
                }
                for (int r = 0; r < pc.Length / 2; r++)
                    c[tt, r] = c0[r];
            }

            for (int tt = 0; tt < shift.Length; tt++)
            {
                for (int j = 0; j < shift[tt]; ++j)
                {
                    int temp = d0[0];
                    for (int i = 0; i < d0.Length - 1; i++)
                        d0[i] = d0[i + 1];
                    d0[d0.Length - 1] = temp;
                }
                for (int r = 0; r < pc.Length / 2; r++)
                    d[tt, r] = d0[r];
            }

            for (int i = 0; i < shift.Length; i++)
                for (int j = 0; j < pc.Length; j++)
                    cd[i, j] = (j > 27) ? d[i, j - 28] : c[i, j];

            for (int i = 0; i < shift.Length; i++)
                for (int j = 0; j < pc1.Length; j++)
                    key[i, j] = cd[i, pc1[j] - 1];

            return key;
        }

        // Проверка ключа
        static public bool CheckKey(int[,] key)
        {
            for (int z = 0; z < key.GetLength(0); z++)
            {
                int sum = 0;
                for (int j = 0; j < key.GetLength(1); j++)
                {
                    sum += key[z, j];
                    if (j == key.GetLength(1) - 1)
                    {
                        if (key[z, j] == sum % 2)
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
