using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class ClassTeacherTracking
    {
        [Key]
        public Guid Id { get; set; }

        public Guid SchedulerId { get; set; }

        public Guid TeacherId { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        [ForeignKey("SchedulerId")]
        public Scheduler Scheduler { get; set; }
    }
}
