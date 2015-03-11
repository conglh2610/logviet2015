using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class StudentFee
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClassStudentId { get; set; }

        public Guid? TransferedStudentId { get; set; }

        public decimal TotalPay { get; set; }

        public decimal RemainMoney { get; set; }

        public DateTime CreateDate { get; set; }

        public FeePaymentStatus FeePaymentStatus { get; set; }


        [ForeignKey("ClassStudentId")]
        public ClassStudent ClassStudent { get; set; }

        [ForeignKey("TransferedStudentId")]
        public Student StudentTransferred { get; set; }

    }


}
