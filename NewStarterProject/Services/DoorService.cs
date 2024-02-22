using Microsoft.EntityFrameworkCore;
using NewStarterProject.Dtos;
using NewStarterProject.Model;

namespace NewStarterProject.Services
{
    public class DoorService
    {
        private StarterProjectContext _context;


        public DoorService(StarterProjectContext context)
        {
            _context = context;
        }


        #region Create or Update

        /// <summary>
        /// Create or update a door
        /// </summary>
        /// <param name="doorDto"></param>
        /// <returns></returns>
        public async Task<DoorDto> CreateOrUpdateDoor(DoorDto doorDto)
        {
            Door door;

            // If there's an ID then it's an existing door
            if (doorDto.DoorId > 0)
            {
                door = await _context.Doors.SingleAsync(a => a.DoorId == doorDto.DoorId);
            }

            // Otherwise create a new one
            else
            {
                door = new Door
                {
                    IsActive = true
                };

                _context.Doors.Add(door);
                
            }

            if (door.DoorName != doorDto.DoorName)
            {
                door.DoorName = doorDto.DoorName;
            }

            await _context.SaveChangesAsync();
            doorDto.DoorId = door.DoorId;

            return doorDto;

        }

        #endregion

        /// <summary>
        /// Get a Door by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DoorDto> Get(int id)
        {
            var door = await _context.Doors.SingleOrDefaultAsync(a => a.DoorId == id && a.IsActive);

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
        public async Task<List<DoorDto>> GetAllDoors()
        {
            return await _context.Doors.Where(a => a.IsActive).Select(a => new DoorDto
            {
                DoorId = a.DoorId,
                DoorName = a.DoorName
            }).ToListAsync();
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
                var door = await _context.Doors.SingleAsync(a => a.DoorId == id && a.IsActive);
                door.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



    }
}
