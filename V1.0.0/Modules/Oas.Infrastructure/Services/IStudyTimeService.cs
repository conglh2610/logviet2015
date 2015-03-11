using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IStudyTimeService
    {
        #region StudyTime

        IQueryable<StudyTime> GetStudyTimes();

        IQueryable<StudyTime> SearchStudyTime(StudyTimeCriteria criteria, ref int totalRecords);

        StudyTime GetStudyTime(Guid studytimesId);

        OperationStatus AddStudyTime(StudyTime studytimes);

        OperationStatus UpdateStudyTime(StudyTime studytimes);

        OperationStatus DeleteStudyTime(Guid studytimesId);

        #endregion


    }
}
