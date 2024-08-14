using Domain.DTOs;
using Domain.Entities;

namespace Domain.Services.GameServices
{
    public interface IGameService
    {
        void CreateGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(Game game);
        Game? GetById(Guid id);
        List<Game> GetAll(int? pagina = 1, int? quantidade = 10);
        List<Game> GetByPlatform(string platform, int? pagina = 1, int? quantidade = 10);
        List<Game> GetByYear(int year, int? pagina = 1, int? quantidade = 10);

    }
}
