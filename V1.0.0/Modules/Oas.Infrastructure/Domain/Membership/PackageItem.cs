using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class PackageItem
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        
        public Guid MembershipPackageId { get; set; }
        [ForeignKey("MembershipPackageId")]
        public MembershipPackage MembershipPackage { get; set; } 
    }
}
