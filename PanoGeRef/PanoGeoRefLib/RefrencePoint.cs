using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PanoGeoRefLib
{
    internal class RefrencePoint
    {
        internal string Id { get; set; }
        internal PointF MapReference { get; set; }
        internal PointF PanoReference { get; set; }

        internal RefrencePoint(XElement ReferencePointNode)
        {
            Id = ReferencePointNode.Attribute("id").Value;
            
            MapReference = new PointF(
                float.Parse(ReferencePointNode.Descendants("MapReference").FirstOrDefault().Descendants("X").FirstOrDefault().Value),
                float.Parse(ReferencePointNode.Descendants("MapReference").FirstOrDefault().Descendants("Y").FirstOrDefault().Value));

            PanoReference = new PointF(
                float.Parse(ReferencePointNode.Descendants("PanoReference").FirstOrDefault().Descendants("X").FirstOrDefault().Value),
                float.Parse(ReferencePointNode.Descendants("PanoReference").FirstOrDefault().Descendants("Y").FirstOrDefault().Value));

        }
    }
}
