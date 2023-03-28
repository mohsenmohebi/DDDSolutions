namespace DDDApi.Dtos
{
    // DTO for resetting a user's password
    public class ResetPasswordDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
