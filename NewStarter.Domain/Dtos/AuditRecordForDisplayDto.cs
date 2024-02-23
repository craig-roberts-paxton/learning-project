namespace NewStarter.Domain.Dtos
{
    public class AuditRecordForDisplayDto
    {
        /// <summary>
        /// The Door name
        /// </summary>
        public string DoorName { get; set; }

        /// <summary>
        /// The user who tried to access the door
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// When?
        /// </summary>
        public DateTime AuditDateTime { get; set; }

        /// <summary>
        /// Did they get in?
        /// </summary>
        public string AccessGranted { get; set; }
    }
}
