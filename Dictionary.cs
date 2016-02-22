using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace CourseWorkTweets
{
    class Dictionary
    {
        private Hashtable hashTable;
        private string fileName = "dictionary.xml";

        public Dictionary() 
        {
            DictionaryReader reader = new DictionaryReader(fileName);
            hashTable = new Hashtable();
            reader.readingInfo();
            hashTable = reader.getHashTable();
        }

        public double searching(string word)
        {
            foreach (DictionaryEntry de in hashTable)
            {
                if (de.Key.Equals(word))
                {
                    return Convert.ToDouble(de.Value);
                }
            }
            return 0.0;
        }
    }
}
