using System.IO;
using System.Text.Json;
using BlazorBookGroup.Data;

namespace BlazorBookGroup.Services
{
    public class PostListService
    {
        private readonly UserService userService;

        public PostListService(IWebHostEnvironment webHostEnvironment, UserService userService)
        {
            WebHostEnvironment = webHostEnvironment;
            this.userService = userService;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "posts.json"); }
        }

        private ICollection<PostDataModel> GetData()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                var posts = JsonSerializer.Deserialize<PostDataModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return posts;
            }
        }

        public IEnumerable<Post> GetPosts()
        {
            var posts = GetData();
            var users = userService.GetUsers();

            return posts.Select(x => new Post(x, users.FirstOrDefault(y => y.Id == x.CreatorUserId)));

        }
        public void AddPost(Post post)
        {
            List<Post> posts = GetPosts().ToList();
            posts.Add(post);
            using(var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Post>>(
                        new Utf8JsonWriter(outputStream, new JsonWriterOptions
                        {
                            SkipValidation = true,
                            Indented = true
                        }),
                        posts
                    );
            }
        }
        public int GetNewId()
        {
            IEnumerable<PostDataModel> posts = GetData();
            int maxId = 0;
            foreach (PostDataModel post in posts)
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
