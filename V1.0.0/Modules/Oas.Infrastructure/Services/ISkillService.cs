using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ISkillService
    {
        #region Skill

        IQueryable<Skill> GetSkills();

        IQueryable<Skill> SearchSkill(SkillCriteria criteria, ref int totalRecords);

        Skill GetSkill(Guid skillsId);

        OperationStatus AddSkill(Skill skills);

        OperationStatus UpdateSkill(Skill skills);

        OperationStatus DeleteSkill(Guid skillsId);

        #endregion


    }
}
