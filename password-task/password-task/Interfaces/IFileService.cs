namespace password_task.Interfaces
{
    public interface IFileService
    {
        int GetValidPasswordsCount(IFormFile file);
    }
}
