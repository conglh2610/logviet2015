using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IEmailTemplateService
    {
        #region EmailTemplate

        IQueryable<EmailTemplate> GetEmailTemplates();

        IQueryable<EmailTemplate> SearchEmailTemplate(EmailTemplateCriteria criteria, ref int totalRecords);

        EmailTemplate GetEmailTemplate(Guid emailtemplatesId);

        OperationStatus AddEmailTemplate(EmailTemplate emailtemplates);

        OperationStatus UpdateEmailTemplate(EmailTemplate emailtemplates);

        OperationStatus DeleteEmailTemplate(Guid emailtemplatesId);

        #endregion


    }
}
