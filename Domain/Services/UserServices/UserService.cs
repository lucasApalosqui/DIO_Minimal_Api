using Domain.Entities;
using Domain.Enums;
using Domain.Infra.Contexts;

namespace Domain.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }


        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers(int? pagina = 1, int? quantidade = 10)
        {
            if (pagina == null)
                pagina = 1;
            if (quantidade == null)
                quantidade = 10;

            var query = _context.Users.AsQueryable();
            query = query.Skip(((int)pagina - 1) * (int)quantidade).Take((int)quantidade);

            return query.ToList();
        }

        public User? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        public User? GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetUsersByRole(Role role, int? pagina = 1, int? quantidade = 10)
        {
            if (pagina == null)
                pagina = 1;
            if (quantidade == null)
                quantidade = 10;

            var query = _context.Users.AsQueryable();
            query = query.Where(x => x.Role == role);

            query = query.Skip(((int)pagina - 1) * (int)quantidade).Take((int)quantidade);

            return query.ToList();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
