using System.Text.Json;
using BlazorBookGroup.Data;

namespace BlazorBookGroup.Services
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
        public IEnumerable<UserDataModel> GetUsers()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<UserDataModel[]>(
                    jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            }
        }
        public UserDataModel GetUserById(int id)
        {
            IEnumerable<UserDataModel> users = GetUsers();
            return users.First(users => users.Id == id);
        }
        public void AddUser(int userId, string firstName, string lastName)
        {
            IEnumerable<UserDataModel> users = GetUsers();

        }
        public int GetNewId()
        {
            IEnumerable<UserDataModel> users = GetUsers();
            int maxId = 0;
            foreach (UserDataModel user in users)
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

