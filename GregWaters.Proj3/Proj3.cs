using System;
using System.IO;

public class Proj3
{

    public static void Main(string[] args)
    {

        #region Lexicon/Trie
        string inLex = "lexicon.txt";
        TrieInterface MasterLex = new EmptyTrie(false);
        TextReader readD = new StreamReader(inLex);
        int Count = Convert.ToInt32(readD.ReadLine());
        int DCount = 0;
        string[] Dictionary = new string[Count];
        int compare1 = 0;
        int compare2 = 0;

        //Populates trie with all words into the dictionary trie
        while (DCount < Count)
        {
            string Temp = readD.ReadLine();
            if (Temp != null)
            {
                compare1 = Temp.Length;
                if (compare1 > compare2)
                {
                    compare2 = compare1;
                }
                MasterLex = MasterLex.Add(Temp);
                Dictionary[DCount] = Temp;

                DCount++;
            }
            else
            {
                break;
            }
        }
        #endregion

        StreamReader sr = new StreamReader(args[0]);
        Analyze[] Freq = new Analyze[3];
        string[] CipherIn = new string[3];
        Affine Aff = new Affine();
        string temp;
        temp = sr.ReadLine();
        for (int i = 0; i < CipherIn.Length; i++)
        {
            if (temp == null)
            {
                break;
            }
            if (temp.Equals(""))
            {
                temp = sr.ReadLine();
            }
            CipherIn[i] = temp;
            Freq[i] = new Analyze(temp);
            Freq[i].Run();
            temp = sr.ReadLine();
        }

        StreamWriter sw = new StreamWriter("Frequency.txt");
        for (int j = 0; j < Freq.Length; j++)
        {
            sw.WriteLine("Cipher Text " + (j + 1));
            for (int i = 0; i < Freq[j].alpha.Length; i++)
            {
                sw.WriteLine(Freq[j].alpha[i] + " = " + Freq[j].index[i]);
            }

            sw.WriteLine("");
        }

        sr.Close();

        sw = new StreamWriter("CT.txt");
        string current = CipherIn[2];
        int count = 0;
        string[] Output = new string[24];
        int[][] In4 = new int[24][];
        int[][] In5 = new int[120][];
        int[][] In6 = new int[720][];
        int[][] In7 = new int[5040][];
        //int[][] In8 = new int[40320][];

        In4 = Populate(In4, 4);
        In5 = Populate(In5, 5);
        In6 = Populate(In6, 6);
        In7 = Populate(In7, 7);
        //In8 = Populate(In8, 8);

        ColumnarTransposition Master = new ColumnarTransposition();

        for (int i = 0; i < Output.Length; i++)
        {
            Output[i] = Master.encrypt2(current, In4[i]);
            if (Find(Output[i].ToLower(), MasterLex))
            {
                Console.WriteLine(Output[i]);
                Console.Write("Key = ");
                foreach (int x in In4[i])
                {
                    Console.Write(x + " ");
                }
                Console.WriteLine();
                break;
            }
            sw.WriteLine(Output[i]);
        }

        
        sw.WriteLine();
        Output = new string[120];
        for (int i = 0; i < Output.Length; i++)
        {
            Output[i] = Master.encrypt2(current, In5[i]);
            if (Find(Output[i].ToLower(), MasterLex))
            {
                Console.WriteLine(Output[i]);
                Console.Write("Key = ");
                foreach (int x in In5[i])
                {
                    Console.Write(x + " ");
                }
                Console.WriteLine();
                break;
            }
            sw.WriteLine(Output[i]);
        }

        
        sw.WriteLine();
        Output = new string[720];
        for (int i = 0; i < Output.Length; i++)
        {
            Output[i] = Master.encrypt2(current, In6[i]);
            if (Find(Output[i].ToLower(), MasterLex))
            {
                Console.WriteLine(Output[i]);
                Console.Write("Key = ");
                foreach (int x in In6[i])
                {
                    Console.Write(x + " ");
                }
                Console.WriteLine();
                break;
            }
            sw.WriteLine(Output[i]);
        }


        sw.WriteLine();

        Output = new string[5040];
        for (int i = 0; i < Output.Length; i++)
        {
            Output[i] = Master.encrypt2(current, In7[i]);
         if (Find(Output[i].ToLower(), MasterLex))
            {
                Console.WriteLine(Output[i]);
                Console.Write("Key = ");
                foreach (int x in In7[i])
                {
                    Console.Write(x + " ");
                }
                Console.WriteLine();
                break;
            }
            sw.WriteLine(Output[i]);
        }

        sw.WriteLine();

        /*Output = new string[40320];
        for (int i = 0; i < Output.Length; i++)
        {
            Output[i] = Master.encrypt2(current, In8[i]);
            sw.WriteLine(Output[i]);
        }*/


        sw.Close();

        sw = new StreamWriter("Hill.txt");
        Hill HillCipher = new Hill(current);
        count = 0;
        string hold;
        Matrix HillM = new Matrix(2, 2);
        for (int i = 1; i < 26; i++)
        {
            for (int j = 1; j < 26; j++)
            {
                for (int k = 1; k < 26; k++)
                {
                    for (int l = 1; l < 26; l++)
                    {
                        HillM.setElem(0, 0, i);
                        HillM.setElem(0, 1, j);
                        HillM.setElem(1, 0, k);
                        HillM.setElem(1, 1, l);
                        hold = HillCipher.encrypt2(current, HillM);
                        if (Find(hold.ToLower(), MasterLex))
                        {
                            Console.WriteLine(hold);
                            Console.Write("Key = ");
                            Console.Write(HillM.getElem(0, 0) + " " + HillM.getElem(1, 0) + " " + HillM.getElem(0, 1) + " " + HillM.getElem(1, 1));
                            Console.WriteLine();
                            i = 26;
                            j = 26;
                            k = 26;
                            l = 26;
                            break;
                        }
                        sw.WriteLine(hold);
                        count++;

                    }
                }
            }
        }

        sw.Close();
        sw = new StreamWriter("Affine.txt");

        Output = new string[676];
        current = CipherIn[1];
        count = 0;
        for (int i = 1; i < 26; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                Output[count] = Aff.encrypt2(i, j, current);

                if (Find(Output[count].ToLower(), MasterLex))
                {
                    Console.WriteLine(Output[count].ToLower());
                    Console.Write("Key:  Multi = " + i + " Add = " + j);
                    i = 26;
                    j = 26;
                    break;
                }
                sw.WriteLine(Output[count]);
                sw.WriteLine("");
                count++;
            }
        }


