namespace PraeceptorCQRS.Domain.Entities
{
    public class Variavel
    {
        public Guid Id { get; set; }
        public string NomeDoGrupo { get; set; } = default!;
        public Guid GrupoId { get; set; }
        public string NomeDaVariavel { get; set; } = default!;
        public string? Valor { get; set; }
    }
}
