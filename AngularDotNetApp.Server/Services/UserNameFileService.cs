using AngularDotNetApp.Server.Model;
using System.Text.Json;

namespace AngularDotNetApp.Server.Services;

public class UserNameFileService
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<UserNameFileService> _logger;

    public UserNameFileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task SaveUserNameAsync(UserName username)
    {
        if((username == null)
    || (String.IsNullOrEmpty(username.LastName))
    || (String.IsNullOrEmpty(username.FirstName)))
        {
            throw new ArgumentException("Argument error, null or empty...");
        }

        string usernameFilePath = Path.Combine(_environment.ContentRootPath, "usernames");
        if(!Directory.Exists(usernameFilePath))
        {
            Directory.CreateDirectory(usernameFilePath);
        }
        string filename = $"{username.FirstName}_{username.LastName}_{DateTime.Now:yyyyMMddHHmmss}.json";
        string fullPath = Path.Combine(usernameFilePath, filename);

        //save json file
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonStr = System.Text.Json.JsonSerializer.Serialize(username, options);
            await File.WriteAllTextAsync(fullPath, jsonStr);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new Exception("Write jason file failed...");
        }
    }

}
