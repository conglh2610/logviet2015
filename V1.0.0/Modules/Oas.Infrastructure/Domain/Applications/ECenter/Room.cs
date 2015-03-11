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
    public class Room
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Description("The area that its belong to")]
        public Guid? AreaId { get; set; }

        [ForeignKey("AreaId")]
        public Area Area { get; set; }
    }
}
