using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Program
    {
        [Key]
        public Guid Id { get; set; }

        public Guid LanguageId { get; set; }

        public string Name { get; set; }

        public int TotalMonths { get; set; }

        public decimal Price { get; set; }

        public LanguageLevel LanguageLevel { get; set; }

        [ForeignKey("LanguageId")]
        public Language Language { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
