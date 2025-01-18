using Biblia.Integracao.Interfaces;
using Biblia.Integracao.Response;
using Biblia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Biblia.Controllers
{
    public class BibliaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IBibliaIntegracao _bibliaIntegracao;

        public BibliaController(IBibliaIntegracao bibliaIntegracao)
        {
            _bibliaIntegracao = bibliaIntegracao;
        }

        [HttpGet("{biblia}")]
        public async Task<ActionResult<BibliaResponse>> ListarBibliaVersiculo(string biblia)
        {
            var responseData = await _bibliaIntegracao.ObterDadosBiblia(biblia);

            if (responseData == null)
            {
                return BadRequest("Livro não encontrado!");
            }

            return Ok(responseData);
        }


        //public BibliaController(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;

        //    // Adicione a chave da API diretamente no cabeçalho
        //    _httpClient.DefaultRequestHeaders.Add("api-key", "241828a2e9f4def2387d9b2c82519e11"); // Substitua pela sua chave
        //}

        public async Task<IActionResult> ObterTraducao()
        {
            var bibleId = "90799bb5b996fddc-01"; // ID da tradução específica
            var url = $"https://api.scripture.api.bible/v1/bibles/{bibleId}";

            // Requisição para a API
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                // Retorna uma página de erro caso a requisição falhe
                return View("Error");
            }

            // Processa a resposta JSON
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var bibleDetails = JObject.Parse(jsonResponse);

            // Passa os detalhes da tradução para a view
            return View("ObterTraducao", bibleDetails);
        }

        public IActionResult Biblia()
        {
            BibliaModel biblia = new BibliaModel();

            biblia.IdLivro = 1;
            biblia.Livro = "Genesis";
            biblia.Capitulo = 1;
            biblia.Versiculo = 23;
            
            return View("/Views/Biblia/Biblia.cshtml", biblia);
        }

        //public IActionResult Usuario()
        //{
        //    UsuarioModel usuario = new UsuarioModel();

        //    usuario.Id = 1;
        //    usuario.Nome = "Genesis";
        //    usuario.Email = "caio@gmail.com";

        //    return View(usuario);
        //}   
        
        
        
    }

}

