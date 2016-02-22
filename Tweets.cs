using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CourseWorkTweets
{
    public class Tweets
    {
        [Category("Tweet Information")]
        public string Time { set; get; }

        [Browsable(false)]
        public string Tweet { set; get; }

        [Category("Tweet Information")]
        public string State { set; get; }

        [Category("Tweet Information")]
        public double Coloring { set; get; }

        [Browsable(false)]
        public double coordX { set; get; }

        [Browsable(false)]
        public double coordY { set; get; }

        [Browsable(false)]
        public string Type { set; get; }

        public Tweets()
        {

        }

        public Tweets(string state, string time, string tweet, double emotionalColoring, double coordX, double coordY)
        {
            this.coordX = coordX;
            this.coordY = coordY;
            this.Time = time;
            this.Tweet = tweet;
        }

        public override string ToString()
        {
                return "•  " + Tweet;
        }
    }

     class FoodTweet : Tweets
    {
        public FoodTweet(string state, string time, string tweet, double emotionalColoring, double coordX, double coordY)
            : base(state, time, tweet, emotionalColoring, coordX, coordY)
        {
            this.State = state;
            this.Coloring = emotionalColoring;
            this.Type = "food";
        }
    }

    class JobTweet : Tweets
    {
        public JobTweet(string state, string time, string tweet, double emotionalColoring, double coordX, double coordY)
            : base(state, time, tweet, emotionalColoring, coordX, coordY)
        {
            this.State = state;
            this.Type = "job";
            this.Coloring = emotionalColoring;
        }
    }

     class LifeTweet : Tweets
    {
        public LifeTweet(string state, string time, string tweet, double emotionalColoring, double coordX, double coordY)
            : base(state, time, tweet, emotionalColoring, coordX, coordY)
        {
            this.State = state;
            this.Type = "life";
            this.Coloring = emotionalColoring;
        }
    }
}
