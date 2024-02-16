namespace NewStarterProject.Model
{
    /// <summary>
    /// Access attempt audit record
    /// </summary>
    public class AccessAuditDto
    {
        /// <summary>
        /// Unique Id of the Audit record
        /// </summary>
        public int AccessAuditId { get; set; }

        /// <summary>
        /// The Id of the user
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// The door Id the user tried to access
        /// </summary>
        public int DoorId {get; set; }

        

        /// <summary>
        /// Was access granted?
        /// </summary>
        public bool AccessGranted { get; set; }



        /// <summary>
        /// The audit datetime
        /// </summary>
        public DateTime AuditDateTime { get; set; }


    }
}
