﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;


namespace BlazorBookGroup.Data
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public int CreatorUserId { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Please give me something longer than that UwU.")]
        public string Quote { get; set; }
        public string? QuoteLocation { get; set; }
        public string? Note { get; set; }
        public override string ToString() => JsonSerializer.Serialize<Post>(this);
        public User GetUser()
        {
            IWebHostEnvironment env;
            UserService userService = new();
            IEnumerable<User> users = userService.GetUsers();
            return users.First(user => user.Id == Id);
        }
    }
}
