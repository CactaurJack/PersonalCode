using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

    public class Analyze
    {
        private string text;
        public int[] index = new int[26];
        public string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //private int hold = 0;

        public Analyze(string _text)
        {
            text = _text;
            for (int i = 0; i < 26; i++)
            {
                index[i] = 0;
            }
        }

        public void Run()
        {
            for (int i = 0; i < text.Length; i++)
            {
                //hold = alpha.IndexOf(text[i]);
                index[alpha.IndexOf(text[i])]++;
            }
        }

    }

