using NewStarter.Api.Dtos;
using NewStarter.Api.Interfaces;
using NewStarter.Domain.Model;
using NewStarterProject.NewStarter.Domain.Model;

namespace NewStarter.Application
{
    public class AccessControlService : IAccessControlService
    {

        private readonly IDataStore _context;

        public AccessControlService(IDataStore dbContext)
        {
            _context = dbContext;
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
                    await _context.Users.SingleAsync(a => a.UserName.ToLower() == userDto.UserName.ToLower() && a.PinCode == userDto.PinCode && a.IsActive);
            }
            catch (Exception e)
            {
                // If there's no valid user record, they obviously don't have access
                await _context.AccessAudits.AddAsync(new AccessAudit
                {
                    DoorId = doorId,
                    AccessGranted = validAccess,
                });

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }

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

            // Already exist - just return original record
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
                return new AccessControlDoorsToUserDto
                {
                    AccessControlDoorsToUsersId = -1
                };
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
                    LastName = a.User.LastName,
                    PinCode = "XXXXXXXX"
                }).ToListAsync();
        }


        /// <summary>
        /// Returns a collection of Users who can be added to a door
        /// </summary>
        /// <param name="doorId"></param>
        /// <returns></returns>
        public async Task<List<UserDto>> GetUsersNotAssignedToDoor(int doorId)
        {
            try
            {
                var assignedUserIds = await _context.AccessControlDoorsToUsers.Where(a => a.DoorId == doorId).Select(a => a.UserId).ToListAsync();

                // Shouldn't have to do this in 2 steps,
                // but I think there's a SQL setting I'm missing or something.
                // But for the purposes of demo code, I think this is okay as there won't be hundreds of users
                var users = await _context.Users.Select(a => new UserDto()
                {
                    UserId = a.UserId,
                    UserName = a.UserName ?? "",
                    FirstName = a.FirstName ?? "",
                    LastName = a.LastName
                }).ToListAsync();

                return users.Where(a => !assignedUserIds.Contains(a.UserId)).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
        public async Task<List<AuditRecordForDisplayDto>> GetAccessRecords(int doorId, int userId)
        {
            return await _context.AccessAudits
                                 .Include(a => a.Door)
                                 .Include(a => a.User)
                                 .Where(a => doorId == 0 || a.DoorId == doorId)
                                 .Where(a => userId == 0 || a.UserId == userId)
                                 .Select(a => new AuditRecordForDisplayDto()
                                 {
                                     DoorName = a.Door.DoorName,
                                     UserName = a.UserId == null ? "Unknown" : $"{a.User.FirstName} {a.User.LastName}",
                                     AuditDateTime = a.AuditDateTime,
                                     AccessGranted = a.AccessGranted ? "Granted" : "Denied"
                                 }).OrderByDescending(a => a.AuditDateTime).ToListAsync();
        }

    }
}
