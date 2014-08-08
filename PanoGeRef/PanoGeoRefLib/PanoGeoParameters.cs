using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PanoGeoRefLib
{
    public class PanoGeoParameters
    {
        private string _xmlFileName;
        private XElement _xelements;
        private IEnumerable<XElement> geoPanoDatas;

        public List<GeoPanoData> geoPanoLst
        {
            get
            {
                return _geoPanoLst;
            }
        }
        private List<GeoPanoData> _geoPanoLst;

        public PanoGeoParameters(string xmlFileName)
        {
            _geoPanoLst = new List<GeoPanoData>();
            if (File.Exists(xmlFileName))
            {
                _xmlFileName = xmlFileName;
                _xelements = XElement.Load(xmlFileName);
                geoPanoDatas = _xelements.Elements("GeoPanoData");

                foreach (XElement geoDataEmnt in geoPanoDatas)
                {
                    GeoPanoData gpObj = ReadGeoPanoData(geoDataEmnt);
                    _geoPanoLst.Add(gpObj);
                }
            }
            else
            {
                throw new System.IO.FileNotFoundException("Attention file missed", xmlFileName);
            }
        }

        private GeoPanoData ReadGeoPanoData(XElement geoDataEmnt)
        {
            GeoPanoData gpObj = new GeoPanoData();
            gpObj.Id = geoDataEmnt.Attribute("id").Value;
            gpObj.MapCoordinate = ReadMapCoordinate(geoDataEmnt);
            gpObj.PanoImgLst = ReadPanoImgLst(geoDataEmnt);
            
            return gpObj;

        }

        private List<PanoImage> ReadPanoImgLst(XElement geoDataEmnt)
        {
            List<PanoImage> returnLst = new List<PanoImage>();
            List<XElement> PanoImages = geoDataEmnt.Element("PanoImages").Elements("PanoImage").ToList();
            foreach (var pi in PanoImages)
            {
                PanoImage PanoImageObj = new PanoImage(pi);
                returnLst.Add(PanoImageObj);
            }
            return returnLst;
        }

        private MapCoordinate ReadMapCoordinate(XElement geoDataEmnt)
        {
            MapCoordinate mc = new MapCoordinate();
            XElement mpElement=geoDataEmnt.Element("MapCoordinate");
            mc.MapFileName = mpElement.Attribute("Mapfilename").Value;
            mc.MapPointsLst = new List<MapPoint>();
            foreach (XElement mpoint in mpElement.Element("MapPoints").Elements("MapPoint"))
            {
                MapPoint mp=ReadMapPoint(mpoint);
                if (mp != null)
                {
                    mc.MapPointsLst.Add(mp);
                }
            }
            return mc;
        }

        private MapPoint ReadMapPoint(XElement mapPoint)
        {
            MapPoint mp = new MapPoint();

            mp.PointName = mapPoint.Attribute("id").Value;
            mp.Lat = float.Parse(mapPoint.Element("Lat").Value);
            mp.Lon = float.Parse(mapPoint.Element("Lon").Value);
            mp.X = float.Parse(mapPoint.Element("X").Value);
            mp.Y = float.Parse(mapPoint.Element("Y").Value);


            return mp;
        }
    }
}
