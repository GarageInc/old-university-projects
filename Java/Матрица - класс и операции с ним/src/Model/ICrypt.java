package Model;
public abstract class ICrypt {
	
	static public Crypt CreateCrypt(int textlength)
	{	
		return new Crypt(textlength);
	}
}

