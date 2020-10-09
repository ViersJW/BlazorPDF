using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPDF.Data
{
    public class Mark
    {
        public int MardId { get; set; } = 0;
        public int StudentId { get; set; } = 0;
        public string Subject { get; set; } = "";
        public string Grade { get; set; } = "";
    }
}
