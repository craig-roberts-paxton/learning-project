using NewStarter.Application.Interfaces;
using NewStarter.Domain.Dtos;
using NewStarter.Domain.Model;

namespace NewStarter.Application.Services
{
    public class DoorService : IDoorService
    {
        
        private readonly IDataStore<Door> _doorContext;

        public DoorService(IDataStore<Door> dbContext)
        {
            _doorContext = dbContext;
        }


        #region Create or Update

        /// <summary>
        /// Create or update a door
        /// </summary>
        /// <param name="doorDto"></param>
        /// <returns></returns>
        public async Task<DoorDto> CreateDoor(DoorDto doorDto)
        {
            var door = new Door
            {
                DoorName = doorDto.DoorName,
                IsActive = true
            };

            await _doorContext.Create(door);
            door.DoorName = doorDto.DoorName;
            doorDto.DoorId = door.DoorId;

            return doorDto;

        }


        public async Task<DoorDto> UpdateDoor(DoorDto doorDto)
        {

            try
            {
                //TODO - Decouple this into an injectable service
                var door = await _doorContext.GetById(doorDto.DoorId);

                if (door.DoorName != doorDto.DoorName)
                {
                    door.DoorName = doorDto.DoorName;
                }

                await _doorContext.Update(door);
            }
            catch (Exception e)
            {
                // The door probably wasn't found.
                Console.WriteLine(e);
                throw;
            }

            return doorDto;

        }


        #endregion

        /// <summary>
        /// Get a Door by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DoorDto> GetDoor(int id)
        {
            var door = await _doorContext.GetById(id);

            if (!door.IsActive)
                door = null;

            return new DoorDto()
            {
                DoorId = door?.DoorId ?? -1,
                DoorName = door.DoorName ?? "",
            };
        }


        /// <summary>
        /// Returns a collection of Doors
        /// </summary>
        /// <returns></returns>
        public async Task<List<DoorDto>> GetActiveDoorList()
        {
            return _doorContext.GetAll().Where(a => a.IsActive).Select(a => new DoorDto
            {
                DoorId = a.DoorId,
                DoorName = a.DoorName
            }).ToList();
        }

        /// <summary>
        /// Sets a door to inactive - we can't delete the actual record due to FK constraints on the audit. In real world,
        /// we could decide not to care as much
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteDoor(int id)
        {
            try
            {
                var door = await _doorContext.GetById(id);
                door.IsActive = false;
                await _doorContext.Update(door);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



    }
}
