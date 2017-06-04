using System;
namespace GigHub.Core.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string SessionId { get; set; }
        public DateTime Date { get; set; }
    }
}