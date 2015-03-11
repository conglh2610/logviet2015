using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class ClassTeacher
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }

        public Guid TeacherId { get; set; }

        public ClassTeacherStatus Status { get; set; }

        public decimal SalaryByHour { get; set; }

        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
        
    }
}
