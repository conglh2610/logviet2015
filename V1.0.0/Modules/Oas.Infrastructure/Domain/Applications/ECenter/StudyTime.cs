using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class StudyTime
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int StartHour { get; set; }

        public int EndHour { get; set; }
        
    }
}
