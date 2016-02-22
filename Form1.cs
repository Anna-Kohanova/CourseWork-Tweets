using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET;
using GMap.NET.MapProviders;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;


namespace CourseWorkTweets
{
    public partial class Form1 : Form
    {
        private TweetsAnalysis tweetsAnalisys;
        public Form1()
        {            
            InitializeComponent();
            tweetsAnalisys = new TweetsAnalysis();
        
            comboBox1.Items.Add("    ---All states---     ");
            for (int i = 0; i < tweetsAnalisys.tweets.Count; i++)
            {
                if (comboBox1.FindString(tweetsAnalisys.tweets[i].State) == -1)
                {
                    comboBox1.Items.Add(tweetsAnalisys.tweets[i].State);
                }
            }
            comboBox1.Sorted = true;
            comboBox1.SelectedIndex = 0;

            trackBar1.Maximum = 8;
            trackBar1.Minimum = 2;
            trackBar1.Value = (int)gMapControl1.Zoom;


            radioButton3.Checked = true; 
            radioButton6.Checked = true;
            columnsRadioButton.Checked = true;
            showButton.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.SetPositionByKeywords("USA");
        }

        private void gMapControl1_OnMapZoomChanged()
        {
            trackBar1.Value = (int)gMapControl1.Zoom;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            gMapControl1.Zoom = trackBar1.Value;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                propertyGrid1.SelectedObject = listBox1.SelectedItem;
                richTextBox1.Text = listBox1.Text;

                GMapOverlay markersOverlay = new GMapOverlay("markers");
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng((listBox1.SelectedItem as Tweets).coordX, (listBox1.SelectedItem as Tweets).coordY), GMarkerGoogleType.orange);
                gMapControl1.UpdateMarkerLocalPosition(marker);
                marker.ToolTipText = (listBox1.SelectedItem as Tweets).State;
                markersOverlay.Markers.Add(marker);
                gMapControl1.Overlays.Clear();
                gMapControl1.Overlays.Add(markersOverlay);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpReader hr = new HelpReader("help.txt");            
            MessageBox.Show(hr.Text, "Help");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            richTextBox1.Clear();
            propertyGrid1.SelectedObject = null;
            gMapControl1.Overlays.Clear();

            showButton.Enabled = false;            
            foodRadioButton.Checked = false;
            jobRadioButton.Checked = false;
            lifeRadioButton.Checked = false;
            allRadioButton.Checked = false;
        }

        private void listFill(RadioButton radioButton, string type)
        {           
            listBox1.Items.Clear();
            richTextBox1.Clear();
            propertyGrid1.SelectedObject = null;
            gMapControl1.Overlays.Clear();

            if (comboBox1.SelectedIndex == 0)
            {
                for (int i = 0; i < tweetsAnalisys.tweets.Count; i++)
                {
                    if ((tweetsAnalisys.tweets[i]).Type == type)
                    {
                        listBox1.Items.Add(tweetsAnalisys.tweets[i]);
                    }
                    else if (type == "all")
                    {
                        listBox1.Items.Add(tweetsAnalisys.tweets[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < tweetsAnalisys.tweets.Count; i++)
                {
                    if (comboBox1.Text == tweetsAnalisys.tweets[i].State && ((tweetsAnalisys.tweets[i]).Type == type || type == "all"))
                    {
                        listBox1.Items.Add(tweetsAnalisys.tweets[i]);
                    }
                }
            }

            if (listBox1.Items.Count != 0)
            {
                showButton.Enabled = true;  
            }
            else
            {
                showButton.Enabled = false;  
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMap.NET.MapProviders.YandexMapProvider.Instance;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
        }

        private void foodRadioButton_CheckedChanged_1(object sender, EventArgs e)
        {
            if (foodRadioButton.Checked)
            {
                this.listFill(foodRadioButton, "food");
            }   
        }       

        private void jobRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (jobRadioButton.Checked)
            {
                this.listFill(jobRadioButton, "job");
            }
        }

        private void lifeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (lifeRadioButton.Checked)
            {
                this.listFill(lifeRadioButton, "life");
            }        
        }

        private void allRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (allRadioButton.Checked)
            {
                this.listFill(allRadioButton, "all");
            }    
        }

        private string checkGraphButtons()
        {
            if (pieRadioButton.Checked == true)
            {
                return "Pie";
            }
            else if (columnsRadioButton.Checked == true)
            {
                return "Column";
            }
            else if (bubbleRadioButton.Checked == true)
            {
                return "Bubble";
            }
            else
                return "Pyramid";                       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            string graphType = "";
            string seriesName = "";

            if (radioButton6.Checked == true)
            {
                ht = tweetsAnalisys.dayTime(listBox1);
                Analyzer analys = new Analyzer(tweetsAnalisys.dayTime);
                ht = analys(listBox1);
                seriesName = "Emotional Coloring";
            }

            else if (radioButton7.Checked == true)
            {
                Analyzer analys = new Analyzer(tweetsAnalisys.EmotionalColoring);
                ht = analys(listBox1);
                seriesName = "Emotional Coloring";
            }

            else if (radioButton8.Checked == true)
            {
                Analyzer analys = new Analyzer(tweetsAnalisys.stateTweetsNumber);
                ht = analys(listBox1);
                seriesName = "Number of tweets";
            }
            else
            {
                Analyzer analys = new Analyzer(tweetsAnalisys.dayTimeTweetsNumber);
                ht = analys(listBox1);
                seriesName = "Number of tweets";
            }

            graphType = checkGraphButtons();        


            Form2 form2 = new Form2(graphType, ht, seriesName);
            form2.ShowDialog();
        }    
    }
}
