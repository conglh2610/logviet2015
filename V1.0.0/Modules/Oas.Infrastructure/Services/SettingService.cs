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
    public class SettingService : ISettingService
    {
        #region fields
        private readonly IRepository<Setting> settingsRepository;
        #endregion

		#region constructors
        public SettingService(IRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Setting> SearchSetting(SettingCriteria criteria, ref int totalRecords)
        {
            var query = settingsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.IsEnableChat==null || t.IsEnableChat.Equals(criteria.IsEnableChat) )
&&(criteria.AllowLocationTracking==null || t.AllowLocationTracking.Equals(criteria.AllowLocationTracking) )
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "defaultglng" :
query = isAsc ? query.OrderBy(t => t.DefaultGLng) : query.OrderByDescending(t => t.DefaultGLng);
break;
case "defaultgla" :
query = isAsc ? query.OrderBy(t => t.DefaultGLa) : query.OrderByDescending(t => t.DefaultGLa);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Setting> GetSettings()
        {
            return settingsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Setting GetSetting(Guid settingsId)
        {
            return settingsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(settingsId));
        }

        public OperationStatus AddSetting(Setting settings)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                settingsRepository.Add(settings);
                settingsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateSetting(Setting settings)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                settingsRepository.Update(settings);
                settingsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteSetting(Guid settingsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var settings = settingsRepository.Get.SingleOrDefault(t => t.Id.Equals(settingsId));
                if (settings != null)
                {
                    settingsRepository.Remove(settings);
                    settingsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Setting not found";
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
