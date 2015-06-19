using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WordAnalytics
{
    class Program
    {

        public static int wordcount = 0;
        public static int lettercount;
        public static int finalWordcount = 0;
        public static int symbolcount = 0;

        static void Main(string[] args)
        {
            string location = args[0];
            StreamReader sr = new StreamReader(location);
            Word[] Words = new Word[1000];
            Letter[] Letters = new Letter[26];
            Letters = PopulateLetters(Letters);
            string input = sr.ReadToEnd();
            sr.Close();
            string wordHold = "";
            

            for (int i = 0; i < input.Length; i++)
            {
                letterCheck(input[i], Letters);
                if (input[i].Equals(' ') || input[i].Equals('.') || input[i].Equals(','))
                {
                    wordCheck(wordHold, wordcount, Words);
                    finalWordcount++;
                    wordHold = "";
                }
                else
                {
                    wordHold += input[i];
                }
            }

            Word[] finalWords = topWords(Words);
            Letter[] finalLetters = topLetters(Letters);

            Console.WriteLine("Letter count = " + lettercount);
            Console.WriteLine("Word count = " + finalWordcount);
            Console.WriteLine("Symbol count = " + symbolcount);
            Console.WriteLine("Three most used words = " + finalWords[0].word + " " + finalWords[1].word + " " + finalWords[2].word);
            Console.WriteLine("Three most used words = " + finalLetters[0].letter + " " + finalLetters[1].letter + " " + finalLetters[2].letter);
            Console.WriteLine("Letters not used = " + noLetters(Letters));
            Console.WriteLine("Words used only once = " + oneWord(Words));
            Console.ReadLine();
        }

        static string noLetters(Letter[] Master)
        {

            string output = "";
            for (int i = 0; i < Master.Length; i++)
            {
                if (Master[i].count == 0)
                {
                    output = output + Master[i].letter + ",";
                }
            }

            return output;
        }

        static string oneWord(Word[] Master)
        {
            string output = "";
            for (int i = 0; i < wordcount; i++)
            {
                if (Master[i].count == 1)
                {
                    output = output + Master[i].word + ", ";
                }
            }

            return output;
        }

        static Word[] topWords(Word[] Master)
        {
            Word[] Top = new Word[3];
            Top[0] = new Word(" ");
            Top[0] = new Word(" ");
            Top[0] = new Word(" ");
            int compare = 0;
            for (int i = 0; i < wordcount; i++)
            {
                if (Master[i].count > compare  && Master[i].word.Length > 1)
                {
                    Top[2] = Top[1];
                    Top[1] = Top[0];
                    Top[0] = Master[i];
                    compare = Master[i].count;
                    continue;
                }

                if (Master[i].count > Top[1].count && Master[i].word.Length > 1)
                {
                    Top[2] = Top[1];
                    Top[1] = Master[i];
                    continue;
                }

                if (Master[i].count > Top[2].count && Master[i].word.Length > 1)
                {
                    Top[2] = Master[i];
                }
            }
            return Top;
        }

        static Letter[] topLetters(Letter[] Master)
        {
            Letter[] Top = new Letter[3];
            Top[0] = new Letter(' ');
            Top[1] = new Letter(' ');
            Top[2] = new Letter(' ');
            int compare = 0;
            for (int i = 0; i < Master.Length; i++)
            {
                if (Master[i].count > compare)
                {
                    Top[2] = Top[1];
                    Top[1] = Top[0];
                    Top[0] = Master[i];
                    compare = Master[i].count;
                    continue;
                }

                if (Master[i].count > Top[1].count)
                {
                    Top[2] = Top[1];
                    Top[1] = Master[i];
                    continue;
                }

                if (Master[i].count > Top[2].count)
                {
                    Top[2] = Master[i];
                }
            }

            return Top;
        }

        static Letter[] PopulateLetters(Letter[] Master)
        {
            for (int i = 0; i < Master.Length; i++)
            {
                Master[i] = new Letter(Convert.ToChar(i + 97));
            }

            return Master;
        }

        static void wordCheck(string inWord, int count, Word[] Master)
        {
            bool check = false;

            if (wordcount > 1)
            {
                for (int i = 0; i < wordcount; i++)
                {
                    check = Master[i].Compare(inWord);
                }
            }

            if (!check)
            {
                Master[count] = new Word(inWord);
                wordcount++;
            }
        }

        static void letterCheck(char inLetter, Letter[] Master)
        {
            //minus 96
            if (Convert.ToInt32(inLetter) < 65 || Convert.ToInt32(inLetter) > 123 || Convert.ToInt32(inLetter) == 95)
            {
                if(!inLetter.Equals(' '))
                {
                    symbolcount++;
                }
            }

            else
            {
                int test = Convert.ToInt32(inLetter) - 96;
                if (test < 0)
                {
                    test += 32;
                }

                lettercount++;
                Master[test].Increment();
            }
        }
    }

    class Word
    {
        public string word;
        public int count;

        public Word(string _input)
        {
            word = _input;
            count = 1;
        }

        public bool Compare(string _input)
        {
            if (_input.Equals(word))
            {
                count++;
                return true;
            }

            else
            {
                return false;
            }
        }
    }

    class Letter
    {
        public char letter;
        public int count;

        public Letter(char _input)
        {
            letter = _input;
            count = 0;
        }

        public void Increment()
        {
            count++;
        }
    }
}
