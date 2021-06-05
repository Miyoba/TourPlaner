using System;
using System.Collections.Generic;
using TourPlanner.Models;
using IronPdf;

namespace TourPlanner.BusinessLayer {
    public class ReportGenerator :IReportGenerator
    {
        private ISaveFileDialog _saveFileDialog;
        public ReportGenerator(ISaveFileDialog sfd)
        {
            _saveFileDialog = sfd;
        }
        public bool GeneratePDFReportForTours(IEnumerable<Tour> tours, IEnumerable<TourLog> logs, bool summarize)
        {
            _saveFileDialog.Filter = "PDF Files (*.pdf) | *.pdf"; //Here you can filter which all files you wanted allow to open  
            _saveFileDialog.ShowDialog();
            if (!_saveFileDialog.FileName.Equals(""))
            {
                var htmlToPdf = new HtmlToPdf();
                var pdf = htmlToPdf.RenderHtmlAsPdf(ConvertDataToHTML(tours, logs, summarize));
                pdf.SaveAs(_saveFileDialog.FileName);
                return true;
            }

            return false;
        }

        private string ConvertDataToHTML(IEnumerable<Tour> tours, IEnumerable<TourLog> logs, bool summarize)
        {
            int totalDistance = 0;
            double ratingTotal = 0;
            int tourCount = 0;
            int logCount = 0;

            string htmlText = @"<html>"+
                "<head>" +
                "<title>TourPlanner Report</title>" +
                "</head>";

            foreach (var tour in tours)
            {
                tourCount++;
                htmlText += @"<body>"+
                            "<h1>Tour: "+tour.Name+"</h1>" +
                            "<b>From: </b> "+tour.FromLocation+"<br>" +
                            "<b>To: </b> "+tour.ToLocation+"<br>" +
                            "<b>Description: </b> "+tour.Description+"<br>" +
                            "<b>Distance: </b> "+tour.Distance+" km <br>" +
                            "<b>Route information: </b><br>"+
                            "<img src='"+tour.ImagePath+"' alt='TourRoute' width='500'>"+
                            "<h2>Logs from Tour: " + tour.Name + "</h2>";
                foreach (var log in logs)
                {
                    if (log.TourId == tour.Id)
                    {
                        logCount++;
                        totalDistance += log.Distance;
                        ratingTotal += log.Rating;
                        htmlText += @"<h3>Date/Time: "+log.DateTime+"</h3>"+
                                    "<b>Report: </b> "+log.Report+"<br>"+ 
                                    "<b>Distance: </b> "+log.Distance+" km<br>"+ 
                                    "<b>Total-time: </b> "+log.TotalTime+" (hh:mm)<br>"+ 
                                    "<b>Rating: </b> "+log.Rating+"<br>"+
                                    "<b>Vehicle: </b> "+log.Vehicle+"<br>"+
                                    "<b>Avg. Speed: </b> "+log.AvgSpeed+" km/h<br>"+
                                    "<b>People: </b> "+log.People+"<br>"+
                                    "<b>Travel breaks: </b> "+log.Breaks+"<br>"+
                                    "<b>Linear Distance: </b> "+log.LinearDistance+" km";
                    }
                }
            }

            if (summarize)
            {
                htmlText += @"<h1>Summarization</h1>"+
                            "<b>Total Tours: </b> "+tourCount+"<br>"+
                            "<b>Total Logs: </b> "+logCount+"<br>"+
                            "<b>Total Distance: </b> "+totalDistance+"<br>"+
                            "<b>Avg Rating: </b> "+Math.Truncate((ratingTotal/logCount)*100)/100+"<br>";
            }

            htmlText += @"</body></html>";
            return htmlText;
        }
    }
}
