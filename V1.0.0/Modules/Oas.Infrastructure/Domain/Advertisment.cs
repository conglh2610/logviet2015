using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
   public  class Advertisment
    {
       [Key]
       public Guid Id { get; set; }

       public string CustomerName { get; set; }

       public bool IsActive { get; set; }
       public string ImageUrl { get; set; }
       public string Url { get; set; }
       public string Description { get; set; }
       public decimal ClickCost { get; set; }

       // We will define the position as a interger to determine where will this adv be displayed.
       public int Position { get; set; }

       public long TotalClicked { get; set; }
       
    }
}
