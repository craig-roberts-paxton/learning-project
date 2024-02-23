using NewStarter.Api.Dtos;

namespace NewStarter.Api.Interfaces
{
    public interface IDoorService
    {
        /// <summary>
        /// Create a door from a provided Door DTO
        /// </summary>
        /// <param name="door"></param>
        /// <returns></returns>
        Task<DoorDto> CreateDoor(DoorDto door);

        /// <summary>
        /// Updates an existing door from a provided Door DTO
        /// </summary>
        /// <param name="door"></param>
        /// <returns></returns>
        Task<DoorDto> UpdateDoor(DoorDto door);

        /// <summary>
        /// Gets a single door from a provided ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DoorDto> GetDoor(int id);


        /// <summary>
        ///  Gets all active doors as a list
        /// </summary>
        /// <returns></returns>
        Task<List<DoorDto>> GetActiveDoorList();


        /// <summary>
        /// Deletes / Deactivates a door 
        /// </summary>
        /// <param name="doorId"></param>
        /// <returns></returns>
        Task<bool> DeleteDoor(int doorId);
    }
}
