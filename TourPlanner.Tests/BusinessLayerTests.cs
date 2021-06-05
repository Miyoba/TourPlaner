using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;

namespace TourPlanner.Tests
{
    public class BusinessLayerTests
    {
        [Test]
        public void DataFileManagerTest_ExportGet1TourAnd1Log_ShouldReturnTrue()
        {
            //arrange
            var expectedFileName = @"..\..\..\TestFiles\ExportTest.json";

            var saveDialog = new Mock<ISaveFileDialog>();
            var openDialog = new Mock<IOpenFileDialog>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();

            openDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            openDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();


            IDataFileManager dfm = new DataFileManager(saveDialog.Object,openDialog.Object);

            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123);
            TourLog log = new TourLog(1, 1, "05-12-2021 18:23","Report",123,"11:03",2,"Car",12,"Wolfgang",1,100);

            List<Tour> tourList = new List<Tour>() {tour};
            List<TourLog> logList = new List<TourLog>() {log};

            //act
            bool erg = dfm.ExportData(tourList, logList);

            //assert
            saveDialog.Verify();
            Assert.True(erg);
            Assert.True(File.Exists(expectedFileName));

            //cleanup
            if(File.Exists(expectedFileName))
                File.Delete(expectedFileName);
        }

        [Test]
        public void DataFileManagerTest_ExportCancelled_ShouldReturnFalse()
        {
            //arrange
            var expectedFileName = "";

            var saveDialog = new Mock<ISaveFileDialog>();
            var openDialog = new Mock<IOpenFileDialog>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();

            openDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            openDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();

            IDataFileManager dfm = new DataFileManager(saveDialog.Object,openDialog.Object);

            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123);
            TourLog log = new TourLog(1, 1, "05-12-2021 18:23","Report",123,"11:03",2,"Car",12,"Wolfgang",1,100);

            List<Tour> tourList = new List<Tour>() {tour};
            List<TourLog> logList = new List<TourLog>() {log};

            //act
            bool erg = dfm.ExportData(tourList, logList);

            //assert
            saveDialog.Verify();
            Assert.False(erg);
            Assert.False(File.Exists(expectedFileName));
        }

        [Test]
        public void DataFileManagerTest_ImportCancelled_ShouldReturnNull()
        {
            //arrange
            var expectedFileName = "";

            var saveDialog = new Mock<ISaveFileDialog>();
            var openDialog = new Mock<IOpenFileDialog>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();

            openDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            openDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();


            IDataFileManager dfm = new DataFileManager(saveDialog.Object,openDialog.Object);

            //act
            JsonData erg = dfm.ImportData();

            //assert
            openDialog.Verify();
            Assert.Null(erg);
        }

        [Test]
        public void DataFileManagerTest_ImportJsonWith1Tour1Log_ShouldReturnJsonDataObjectContainingToursAndLogs()
        {
            //arrange
            var expectedFileName = @"..\..\..\TestFiles\ImportTest1Tour1Log.json";

            var saveDialog = new Mock<ISaveFileDialog>();
            var openDialog = new Mock<IOpenFileDialog>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();

            openDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            openDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();


            IDataFileManager dfm = new DataFileManager(saveDialog.Object,openDialog.Object);

            //act
            JsonData erg = dfm.ImportData();

            //assert
            openDialog.Verify();
            Assert.True(erg.Tours.ToList().Count == 1);
            Assert.True(erg.Logs.ToList().Count == 1); ;
        }

        [Test]
        public void DataFileManagerTest_ImportJsonWith4Tours7Logs_ShouldReturnJsonDataObjectContainingToursAndLogs()
        {
            //arrange
            var expectedFileName = @"..\..\..\TestFiles\ImportTest4Tours7Logs.json";

            var saveDialog = new Mock<ISaveFileDialog>();
            var openDialog = new Mock<IOpenFileDialog>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();

            openDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            openDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();


            IDataFileManager dfm = new DataFileManager(saveDialog.Object,openDialog.Object);

            //act
            JsonData erg = dfm.ImportData();

            //assert
            openDialog.Verify();
            Assert.True(erg.Tours.ToList().Count == 4);
            Assert.True(erg.Logs.ToList().Count == 7); ;
        }

        [Test]
        public void DataFileManagerTest_ImportEmptyJsonFile_ShouldReturnEmptyJsonDataObject()
        {
            //arrange
            var expectedFileName = @"..\..\..\TestFiles\ImportTestEmpty.json";

            var saveDialog = new Mock<ISaveFileDialog>();
            var openDialog = new Mock<IOpenFileDialog>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();

            openDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            openDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();


            IDataFileManager dfm = new DataFileManager(saveDialog.Object,openDialog.Object);

            //act
            JsonData erg = dfm.ImportData();

            //assert
            openDialog.Verify();
            Assert.IsEmpty(erg.Tours.ToList());
            Assert.IsEmpty(erg.Logs.ToList());
        }

        [Test]
        public void ReportGeneratorTest__PDFGenerationCancelled_ShouldReturnFalse()
        {
            //arrange
            var expectedFileName = "";

            var saveDialog = new Mock<ISaveFileDialog>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();

            IReportGenerator rg = new ReportGenerator(saveDialog.Object);

            List<Tour> tours = new List<Tour>();
            List<TourLog> logs = new List<TourLog>();

            //act
            bool erg = rg.GeneratePDFReportForTours(tours, logs, true);

            //assert
            saveDialog.Verify();
            Assert.False(erg);
        }

        [Test]
        public void ReportGeneratorTest__PDFGenerationEmptyDataWithValidPath_ShouldReturnTrue()
        {
            //arrange
            var expectedFileName = @"..\..\..\TestFiles\ReportTest.pdf";

            var saveDialog = new Mock<ISaveFileDialog>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();

            IReportGenerator rg = new ReportGenerator(saveDialog.Object);

            List<Tour> tour = new List<Tour>();
            List<TourLog> log = new List<TourLog>();

            //act
            bool erg = rg.GeneratePDFReportForTours(tour, log, true);

            //assert
            saveDialog.Verify();
            Assert.True(erg);
            Assert.True(File.Exists(expectedFileName));

            //cleanup
            if(File.Exists(expectedFileName))
                File.Delete(expectedFileName);
        }

        [Test]
        public void ReportGeneratorTest__PDFGeneration1Tour1LogWithValidPath_ShouldReturnTrue()
        {
            //arrange
            var expectedFileName = @"..\..\..\TestFiles\ReportTest.pdf";

            var saveDialog = new Mock<ISaveFileDialog>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(expectedFileName).Verifiable();

            IReportGenerator rg = new ReportGenerator(saveDialog.Object);

            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123);
            TourLog log = new TourLog(1, 1, "05-12-2021 18:23","Report",123,"11:03",2,"Car",12,"Wolfgang",1,100);

            List<Tour> tourList = new List<Tour>() {tour};
            List<TourLog> logList = new List<TourLog>() {log};

            //act
            bool erg = rg.GeneratePDFReportForTours(tourList, logList, true);

            //assert
            saveDialog.Verify();
            Assert.True(erg);
            Assert.True(File.Exists(expectedFileName));

            //cleanup
            if(File.Exists(expectedFileName))
                File.Delete(expectedFileName);
        }
    }
}