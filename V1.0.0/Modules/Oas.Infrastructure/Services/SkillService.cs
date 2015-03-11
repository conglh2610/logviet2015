using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Oas.Infrastructure.Criteria;

namespace Oas.Infrastructure.Services
{
    public class SkillService : ISkillService
    {
        #region fields
        private readonly IRepository<Skill> skillsRepository;
        #endregion

		#region constructors
        public SkillService(IRepository<Skill> skillsRepository)
        {
            this.skillsRepository = skillsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Skill> SearchSkill(SkillCriteria criteria, ref int totalRecords)
        {
            var query = skillsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "name" :
query = isAsc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Skill> GetSkills()
        {
            return skillsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Skill GetSkill(Guid skillsId)
        {
            return skillsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(skillsId));
        }

        public OperationStatus AddSkill(Skill skills)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                skillsRepository.Add(skills);
                skillsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateSkill(Skill skills)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                skillsRepository.Update(skills);
                skillsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteSkill(Guid skillsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var skills = skillsRepository.Get.SingleOrDefault(t => t.Id.Equals(skillsId));
                if (skills != null)
                {
                    skillsRepository.Remove(skills);
                    skillsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Skill not found";
                }
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        #endregion

    }
}
