using Microsoft.EntityFrameworkCore;
using NewStarterProject.Dtos;
using NewStarterProject.Model;

namespace NewStarterProject.Services
{
    public class UserService
    {
        
        private StarterProjectContext _context;

        public UserService(StarterProjectContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto> CreateOrUpdateUser(UserDto userDto)
        {
            User user;

            // If the user already exists (has an Id), get it from the Db
            if (userDto.UserId > 0)
            {
                user = await _context.Users.SingleAsync(a => a.UserId == userDto.UserId);
            }
            else
            {
                user = new User();
                _context.Users.Add(user);
            }

            // Update the values - obviously these will be empty for a new user

            if (user.FirstName != userDto.FirstName)
            {
                user.FirstName = userDto.FirstName;
            }

            if (user.LastName != userDto.LastName)
            {
                user.LastName = userDto.LastName;
            }

            if (user.UserName != userDto.UserName)
            {
                user.UserName = userDto.UserName;
            }

            if (user.PinCode != userDto.PinCode)
            {
                user.PinCode = userDto.PinCode;
            }

            await _context.SaveChangesAsync();
            userDto.UserId = userDto.UserId;

            return userDto;

        }

        /// <summary>
        /// Get a User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto> Get(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.UserId == id);

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
            return await _context.Users.Select(a => new UserDto
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
                var user = await _context.Users.SingleAsync(a => a.UserId == id);
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
