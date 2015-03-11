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
    public class LanguageService : ILanguageService
    {
        #region fields
        private readonly IRepository<Language> languagesRepository;
        #endregion

		#region constructors
        public LanguageService(IRepository<Language> languagesRepository)
        {
            this.languagesRepository = languagesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Language> SearchLanguage(LanguageCriteria criteria, ref int totalRecords)
        {
            var query = languagesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(string.IsNullOrEmpty(criteria.Description)|| ( t.Description.Contains(criteria.Description) || criteria.Description.Contains(t.Description) ))
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
case "description" :
query = isAsc ? query.OrderBy(t => t.Description) : query.OrderByDescending(t => t.Description);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Language> GetLanguages()
        {
            return languagesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Language GetLanguage(Guid languagesId)
        {
            return languagesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(languagesId));
        }

        public OperationStatus AddLanguage(Language languages)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                languagesRepository.Add(languages);
                languagesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateLanguage(Language languages)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                languagesRepository.Update(languages);
                languagesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteLanguage(Guid languagesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var languages = languagesRepository.Get.SingleOrDefault(t => t.Id.Equals(languagesId));
                if (languages != null)
                {
                    languagesRepository.Remove(languages);
                    languagesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Language not found";
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
