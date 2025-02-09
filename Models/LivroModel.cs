namespace Biblia.Models
{
    public class LivroModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Abreviacao { get; set; } 
        public int Testamento { get; set; }
        public int TotalCapitulos { get; set; } 
        public string Resumo { get; set; }
    }
}
