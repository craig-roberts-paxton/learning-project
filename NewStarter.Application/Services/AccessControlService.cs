using NewStarter.Application.Interfaces;
using NewStarter.Domain.Dtos;
using NewStarter.Domain.Model;


namespace NewStarter.Application
{
    public class AccessControlService : IAccessControlService
    {

        private readonly IDataStore<AccessAudit> _auditContext;
        private readonly IDataStore<User> _userContext;
        private readonly IDataStore<AccessControlDoorsToUser> _accessControlContext;

        public AccessControlService(IDataStore<AccessAudit> accessAuditRepository,
            IDataStore<User> userAuditRepository,
            IDataStore<AccessControlDoorsToUser> accessControlRepository)
        {
            _auditContext = accessAuditRepository;
            _userContext = userAuditRepository;
            _accessControlContext = accessControlRepository;
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
                user = _userContext.GetAll().SingleOrDefault(a =>
                        a.UserName.ToLower() == userDto.UserName.ToLower() && a.PinCode == userDto.PinCode &&
                        a.IsActive);
            }
            catch (Exception e)
            {
                // If there's no valid user record, they obviously don't have access
                await _auditContext.Create(new AccessAudit
                {
                    DoorId = doorId,
                    AccessGranted = validAccess,
                });

                return validAccess;
            }

            // Check if there's a record for this door / user combination. If there is, we can grant access
            validAccess = _accessControlContext.GetAll(a => a.User)
                                                .Any(a => a.DoorId == doorId && a.UserId == user.UserId);

            // Log the attempt
            await _auditContext.Create(new AccessAudit
            {
                UserId = user.UserId,
                DoorId = doorId,
                AccessGranted = validAccess
            });

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
            var existingRecord = _accessControlContext.GetAll().SingleOrDefault(a => a.DoorId == doorId && a.UserId == userId);

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

            await _accessControlContext.Create(accessRecord);

            return new AccessControlDoorsToUserDto
            {
                AccessControlDoorsToUsersId = accessRecord.AccessControlDoorsToUsersId,
                DoorId = accessRecord.DoorId,
                UserId = accessRecord.UserId
            };
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
                 _accessControlContext.GetAll().Single(a =>
                    a.DoorId == doorId && a.UserId == userId);

            _accessControlContext.Delete(accessRecord.AccessControlDoorsToUsersId);

            return true;
        }


        /// <summary>
        /// Returns a collection of Users who can access a door
        /// </summary>
        /// <param name="doorId"></param>
        /// <returns></returns>
        public async Task<List<UserDto>> GetUsersAssignedToDoor(int doorId)
        {
            return _accessControlContext.GetAll(a => a.User)
                .Where(a => a.DoorId == doorId).Select(a => new UserDto()
                {
                    UserId = a.User.UserId,
                    UserName = a.User.UserName ?? "",
                    FirstName = a.User.FirstName ?? "",
                    LastName = a.User.LastName,
                    PinCode = "XXXXXXXX"
                }).ToList();
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
                var assignedUserIds = _accessControlContext.GetAll().Where(a => a.DoorId == doorId).Select(a => a.UserId).ToList();

                // Shouldn't have to do this in 2 steps,
                // but I think there's a SQL setting I'm missing or something.
                // But for the purposes of demo code, I think this is okay as there won't be hundreds of users
                var users = _userContext.GetAll().Select(a => new UserDto()
                {
                    UserId = a.UserId,
                    UserName = a.UserName ?? "",
                    FirstName = a.FirstName ?? "",
                    LastName = a.LastName
                }).ToList();

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
            return _accessControlContext.GetAll(a => a.Door)
                .Where(a => a.UserId == userId).Select(a => new DoorDto()
                {
                    DoorId = a.DoorId,
                    DoorName = a.Door.DoorName
                }).ToList();
        }



        /// <summary>
        /// Returns audit records - can be all records, or for a door or a user or a combination of both
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<AuditRecordForDisplayDto>> GetAccessRecords(int doorId, int userId)
        {
            return _auditContext.GetAll(a => a.Door, a => a.User)

                                 .Where(a => doorId == 0 || a.DoorId == doorId)
                                 .Where(a => userId == 0 || a.UserId == userId)
                                 .Select(a => new AuditRecordForDisplayDto()
                                 {
                                     DoorName = a.Door.DoorName,
                                     AuditDateTime = a.AuditDateTime,
                                     AccessGranted = a.AccessGranted ? "Granted" : "Denied"
                                 }).OrderByDescending(a => a.AuditDateTime).ToList();
        }

    }
}
