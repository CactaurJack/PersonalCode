using System;

public class Affine : Cipher {
	private int add;
	private int mult;
	private bool legalKey;
	
	//assumes the first key is multiplicative and the second is additive
	public Affine(string key) 
    {
		char[] delim = {' '};
		try 
        {
			string[] tokens = key.Split(delim);
			mult = Convert.ToInt32(tokens[0]);
			add = Convert.ToInt32(tokens[1]);
			legalKey = true;
		}
		catch (Exception) 
        {
			mult = 1;
			add = 0;
			legalKey = false;
		}
	}

    public Affine()
    {

    }

	public override string encrypt(string msg) 
    {
		if (legalKey == false) return "";
	
		msg = formatPlaintext(msg);
	
		string result = "";
		foreach(char c in msg) 
        {
			int orig = charToInt(c);
			int change = ((orig*mult) + add) % alpha.Length;
			result += intToChar(change);
		}
	
		return result;
	}

    public string encrypt2(int m, int a, string msg)
    {
        string result = "";
        foreach (char c in msg)
        {
            int orig = charToInt(c);
            int change = ((orig*m) + a) % alpha.Length;
            result += intToChar(change);
        }
        return result;
    }
	
	private int multInverse(int num, int n) 
    {
		int r1 = n;
		int r2 = num;
		int t1 = 0;
		int t2 = 1;

		while (r2 > 0) 
        {
			int q = r1/r2;
			int r = r1 - q*r2;
			r1 = r2;
			r2 = r;

			int t = t1 - q*t2;
			t1 = t2;
			t2 = t;
		}

		if (r1 == 1) return (t1+n)%n;
		else return -1;
	}
	
	public override bool keyLegal() 
    {
		if (add < 0 || add > 25 || mult < 0 || mult > 25) return false;

		if (multInverse(mult, alpha.Length) == -1) 
        {
			Console.WriteLine("no inverse");
			return false;
		}

		return legalKey;
	}
}