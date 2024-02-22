namespace NewStarterProject.Dtos
{
    /// <summary>
    /// DTO for AccessControlDoorsToUser record
    /// </summary>
    public class AccessControlDoorsToUserDto
    {

        /// <summary>
        /// Unique Id
        /// </summary>
        public int AccessControlDoorsToUsersId { get; set; }

        /// <summary>
        /// A door
        /// </summary>
        public int DoorId { get; set; }

        /// <summary>
        /// A user
        /// </summary>
        public int UserId { get; set; }
    }
}
