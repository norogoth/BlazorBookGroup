using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;


namespace BlazorBookGroup.Data
{
    public class PostDataModel
    {
        public int Id { get; set; }
        [Required]
        public int CreatorUserId { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "too short")]
        public string? Quote { get; set; }
        public string? QuoteLocation { get; set; }
        public string? Note { get; set; }
        public override string ToString() => JsonSerializer.Serialize<PostDataModel>(this);

        //private readonly UserService? _userService;
        //public Post(UserService userService)
        //{
        //    _userService = userService;
        //}
        //public User GetUser()
        //{
        //    IEnumerable<User> users = _userService.GetUsers();
        //    return users.First(user => user.Id == Id);
        //}

    }
}
