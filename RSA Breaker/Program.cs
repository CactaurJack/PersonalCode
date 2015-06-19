using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Text;

namespace Proj4
{
        //Class to hold a key text pair
        public class MiddleText
        {
            public string text;
            public string key;
            public MiddleText()
            {
                text = "";
                key = "";
            }
        }

    public class Program
    {

        //Global variable to hold key for non-discovery mode
        public static string FinalKey;

        static void Main(string[] args)
        {
            //Master varible declarations
            string key;
            string plainText;
            string cryptText;
            byte[] key_1;
            byte[] key_2;
            int[] Byte_Counter1;
            int[] Byte_Counter2;
            byte[] plainText_Byte;
            byte[] cryptText_Byte;
            byte[] Midtext_Byte2;

            //Tests to see if command line puts it in Encrypt/Decrypt mode or into Discovery Mode
            //Based on length of args array
            if (args.Length == 3)
            {
                //Parsing of input from command line
                plainText = args[0];
                plainText = HexFixer(plainText);
                plainText_Byte = HexToByte(plainText);
                cryptText = args[1];
                cryptText_Byte = HexToByte(cryptText);
                key = args[2];
                string key1 = key.Substring(0, 14) + "00";
                string key2 = key.Substring(14, 14) + "00";
                Byte_Counter1 = FindVars(key1);
                Byte_Counter2 = FindVars(key2);
                key1 = RemoveMarks(key1);
                key2 = RemoveMarks(key2);
                key_1 = HexToByte(key1);
                key_2 = HexToByte(key2);
                Midtext_Byte2 = Decrypt(HexToByte(cryptText), key_2, key_2);

                //Uses parsed data from partial key to determine how many unknown bytes are present
                MiddleText[] Final = new MiddleText[1];
                if (Byte_Counter1.Length == 1)
                {
                    Final = Test1_Encrypt(key_1, plainText_Byte);
                }
                if (Byte_Counter1.Length == 2)
                {
                    Final = Test2_Encrypt(key_1, plainText_Byte, Byte_Counter1);
                }
                if (Byte_Counter1.Length == 3)
                {
                    Final = Test2_Encrypt(key_1, plainText_Byte, Byte_Counter1);
                }

                MiddleText[] Final2 = new MiddleText[1];
                if (Byte_Counter2.Length == 1)
                {
                    Final2 = Test1_Decrypt(key_2, cryptText_Byte);
                }
                if (Byte_Counter2.Length == 2)
                {
                    Final2 = Test2_Decrypt(key_2, cryptText_Byte, Byte_Counter2);
                }
                if (Byte_Counter2.Length == 3)
                {
                    Final2 = Test3_Decrypt(key_2, cryptText_Byte, Byte_Counter2);
                }

                //Starts the Midtext checker
                MiddleText check = FindMid(Final, Final2);
                if (!check.text.Equals("NULL"))
                {
                    Console.WriteLine("It worked!");
                    Console.WriteLine("Final Key = " + check.key);
                    Console.WriteLine("Final Midtext = " + check.text);
                }
                else
                {
                    Console.WriteLine("Aw snap, no match found =(");
                }
                string Finaltext_String = ByteToHex(HexToByte(cryptText));
                Console.ReadLine();

            }

            //Straight Encrypt/Decrypt
            else
            {
                key = HexFixer(args[0]);
                plainText = args[1];
                key_1 = HexToByte(key.Substring(0, 14) + "00");
                key_2 = HexToByte(key.Substring(14, 14) + "00");
                plainText_Byte = HexToByte(plainText);
                byte[] Encrypt1_Byte = Encrypt(plainText_Byte, key_1, key_1);
                string Tester = ByteToHex(Encrypt1_Byte);
                byte[] Encrypt2_Byte = Encrypt(Encrypt1_Byte, key_2, key_2);
                string Final_Encrypt = HexFixer(ByteToHex(Encrypt2_Byte));
                byte[] Decrypt1_Byte = Decrypt(Encrypt2_Byte, key_2, key_2);
                string Tester2 = ByteToHex(Decrypt1_Byte);
                byte[] Decrypt2_Byte = Decrypt(Decrypt1_Byte, key_1, key_1);
                string Final_Decrypt = HexFixer(ByteToHex(Decrypt2_Byte));
                Console.WriteLine("Encrypted String(Hex) = " + Final_Encrypt);
                Console.WriteLine("Decrypted String(Hex) = " + Final_Decrypt);
                Console.ReadLine(); 
            }

             
        }

