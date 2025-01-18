using Biblia.Integracao.Response;

namespace Biblia.Integracao.Interfaces
{
    public interface IBibliaIntegracao
    {
        Task<BibliaResponse> ObterDadosBiblia(string biblia);
    }
}
