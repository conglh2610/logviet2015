using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class BusinessComment
    {
        [Key]
        public Guid Id { get; set; }

        public string UserId { get; set; }
        public Guid BusinessId { get; set; }

        [ForeignKey("BusinessId")]
        public Business Business { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }

        public BusinessRate BusinessRate { get; set; }

        public string Comment { get; set; }

        public string CreateDate { get; set; }
    }

    public enum BusinessRate
    {
        None = 0,
        OneStar = 1,
        TwoStars = 2,
        ThreeStars = 3,
        FourStars = 4,
        FiveStars = 5
    }
}
