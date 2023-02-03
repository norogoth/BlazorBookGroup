using System.Text.Json;

namespace BlazorBookGroup.Data
{
    public class UserService
    {
        public UserService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "users.json"); }
        }
        public IEnumerable<User> GetUsers()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<User[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            }
        }
        public void AddUser(int userId, string firstName, string lastName)
        {
            IEnumerable<User> users = GetUsers();
            
        }
        public int GetNewId()
        {
            IEnumerable<User> users = GetUsers();
            int maxId = 0;
            foreach (User user in users)
            {
                if (user.Id > maxId)
                {
                    maxId = user.Id;
                }
            }
            return maxId + 1;
        }

    }
}

