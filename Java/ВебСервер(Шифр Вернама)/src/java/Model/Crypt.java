    package Model;
    public class Crypt extends ICrypt{
                public int[] backtextmass;
		public int[] textmass;

		public int[] keymass;

                Crypt() {
                }
                
		public Crypt(int textlength)
		{
			// Расширим наш ключ
			textmass = new int[textlength];
			backtextmass = new int[textlength];
			keymass = new int[textlength];

			java.util.Random r = new java.util.Random();
			for (int i = 0; i < textlength;)
			{
				int res = r.nextInt(textlength + 1);
				boolean notExist = true;
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
				int res = r.nextInt(textlength + 1);
				boolean notExist = true;
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
		public final byte[] EncodeVernamCipher(byte[] text, byte[] key)
		{
			// Сделаем ключ длиной как текст(для XORа)
			byte[] newKey = new byte[this.textmass.length];
			for (int i = 0, j = 0; i < text.length; i++, j++)
			{
				newKey[keymass[i] - 1] = key[j];
				if (j == key.length - 1)
				{
					j = 0;
				}
			}

			// XOR ключа и текста
			byte[] result = new byte[text.length];
			for (int i = 0, j = 0; i < text.length; i++)
			{
				result[i] = (byte)(text[i] ^ newKey[j++]);
			}

			// Перемешиваем новый текст
			byte[] newResult = new byte[result.length];
			for (int i = 0; i < textmass.length; i++)
			{
				newResult[textmass[i] - 1] = result[i];
			}

			return newResult;
		}

		// Принимает текст и ключ. XOR - основная операция - поступают блоки не меньше 8 байт
		public final byte[] DecodeVernamCipher(byte[] text, byte[] key)
		{
			// Сделаем ключ длиной как текст(для XORа)
			byte[] newKey = new byte[this.textmass.length];
			for (int i = 0, j = 0; i < text.length; i++, j++)
			{
				newKey[keymass[i] - 1] = key[j];
				if (j == key.length - 1)
				{
					j = 0;
				}
			}

			// Проведем обратную перестановку;
			byte[] newText = new byte[text.length];
			for (int i = 0; i < textmass.length; i++)
			{
				newText[backtextmass[i]] = text[i];
			}

			// XOR текста
			byte[] result = new byte[text.length];
			for (int i = 0, j = 0; i < text.length; i++)
			{
				result[i] = (byte)(newText[i] ^ newKey[j++]);
			}

			// Возвращаем результат
			return result;
		}

        
        
        
}