        //Midtext finder
        public static MiddleText FindMid(MiddleText[] Mid1, MiddleText[] Mid2)
        {
            MiddleText Found = new MiddleText();
            string finder = "NULL";
            Found.text = finder;
            Console.WriteLine("Starting Midtext Comparison");
            for(int i = 0; i < Mid1.Length; i++)
            {
                for(int j = 0; j < Mid2.Length; j++)
                {
                    if (Mid1[i].text.Equals(Mid2[j].text))
                    {
                        Found.key = Mid1[i].key.Substring(0,14) + " " + Mid2[j].key.Substring(0,14);
                        Found.text = Mid1[i].text;
                        return Found;
                    }
                }
            }
            return Found;
        }

        //Seris of encryption for various byte configs
        public static MiddleText[] Test1_Encrypt(byte[] key, byte[] plainText)
        {
            MiddleText[] Final = new MiddleText[256];
            for (int i = 0; i < 256; i++)
            {
                Final[i] = new MiddleText();
                Final[i].text = ByteToHex(Encrypt(plainText, key, key));
                string test = ByteToHex(key);
                Final[i].key = ByteToHex(key);
                key[0]++;
            }
            return Final;
        }

        public static MiddleText[] Test2_Encrypt(byte[] key, byte[] plainText, int[] ByteCounter)
        {
            MiddleText[] Final = new MiddleText[65536];
            int count = 0;

            Console.WriteLine("Encrypt Half Started...");
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    Final[count] = new MiddleText();
                    Final[count].text = ByteToHex(Encrypt(plainText, key, key));
                    Final[count].key = ByteToHex(key);
                    key[ByteCounter[0]]++;
                    count++;
                }
                key[ByteCounter[1]]++;
            }
            Console.WriteLine("Encrypt Half Finished");
            return Final;
        }

        public static MiddleText[] Test3_Encrypt(byte[] key, byte[] plainText, int[] ByteCounter)
        {
            MiddleText[] Final = new MiddleText[16777216];
            int count = 0;

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    for (int k = 0; k < 256; k++)
                    {
                        Final[count] = new MiddleText();
                        Final[count].text = ByteToHex(Encrypt(plainText, key, key));
                        Final[count].key = ByteToHex(key);
                        key[ByteCounter[0]]++;
                        count++;
                    }
                    key[ByteCounter[1]]++;
                    count++;
                }
                key[ByteCounter[2]]++;
                count++;
            }
            return Final;
        }

        //Decryption seris, same as encryption
        public static MiddleText[] Test1_Decrypt(byte[] key, byte[] plainText)
        {
            MiddleText[] Final = new MiddleText[256];
            for (int i = 0; i < 256; i++)
            {
                Final[i] = new MiddleText();
                Final[i].text = ByteToHex(Decrypt(plainText, key, key));
                string test = ByteToHex(key);
                Final[i].key = ByteToHex(key);
                key[0]++;
            }
            return Final;
        }

        public static MiddleText[] Test2_Decrypt(byte[] key, byte[] plainText, int[] ByteCounter)
        {
            MiddleText[] Final = new MiddleText[65536];
            int count = 0;

            Console.WriteLine("Decrypt Half Started...");
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    Final[count] = new MiddleText();
                    Final[count].text = ByteToHex(Decrypt(plainText, key, key));
                    Final[count].key = ByteToHex(key);
                    key[ByteCounter[0]]++;
                    count++;
                }
                key[ByteCounter[1]]++;
            }
            Console.WriteLine("Decrypt Half Finished");
            return Final;
        }

        public static MiddleText[] Test3_Decrypt(byte[] key, byte[] plainText, int[] ByteCounter)
        {
            MiddleText[] Final = new MiddleText[16777216];
            int Ten = 16777216/10;
            int Tens = 1;
            int count = 0;

            Console.WriteLine("Decrypt Half Started...");
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    for (int k = 0; k < 256; k++)
                    {
                        Final[count] = new MiddleText();
                        Final[count].text = ByteToHex(Decrypt(plainText, key, key));
                        Final[count].key = ByteToHex(key);
                        key[ByteCounter[0]]++;
                        count++;
                        if (Ten * Tens == count)
                        {
                            Console.Write(Tens + "0% ");
                            Tens++;
                        }
                    }
                    key[ByteCounter[1]]++;
                    count++;
                }
                key[ByteCounter[2]]++;
                count++;
            }
            return Final;
        }

        //String parsing
        public static string RemoveMarks(string key)
        {
            string temp = "";
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i].Equals('?'))
                {
                    temp += "0";
                }
                else
                {
                    temp += key[i];
                }
            }
            return temp;
        }

        //Input parsing
        public static int[] FindVars(string key)
        {
            int count = 0;
            int[] Final;
            foreach (char c in key)
            {
                if (c == '?')
                {
                    count++;
                }
            }

            count = count / 2;
            Final = new int[count];
            count = 0;

            for (int i = 0; i < key.Length; i++)
            {
                if (key[i].Equals('?'))
                {
                    Final[count] = (1 + i) / 2;
                    count++;
                    i++;
                }
            }
            return Final;
        }

        //Encrypt method
        public static byte[] Encrypt(byte[] plainText, byte[] Key, byte[] IV)
        {
            MemoryStream MemStream = new MemoryStream();
            DES Des = DES.Create();
            Des.BlockSize = 64;
            Des.Padding = PaddingMode.None;
            Des.Key = Key;
            Des.IV = IV; 
            CryptoStream CryStream = new CryptoStream(MemStream, Des.CreateEncryptor(), CryptoStreamMode.Write);
            CryStream.Write(plainText, 0, plainText.Length);
            CryStream.Close();
            byte[] Encrypt_Byte = MemStream.ToArray();
            return Encrypt_Byte;
        }

        //Decrypt
        public static byte[] Decrypt(byte[] plainText, byte[] Key, byte[] IV)
        {
            MemoryStream MemStream = new MemoryStream();
            DES Des = DES.Create();
            Des.BlockSize = 64;
            Des.Padding = PaddingMode.None;
            Des.Key = Key;
            Des.IV = IV;
            CryptoStream CryStream = new CryptoStream(MemStream, Des.CreateDecryptor(), CryptoStreamMode.Write);
            CryStream.Write(plainText, 0, plainText.Length);
            CryStream.Close();
            byte[] Decrypt_Byte = MemStream.ToArray();
            return Decrypt_Byte;
        }

        //Hex string to Byte
        public static byte[] HexToByte(string hex)
        {
            byte[] Converted_String = Enumerable.Range(0, hex.Length).Where(x => 0 == x % 2).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
            return Converted_String;
        }

        //Byte to Hex
        public static string ByteToHex(byte[] in_ByteArray)
        {
            StringBuilder sb = new StringBuilder(in_ByteArray.Length * 2);
            foreach (byte b in in_ByteArray)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return HexFixer(sb.ToString());
        }

        //Captiolize
        public static string HexFixer(string key)
        {
            string keyFix = "";
            foreach (char c in key)
            {
                int temp = Convert.ToInt32(c);
                if (temp < 97)
                {
                    keyFix += c;
                }
                else
                {
                    keyFix += Convert.ToChar(temp - 32);
                }
            }
            return keyFix;
        }
        


    }
}
