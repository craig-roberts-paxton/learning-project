using Microsoft.EntityFrameworkCore;
using NewStarterProject.NewStarter.Domain.Dtos;
using NewStarterProject.NewStarter.Domain.Interfaces;
using NewStarterProject.NewStarter.Domain.Model;

namespace NewStarterProject.NewStarter.Domain.Services
{
    public class UserService : IUserService
    {

        private readonly IDataStore _context;

        public UserService(IDataStore dbContext)
        {
            _context = dbContext;
        }


        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto> CreateUser(UserDto userDto)
        {

            var newUser = new User
            {
                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PinCode = userDto.PinCode,
                IsActive = true,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            userDto.UserId = newUser.UserId;

            return userDto;

        }



        public async Task<UserDto> UpdateUser(UserDto userDto)
        {
            try
            {
                var userRecord = await _context.Users.SingleAsync(a => a.UserId == userDto.UserId);

                // Update the values

                if (userRecord.FirstName != userDto.FirstName)
                {
                    userRecord.FirstName = userDto.FirstName;
                }

                if (userRecord.LastName != userDto.LastName)
                {
                    userRecord.LastName = userDto.LastName;
                }

                if (userRecord.UserName != userDto.UserName)
                {
                    userRecord.UserName = userDto.UserName;
                }

                if (userRecord.PinCode != userDto.PinCode)
                {
                    userRecord.PinCode = userDto.PinCode;
                }

                await _context.SaveChangesAsync();
                return userDto;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        /// <summary>
        /// Get a User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto> GetUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.UserId == id && a.IsActive);

            return new UserDto
            {
                UserId = user?.UserId ?? -1,
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                PinCode = user.PinCode ?? "",
                UserName = user.UserName ?? ""
            };
        }


        /// <summary>
        /// Returns a collection of Users
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDto>> GetAllUsers()
        {
            return await _context.Users.Where(a => a.IsActive).Select(a => new UserDto
            {
                UserId = a.UserId,
                UserName = a.UserName,
                FirstName = a.FirstName,
                LastName = a.LastName,
                PinCode = a.PinCode
            }).ToListAsync();
        }




        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.SingleAsync(a => a.UserId == id && a.IsActive);
                user.IsActive = false;
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
