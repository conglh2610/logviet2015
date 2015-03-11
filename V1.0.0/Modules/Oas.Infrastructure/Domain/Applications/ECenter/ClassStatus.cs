using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public enum ClassStatus
    {
        OpenForRegistering,
        OnHolding,
        Cancelled,
        Studying,
        Closed
    }

    //public enum Gender
    //{
    //    Male,
    //    FeMale,
    //    Other
    //}

    public enum ParentType
    {
        Mother,
        Father,
        Sister,
        Brother,
        Other
    }

    public enum LanguageLevel
    {
        [Description("Level 0")]
        Level0,
        [Description("Level 1")]
        Level1,
        [Description("Level 2")]
        Level2,
        [Description("Level 3")]
        Level3,
        [Description("Level 4")]
        Level4,
        [Description("Level 5")]
        Level5
    }


    public enum EmployeeLevel
    {
        [Description("Level 0")]
        Level0,
        [Description("Level 1")]
        Level1,
        [Description("Level 2")]
        Level2,
        [Description("Level 3")]
        Level3,
        [Description("Level 4")]
        Level4,
        [Description("Level 5")]
        Level5
    }


    public enum RegisterStatus
    {
        [Description("Waiting for assigning to a new class")]
        Waiting,
        [Description("Cancelled")]
        Cancelled,
        [Description("Resolved")]
        Resolved
    }


    public enum StudyResult
    {
        None,
        Average,
        Good,
        Excellence
    }

    public enum ClassStudentStatus
    {
        Pending,
        OnHolding,
        Studying,
        Finished
    }

    public enum ClassTeacherStatus
    {
        Pending,
        Cancelled,
        Teaching,
        Finished
    }

    public enum PaymentType
    {
        ByCash,
        ATM,
        Visa,
        Paypal,
        Other
    }

    public enum PaymentStatus
    {
        Success,
        Fail
    }

    public enum FeePaymentStatus
    {
        None,
        Returned,
        Moved
    }

}
