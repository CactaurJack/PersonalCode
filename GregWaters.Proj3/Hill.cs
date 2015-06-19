using System;

public class Hill : Cipher {
	private Matrix key;
	private bool legalKey;
	
	public Hill(string k) 
    {
		k = k.Replace(" //", "");
		char[] delim = {' '};
		string[] tokens = k.Split(delim);
		int size = (int)Math.Sqrt(tokens.Length);
		key = new Matrix(size, size, alpha.Length);
		
		int pos = 0;
		try 
        {
			for (int i = 0; i < size; i++) 
            {
				for (int j = 0; j < size; j++) 
                {
					int val = Convert.ToInt32(tokens[pos]);
					if (val < 0 || val > 25) legalKey = false;

					key.setElem(i,j,val);
					pos++;
				}
			}
			key.invert();
			
			legalKey = true;
		}
		catch (Exception) 
        {
			legalKey = false;
		}
		
	}

	public override string encrypt(string msg) 
    {
		if (legalKey == false) return "";
	
		msg = formatPlaintext(msg);
	
		//build plaintext matrix, plus padding
		int numRows = msg.Length/key.getCols();

		if (msg.Length % key.getCols() != 0) numRows++;
		
		Matrix plain = new Matrix(numRows, key.getCols(), alpha.Length);
		int pos = 0;
		Random rand = new Random();

		for (int i = 0; i < numRows; i++) 
        {
			for (int j = 0; j < plain.getCols(); j++) 
            {
				if (pos >= msg.Length) 
                {
					plain.setElem(i,j,rand.Next(alpha.Length));
				}

				else 
                {
					plain.setElem(i,j,charToInt(msg[pos]));
					pos++;
				}
			}
		}
		
		Matrix cipher = plain.multiply(key);
		
		//build ciphertext string
		string result = "";
		for (int i = 0; i < cipher.getRows(); i++) 
        {
			for (int j = 0; j < cipher.getCols(); j++) 
            {
				result += intToChar(cipher.getElem(i,j));
			}
		}
				
		return result;
	}

    public string encrypt2(string msg, Matrix _key)
    {
        key = _key;
        //if (legalKey == false) return "";

        //msg = formatPlaintext(msg);

        //build plaintext matrix, plus padding
        int numRows = msg.Length / key.getCols();

        if (msg.Length % key.getCols() != 0) numRows++;

        Matrix plain = new Matrix(numRows, key.getCols(), alpha.Length);
        int pos = 0;
        Random rand = new Random();

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < plain.getCols(); j++)
            {
                if (pos >= msg.Length)
                {
                    plain.setElem(i, j, rand.Next(alpha.Length));
                }

                else
                {
                    plain.setElem(i, j, charToInt(msg[pos]));
                    pos++;
                }
            }
        }

        Matrix cipher = plain.multiply(key);

        //build ciphertext string
        string result = "";
        for (int i = 0; i < cipher.getRows(); i++)
        {
            for (int j = 0; j < cipher.getCols(); j++)
            {
                result += intToChar(cipher.getElem(i, j));
            }
        }

        return result;
    }
	
	/*private Matrix stringToMatrix(string s, int cols) {
		int rows = s.Length/cols;
		Matrix m = new Matrix(rows, cols, alpha.Length);
		int count = 0;
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < cols; j++) {
				m.setElem(i, j, charToInt(s[count]));
				count++;
			}
		}

		return m;
	}*/
	
	public override bool keyLegal() {
		return legalKey;
	}
}