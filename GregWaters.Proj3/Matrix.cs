//handles matrices with 3 columns
using System;
public class Matrix {
	public int[,] entries;
	public int rows;
	public int cols;
	private int modulus;
	
	public Matrix(int r, int c, int m) 
    {
		entries = new int[r,c];
		rows = r;
		cols = c;
		modulus = m;
	}

    public Matrix(int r, int c)
    {
        entries = new int[r, c];
        rows = r;
        cols = c;
        modulus = 26;
    }

	public void setElem(int i, int j, int val) 
    {
		entries[i,j] = val;
	}
	
	public int getElem(int i, int j) 
    {
		return entries[i,j];
	}
	
	public int getRows() 
    {
		return rows;
	}
	
	public int getCols() 
    {
		return cols;
	}
	
	//returns THIS times m
	public Matrix multiply(Matrix m) 
    {
		int newRows = rows;
		int newCols = m.cols;
		Matrix result = new Matrix(newRows, newCols, modulus);

		for (int i = 0; i < newRows; i++) 
        {
			for (int j = 0; j < newCols; j++) 
            {
				int entry = 0;
				for (int k = 0; k < m.rows; k++) 
                {
					entry += entries[i,k]*m.entries[k,j];
				}
				result.setElem(i,j, entry % modulus);
			}
		}
		return result;
	}
	
	private Matrix delRowCol(int i, int j) 
    {
		Matrix result = new Matrix(rows-1, cols-1, modulus);
		for (int x = 0; x < rows-1; x++) 
        {
			int newRow = x;
			if (x >= i) newRow++;
			for (int y = 0; y < cols-1; y++) 
            {
				int newCol = y;
				if (y >= j) newCol++;
				result.setElem(x,y,entries[newRow,newCol]);
			}
		}
		
		return result;
	}
	
    /*
	public Matrix invert() 
    {
		//Assumes this matrix is bigger than 2x2
		Matrix inverse = new Matrix(rows,cols,modulus);
		
		int detOrig = det();
		int detMultiplier = multInverse(detOrig, modulus);
		for (int i = 0; i < rows; i++) 
        {
			for (int j = 0; j < cols; j++) 
            {
				Matrix del = delRowCol(j,i);
				int detDel = del.det();
				int entry = (detDel * detMultiplier) % modulus;
				if ((i+j) % 2 != 0 && entry != 0) entry = modulus - entry;
				inverse.entries[i,j] = entry;
			}
		}
		
		return inverse;
	}*/
	
	public Matrix invert() {
		//Assumes this matrix is 2x2
		Matrix inverse = new Matrix(rows, cols);

		int detOrig = det();
		int detMultiplier = multInverse(detOrig, modulus);

		inverse.entries[0,0] = (entries[1,1]*detMultiplier) % modulus;
		inverse.entries[0,1] = ((modulus-entries[0,1])*detMultiplier) % modulus;
		inverse.entries[1,0] = ((modulus-entries[1,0]+modulus)*detMultiplier) % modulus;
		inverse.entries[1,1] = (entries[0,0]*detMultiplier) % modulus;

		return inverse;
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

		if (r1 == 1) 
        {
			while (t1 < 0) t1 += n;
			return t1;
		}
		else throw new Exception("no inverse: " + num);
	}
	
	public void print() 
    {
		for (int i = 0; i < rows; i++) 
        {
			for (int j = 0; j < cols; j++) 
            {
				Console.Write(entries[i,j] + " ");
			}
			Console.WriteLine();
		}
		Console.WriteLine();
	}
	
	public Matrix copy() 
    {
		Matrix m = new Matrix(rows,cols,modulus);
		for (int i = 0; i < rows; i++) 
        {
			for (int j = 0; j < cols; j++) 
            {
				m.entries[i,j] = entries[i,j];
			}
		}
		
		return m;
	}
	
	public int det() 
    {
		//Assumes this matrix is 2x2 or 3x3
		if (rows == 2 && cols == 2) 
        {
			int sum = entries[0,0]*entries[1,1] - entries[0,1]*entries[1,0];
			while (sum < 0) sum += modulus;
			return sum % modulus;
		}

		else 
        {
			int total = 0;
			for (int i = 0; i < cols; i++) 
            {
				Matrix del = delRowCol(0, i);
				if (i % 2 == 0) total += entries[0,i] * del.det();
				else total -= entries[0,i] * del.det();
			}
		
			while (total < 0) total += modulus;
			return total % modulus;	
		}
	}
}