namespace Domain.DTOs
{
    public class GetGameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Plataform { get; set; }
        public int Ano {  get; set; }
    }
}
