using Biblia.Integracao.Interfaces;
using Biblia.Integracao.Refit;
using Biblia.Integracao.Response;

namespace Biblia.Integracao
{
    public class BibliaIntegracao : IBibliaIntegracao
    {
        private readonly IBibliaIntegracaoRefit _bibliaIntegracaoRefit;

        public BibliaIntegracao(IBibliaIntegracaoRefit bibliaIntegracaoRefit)
        {
            _bibliaIntegracaoRefit = bibliaIntegracaoRefit;
        }

        public async Task<BibliaResponse> ObterDadosBiblia(string biblia)
        {
             var responseData = await _bibliaIntegracaoRefit.ObterDadosBiblia(biblia);

            if (responseData != null && responseData.IsSuccessStatusCode)
            {
                return responseData.Content;
            }

            return null;
        }
    }
}
