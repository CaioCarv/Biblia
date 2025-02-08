using Microsoft.AspNetCore.Mvc;
using Biblia.Models;
using Biblia.Data.Repository;

namespace Biblia.Controllers
{
    public class BibliaController : Controller
    {
        private readonly MySqlRepository _repository;

        public BibliaController()
        {
            string connectionString = "Server=localhost;Database=Bible;User ID=root;Password=Mayra@027;";
            _repository = new MySqlRepository(connectionString);
        }

        [HttpGet]
        public IActionResult Livro()
        {
            LivroModel livro = new LivroModel
            {
                ID = 1,
                Nome = "Gênesis"
            };

            return View("/Views/Livro/Livro.cshtml", livro);
        }

        [HttpGet]
        public IActionResult Livros()
        {
            var livros = new List<LivroModel>
            {
                new LivroModel { ID = 1, Nome = "Gênesis" },
                new LivroModel { ID = 2, Nome = "Êxodo" },
                new LivroModel { ID = 3, Nome = "Levítico" },
                new LivroModel { ID = 4, Nome = "Números" },
                new LivroModel { ID = 5, Nome = "Deuteronômio" }
            };

            return View("/Views/Livro/Livros.cshtml", livros);
        }
    }
}
