using System.IO;
using System.Text.Json;

namespace BlazorBookGroup.Data
{
    public class PostListService
    {
        public PostListService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "posts.json"); }
        }
        public IEnumerable<Post> GetPosts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Post[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            }
        }
        public void AddPost(int userId, string quote)
        {
            IEnumerable<Post> posts = GetPosts();
            //Post newPost = new Post(userId, quote);
            //posts.Add(newPost);
        }
        public int GetNewId()
        {
            IEnumerable<Post> posts = GetPosts();
            int maxId = 0;
            foreach (Post post in posts)
            {
                if (post.Id > maxId)
                {
                    maxId = post.Id;
                }
            }
            return maxId + 1;
        }
    }
}
