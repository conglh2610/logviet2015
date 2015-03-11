using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IRoomService
    {
        #region Room

        IQueryable<Room> GetRooms();

        IQueryable<Room> SearchRoom(RoomCriteria criteria, ref int totalRecords);

        Room GetRoom(Guid roomsId);

        OperationStatus AddRoom(Room rooms);

        OperationStatus UpdateRoom(Room rooms);

        OperationStatus DeleteRoom(Guid roomsId);

        #endregion


    }
}
