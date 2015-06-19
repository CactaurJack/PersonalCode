using System;

public class ColumnarTransposition : Cipher {
	private int[] key;
	private bool legalKey;
	
	public ColumnarTransposition(string k) 
    {
		if (k[0] != '(' || k[k.Length-1] != ')') legalKey = false;
		else 
        {
			k = k.Replace("(", "");
			k = k.Replace(")", "");
			char[] delim = {','};
			
			try 
            {
				string[] tokens = k.Split(delim);
				key = new int[tokens.Length];

				for (int i = 0; i < key.Length; i++) 
                {
					key[i] = Convert.ToInt32(tokens[i]);
				}
				legalKey = true;
			}

			catch (Exception) 
            {
				legalKey = false;
			}
		}
	}

    public ColumnarTransposition()
    {

    }
	
	public override string encrypt(string msg) 
    {
		if (legalKey == false) return "";
	
		msg = formatPlaintext(msg);
	
		int numRows = msg.Length/key.Length;
		if (msg.Length % key.Length != 0) numRows++;
		
		char[,] matrix = new char[numRows, key.Length];
		int pos = 0;
		Random rand = new Random();
		
		for (int i = 0; i < numRows; i++) 
        {
			for (int j = 0; j < key.Length; j++) 
            {
				if (pos >= msg.Length) 
                {
					matrix[i,j] = intToChar(rand.Next(alpha.Length));
				}

				else 
                {
					matrix[i,j] = msg[pos];
					pos++;
				}
			}
		}
		
		string result = "";
		for (int i = 0; i < key.Length; i++) 
        {
			for (int j = 0; j < numRows; j++) 
            {
				result += matrix[j, key[i]-1];
			}
		}
		
		return result;
	}

    public string encrypt2(string msg, int[] key)
    {
        int numRows = msg.Length / key.Length;
        if (msg.Length % key.Length != 0) numRows++;

        char[,] matrix = new char[numRows, key.Length];
        int pos = 0;
        Random rand = new Random();

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < key.Length; j++)
            {
                if (pos >= msg.Length)
                {
                    matrix[i, j] = intToChar(rand.Next(alpha.Length));
                }

                else
                {
                    matrix[i, j] = msg[pos];
                    pos++;
                }
            }
        }

        string result = "";
        for (int i = 0; i < key.Length; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                result += matrix[j, key[i] - 1];
            }
        }

        return result;
    }

	public override bool keyLegal() 
    {
		if (legalKey == false) return false;
		
		bool[] counts = new bool[key.Length];
		for (int i = 0; i < key.Length; i++) 
        {
			if (key[i] < 1 || key[i] > key.Length) 
            {
				legalKey = false;
				break;
			}
			counts[key[i]-1] = true;
		}
		
		for (int i = 0; i < counts.Length; i++) 
        {
			if (counts[i] == false) legalKey = false;
		}
		
		return legalKey;
	}
}