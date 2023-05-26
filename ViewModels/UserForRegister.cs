using System;
namespace WajedApi.ViewModels
{
    public class UserForRegister
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? ProfileImage { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public DateTime? Birth { get; set; }
    }
}

