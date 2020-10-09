using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPDF.Reports
{
    public class PdfFooterPart : PdfPageEventHelper
    {
        private readonly Font pageNumberFont = new Font(Font.NORMAL, 8f, Font.NORMAL, BaseColor.Black);

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            this.AddPageNumber(writer, document);
        }

        public void AddPageNumber(PdfWriter writer, Document document)
        {
            var numberTable = new PdfPTable(1);
            string text = "Page No : " + writer.PageNumber.ToString("00"), 
                text1 = "Generated Time : " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");

            var pdfCell = new PdfPCell(new Phrase(text, pageNumberFont));
            pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfCell.Border = 0;
            pdfCell.BackgroundColor = BaseColor.White;
            numberTable.AddCell(pdfCell);

            pdfCell = new PdfPCell(new Phrase(text1, pageNumberFont));
            pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell.Border = 0;
            pdfCell.BackgroundColor = BaseColor.White;
            numberTable.AddCell(pdfCell);

            numberTable.TotalWidth = 450;
            numberTable.WriteSelectedRows(0, -1, document.Left+80,document.Bottom+10,writer.DirectContent);




        }
    }
}
