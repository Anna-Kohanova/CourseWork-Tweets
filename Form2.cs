using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CourseWorkTweets
{
    public partial class Form2 : Form
    {
        public Form2(string type, Hashtable ht, string seriesName)
        {
            InitializeComponent();

            this.chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), type);
            this.chart1.Series["Series1"].Name = seriesName;

            foreach (DictionaryEntry de in ht)
            {
                if (type != "Pie")
                {
                    this.chart1.Series[0].Points.AddXY(de.Key, de.Value);
                }
                else if (Convert.ToDouble(de.Value) != 0)
                {
                    this.chart1.Series[0].Points.AddXY(de.Key, de.Value);
                }
            }

            chart1.ChartAreas[0].AxisX.ScaleView.Size = 4;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
        }
    }
}
