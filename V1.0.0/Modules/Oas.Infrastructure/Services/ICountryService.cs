using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ICountryService
    {
        #region Country

        IQueryable<Country> GetCountrys();

        IQueryable<Country> SearchCountry(CountryCriteria criteria, ref int totalRecords);

        Country GetCountry(Guid countriesId);

        OperationStatus AddCountry(Country countries);

        OperationStatus UpdateCountry(Country countries);

        OperationStatus DeleteCountry(Guid countriesId);

        #endregion


    }
}
