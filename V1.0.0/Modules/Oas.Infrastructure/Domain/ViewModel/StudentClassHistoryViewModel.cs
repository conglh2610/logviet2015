using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure
{
    public class StudentClassHistoryViewModel
    {
        public string LanguageName { get; set; }

        public string ProgramName { get; set; }

        public string ClassName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Status { get; set; }

        public int Result { get; set; }
    }
}
