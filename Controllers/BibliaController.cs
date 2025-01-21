using Microsoft.AspNetCore.Mvc;
using Biblia.Models;
using Biblia.Data.Repository;

namespace Biblia.Controllers
{
    public class BibliaController : Controller
    {
        private readonly MySqlRepository _repository;

        // Construtor que inicializa o repositório
        public BibliaController()
        {
            string connectionString = "Server=localhost;Database=Bible;User ID=root;Password=Mayra@027;";
            _repository = new MySqlRepository(connectionString);
        }

        // Método para exibir os detalhes de um livro
        [HttpGet]
        public IActionResult Livro()
        {
            // Inicializa o modelo do livro
            LivroModel livro = new LivroModel
            {
                ID = 1,                // ID do Livro
                Nome = "Gênesis"       // Nome do Livro
            };

            // Retorna a view "Livro.cshtml" passando o modelo como parâmetro
            return View("/Views/Livro/Livro.cshtml", livro);
        }

        // Método para exibir uma lista de livros
        [HttpGet]
        public IActionResult Livros()
        {
            // Inicializa uma lista de livros
            var livros = new List<LivroModel>
            {
                new LivroModel { ID = 1, Nome = "Gênesis" },
                new LivroModel { ID = 2, Nome = "Êxodo" },
                new LivroModel { ID = 3, Nome = "Levítico" },
                new LivroModel { ID = 4, Nome = "Números" },
                new LivroModel { ID = 5, Nome = "Deuteronômio" }
                // Adicione os demais livros aqui
            };

            // Retorna a view "Livros.cshtml" passando a lista de livros como parâmetro
            return View("/Views/Livro/Livros.cshtml", livros);
        }
    }
}
