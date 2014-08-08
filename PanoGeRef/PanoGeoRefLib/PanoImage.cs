using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Accord.Imaging;

namespace PanoGeoRefLib
{
    internal class PanoImage
    {
        internal string Panofilename { get; set; }
        internal List<RefrencePoint> ReferencePointLst { get; set; }
        internal string Id { get; set; }

        /// <summary>
        /// Matrix to transform ImageMap points into Coordinates
        /// </summary>
        internal Accord.Imaging.MatrixH MatrixImageMap2Pano
        {
            get
            {
                if (_matrixImageMap2Pano == null)
                {


                    List<PointF> sourcePoints = new List<PointF>();
                    List<PointF> destinationPoints = new List<PointF>();


                    for (int i = 0; i < ReferencePointLst.Count; i++)
                    {
                        sourcePoints.Add(new PointF(ReferencePointLst[i].MapReference.X, ReferencePointLst[i].MapReference.Y));
                        destinationPoints.Add(new PointF(ReferencePointLst[i].PanoReference.X, ReferencePointLst[i].PanoReference.Y));
                    }

                    if (sourcePoints.Count >= 4)
                    {

                        _matrixImageMap2Pano = Accord.Imaging.Tools.Homography(sourcePoints.ToArray(), destinationPoints.ToArray());

                    }
                }
                return _matrixImageMap2Pano;
            }
        }
        private MatrixH _matrixImageMap2Pano = null;


        internal Accord.Imaging.MatrixH MatrixPano2ImageMap
        {
            get
            {
                if (_matrixPano2ImageMap == null)
                {


                    List<PointF> sourcePoints = new List<PointF>();
                    List<PointF> destinationPoints = new List<PointF>();


                    for (int i = 0; i < ReferencePointLst.Count; i++)
                    {
                        sourcePoints.Add(new PointF(ReferencePointLst[i].PanoReference.X, ReferencePointLst[i].PanoReference.Y));
                        destinationPoints.Add(new PointF(ReferencePointLst[i].MapReference.X, ReferencePointLst[i].MapReference.Y));
                    }

                    if (sourcePoints.Count >= 4)
                    {

                        _matrixPano2ImageMap = Accord.Imaging.Tools.Homography(sourcePoints.ToArray(), destinationPoints.ToArray());

                    }
                }
                return _matrixPano2ImageMap;
            }
        }
        private MatrixH _matrixPano2ImageMap = null;


        internal PanoImage(XElement PanoImageNode)
        {
            ReferencePointLst = new List<RefrencePoint>();
            Panofilename = PanoImageNode.Attribute("Panofilename").Value;
            Id = PanoImageNode.Attribute("Id").Value;

            var referencePntsLst = PanoImageNode.Element("ReferencePoints").Elements("ReferencePoint").ToList();
            foreach (XElement refPnt in referencePntsLst)
            {
                RefrencePoint pnt = new RefrencePoint(refPnt);
                ReferencePointLst.Add(pnt);
            }


        }

        /// <summary>
        /// transformation function: ImageMap coordinate to Pano coordinate
        /// </summary>
        /// <param name="X">X coordinate on Image Map</param>
        /// <param name="Y">X coordinate on Image Map</param>
        /// <returns>Point on Pano image</returns>
        internal PointF ImageMap2Pano(float X, float Y)
        {
            PointF[] sourcePoints = new PointF[] { new PointF(X, Y) };

            PointF[] destinationPoints = MatrixImageMap2Pano.TransformPoints(sourcePoints);


            if (destinationPoints != null)
            {
                return destinationPoints[0];
            }
            else return PointF.Empty;
        }

        /// <summary>
        /// transformation function:  Pano point coordinate to Image Map coordinate
        /// </summary>
        /// <param name="X">XCoor Geo Coordinate</param>
        /// <param name="Y">YCoor Geo Coordinate</param>
        /// <returns>Pixel coordinate</returns>
        internal PointF Pano2ImageMap(float X, float Y)
        {
            PointF[] sourcePoints = new PointF[] { new PointF(X, Y) };

            PointF[] destinationPoints = MatrixPano2ImageMap.TransformPoints(sourcePoints);


            if (destinationPoints != null)
            {
                return destinationPoints[0];
            }
            else return PointF.Empty;
        }
    }
}
