namespace DDDApi.Models
{
    // View model representing a user
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

    // View model for resetting a user's password
    public class ResetPasswordViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
