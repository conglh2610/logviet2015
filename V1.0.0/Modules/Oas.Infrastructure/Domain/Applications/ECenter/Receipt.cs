using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Receipt
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Content { get; set; }

        public decimal Amount { get; set; }

        public int Source { get; set; }

        public Guid TeacherId { get; set; }

        public int ReceiptType { get; set; }
    }
}
