using BlazorBookGroup.Data;

namespace BlazorBookGroup.Services
{
    public class Post
    {
        public PostDataModel Content { get; }
        public UserDataModel? User { get; }

        public Post(PostDataModel content, UserDataModel? user)
        {
            Content = content;
            User = user;
        }


    }
}
