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
    public class Registration : Person
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ProgramId { get; set; }

        public Guid CreatedEmployeeId { get; set; }

        public Guid ResolvedEmployeeId { get; set; }

        public DateTime RegisterDate { get; set; }

        public RegisterStatus Status { get; set; }

        public string Note { get; set; }

        [ForeignKey("CreatedEmployeeId")]
        public Employee CreatedEmployee { get; set; }

        [ForeignKey("ResolvedEmployeeId")]
        public Employee ResolvedEmployee { get; set; }

        [ForeignKey("ProgramId")]
        public Program Program { get; set; }



    }
}
