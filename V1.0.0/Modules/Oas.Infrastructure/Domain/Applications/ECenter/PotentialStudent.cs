using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class PotentialStudent
    {
        public Guid Id { get; set; }

        public Guid? StudentId { get; set; }

        public Guid? EmployeeId { get; set; }
        public string Name { get; set; }

        public string Note { get; set; }

        public Gender Gender { get; set; }

        public Guid? ProgramId { get; set; }

        public DateTime RegisterDate { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [Description("The emplyee takes care this employee")]
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [Description("This field is true if this student had been learned at this Ecenter")]
        public bool  HasBeenLearned { get; set; }
    }
}
