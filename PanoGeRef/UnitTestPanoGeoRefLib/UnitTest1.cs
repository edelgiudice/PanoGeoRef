using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestPanoGeoRefLib
{
    [TestClass]
    public class UnitTest1
    {
        private string xmlParameterFile = @"c:\users\emilio\documents\visual studio 2012\Projects\PanoGeRef\PanoGeoRefLib\GeoPanoParameters.xml";

        [TestMethod]
        public void ReadGeoPanoXml_file_not_found()
        {
            try
            {
                PanoGeoRefLib.PanoGeoParameters pgp = new PanoGeoRefLib.PanoGeoParameters("none.xml");
            }
            catch (System.IO.FileNotFoundException)
            {
                //Test Passed
                return;
            }
            Assert.Fail("No exception rised");

        }

        [TestMethod]
        public void ReadGeoPanoXml_file_found()
        {
            try
            {

                PanoGeoRefLib.PanoGeoParameters pgp = new PanoGeoRefLib.PanoGeoParameters(xmlParameterFile);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                //Test Passed
                Assert.Fail(ex.ToString());
            }
            return;

        }


        [TestMethod]
        public void GeoPanoMapping_Point2Geo()
        {
            try
            {

                PanoGeoRefLib.PanoGeoParameters pgp = new PanoGeoRefLib.PanoGeoParameters(xmlParameterFile);
                if (pgp.geoPanoLst.Count > 0)
                {
                    int startX=540;
                    int startY=687;
                    PointF coordinate = pgp.geoPanoLst[0].Point2Coordinate(startX, startY);
                    if (coordinate != null)
                    {
                        float X = coordinate.X;
                        float Y= coordinate.Y;

                        PointF returnCoordinate = pgp.geoPanoLst[0].Coordinate2Point(X, Y);

                        Assert.IsTrue(Math.Abs(startX - returnCoordinate.X) < 2);
                        Assert.IsTrue(Math.Abs(startY - returnCoordinate.Y) < 2);


                    }
                }
                
            }
            catch (System.IO.FileNotFoundException ex)
            {
                //Test Passed
                Assert.Fail(ex.ToString());
            }
            return;

        }

        [TestMethod]
        public void GeoPanoMapping_ImageMap2Pano()
        {
            try
            {

                PanoGeoRefLib.PanoGeoParameters pgp = new PanoGeoRefLib.PanoGeoParameters(xmlParameterFile);
                if (pgp.geoPanoLst.Count > 0)
                {
                    int startX = 541;
                    int startY = 334;
                    PointF coordinate = pgp.geoPanoLst[0].MapImage2Pano("0",startX, startY);
                    if (coordinate != null)
                    {
                        float X = coordinate.X;
                        float Y = coordinate.Y;

                        PointF returnCoordinate = pgp.geoPanoLst[0].Pano2MapImage("0",X, Y);

                        Assert.IsTrue(Math.Abs(startX - returnCoordinate.X) < 6);
                        Assert.IsTrue(Math.Abs(startY - returnCoordinate.Y) < 6);


                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                //Test Passed
                Assert.Fail(ex.ToString());
            }
            return;

        }

        [TestMethod]
        public void GeoPanoMapping_Coordinate2Pano()
        {
            try
            {

                PanoGeoRefLib.PanoGeoParameters pgp = new PanoGeoRefLib.PanoGeoParameters(xmlParameterFile);
                if (pgp.geoPanoLst.Count > 0)
                {
                    float startX = 9.059067f;
                    float startY = 39.253398f;
                    PointF coordinate = pgp.geoPanoLst[0].Coordinate2Pano("0", startX, startY);
                    if (coordinate != null)
                    {
                        float X = coordinate.X;
                        float Y = coordinate.Y;

                        PointF returnCoordinate = pgp.geoPanoLst[0].Pano2Coordinate("0", X, Y);

                        Assert.IsTrue(Math.Abs(startX - returnCoordinate.X) < 0.0005);
                        Assert.IsTrue(Math.Abs(startY - returnCoordinate.Y) < 0.0005);


                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                //Test Passed
                Assert.Fail(ex.ToString());
            }
            return;

        }
    }
}
