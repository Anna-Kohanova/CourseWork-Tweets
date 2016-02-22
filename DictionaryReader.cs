using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CourseWorkTweets
{
    class DictionaryReader : XmlReader
    {
        private Hashtable hashTable;

        public DictionaryReader(string fileName)
        {
            base.fileName = fileName;
            xDoc = XDocument.Load(base.fileName);
            hashTable = new Hashtable();
        }

        public Hashtable getHashTable()
        {
            return hashTable;
        }

        public override void readingInfo()
        {
            foreach (XElement elm in xDoc.Descendants())     
            {
                if (elm.Element("Word") != null && elm.Element("value") != null)
                {
                    hashTable.Add(elm.Element("Word").Value, elm.Element("value").Value);
                }
            }
        }
    }
}
