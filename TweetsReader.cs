using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace CourseWorkTweets
{

    [Serializable]
    class TweetsReader : XmlReader
    {
        public List<Tweets> Tweets;

        public TweetsReader(string fileName)
        {
            Tweets = new List<Tweets>();
            base.fileName = fileName;
            xDoc = XDocument.Load(base.fileName);
            this.readingInfo();
        }

        //запись
        //public void Serialize(List<LifeTweet> books, string type)
        //{
        //    FileStream fs = new FileStream("job.xml", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<LifeTweet>));
        //    xmlSerializer.Serialize(fs, books);
        //    fs.Close();
        //}

        //чтение
        //public List<Tweets> Deserialize(List<Tweet> tweets, string type)
        //{
        //    FileStream fs = new FileStream("food.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Tweets>));
        //    tweets = (List<Tweets>)xmlSerializer.Deserialize(fs);
        //    fs.Close();

        //    return tweets;
        //}

        public override void readingInfo()
        {
            double coordX = 0.0;
            double coordY = 0.0;
            string time = "";
            string tweet = "";

            foreach (XElement elm in xDoc.Descendants())
            {
                if (elm.Element("coordX") != null && elm.Element("coordY") != null && elm.Element("time") != null && elm.Element("tweet") != null)
                {
                    coordX = Convert.ToDouble(elm.Element("coordX").Value);
                    coordY = Convert.ToDouble(elm.Element("coordY").Value);
                    time = elm.Element("time").Value;
                    tweet = elm.Element("tweet").Value;

                    Tweets tw = new Tweets("", time, tweet, 0.0, coordX, coordY);
                    this.Tweets.Add(tw);
                }
            }
        }
    }
}
