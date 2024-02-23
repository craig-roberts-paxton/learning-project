using NewStarter.Domain.Dtos;

namespace NewStarter.Api.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(UserDto userDto);
        Task<UserDto> UpdateUser(UserDto userDto);
        Task<bool> DeleteUser(int id);
        Task<UserDto> GetUser(int id);
        Task<List<UserDto>> GetAllUsers();
    }
}
