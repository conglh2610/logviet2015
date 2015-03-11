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
    public class EmailTemplateService : IEmailTemplateService
    {
        #region fields
        private readonly IRepository<EmailTemplate> emailtemplatesRepository;
        #endregion

		#region constructors
        public EmailTemplateService(IRepository<EmailTemplate> emailtemplatesRepository)
        {
            this.emailtemplatesRepository = emailtemplatesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<EmailTemplate> SearchEmailTemplate(EmailTemplateCriteria criteria, ref int totalRecords)
        {
            var query = emailtemplatesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(string.IsNullOrEmpty(criteria.Content)|| ( t.Content.Contains(criteria.Content) || criteria.Content.Contains(t.Content) ))
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
case "content" :
query = isAsc ? query.OrderBy(t => t.Content) : query.OrderByDescending(t => t.Content);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<EmailTemplate> GetEmailTemplates()
        {
            return emailtemplatesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public EmailTemplate GetEmailTemplate(Guid emailtemplatesId)
        {
            return emailtemplatesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(emailtemplatesId));
        }

        public OperationStatus AddEmailTemplate(EmailTemplate emailtemplates)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                emailtemplatesRepository.Add(emailtemplates);
                emailtemplatesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateEmailTemplate(EmailTemplate emailtemplates)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                emailtemplatesRepository.Update(emailtemplates);
                emailtemplatesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteEmailTemplate(Guid emailtemplatesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var emailtemplates = emailtemplatesRepository.Get.SingleOrDefault(t => t.Id.Equals(emailtemplatesId));
                if (emailtemplates != null)
                {
                    emailtemplatesRepository.Remove(emailtemplates);
                    emailtemplatesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "EmailTemplate not found";
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
