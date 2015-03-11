using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CarModelId { get; set; }

        public string Name { get; set; }

        public string Year { get; set; }
        public string Description { get; set; }

        public bool IsMT { get; set; }

        public bool IsAT { get; set; }

        public int TotalOfSeating { get; set; }

        [ForeignKey("CarModelId")]
        public CarModel CarModel { get; set; }
        
        public virtual ICollection<Image> Images { get; set; }
    }
}
