using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CourseWorkTweets
{
    class StatesCoordinatesReader : TxtReader
    {
        private List<string> stateInfo;
        private List<string> stateNames;
        private List<List<Coordinates>> stateCoordinates;
        
        public StatesCoordinatesReader(string fileName)
        {
            base.fileName = fileName;
            reader = new StreamReader(base.fileName);
            stateInfo = new List<string>();
            stateNames = new List<string>();
            stateCoordinates = new List<List<Coordinates>>();
        }

        public List<List<Coordinates>> getStateCoordinates()
        {
            return stateCoordinates;
        }

        public List<string> getStateNames()
        {
            return stateNames;
        }

        public override void readingInfo()
        {
            string line = " ";
            string state = " ";
            int p = 0;
            int count = 0;            
            double x = 0.0;
            double y = 0.0;

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                line = line.Replace('.', ',');
                stateInfo.Add(line);
            }

            for (int j = 0; j < stateInfo.Count; j++)
            {
                count = 0;
                p = 0;
                stateCoordinates.Add(new List<Coordinates>());

                for (int i = p; i < stateInfo[j].Length; i++)
                {
                    if (p == 0)
                    {
                        if (stateInfo[j][i] != ' ')
                        {
                            count++;
                        }
                        else
                        {
                            state = stateInfo[j].Substring(0, count);
                            stateNames.Add(state);
                            p = count;
                        }
                    }
                    else
                    {
                        int i1 = i + 1;
                        count = 0;

                        for (int k = i1; k < stateInfo[j].Length; k++)
                        {
                            if ((stateInfo[j][k] != ',' && stateInfo[j][k + 1] != ' ') || (stateInfo[j][k] == ',' && stateInfo[j][k + 1] != ' '))
                            {
                                count++;
                            }
                            else if (stateInfo[j][k] == ',' && stateInfo[j][k + 1] == ' ')
                            {
                                x = double.Parse(stateInfo[j].Substring(i1, count));
                                int k1 = k + 2;
                                count = 0;

                                for (int l = k1; l < stateInfo[j].Length; l++)
                                {
                                    if (stateInfo[j][l] != ']')
                                    {
                                        count++;
                                    }

                                    else
                                    {
                                        i = l + 2;
                                        y = double.Parse(stateInfo[j].Substring(k1, count));
                                        break;
                                    }
                                }
                                Coordinates c = new Coordinates(x, y);
                                stateCoordinates[j].Add(c);
                                break;
                            }
                        }
                    }
                }
            }
            reader.Close();
        }
    }
}
