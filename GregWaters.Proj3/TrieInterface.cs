using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

    public interface TrieInterface
    {
       //Interface keeps Master from calling null items when diffrent child configurations are used to store words
            
            int Count { get; }
            bool Contains(string word);
            TrieInterface Child(char label);
            TrieInterface Add(string word);
            ICollection<KeyValuePair<char, TrieInterface>> Children { get; }
        

    }

