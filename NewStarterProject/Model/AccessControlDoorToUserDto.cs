namespace NewStarterProject.Model
{
    /// <summary>
    /// Associates a Door with a User that is allowed to access it
    /// </summary>
    public class AccessControlDoorToUserDto
    {

        /// <summary>
        /// Unique Id of the Door to User Access
        /// </summary>
        public int AccessControlAssociationId { get; set; }

        /// <summary>
        /// The Id of the door in this association
        /// </summary>
        public int DoorId { get; set; }


        /// <summary>
        /// The Id of the User in this association
        /// </summary>
        public int UserId { get; set; }

    }
}
