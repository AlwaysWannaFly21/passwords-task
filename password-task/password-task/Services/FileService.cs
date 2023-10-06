using password_task.Interfaces;
using password_task.Models;
using System.Text.RegularExpressions;

namespace password_task.Services;

public class FileService : IFileService
{
    private readonly Regex _regex = new(@"(\w) (\d+)-(\d+): (.+)");

    public int GetValidPasswordsCount(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());

        var rules = new List<PasswordRule>();

        while (reader.ReadLine() is { } line)
        {
            var match = _regex.Match(line);

            if (!match.Success)
                continue;

            var symbol = match.Groups[1].Value[0];
            var minCount = int.Parse(match.Groups[2].Value);
            var maxCount = int.Parse(match.Groups[3].Value);
            var password = match.Groups[4].Value;

            var rule = new PasswordRule
            {
                Symbol = symbol,
                MinCount = minCount,
                MaxCount = maxCount,
                Password = password
            };

            rules.Add(rule);
        }

        return CountValidPasswords(rules);
    }

    private static int CountValidPasswords(IEnumerable<PasswordRule> rules)
    {
        var validPasswordsCount = 0;
        foreach (var rule in rules)
        {
            var symbolCount = rule.Password.Count(c => c == rule.Symbol);
            if (symbolCount >= rule.MinCount && symbolCount <= rule.MaxCount)
            {
                validPasswordsCount++;
            }
        }
        return validPasswordsCount;
    }
}