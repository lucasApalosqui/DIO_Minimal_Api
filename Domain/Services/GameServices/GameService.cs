using Domain.Entities;
using Domain.Infra.Contexts;


namespace Domain.Services.GameServices
{
    public class GameService : IGameService
    {
        private readonly DataContext _context;
        public GameService(DataContext context)
        {
            _context = context;
        }

        public void CreateGame(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public void DeleteGame(Game game)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
        }

        public List<Game> GetAll(int? pagina = 1, int? quantidade = 10)
        {
            if(pagina == null)
                pagina = 1;
            if(quantidade == null)
                quantidade = 10;

            var query = _context.Games.AsQueryable();

            query = query.Skip(((int)pagina - 1) * (int)quantidade).Take((int)quantidade);

            return query.ToList();
        }

        public Game? GetById(Guid id)
        {
            return _context.Games.FirstOrDefault(x => x.Id == id);
        }

        public List<Game> GetByPlatform(string platform, int? pagina = 1, int? quantidade = 10)
        {
            if (pagina == null)
                pagina = 1;
            if (quantidade == null)
                quantidade = 10;

            var query = _context.Games.AsQueryable();
            query = query.Where(x => x.Plataform.Contains(platform));

            query = query.Skip(((int)pagina - 1) * (int)quantidade).Take((int)quantidade);

            return query.ToList();
        }

        public List<Game> GetByYear(int year, int? pagina = 1, int? quantidade = 10)
        {
            if (pagina == null)
                pagina = 1;
            if (quantidade == null)
                quantidade = 10;

            var query = _context.Games.AsQueryable();
            query = query.Where(x => x.Ano == year);

            query = query.Skip(((int)pagina - 1) * (int)quantidade).Take((int)quantidade);

            return query.ToList();
        }

        public void UpdateGame(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }
    }
}
