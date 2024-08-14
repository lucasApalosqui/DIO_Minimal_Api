namespace Domain.Entities
{
    public class Game
    {
        public Game(string name, string plataform, int ano)
        {
            Id = Guid.NewGuid();
            Name = name;
            Plataform = plataform;
            Ano = ano;
        }

        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Plataform { get; set; }
        public int Ano { get; set; }
    }
}
