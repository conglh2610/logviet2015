using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class StudentFeeCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? ClassStudentId {get;set;}


public Guid? TransferedStudentId {get;set;}


public decimal? TotalPay {get;set;}


public decimal? RemainMoney {get;set;}


public DateTime? CreateDate {get;set;}


public int? FeePaymentStatus {get;set;}
    }
}
