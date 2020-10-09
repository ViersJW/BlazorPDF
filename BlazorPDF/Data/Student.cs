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
    }
}
