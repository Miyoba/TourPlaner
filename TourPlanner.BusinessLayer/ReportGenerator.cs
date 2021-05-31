using TourPlanner.Models;
using IronPdf;
using Microsoft.Win32;

namespace TourPlanner.BusinessLayer {
    public class ReportGenerator
    {
        public void GeneratePDFReportForTours(JsonData data)
        {

            SaveFileDialog sfdlg = new SaveFileDialog();  
            sfdlg.Filter = "PDF Files (*.pdf) | *.pdf"; //Here you can filter which all files you wanted allow to open  
            sfdlg.ShowDialog();
            if (!sfdlg.FileName.Equals(""))
            {
                var htmlToPdf = new HtmlToPdf();
                var pdf = htmlToPdf.RenderHtmlAsPdf(ConvertDataToHTML(data));
                pdf.SaveAs(sfdlg.FileName);
            }
        }

        private string ConvertDataToHTML(JsonData data)
        {
            string htmlText = @"<html>"+
                "<head>" +
                "<title>TourPlanner Report</title>" +
                "</head>";

            foreach (var tour in data.Tours)
            {
                htmlText += @"<body>"+
                            "<h1>Tour: "+tour.Name+"</h1>" +
                            "<b>From: </b> "+tour.FromLocation+"<br>" +
                            "<b>To: </b> "+tour.ToLocation+"<br>" +
                            "<b>Description: </b> "+tour.Description+"<br>" +
                            "<b>Distance: </b> "+tour.Distance+" km <br>" +
                            "<b>Route information: </b><br>"+
                            "<img src='"+tour.ImagePath+"' alt='TourRoute' width='500'>"+
                            "<h2>Logs from Tour: " + tour.Name + "</h2>";
                foreach (var log in data.Logs)
                {
                    if (log.TourId == tour.Id)
                    {
                        htmlText += @"<h3>Date/Time: "+log.DateTime+"</h3>"+
                                    "<b>Report: </b> "+log.Report+"<br>"+ 
                                    "<b>Distance: </b> "+log.Distance+" km<br>"+ 
                                    "<b>Total-time: </b> "+log.TotalTime+" (hh:mm)<br>"+ 
                                    "<b>Rating: </b> "+log.Rating;
                    }
                }
            }

            htmlText += @"</body></html>";
            return htmlText;
        }
    }
}
