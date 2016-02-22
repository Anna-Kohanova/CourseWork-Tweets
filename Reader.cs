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
    public class Reader
    {
        protected string fileName = "";
        public virtual void readingInfo() { }
    }

    public class TxtReader : Reader
    {
        protected StreamReader reader;
    }

    public class HelpReader : TxtReader
    {
        public string Text { get; set; }
        public HelpReader(string fileName)
        {
            base.fileName = fileName;
            reader = new StreamReader(base.fileName);
            this.Text = reader.ReadToEnd();
        }
    }

    public class XmlReader : Reader
    {
        protected XDocument xDoc;
    }
}
