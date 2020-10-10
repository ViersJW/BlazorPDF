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

        public static Student GetStudentInfo()
        {
            Student student = new Student
            {
                StudentId = 3,
                Name = "Jan Nowak",
                Class = 6,
                Roll = 1001,
                Section = "A",
                FatherName = "Roman",
                MotherName = "Maria"
            };

            Mark mark = new Mark
            {
                MardId = 1,
                StudentId = 3,
                Subject = "English",
                Grade = "A"
            };
            student.Marks.Add(mark);

            mark = new Mark
            {
                MardId = 2,
                StudentId = 3,
                Subject = "History",
                Grade = "B-"
            };
            student.Marks.Add(mark);

            return student;
        }
    }
}
