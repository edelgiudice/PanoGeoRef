using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Imaging;
using System.Drawing;

namespace PanoGeoRefLib
{
    /// <summary>
    /// Mapping class to manage coordinate transformation
    /// between ImageMap (Google aerial images) and (Geo Coordinates)
    /// </summary>
    internal class MapCoordinate
    {
        /// <summary>
        /// Map image file name
        /// </summary>
        internal string MapFileName { get; set; }

        /// <summary>
        /// List of map points to georeferencing ImageMap (at least 4 points)
        /// </summary>
        internal List<MapPoint> MapPointsLst { get; set; }

     

        /// <summary>
        /// Matrix to transform ImageMap points into Coordinates
        /// </summary>
        internal Accord.Imaging.MatrixH MatrixImageMap2Geo
        {
            get
            {
                if (_matrixImageMap2Geo == null)
                {


                    List<PointF> sourcePoints = new List<PointF>();
                    List<PointF> destinationPoints = new List<PointF>();


                    for (int i = 0; i < MapPointsLst.Count; i++)
                    {
                        sourcePoints.Add(new PointF(MapPointsLst[i].X,MapPointsLst[i].Y));
                        destinationPoints.Add(new PointF(MapPointsLst[i].Lon, MapPointsLst[i].Lat));
                    }

                    if (sourcePoints.Count >= 4)
                    {

                        _matrixImageMap2Geo = Accord.Imaging.Tools.Homography(sourcePoints.ToArray(), destinationPoints.ToArray());
                        
                    }
                }
                return _matrixImageMap2Geo;
            }
        }
        private MatrixH _matrixImageMap2Geo = null;

        /// <summary>
        ///  Matrix to transform Coordinates points into image points
        /// </summary>
        internal Accord.Imaging.MatrixH MatrixGeo2ImageMap
        {
            get
            {
                if (_matrixGeo2ImageMap == null)
                {


                    List<PointF> sourcePoints = new List<PointF>();
                    List<PointF> destinationPoints = new List<PointF>();


                    for (int i = 0; i < MapPointsLst.Count; i++)
                    {
                        sourcePoints.Add(new PointF(MapPointsLst[i].Lon,MapPointsLst[i].Lat));
                        destinationPoints.Add(new PointF(MapPointsLst[i].X, MapPointsLst[i].Y));
                        
                    }

                    if (sourcePoints.Count >= 4)
                    {

                        _matrixGeo2ImageMap = Accord.Imaging.Tools.Homography(sourcePoints.ToArray(), destinationPoints.ToArray());

                    }
                }
                return _matrixGeo2ImageMap;
            }
        }
        private MatrixH _matrixGeo2ImageMap = null;

        /// <summary>
        /// transformation function: MapPoints to Coordinates
        /// </summary>
        /// <param name="X">X coordinate on Image Map</param>
        /// <param name="Y">X coordinate on Image Map</param>
        /// <returns>Geo Coordinate</returns>
        internal PointF Point2Coordinate(float X, float Y)
        {
            PointF[] sourcePoints = new PointF[] { new PointF(X, Y) };

            PointF[] destinationPoints = MatrixImageMap2Geo.TransformPoints(sourcePoints);

         
             if (destinationPoints != null)
             {
                 return destinationPoints[0];
             }
             else return PointF.Empty;
        }

        /// <summary>
        /// transformation function:  Coordinates to Image coordinates
        /// </summary>
        /// <param name="XCoor">XCoor Geo Coordinate</param>
        /// <param name="YCoor">YCoor Geo Coordinate</param>
        /// <returns>Pixel coordinate</returns>
        internal PointF Coordinate2Point(float XCoor, float YCoor)
        {
            PointF[] sourcePoints = new PointF[] { new PointF(XCoor, YCoor) };

            PointF[] destinationPoints = MatrixGeo2ImageMap.TransformPoints(sourcePoints);


            if (destinationPoints != null)
            {
                return destinationPoints[0];
            }
            else return PointF.Empty;
        }

    }

}
