using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PanoGeoRefLib
{
    public class MapPoint
    {
        /// <summary>
        /// Point name
        /// </summary>
        public string PointName { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        public float Lat { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public float Lon { get; set; }

        /// <summary>
        /// X coordinate on image of Point
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y coordinate on image of Point
        /// </summary>
        public float Y{ get; set; }
    }
}
