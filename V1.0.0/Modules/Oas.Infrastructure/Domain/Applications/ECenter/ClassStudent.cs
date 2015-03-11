using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class ClassStudent
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }

        public Guid StudentId { get; set; }

        public ClassStudentStatus Status { get; set; }

        public int Discount { get; set; }

        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public virtual ICollection<StudentFee> StudentFees { get; set; }
        public virtual ICollection<Result> Results { get; set; }

        public StudyResult FinalResult { get; set; }
    }
}
