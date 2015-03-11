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
    public class AreaService : IAreaService
    {
        #region fields
        private readonly IRepository<Area> areasRepository;
        #endregion

		#region constructors
        public AreaService(IRepository<Area> areasRepository)
        {
            this.areasRepository = areasRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Area> SearchArea(AreaCriteria criteria, ref int totalRecords)
        {
            var query = areasRepository
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

        public IQueryable<Area> GetAreas()
        {
            return areasRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Area GetArea(Guid areasId)
        {
            return areasRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(areasId));
        }

        public OperationStatus AddArea(Area areas)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                areasRepository.Add(areas);
                areasRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateArea(Area areas)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                areasRepository.Update(areas);
                areasRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteArea(Guid areasId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var areas = areasRepository.Get.SingleOrDefault(t => t.Id.Equals(areasId));
                if (areas != null)
                {
                    areasRepository.Remove(areas);
                    areasRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Area not found";
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
