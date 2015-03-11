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
    public class CountryService : ICountryService
    {
        #region fields
        private readonly IRepository<Country> countriesRepository;
        #endregion

		#region constructors
        public CountryService(IRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Country> SearchCountry(CountryCriteria criteria, ref int totalRecords)
        {
            var query = countriesRepository
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

        public IQueryable<Country> GetCountrys()
        {
            return countriesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Country GetCountry(Guid countriesId)
        {
            return countriesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(countriesId));
        }

        public OperationStatus AddCountry(Country countries)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                countriesRepository.Add(countries);
                countriesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateCountry(Country countries)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                countriesRepository.Update(countries);
                countriesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteCountry(Guid countriesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var countries = countriesRepository.Get.SingleOrDefault(t => t.Id.Equals(countriesId));
                if (countries != null)
                {
                    countriesRepository.Remove(countries);
                    countriesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Country not found";
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
