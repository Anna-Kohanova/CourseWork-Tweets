using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using GMap.NET;
using System.Threading.Tasks;

namespace CourseWorkTweets
{
    class PolygonCalculation
    {
         private List<GMapPolygon> polygons = new List<GMapPolygon>();
         private StatesCoordinates st = new StatesCoordinates();

         public List<GMapPolygon> getPolygons()
         {
             return polygons;
         }
         public PolygonCalculation()
         {
             List<PointLatLng> points = new List<PointLatLng>();

             for (int i = 0; i < st.StateCoordinates.Count; i++)
             {
                 for (int j = 0; j < 8; j++)
                 {
                     points.Add(new PointLatLng(st.StateCoordinates[i][j].X, st.StateCoordinates[i][j].Y));
                 }
                 GMapPolygon polygon = new GMapPolygon(points, "mypoligon");
                 polygons.Add(polygon);
             }
         }

         public string determiningState(double coordX, double coordY)
         {
             PointLatLng p = new PointLatLng(coordX, coordY);
             string state = "";
             for (int i = 0; i < polygons.Count; i++)
             {
                 if (polygons[i].IsInside(p))
                 {
                     state = st.StateNames[i];
                     break;
                 }
             }
             return state;
         }
    }
}
