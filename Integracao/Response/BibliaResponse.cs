namespace Biblia.Integracao.Response
{
    public class BibliaResponse
    {
        public string? Id { get; set; }
        public string? BookId { get; set; }
        public string? CapituloId { get; set; }
        public string? BibliaId { get; set; }
        public string? Referencia { get; set; }
        public string? Conteudo { get; set; }
        public string? Copyright { get; set; }
        public class Proximo
        {
            public string? Id { get; set; }
            public string? Numero { get; set; }
        }
        public class Anterior
        {
            public string? Id { get; set; }
            public string? Numero { get; set; }
        }
        
    }
}
