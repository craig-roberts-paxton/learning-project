using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NewStarterProject.Dtos;
using NewStarterProject.Model;

namespace NewStarterProject.Services
{
    public class AccessControlService
    {

        private readonly StarterProjectContext _context;

        public AccessControlService(StarterProjectContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Validates whether a provided user / pin combination can access a door
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<bool> ValidateAccess(int doorId, UserDto userDto)
        {
            User user;
            var validAccess = false;

            try
            {
                user =
                    await _context.Users.SingleAsync(a => a.UserName == userDto.UserName && a.PinCode == userDto.PinCode);
            }
            catch (Exception e)
            {
                // If there's no valid user record, they obviously don't have access
                await _context.AccessAudits.AddAsync(new AccessAudit
                {
                    DoorId = doorId,
                    AccessGranted = validAccess
                });

                await _context.SaveChangesAsync();

                return validAccess;
            }

            // Check if there's a record for this door / user combination. If there is, we can grant access
            validAccess = await _context.AccessControlDoorsToUsers.Include(a => a.User)
                                                                  .AnyAsync(a => a.DoorId == doorId && a.UserId == user.UserId);

            // Log the attempt
            await _context.AccessAudits.AddAsync(new AccessAudit
            {
                UserId = user.UserId,
                DoorId = doorId,
                AccessGranted = validAccess
            });

            await _context.SaveChangesAsync();

            return validAccess;

        }

        /// <summary>
        /// Allow a user to access a door
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<AccessControlDoorsToUserDto> AllowAccessToDoor(int doorId, int userId)
        {
            var existingRecord = await
                _context.AccessControlDoorsToUsers.SingleOrDefaultAsync(a => a.DoorId == doorId && a.UserId == userId);

            if (existingRecord != null)
            {
                return new AccessControlDoorsToUserDto
                {
                    AccessControlDoorsToUsersId = existingRecord.AccessControlDoorsToUsersId,
                    DoorId = existingRecord.DoorId,
                    UserId = existingRecord.UserId
                };
            }

            
            var accessRecord = new AccessControlDoorsToUser
            {
                DoorId = doorId,
                UserId = userId
            };

            await _context.AccessControlDoorsToUsers.AddAsync(accessRecord);

            try
            {
                await _context.SaveChangesAsync();

                return new AccessControlDoorsToUserDto
                {
                    AccessControlDoorsToUsersId = accessRecord.AccessControlDoorsToUsersId,
                    DoorId = accessRecord.DoorId,
                    UserId = accessRecord.UserId
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Remove access to a door
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAccessFromDoor(int doorId, int userId)
        {
            var accessRecord =
                await _context.AccessControlDoorsToUsers.SingleOrDefaultAsync(a =>
                    a.DoorId == doorId && a.UserId == userId);

            if (accessRecord != null)
            {
                _context.AccessControlDoorsToUsers.Remove(accessRecord);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }


        /// <summary>
        /// Returns a collection of Users who can access a door
        /// </summary>
        /// <param name="doorId"></param>
        /// <returns></returns>
        public async Task<List<UserDto>> GetUsersAssignedToDoor(int doorId)
        {
            return await _context.AccessControlDoorsToUsers.Include(a => a.User)
                .Where(a => a.DoorId == doorId).Select(a => new UserDto()
            {
                UserId = a.User.UserId,
                UserName = a.User.UserName ?? "",
                FirstName = a.User.FirstName ?? "",
                LastName = a.User.LastName
            }).ToListAsync();
        }


        /// <summary>
        /// Returns a collection of Users who can access a door
        /// </summary>
        /// <param name="doorId"></param>
        /// <returns></returns>
        public async Task<List<DoorDto>> GetDoorsAssignedToUser(int userId)
        {
            return await _context.AccessControlDoorsToUsers.Include(a => a.Door)
                .Where(a => a.UserId == userId).Select(a => new DoorDto()
                {
                    DoorId = a.DoorId,
                    DoorName = a.Door.DoorName
                }).ToListAsync();
        }



        /// <summary>
        /// Returns audit records - can be all records, or for a door or a user or a combination of both
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<AuditRecordForDisplayDto>> GetAccessRecords(int? doorId, int? userId)
        {
            return await _context.AccessAudits
                                 .Include(a => a.Door)
                                 . Include(a => a.User)
                                 .Where(a => (doorId == null || a.DoorId == doorId))
                                 .Where(a => userId == null || a.UserId == userId)
                                 .Select(a => new AuditRecordForDisplayDto()
                                 {
                                    DoorName = a.Door.DoorName,
                                    UserName = a.User.UserName,
                                    AuditDateTime = a.AuditDateTime,
                                    AccessGranted = a.AccessGranted ? "Granted" : "Denied"
                                 }).ToListAsync();
        }

    }
}
