using Microsoft.AspNetCore.Identity;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPDFExport.Data
{
    public class ExportService
    {
        public MemoryStream CreatePdf(WeatherForecast[] forecasts)
        {
            if (forecasts == null)
            {
                throw new ArgumentNullException("Data cannot be null");
            }

            using PdfDocument pdfDocument = new PdfDocument();

            int paragraphAfterSpacing = 8;
            int cellMargin = 8;

            PdfPage page = pdfDocument.Pages.Add();

            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);

            PdfTextElement title = new PdfTextElement("Weather Forecast", font, PdfBrush);

        }


    }
}
