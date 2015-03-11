using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Supply
    {
        [Key]
        public Guid Id { get; set; }

        public string SupplyName { get; set; }

        public int NumberImport { get; set; }

        public int NumberExport { get; set; }

        public decimal PriceImport { get; set; }

        public decimal PriceExport { get; set; }
    }
}
