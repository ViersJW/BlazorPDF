using BlazorPDF.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPDF.Reports
{
    public class RptResult : PdfFooterPart
    {
        #region Declaration

        PdfWriter _pdfWriter;
        int _maxColumn = 8;
        Document _document;
        PdfPTable _pdfPTable = new PdfPTable(8);
        PdfPCell _pdfPCell;
        Font _fontStyle;
        MemoryStream _memoryStream = new MemoryStream();
        Student _student = new Student();

        #endregion

        public byte[] Report(Student student)
        {
            _student = student;
            _document = new Document(PageSize.A4, 10f, 10f, 20f, 30f);
            _pdfPTable.WidthPercentage = 100;
            _pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);

            _pdfWriter = PdfWriter.GetInstance(_document, _memoryStream);
            _pdfWriter.PageEvent = new PdfFooterPart();

            _document.Open();

            float[] sizes = new float[_maxColumn];

            for (int i = 0; i < _maxColumn; i++)
            {
                if (i == 0)
                {
                    sizes[i] = 50;
                }
                else
                {
                    sizes[i] = 100;
                }
            }

            _pdfPTable.SetWidths(sizes);

            ReportHeader();
            ReportBody();

            _pdfPTable.HeaderRows = 2;
            _document.Add(_pdfPTable);

            this.OnEndPage(_pdfWriter, _document);

            _document.Close();

            return _memoryStream.ToArray();
        }

        private void ReportBody()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            var fontStyle = FontFactory.GetFont("Tahoma", 10f, 0);

            #region Basic Info 1st Row

            _pdfPCell = new PdfPCell(new Phrase("Klient : ", _fontStyle));
            _pdfPCell.Colspan = 4;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase( _student.Name, fontStyle));
            _pdfPCell.Colspan = 4;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);

            //_pdfPCell = new PdfPCell(new Phrase("Class : ", _fontStyle));
            //_pdfPCell.Colspan = 2;
            //_pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //_pdfPCell.Border = 0;
            //_pdfPCell.ExtraParagraphSpace = 0;
            //_pdfPTable.AddCell(_pdfPCell);

            //_pdfPCell = new PdfPCell(new Phrase(_student.Class.ToString(), fontStyle));
            //_pdfPCell.Colspan = 2;
            //_pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //_pdfPCell.Border = 0;
            //_pdfPCell.ExtraParagraphSpace = 0;
            //_pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();

            #endregion

            #region Basic Info 2nd Row

            _pdfPCell = new PdfPCell(new Phrase("Data Początkowa : ", _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_student.Section, fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Data Końcowa : ", _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_student.Roll.ToString(), fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();

            #endregion

            #region Basic Info 3rd Row

            _pdfPCell = new PdfPCell(new Phrase("Father : ", _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_student.FatherName, fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Mother : ", _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_student.MotherName, fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();

            #endregion

            #region Table Header

            _pdfPCell = new PdfPCell(new Phrase("SL", _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LightGray;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Subject", _fontStyle));
            _pdfPCell.Colspan = 4;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LightGray;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Grade", _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LightGray;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();

            #endregion

            #region Mark Table Body

            int nSL = 1;

            foreach (var mark in _student.Marks)
            {
                _pdfPCell = new PdfPCell(new Phrase(nSL++.ToString(), fontStyle));
                _pdfPCell.Colspan = 2;
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.LightGray;
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(mark.Subject, fontStyle));
                _pdfPCell.Colspan = 4;
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.LightGray;
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(mark.Grade, fontStyle));
                _pdfPCell.Colspan = 2;
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.LightGray;
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPTable.CompleteRow();
            }

            #endregion
        }

        private void ReportHeader()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 18f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Rozliczenie Godzinowe", _fontStyle));
            _pdfPCell.Colspan = _maxColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();
        }
    }
}
