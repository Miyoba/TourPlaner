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
    }
}