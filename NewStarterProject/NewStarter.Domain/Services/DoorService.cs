using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using NewStarterProject.NewStarter.Domain.Dtos;
using NewStarterProject.NewStarter.Domain.Interfaces;
using NewStarterProject.NewStarter.Domain.Model;

namespace NewStarterProject.NewStarter.Domain.Services
{
    public class DoorService : IDoorService
    {
        private readonly IDataStore _context;


        public DoorService(IDataStore dbContext)
        {
            _context = dbContext;
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

            _context.Doors.Add(door);

            door.DoorName = doorDto.DoorName;

            await _context.SaveChangesAsync();
            doorDto.DoorId = door.DoorId;

            return doorDto;

        }


        public async Task<DoorDto> UpdateDoor(DoorDto doorDto)
        {

            try
            {
                //TODO - Decouple this into an injectable service
                var door = await _context.Doors.SingleAsync(a => a.DoorId == doorDto.DoorId);

                if (door.DoorName != doorDto.DoorName)
                {
                    door.DoorName = doorDto.DoorName;
                }

                await _context.SaveChangesAsync();
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
        public async Task<List<DoorDto>> GetActiveDoorList()
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
