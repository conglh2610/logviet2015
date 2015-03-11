using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ISettingService
    {
        #region Setting

        IQueryable<Setting> GetSettings();

        IQueryable<Setting> SearchSetting(SettingCriteria criteria, ref int totalRecords);

        Setting GetSetting(Guid settingsId);

        OperationStatus AddSetting(Setting settings);

        OperationStatus UpdateSetting(Setting settings);

        OperationStatus DeleteSetting(Guid settingsId);

        #endregion


    }
}
