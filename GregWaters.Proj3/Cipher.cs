public abstract class Cipher {

	protected string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	
	protected int charToInt(char c) 
    {
		return alpha.IndexOf(c);
	}

	protected char intToChar(int num) 
    {
		return alpha[num];
	}
	
	protected string formatPlaintext(string msg) 
    {
		string result = "";
		for (int i = 0; i < msg.Length; i++) 
        {
			if (find(alpha, msg.Substring(i,1).ToLower())) 
            {
				result += msg.Substring(i,1).ToLower();
			}
		}
		
		return result;
	}
	
	private bool find(string big, string small) 
    {
		for (int i = 0; i <= big.Length-small.Length; i++) 
        {
			string sub = big.Substring(i, small.Length);
			if (sub == small) return true;
		}
		
		return false;
	}
	
	public abstract string encrypt(string msg);
	
	public abstract bool keyLegal();
}