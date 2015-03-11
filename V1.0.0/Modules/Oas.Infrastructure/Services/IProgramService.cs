using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IProgramService
    {
        #region Program

        IQueryable<Program> GetPrograms();

        IQueryable<Program> SearchProgram(ProgramCriteria criteria, ref int totalRecords);

        Program GetProgram(Guid programsId);

        OperationStatus AddProgram(Program programs);

        OperationStatus UpdateProgram(Program programs);

        OperationStatus DeleteProgram(Guid programsId);

        #endregion


    }
}
