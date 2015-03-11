using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Class
    {
        [Key]
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public Guid? ProgramId { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Size { get; set; }

        public ClassStatus Status { get; set; }

        [ForeignKey("ProgramId")]
        public Program Program { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Assitant { get; set; }

        public virtual ICollection<Scheduler> ClassTimetable { get; set; }

        public virtual ICollection<ClassTeacher> Teachers { get; set; }

        public virtual ICollection<ClassStudent> Students { get; set; }

    }
}
