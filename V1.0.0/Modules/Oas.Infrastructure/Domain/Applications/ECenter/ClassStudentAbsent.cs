using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class ClassStudentAbsent
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }

        public Guid StudentId { get; set; }

        public bool IsAbsent { get; set; }

        public bool IsHasReason { get; set; }

        public DateTime CreateDate { get; set; }

        public string Note { get; set; }

        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}
