namespace DDDApi.Entities
{
    // Entity representing a user
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }

    // Value object representing a new password
    public class NewPassword
    {
        public string Value { get; }

        public NewPassword(string value)
        {
            // Validate the password here
            Value = value;
        }
    }

}
