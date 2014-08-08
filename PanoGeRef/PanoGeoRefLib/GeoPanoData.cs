using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PanoGeoRefLib
{
    public class GeoPanoData
    {
        public string Id { get; set; }
        internal MapCoordinate MapCoordinate { get; set; }
        internal List<PanoImage> PanoImgLst { get; set; }

        public PointF Coordinate2Pano(string PanoId,float XCoor, float YCoor)
        {
            if ((MapCoordinate == null)  || PanoImgLst==null)
            {
                return PointF.Empty;
            }
            var panoImg = PanoImgLst.Where(w => w.Id == PanoId).FirstOrDefault();
            if (panoImg == null)
                return PointF.Empty;
            PointF ImageMapCoordinate = MapCoordinate.Coordinate2Point(XCoor, YCoor);
            return panoImg.ImageMap2Pano(ImageMapCoordinate.X, ImageMapCoordinate.Y);
        }
        public PointF Pano2Coordinate(string PanoId, float X, float Y)
        {
            if ((MapCoordinate == null) || PanoImgLst == null)
            {
                return PointF.Empty;
            }
            var panoImg = PanoImgLst.Where(w => w.Id == PanoId).FirstOrDefault();
            if (panoImg == null)
                return PointF.Empty;

            PointF MapimageCoor = panoImg.Pano2ImageMap(X, Y);
            return MapCoordinate.Point2Coordinate(MapimageCoor.X, MapimageCoor.Y);
        }

        public PointF Point2Coordinate(float X, float Y)
        {
            if (MapCoordinate != null)
            {
                return MapCoordinate.Point2Coordinate(X, Y);
            }
            else
                return PointF.Empty;
        }

        public PointF Coordinate2Point(float XCoor, float YCoor)
        {
            if (MapCoordinate != null)
            {
                return MapCoordinate.Coordinate2Point(XCoor, YCoor);
            }
            else
                return PointF.Empty;
        }

        public PointF MapImage2Pano(string PanoId,float X, float Y)
        {
            if (PanoImgLst == null)
                return PointF.Empty;

            var panoImg = PanoImgLst.Where(w => w.Id == PanoId).FirstOrDefault();

            if (panoImg != null)
            {
                return panoImg.ImageMap2Pano(X, Y);
            }
            else
                return PointF.Empty;
        }

        public PointF Pano2MapImage(string PanoId, float X, float Y)
        {
            if (PanoImgLst == null)
                return PointF.Empty;

            var panoImg = PanoImgLst.Where(w => w.Id == PanoId).FirstOrDefault();

            if (panoImg != null)
            {
                return panoImg.Pano2ImageMap(X, Y);
            }
            else
                return PointF.Empty;
        }
    }
}
