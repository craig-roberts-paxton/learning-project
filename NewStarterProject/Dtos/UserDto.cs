namespace NewStarterProject.Dtos
{
    public class UserDto
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Users surname
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// Users firstname
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Users pin code
        /// </summary>
        public string PinCode {get; set; }

    }
}
