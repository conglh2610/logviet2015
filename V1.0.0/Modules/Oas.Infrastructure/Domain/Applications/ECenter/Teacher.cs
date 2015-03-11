using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Teacher
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string  LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        public decimal SalaryByHour { get; set; }

        public Guid CountryId { get; set; }


    }
}
