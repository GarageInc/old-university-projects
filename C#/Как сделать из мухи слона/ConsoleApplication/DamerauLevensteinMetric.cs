using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    /// <summary>
    /// Damerau-Levenshtein distance
    /// </summary>
    public class DamerauLevensteinMetric
    {
        private const int DEFAULT_LENGTH = 255;
        private int[] _currentRow;
        private int[] _previousRow;
        private int[] _transpositionRow;

        public DamerauLevensteinMetric()
            : this(DEFAULT_LENGTH)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLength"></param>
        public DamerauLevensteinMetric(int maxLength)
        {
            _currentRow = new int[maxLength + 1];
            _previousRow = new int[maxLength + 1];
            _transpositionRow = new int[maxLength + 1];
        }

        /// <summary>
        /// Damerau-Levenshtein distance is computed in asymptotic time O((max + 1) * min(first.length(), second.length()))
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GetDistance(string first, string second, int max)
        {
            int firstLength = first.Length;
            int secondLength = second.Length;

            if (firstLength == 0)
                return secondLength;

            if (secondLength == 0) return firstLength;

            if (firstLength > secondLength)
            {
                string tmp = first;
                first = second;
                second = tmp;
                firstLength = secondLength;
                secondLength = second.Length;
            }

            if (max < 0) max = secondLength;
            if (secondLength - firstLength > max) return max + 1;

            if (firstLength > _currentRow.Length)
            {
                _currentRow = new int[firstLength + 1];
                _previousRow = new int[firstLength + 1];
                _transpositionRow = new int[firstLength + 1];
            }

            for (int i = 0; i <= firstLength; i++)
                _previousRow[i] = i;

            char lastSecondCh = (char)0;
            for (int i = 1; i <= secondLength; i++)
            {
                char secondCh = second[i - 1];
                _currentRow[0] = i;
                
                int from = Math.Max(i - max - 1, 1);
                int to = Math.Min(i + max + 1, firstLength);

                char lastFirstCh = (char)0;
                for (int j = from; j <= to; j++)
                {
                    char firstCh = first[j - 1];
                    
                    int cost = firstCh == secondCh ? 0 : 1;
                    int value = Math.Min(Math.Min(_currentRow[j - 1] + 1, _previousRow[j] + 1), _previousRow[j - 1] + cost);
 
                    if (firstCh == lastSecondCh && secondCh == lastFirstCh)
                        value = Math.Min(value, _transpositionRow[j - 2] + cost);

                    _currentRow[j] = value;
                    lastFirstCh = firstCh;
                }
                lastSecondCh = secondCh;

                int[] tempRow = _transpositionRow;
                _transpositionRow = _previousRow;
                _previousRow = _currentRow;
                _currentRow = tempRow;
            }

            return _previousRow[firstLength];
        }
    }
}
