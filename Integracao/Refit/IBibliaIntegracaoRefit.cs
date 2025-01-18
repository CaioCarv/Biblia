using Biblia.Integracao.Response;
using Refit;

namespace Biblia.Integracao.Refit
{
    public interface IBibliaIntegracaoRefit
    {
        [Get("/bibles/{livro}/{capitulo}")]
        Task<ApiResponse<BibliaResponse>> ObterDadosBiblia(string biblia);
    }
}
