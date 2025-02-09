using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblia.Models;
using Biblia.Data.Repository;
using System.Collections.Generic;

namespace Biblia.Controllers
{
    [Route("biblia")]
    public class BibliaController : Controller
    {
        private readonly MySqlRepository _repository;

        public BibliaController(MySqlRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult Biblia()
        {
            return View("Biblia"); // Garante que "Biblia.cshtml" está sendo carregado
        }


        
        [HttpGet("sortearLivro")]
        public IActionResult SortearLivro()
        {
            var livro = _repository.SortearLivro();
            if (livro == null)
            {
                return NotFound("Nenhum livro encontrado.");
            }

            return Json(new { id = livro.ID, nome = livro.Nome });
        }


        /// <summary>
        /// Retorna um livro e um capítulo aleatório (Usado no botão "SORTEAR LIVRO E CAPÍTULO")
        /// </summary>
        [HttpGet("sortearLivroCapitulo")]
        public IActionResult SortearLivroCapitulo()
        {
            var livros = _repository.GetDadosBiblia();
            if (livros == null)
            {
                return NotFound("Nenhum livro encontrado.");
            }

            var random = new Random();
            var livroAleatorio = livros[random.Next(livros.Count)];
            var totalCapitulos = _repository.BuscarTotalCapitulos(livroAleatorio.ID);
            var capituloAleatorio = totalCapitulos > 0 ? random.Next(1, totalCapitulos + 1) : 1;

            return Json(new { id = livroAleatorio.ID, nome = livroAleatorio.Nome, capitulo = capituloAleatorio });
        }

        /// <summary>
        /// Retorna o resumo de um livro sorteado (Usado no JavaScript após o sorteio)
        /// </summary>
        [HttpGet("buscarResumo/{id:int}")]
        public IActionResult BuscarResumo(int id)
        {
            var livro = _repository.BuscarLivroPorId(id);
            if (livro == null)
            {
                return NotFound("Livro não encontrado.");
            }

            return Json(new { resumo = livro.Resumo });
        }

        [HttpGet("sortearLivroComCapitulo")]
        public IActionResult SortearLivroComCapitulo()
        {
            var livro = _repository.SortearLivro();
            if (livro == null)
            {
                return NotFound("Nenhum livro encontrado.");
            }

            int totalCapitulos = _repository.ObterTotalCapitulos(livro.ID);
            if (totalCapitulos == 0)
            {
                return NotFound("Nenhum capítulo encontrado para este livro.");
            }

            Random rnd = new Random();
            int capituloSorteado = rnd.Next(1, totalCapitulos + 1); // Garante um capítulo válido

            return Json(new { id = livro.ID, nome = livro.Nome, capitulo = capituloSorteado });
        }

    }
}
