using Domain.Entities;
using Domain.Enums;

namespace Domain.Services.UserServices
{
    public interface IUserService
    {
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        User? GetUserById(Guid id);
        List<User> GetAllUsers(int? pagina = 1, int? quantidade = 10);
        List<User> GetUsersByRole(Role role, int? pagina = 1, int? quantidade = 10);
        User? GetByEmail(string email);
    }
}
