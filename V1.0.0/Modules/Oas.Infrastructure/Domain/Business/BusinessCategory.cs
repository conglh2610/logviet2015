using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class BusinessCategory
    {

        [Key]
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public string GooglePlaceIconUrl { get; set; }

        [ForeignKey("ParentId")]
        public BusinessCategory Parent
        {
            get;
            set;
        }

        public int? CategoryId { get; set; }

        public virtual ICollection<Business> Businesses { get; set; }
        public virtual ICollection<BusinessCategory> BusinessCategories { get; set; }
    }
}
