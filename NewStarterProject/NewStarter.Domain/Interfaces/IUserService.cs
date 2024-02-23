using NewStarterProject.NewStarter.Domain.Dtos;

namespace NewStarterProject.NewStarter.Domain.Interfaces
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
