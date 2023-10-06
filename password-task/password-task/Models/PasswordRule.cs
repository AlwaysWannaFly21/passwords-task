namespace password_task.Models
{
    public class PasswordRule
    {
        public char Symbol { get; set; }
        public int MinCount { get; set; }
        public int MaxCount { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}