using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

    public class EmptyTrie : TrieInterface
    {

        //Trie class recycled from a previous assignment, all code was written by me



        //private varibles
        private bool _Empty;
        private TrieInterface _Temp;

        //Constructor that takes one bool arguement
        public EmptyTrie(bool _isEmpty)
        {
            _Empty = _isEmpty;
        }

        //Add meathod as dictated by TrieInterface interface
        public TrieInterface Add(string word)
        {
            //Checks if word is empty
            if (word == "")
            {
                _Empty = true;
                return this;
            }
            //if not empty, new child is created
            _Temp = new SingleChild(word, _Empty);
            return _Temp;
        }

        //node labeler
        public TrieInterface Child(char label)
        {
            _Temp = new EmptyTrie(false);
            return _Temp;
        }

        //ICollection
        public ICollection<KeyValuePair<char, TrieInterface>> Children
        {
            get
            {
                return new List<KeyValuePair<char, TrieInterface>>();
            }
        }

        //check is word is contained within the child
        public bool Contains(string word)
        {
            bool temp = (word == "" && _Empty);
            return temp;
        }

        //Count
        public int Count
        {
            get
            {
                if (_Empty)
                {
                    return 1;
                }
                return 0;
            }
        }
    }

    public class SingleChild : TrieInterface
    {

        #region TrieInterface Members

        //Private varibles
        private TrieInterface _Temp;
        private int _Count;
        private bool _isEmpty = false;
        private char _Point;
        private bool Empty = true;
        private bool NotEmpty = false;
        private int TempCount;

        //Constructor that takes in two arguements
        public SingleChild(string _Word, bool isEmpty)
        {
            //Defines label
            _Point = _Word[0];

            //Checks if the word is actually empty
            if (_Word.Length == 1)
            {
                _Temp = new EmptyTrie(Empty);
            }
            //Otherwise new SingleChild is created
            else
            {
                _Temp = new SingleChild(_Word.Substring(1), NotEmpty);
            }

            //Empty checker
            if (isEmpty == true)
            {
                _Count = 2;
                _isEmpty = true;
            }
        }

        //Add method as dictated by TrieInterface interface
        public TrieInterface Add(string word)
        {
            //Sees if the word coming in is actually empty
            if (word == "")
            {
                if (!(_isEmpty))
                {
                    _Count++;
                    _isEmpty = true;
                }
                return this;
            }

            //Sees if word[0] is equal to the label
            if (word[0] == _Point)
            {
                TempCount = _Temp.Count;
                _Temp = _Temp.Add(word.Substring(1));
                _Count += _Temp.Count - TempCount;
                return this;
            }

            //creates new multichild if nothing else gets returned first
            MultiChild Children = new MultiChild(this, _Point);
            return Children.Add(word);


        }

        //Interface labeler
        public TrieInterface Child(char label)
        {
            if (_Point == label)
            {
                return _Temp;
            }
            return new EmptyTrie(Empty);
        }

        //ICollection section, makes new key value pair of objects read in based on rest of class
        public ICollection<KeyValuePair<char, TrieInterface>> Children
        {
            get
            {

                ICollection<KeyValuePair<Char, TrieInterface>> Collect = new List<KeyValuePair<char, TrieInterface>>();
                Collect.Add(new KeyValuePair<char, TrieInterface>(_Point, _Temp));
                return Collect;

            }


        }

        //Contains meathod
        public bool Contains(string word)
        {
            if (word == "")
            {
                return _isEmpty;
            }
            bool _contain = (((word[0] == _Point) && _Temp.Contains(word.Substring(1))));
            return _contain;

        }

        //Counter
        public int Count
        {
            get { return _Count; }
        }

        #endregion
    }

    public class MultiChild : TrieInterface
    {

        #region TrieInterface Members

        //Varibles
        private TrieInterface[] _Array = new TrieInterface[26];
        private TrieInterface Hold;
        private bool _Empty;
        private bool isEmpty = false;
        private int _Count;
        private int ReCount;

        //Dual arguement constructor with a SingleChild and char label as parameters
        public MultiChild(SingleChild y, char x)
        {
            TrieInterface _Temp = y.Child(x);

            for (int i = 0; i < 26; i++)
            {
                _Array[i] = new EmptyTrie(isEmpty);
            }
            //used this operator due to frustration with trying to define varibles to hold values
            this._Array[x - 'a'] = _Temp;
            this._Empty = y.Contains("");
            this._Count = y.Count;
        }

        //Add Meathod
        public TrieInterface Add(string word)
        {
            //Makes sure its not empty
            if (word == "")
            {
                if (!(_Empty))
                {
                    _Count++;
                    _Empty = true;
                }
                return this;
            }

            //Adds in substring to array based on the value in the first array slot, - a is there to fix a reocuring error (also used further down)
            int indexer = word[0] - 'a';
            ReCount = _Array[indexer].Count;
            _Array[indexer] = _Array[indexer].Add(word.Substring(1));
            _Count += _Array[indexer].Count - ReCount;
            return this;

        }

        //Labeler
        public TrieInterface Child(char label)
        {
            if (char.IsLower(label))
            {
                return _Array[label - 'a'];
            }
            return new EmptyTrie(isEmpty);
        }

        //ICollection
        public ICollection<KeyValuePair<char, TrieInterface>> Children
        {
            get
            {
                ICollection<KeyValuePair<char, TrieInterface>> TempCollect = new List<KeyValuePair<char, TrieInterface>>();
                for (int i = 0; i < 26; i++)
                {
                    if (_Array[i].Count > 0)
                    {
                        //fills _array with increasing letters, took a long time to figure out
                        TempCollect.Add(new KeyValuePair<char, TrieInterface>((char)((int)(0 + i)), _Array[i]));
                    }
                }
                return TempCollect;
            }
        }

        //Contain meathod
        public bool Contains(string word)
        {
            if (word == "")
            {
                return _Empty;
            }
            if (char.IsLower(word[0]))
            {
                //- a to fix error
                int indexer = word[0] - 'a';

                if (_Array[indexer].Count == 0)
                {
                    return isEmpty;
                }
                return _Array[indexer].Contains(word.Substring(1));
            }
            return isEmpty;

        }

        //Count getter
        public int Count
        {
            get { return _Count; }
        }

        #endregion
    }

