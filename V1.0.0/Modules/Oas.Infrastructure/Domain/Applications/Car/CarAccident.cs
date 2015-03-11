using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class CarAccident
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public Guid CarItemId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime AccidentDate { get; set; }

        public decimal Total { get; set; }

        public DateTime ResolveDate { get; set; }

        public decimal Paid { get; set; }

        public AccidentStatus AccidentStatus { get; set; }

        public Guid CarCustomerId { get; set; }

        [ForeignKey("CarCustomerId")]
        public CarCustomer CarCustomer { get; set; }
    }

    public enum AccidentStatus
    {
        Resolved,

        Unresolved
    }
}
