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
    public class RoomService : IRoomService
    {
        #region fields
        private readonly IRepository<Room> roomsRepository;
        #endregion

        #region constructors
        public RoomService(IRepository<Room> roomsRepository)
        {
            this.roomsRepository = roomsRepository;
        }
        #endregion


        #region public methods

        public IQueryable<Room> SearchRoom(RoomCriteria criteria, ref int totalRecords)
        {
            var query = roomsRepository
                       .Get
                       .Include(t => t.Area)
.Where(t => (criteria.Id == null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id))
&& (string.IsNullOrEmpty(criteria.Name) || (t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name)))
&& (criteria.AreaId == null || criteria.AreaId == Guid.Empty || t.AreaId.Equals(criteria.AreaId))
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

            #region sorting
            switch (criteria.SortColumn)
            {
                case "name":
                    query = isAsc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
                    break;
                default: break;
            }
            #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Room> GetRooms()
        {
            return roomsRepository
                        .Get
            #region sorting

            #endregion
.AsQueryable();


        }

        public Room GetRoom(Guid roomsId)
        {
            return roomsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(roomsId));
        }

        public OperationStatus AddRoom(Room rooms)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                roomsRepository.Add(rooms);
                roomsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateRoom(Room rooms)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                roomsRepository.Update(rooms);
                roomsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteRoom(Guid roomsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var rooms = roomsRepository.Get.SingleOrDefault(t => t.Id.Equals(roomsId));
                if (rooms != null)
                {
                    roomsRepository.Remove(rooms);
                    roomsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Room not found";
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
