using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Oas.Infrastructure.Domain;

namespace Oas.Infrastructure.Domain
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        [DefaultValue("/Upload/no-img.jpg")]
        public string ProfileImage { get; set; }

        [DefaultValue(AccountType.User)]
        public AccountType AccountType { get; set; }

        public bool Suspend { get; set; }

        public string Tips { get; set; }
        [DefaultValue(Gender.Male)]
        public Gender Gender { get; set; }

        public string ContactTitle { get; set; }

        public Guid? MembershipPackageId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        [DefaultValue(Status.Pending)]
        public Status Status { get; set; }
        public Decimal? PackagePrice { get; set; }
        public virtual ICollection<Business> Businesses { get; set; }

        public virtual ICollection<CarCustomer> CarCustomers { get; set; }

        public virtual ICollection<BusinessComment> BusinessComments { get; set; }

        public virtual ICollection<MessageHistory> MessageHistories { get; set; }

        [DefaultValue(PaymentMethod.Paypal)]
        public PaymentMethod PaymentMethod { get; set; }

        [DefaultValue(PaymentPeriod.Monthly)]
        public PaymentPeriod PaymentPeriod { get; set; }

        [ForeignKey("MembershipPackageId")]
        public MembershipPackage MembershipPacage { get; set; }


        [DefaultValue(false)]
        public bool IsOnline { get; set; }

    }
}
