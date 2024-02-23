using NewStarter.Api.Dtos;


namespace NewStarter.Api.Interfaces
{
    public interface IAccessControlService
    {

        Task<bool> ValidateAccess(int doorId, UserDto userDto);
    
        Task<AccessControlDoorsToUserDto> AllowAccessToDoor(int doorId, int userId);

        Task<bool> RemoveAccessFromDoor(int doorId, int userId);

        Task<List<UserDto>> GetUsersAssignedToDoor(int doorId);

        Task<List<UserDto>> GetUsersNotAssignedToDoor(int doorId);

        Task<List<DoorDto>> GetDoorsAssignedToUser(int userId);

        Task<List<AuditRecordForDisplayDto>> GetAccessRecords(int doorId, int userId);



    }
}
