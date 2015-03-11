using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ILanguageService
    {
        #region Language

        IQueryable<Language> GetLanguages();

        IQueryable<Language> SearchLanguage(LanguageCriteria criteria, ref int totalRecords);

        Language GetLanguage(Guid languagesId);

        OperationStatus AddLanguage(Language languages);

        OperationStatus UpdateLanguage(Language languages);

        OperationStatus DeleteLanguage(Guid languagesId);

        #endregion


    }
}
