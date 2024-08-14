using Domain.Enums;

namespace Domain.DTOs
{
    public class UpdateUserDto
    {
        public string Email { get; set; }
        public Role role { get; set; }
    }
}
