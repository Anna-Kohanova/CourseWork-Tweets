using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkTweets
{
    delegate Hashtable Analyzer(ListBox listBox);
    class TweetsAnalysis
    {
        public List<Tweets> tweets = new List<Tweets>();
        public TweetsAnalysis()
        {
            List<Tweets> tw = new List<Tweets>();
            TweetsReader reader = new TweetsReader("job.xml");
            tw = reader.Tweets;
            this.infoAnalyzer(tw);
            this.addTweets(tw, "job");

            TweetsReader reader1 = new TweetsReader("food.xml");
            tw = reader1.Tweets;
            this.infoAnalyzer(tw);
            this.addTweets(tw, "food");

            TweetsReader reader2 = new TweetsReader("life.xml");
            tw = reader2.Tweets;
            this.infoAnalyzer(tw);
            this.addTweets(tw, "life");
        }

        private void addTweets(List<Tweets> tw, string type)
        {
            foreach (Tweets tweet in tw)
            {
                switch (type)
                {
                    case "food":
                        {
                            FoodTweet foodTweet = new FoodTweet(tweet.State, tweet.Time, tweet.Tweet, tweet.Coloring, tweet.coordX, tweet.coordY);
                            tweets.Add(foodTweet);
                            break;
                        }
                    case "job":
                        {
                            JobTweet jobTweet = new JobTweet(tweet.State, tweet.Time, tweet.Tweet, tweet.Coloring, tweet.coordX, tweet.coordY);
                            tweets.Add(jobTweet);
                            break;
                        }
                    case "life":
                        {
                            LifeTweet lifeTweet = new LifeTweet(tweet.State, tweet.Time, tweet.Tweet, tweet.Coloring, tweet.coordX, tweet.coordY);
                            tweets.Add(lifeTweet);
                            break;
                        }
                }
            }
        }

        private void infoAnalyzer(List<Tweets> tw)
        {
            foreach (Tweets tweet in tw)
            {
                PolygonCalculation calc = new PolygonCalculation();
                Dictionary dictionary = new Dictionary();

                tweet.State = calc.determiningState(tweet.coordX, tweet.coordY);

                string str = tweet.Tweet;
                string[] words = str.Split(new[] { ' ', ',', '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < words.Length; i++)
                {
                    tweet.Coloring += dictionary.searching(words[i]);
                }
                tweet.Coloring = Math.Round(tweet.Coloring, 1);
            }

        }
        public Hashtable dayTime(ListBox listBox)
        {
            Hashtable ht = new Hashtable();
            double morningColoring = 0.0;
            double dayColoring = 0.0;
            double eveningColoring = 0.0;
            double nightColoring = 0.0;

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                DateTime dt = Convert.ToDateTime((listBox.Items[i] as Tweets).Time);
                if (dt.Hour >= 0 && dt.Hour < 6)
                {
                    nightColoring += tweets[i].Coloring;
                }
                else if (dt.Hour >= 6 && dt.Hour < 12)
                {
                    morningColoring += tweets[i].Coloring;
                }
                else if (dt.Hour >= 12 && dt.Hour < 17)
                {
                    dayColoring += tweets[i].Coloring;
                }
                else if (dt.Hour >= 17 && dt.Hour <= 23)
                {
                    eveningColoring += tweets[i].Coloring;
                }
            }

            ht.Add("Night", nightColoring);
            ht.Add("Morning", morningColoring);
            ht.Add("Day", dayColoring);
            ht.Add("Evening", eveningColoring);

            return ht;
        }

        public Hashtable EmotionalColoring(ListBox listBox)
        {
            Hashtable ht = new Hashtable();

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                if (!ht.ContainsKey((listBox.Items[i] as Tweets).State))
                {
                    ht.Add((listBox.Items[i] as Tweets).State, (listBox.Items[i] as Tweets).Coloring);
                }
                else
                {
                    double coloring = (double)ht[(listBox.Items[i] as Tweets).State];
                    ht[(listBox.Items[i] as Tweets).State] = coloring + (listBox.Items[i] as Tweets).Coloring;
                }
            }
            return ht;
        }

        public Hashtable stateTweetsNumber(ListBox listBox)
        {
            Hashtable ht = new Hashtable();
            int count = 1;

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                if (!ht.ContainsKey((listBox.Items[i] as Tweets).State))
                {
                    ht.Add((listBox.Items[i] as Tweets).State, count);
                    count++;
                }
                else
                {
                    ht[(listBox.Items[i] as Tweets).State] = count++;
                }
            }
            return ht;
        }

        public Hashtable dayTimeTweetsNumber(ListBox listBox)
        {
            Hashtable ht = new Hashtable();
            int morningCount = 0;
            int dayCount = 0;
            int eveningCount = 0;
            int nightCount = 0;

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                DateTime dt = Convert.ToDateTime((listBox.Items[i] as Tweets).Time);
                if (dt.Hour >= 0 && dt.Hour < 6)
                {
                    nightCount++;
                }
                else if (dt.Hour >= 6 && dt.Hour < 12)
                {
                    morningCount++;
                }
                else if (dt.Hour >= 12 && dt.Hour < 17)
                {
                    dayCount++;
                }
                else if (dt.Hour >= 17 && dt.Hour <= 23)
                {
                    eveningCount++;
                }
            }

            ht.Add("Night", nightCount);
            ht.Add("Morning", morningCount);
            ht.Add("Day", dayCount);
            ht.Add("Evening", eveningCount);

            return ht;
        }
    }
}
