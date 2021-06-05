using System;
using NUnit.Framework;
using TourPlanner.Models;

namespace TourPlanner.Tests {
    public class ModelTests {
        [Test]
        public void TourHasImageTest_TourHasNullObjectAsPath_ShouldReturnFalse()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123);
            //act
            tour.ImagePath = null;
            //assert
            Assert.False(tour.HasImage());
        }

        [Test]
        public void TourHasImageTest_TourHasEmptyStringAsPath_ShouldReturnFalse()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123);
            //act
            tour.ImagePath = "";
            //assert
            Assert.False(tour.HasImage());
        }

        [Test]
        public void TourHasImageTest_TourHasPathLeadingToNoFile_ShouldReturnFalse()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123);
            //act
            tour.ImagePath = @"C:\";
            //assert
            Assert.False(tour.HasImage());
        }
        [Test]
        public void TourHasImageTest_TourHasImagePath_ShouldReturnTrue()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123,@"..\..\..\..\TourPlanner\Images\Icon\Search_Icon.png");
            //act
            //assert
            Assert.True(tour.HasImage());
        }

        [Test]
        public void TourGetFieldValueTest_TourGetFieldValueNameCaseSensitive_ShouldReturnNameValue()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123,@"..\..\..\..\TourPlanner\Images\Icon\Search_Icon.png");
            //act
            string nameValue = tour.GetFieldValue("Name",true);
            //assert
            Assert.True(nameValue.Equals(tour.Name));
        }

        [Test]
        public void TourGetFieldValueTest_TourGetFieldValueNameLowerCase_ShouldReturnNameValueLowerCase()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123,@"..\..\..\..\TourPlanner\Images\Icon\Search_Icon.png");
            //act
            string nameValue = tour.GetFieldValue("Name",false);
            //assert
            Assert.True(nameValue.Equals(tour.Name.ToLower()));
        }

        [Test]
        public void TourGetFieldValueTest_TourGetFieldValueDistanceCaseSensitive_ShouldReturnDistanceValue()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123,@"..\..\..\..\TourPlanner\Images\Icon\Search_Icon.png");
            //act
            string distanceValue = tour.GetFieldValue("Distance",true);
            //assert
            Assert.True(distanceValue.Equals(tour.Distance.ToString()));
        }

        [Test]
        public void TourGetFieldValueTest_TourGetFieldValueDistanceLowerCase_ShouldReturnDistanceValue()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123,@"..\..\..\..\TourPlanner\Images\Icon\Search_Icon.png");
            //act
            string distanceValue = tour.GetFieldValue("Distance",false);
            //assert
            Assert.True(distanceValue.Equals(tour.Distance.ToString()));
        }

        [Test]
        public void TourGetFieldValueTest_TourGetUnknownField_ShouldReturnEmptyString()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123,@"..\..\..\..\TourPlanner\Images\Icon\Search_Icon.png");
            //act
            string reportValue = tour.GetFieldValue("Report",true);
            //assert
            Assert.True(reportValue.Equals(""));
        }

        [Test]
        public void TourLogGetFieldValueTest_TourLogGetFieldValueReportCaseSensitive_ShouldReturnReportValue()
        {
            //arrange
            TourLog log = new TourLog(1, 1, "05-12-2021 18:23","Report",123,"11:03",2,"Car",12,"Wolfgang",1,100);
            //act
            string reportValue = log.GetFieldValue("Report",true);
            //assert
            Assert.True(reportValue.Equals(log.Report));
        }

        [Test]
        public void TourLogGetFieldValueTest_TourLogGetFieldValueReportLowerCase_ShouldReturnReportValueLowerCase()
        {
            //arrange
            TourLog log = new TourLog(1, 1, "05-12-2021 18:23","Report",123,"11:03",2,"Car",12,"Wolfgang",1,100);
            //act
            string reportValue = log.GetFieldValue("Report",false);
            //assert
            Assert.True(reportValue.Equals(log.Report.ToLower()));
        }

        [Test]
        public void TourLogGetFieldValueTest_TourLogGetFieldValueDistanceCaseSensitive_ShouldReturnDistanceValue()
        {
            //arrange
            TourLog log = new TourLog(1, 1, "05-12-2021 18:23","Report",123,"11:03",2,"Car",12,"Wolfgang",1,100);
            //act
            string distanceValue = log.GetFieldValue("Distance",true);
            //assert
            Assert.True(distanceValue.Equals(log.Distance.ToString()));
        }

        [Test]
        public void TourLogGetFieldValueTest_TourLogGetFieldValueDistanceLowerCase_ShouldReturnDistanceValue()
        {
            //arrange
            TourLog log = new TourLog(1, 1, "05-12-2021 18:23","Report",123,"11:03",2,"Car",12,"Wolfgang",1,100);
            //act
            string distanceValue = log.GetFieldValue("Distance",false);
            //assert
            Assert.True(distanceValue.Equals(log.Distance.ToString()));
        }

        [Test]
        public void TourLogGetFieldValueTest_TourLogGetUnknownField_ShouldReturnExceptionNullReference()
        {
            //arrange
            TourLog log = new TourLog(1, 1, "05-12-2021 18:23","Report",123,"11:03",2,"Car",12,"Wolfgang",1,100);
            //act
            string nameValue = log.GetFieldValue("Name");
            //assert
            Assert.True(nameValue.Equals(""));
        }
    }
}