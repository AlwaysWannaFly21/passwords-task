using password_task.Interfaces;
using System.Text.RegularExpressions;

namespace password_task.Services;
public class FileService : IFileService
{
    private readonly Regex _regex = new(@"(\w) (\d+)-(\d+): (.+)", RegexOptions.Compiled);

    public int GetValidPasswordsCount(IFormFile file)
    {
        var validPasswordsCount = 0;

        using var reader = new StreamReader(file.OpenReadStream());

        while (reader.ReadLine() is { } line)
        {
            var match = _regex.Match(line);

            if (!match.Success)
                continue;

            var symbol = match.Groups[1].Value[0];
            var minCount = int.Parse(match.Groups[2].Value);
            var maxCount = int.Parse(match.Groups[3].Value);
            var password = match.Groups[4].Value;

            var symbolCount = password.Count(c => c.Equals(symbol));
            if (symbolCount >= minCount && symbolCount <= maxCount)
            {
                validPasswordsCount++;
            }
        }
        return validPasswordsCount;
    }
}