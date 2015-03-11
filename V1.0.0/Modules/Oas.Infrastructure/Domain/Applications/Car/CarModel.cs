using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class CarModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CarCategoryId { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        [ForeignKey("CarCategoryId")]
        public virtual CarCategory CarCategory { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}

