using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkTweets
{
    class StatesCoordinates
    {
        public List<string> StateNames { set; get; }
        public List<List<Coordinates>> StateCoordinates { set; get; }

        public StatesCoordinates()
        {
            StatesCoordinatesReader reader = new StatesCoordinatesReader("states.txt");
            reader.readingInfo();
            this.StateNames = reader.getStateNames();
            this.StateCoordinates = reader.getStateCoordinates();            
        }
    }
}
