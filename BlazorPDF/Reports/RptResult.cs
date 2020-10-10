using BlazorPDF.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
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
        public string FontPath => Path.Combine(_env.WebRootPath, "Fonts");

        #endregion

        private readonly IWebHostEnvironment _env;

        public RptResult(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void GeneratePDF(IJSRuntime jSRuntime)
        {
            Student student = Student.GetStudentInfo();

            jSRuntime.InvokeAsync<Student>(
                    "saveAsFile",
                    "StudentResult.pdf",
                    Convert.ToBase64String(Report(student))
                );
        }


        public byte[] Report(Student student)
        {

            _student = student;
            _document = new Document(PageSize.A4, 10f, 10f, 20f, 30f);
            _pdfPTable.WidthPercentage = 100;
            _pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;

            FontFactory.Register(FontPath);
            _fontStyle = FontFactory.GetFont("Anonymous_Pro", 8f, 1);

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
            _fontStyle = new Font(BaseFont.CreateFont(Path.Combine(FontPath, "Anonymous_Pro.ttf"), BaseFont.CP1250, true));
            var fontStyle = FontFactory.GetFont("Anonymous_Pro", 10f, 0);

            #region Basic Info 1st Row

            _pdfPCell = new PdfPCell(new Phrase("Klient śćżźŚĆŹ --- : ", _fontStyle))
            {
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0,
                ExtraParagraphSpace = 0
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_student.Name, fontStyle))
            {
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                ExtraParagraphSpace = 0
            };
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

            _pdfPCell = new PdfPCell(new Phrase("Data Początkowa : ", _fontStyle))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0,
                ExtraParagraphSpace = 0
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_student.Section, fontStyle))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                ExtraParagraphSpace = 0
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Data Końcowa : ", _fontStyle))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0,
                ExtraParagraphSpace = 0
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_student.Roll.ToString(), fontStyle))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                ExtraParagraphSpace = 0
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();

            #endregion

            #region Basic Info 3rd Row

            _pdfPCell = new PdfPCell(new Phrase("Father : ", _fontStyle))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0,
                ExtraParagraphSpace = 0
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_student.FatherName, fontStyle))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                ExtraParagraphSpace = 0
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Mother : ", _fontStyle))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0,
                ExtraParagraphSpace = 0
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_student.MotherName, fontStyle))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                ExtraParagraphSpace = 0
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();

            #endregion

            #region Table Header

            _pdfPCell = new PdfPCell(new Phrase("SL", _fontStyle))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.LightGray
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Subject", _fontStyle))
            {
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.LightGray
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Grade", _fontStyle))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.LightGray
            };
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();

            #endregion

            #region Mark Table Body

            int nSL = 1;

            foreach (var mark in _student.Marks)
            {
                _pdfPCell = new PdfPCell(new Phrase(nSL++.ToString(), fontStyle))
                {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.LightGray
                };
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(mark.Subject, fontStyle))
                {
                    Colspan = 4,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.LightGray
                };
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(mark.Grade, fontStyle))
                {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.LightGray
                };
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPTable.CompleteRow();
            }

            #endregion
        }

        private void ReportHeader()
        {
            _fontStyle = FontFactory.GetFont("Anonymous_Pro", 18f, 1);

            _pdfPCell = new PdfPCell(new Phrase("Rozliczenie Godzinowe", _fontStyle))
            {
                Colspan = _maxColumn,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                ExtraParagraphSpace = 0
            };

            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();
        }
    }
}
