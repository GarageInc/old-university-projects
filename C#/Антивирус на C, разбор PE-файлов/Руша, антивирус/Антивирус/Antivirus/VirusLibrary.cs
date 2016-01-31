namespace Antivirus
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class VirusLibrary
    {
        public bool ready = false;// Готовность
        public SegmentCode[] allVirus;// Все вирусы
        
        private Model2 db = new Model2();

        // Загрузка библиотеки сигнатур(вирусов) в память
        public virtual void LoadLibrary()
        {
            // Число сегментов(вирусов)
            var sign = db.Signatures;
            long j = 0;
            allVirus=new SegmentCode[sign.Count()];
            foreach (var signatures in sign)
            {
                allVirus[j]=new SegmentCode(signatures.Mass);
                j++;
            }

            ready = true;
        }
        
        // Собственно, сама проверка
        public virtual bool check(ref Parser p)
        {
            bool ans = false;
            
            List<SegmentCode> dangerSegments = new List<SegmentCode>();
            if (p.doneRenamed)
            {
                dangerSegments = p.AllSegments;
            }

            // Теперь, собственно, проверка на вхождение
            foreach (SegmentCode virusSegment in allVirus)// Берем каждый вирус
            {
                foreach (SegmentCode dangerSegment in dangerSegments)// Берем сегмент кода
                {
                    // Передаем в сегмент кода нашего файла сигнатуру вируса
                    if (dangerSegment.Find(virusSegment))// Если сегмент кода содержит в себе "вирус" - возвращаем true
                    {
                        ans = true;
                    }
                }
            }
            
            return ans;
        }
        
        // Собственно, сама функция поиска подстроки mas1 в строке mas2
        // А ПРАВИЛЬНО ЛИ ОНА РАБОТАЕТ?
        public static bool KMP(byte[] mas1, byte[] mas2)
        {
            bool ans = false;
            if (mas1.Length > mas2.Length)
            {
                return ans;
            }
            byte[] all = new byte[mas1.Length + mas2.Length + 1];
            long id = 0;
            for (; id < mas1.Length; id++)
            {
                all[id] = mas1[id];
            }
            all[id] = 0;
            id++;
            for (; id < mas1.Length + mas2.Length + 1; id++)
            {
                all[id] = mas2[id - mas1.Length - 1];
            }

            long[] pref = new long[all.Length];
            for (long i = 0; i < pref.Length; i++)
            {
                pref[i] = 0;
            }
            for (long i = 1; i < all.Length; i++)
            {
                long j = pref[i - 1];
                while (j > 0 && (all[i] != all[j] || i == mas1.Length || j == mas1.Length))
                {
                    j = pref[j - 1];
                }
                if (all[i] == all[j] && i != mas1.Length && j != mas1.Length)
                {
                    j++;
                }
                pref[i] = j;
            }
            for (long i = 0; i < pref.Length; i++)
            {
                if (pref[i] == mas1.Length)
                {
                    ans = true;
                }
            }
            return ans;
        }

    }

}
