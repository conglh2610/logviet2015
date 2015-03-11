using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Result
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClassStudentId { get; set; }

        public Guid ClassId { get; set; }

        public Guid SkillId { get; set; }

        public decimal Score { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("ClassStudentId")]
        public ClassStudent ClassStudent { get; set; }
    }
}