        sw.Close();
        Console.ReadLine();
    }




    public static bool Compare(int[][] x, int[] y)
    {



        bool checker = false;
        int count = 0;
        for (int i = 0; i < x.Length; i++)
        {
            for (int j = 0; j < y.Length; j++)
            {
                if (x[i][j] == y[j])
                {
                    checker = true;
                    count++;
                    if (count == y.Length)
                    {
                        checker = true;
                        return checker;
                    }
                }
                if (x[i][j] != y[j])
                {
                    checker = false;
                }
            }
            count = 0;
        }


        return checker;
    }

    public static int[][] Populate(int[][] InX, int length)
    {
        Random Pop = new Random();
        int count = 0;
        int holder = 0;
        bool check = false;
        int[] Hold = new int[length];

        for (int i = 0; i < InX.Length; i++)
        {
            InX[i] = new int[length];

            if (i < length)
            {
                Hold[i] = 0;
            }
        }


        for (int i = 0; i < InX.Length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                holder = Pop.Next(1, length + 1);
                while (check == false)
                {
                    foreach (int x in Hold)
                    {
                        if (holder == x)
                        {
                            check = false;
                            break;
                        }
                        if (holder != x)
                        {
                            check = true;
                        }
                    }

                    if (check != true)
                    {
                        holder = Pop.Next(1, length + 1);
                    }
                }
                check = false;
                Hold[j] = holder;
            }


            if (Compare(InX, Hold))
            {
                i--;
                for (int k = 0; k < Hold.Length; k++)
                {
                    Hold[k] = 0;
                }
            }

            else
            {
                for (int k = 0; k < Hold.Length; k++)
                {
                    InX[i][k] = Hold[k];
                    Hold[k] = 0;
                }
                if (count == 10000 || count == 20000 || count == 30000 || count == 40000)
                {
                    Console.WriteLine(count);
                }
                count++;
            }
        }
        return InX;
    }

    public static bool Find(string msg, TrieInterface Lex)
    {
        string Temp = msg;

        if (Temp == null)
        {
            return false;
        }

        bool contain = false;
        int counter1 = 0;
        int counter2 = 1;
        int total = 0;
        string comp0 = "";
        string comp1 = "";
        string comp2 = "";
        string Final = "";

        while (total < Temp.Length)
        {
            //string TEST = Temp.Substring(counter1, counter2);
            if ((counter2 - 26) >= 0)
            {
                return false;
            }

            try
            {
                Lex.Contains(Temp.Substring(counter1, counter2));
            }

            catch
            {
                if (Lex.Contains(Temp.Substring(counter1 - 1, counter2)))
                {
                    Final += (Temp.Substring(counter1 - 1, counter2));
                    return false;
                }

            }

            if (Lex.Contains(Temp.Substring(counter1, counter2)))
            {
                try
                {
                    comp0 = Temp.Substring(counter1, counter2 + 1);
                    comp1 = Temp.Substring(counter1, counter2 + 2);
                    comp2 = Temp.Substring(counter1, counter2 + 3);

                }

                catch
                {
                    contain = true;
                    return contain;
                    
                }

                if (Lex.Contains(comp0))
                {
                    contain = true;
                    return contain;
                    
                }

                if (Lex.Contains(comp1))
                {
                    contain = true;
                    return contain;
                    
                }

                if (Lex.Contains(comp2))
                {
                    contain = true;
                    return contain;
                    
                }

                else
                {
                    contain = true;
                    return contain;
                    
                }
            }

            else
            {
                counter2++;
            }
        }
        return contain;
    }



}