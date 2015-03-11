using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class BusinessLike
    {
        [Key]
        public Guid Id { get; set; }

        public string UserId { get; set; }
        public Guid BusinessId { get; set; }

        [ForeignKey("BusinessId")]
        public Business Business { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public bool Like { get; set; }
        
    }
}
