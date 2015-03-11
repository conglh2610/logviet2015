using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class CarAccidentNote
    {

        public Guid Id { get; set; }

        public Guid CarAccident { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
