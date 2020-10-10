using BlazorPDF.Reports;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPDF.Data
{
    public class Student
    {
        public int StudentId { get; set; } = 0;
        public string Name { get; set; } = "";
        public int Class { get; set; } = 0;
        public int Roll { get; set; } = 0;
        public string Section { get; set; } = "";
        public string FatherName { get; set; } = "";
        public string MotherName { get; set; } = "";
        public List<Mark> Marks { get; set; } = new List<Mark>();

        public void GeneratePDF(IJSRuntime jSRuntime)
        {
            Student student = this.GetStudentInfo();
            RptResult rptResult = new RptResult();

            jSRuntime.InvokeAsync<Student>(
                    "saveAsFile",
                    "StudentResult.pdf",
                    Convert.ToBase64String(rptResult.Report(student))
                );
        }

        private Student GetStudentInfo()
        {
            Student student = new Student();
            student.StudentId = 3;
            student.Name = "Jan Nowak";
            student.Class = 6;
            student.Roll = 1001;
            student.Section = "A";
            student.FatherName = "Roman";
            student.MotherName = "Maria";

            Mark mark = new Mark();
            mark.MardId = 1;
            mark.StudentId = 3;
            mark.Subject = "English";
            mark.Grade = "A";
            student.Marks.Add(mark);

            mark = new Mark();
            mark.MardId = 2;
            mark.StudentId = 3;
            mark.Subject = "History";
            mark.Grade = "B-";
            student.Marks.Add(mark);

            return student;
        }
    }
}
