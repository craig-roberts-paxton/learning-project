using NewStarter.Application.Interfaces;
using NewStarter.Domain.Dtos;
using NewStarter.Domain.Model;

namespace NewStarter.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IDataStore<User> _userContext;

        public UserService(IDataStore<User> userContext)
        {
            _userContext = userContext;
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

            await _userContext.Create(newUser);
            userDto.UserId = newUser.UserId;

            return userDto;

        }



        public async Task<UserDto> UpdateUser(UserDto userDto)
        {
            try
            {
                var userRecord = await _userContext.GetById(userDto.UserId);

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

                await _userContext.Update(userRecord);

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
            var user = await _userContext.GetById(id);

            if (!user.IsActive)
                user = null;

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
            return _userContext.GetAll().Where(a => a.IsActive).Select(a => new UserDto
            {
                UserId = a.UserId,
                UserName = a.UserName,
                FirstName = a.FirstName,
                LastName = a.LastName,
                PinCode = a.PinCode
            }).ToList();
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
                var user = await _userContext.GetById(id);

                if (user == null) return true;
                
                user.IsActive = false;
                await _userContext.Update(user);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
