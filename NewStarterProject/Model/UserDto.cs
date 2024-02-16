using System.Security.Cryptography.X509Certificates;

namespace NewStarterProject.Model
{

    /// <summary>
    /// A user
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Unique identifier of the User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's surname
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The username to display in the UI
        /// </summary>
        public string DisplayName => $"{FirstName} {LastName}";


        /// <summary>
        /// The user's pin code
        /// </summary>
        public string PinCode { get; set; }

    }
}
