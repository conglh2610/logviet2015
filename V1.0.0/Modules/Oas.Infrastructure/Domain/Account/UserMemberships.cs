using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class UserMemberships
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid MembershipPackageId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpiredDate { get; set; }

        [ForeignKey("BusinessId")]
        public User Business { get; set; }

        [ForeignKey("MembershipPackageId")]
        public MembershipPackage MembershipPackage { get; set; }

        public Decimal Price { get; set; }

        public Status Status { get; set; }
    }


}
