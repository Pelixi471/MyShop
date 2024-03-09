using MyShop.Domain.Entites;
using System.Net.Http.Json;
using HttpModels;

namespace MyShop.ShopClient
{
    public class ShopClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _host;
        public ShopClient(HttpClient httpClient, string host)
        {
            if (string.IsNullOrWhiteSpace(host))
            {
                throw new ArgumentException($"'{nameof(host)}' host can't be empty"); 
            }
            if(httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));    
            }
            _httpClient = httpClient;
            _host = host;
        }

        public async Task<IReadOnlyList<Domain.Entites.Product>>GetProductsAsync(CancellationToken cancellationToken)
        {
            var uri = $"{_host}/api/products";
            var products = await _httpClient.GetFromJsonAsync<IReadOnlyList<Domain.Entites.Product>>(uri, cancellationToken:cancellationToken);
            return products!;
        }
        public async Task<Account> Register(RegistrationRequest _request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(_request);
            var uri = $"{_host}/api/account/register";
            using var response = await _httpClient.PostAsJsonAsync(uri, _request, cancellationToken);
            if (response == null)
                throw new NullReferenceException(nameof(response));
            var account = await response.Content.ReadFromJsonAsync<Account>();
            if (account == null)
                throw new NullReferenceException(nameof(account));
            return account;
        }

        public async Task<List<VisitingPages>> GetVisitingPages(CancellationToken cancellationToken)
        {
            var uri = $"{_host}/api/metrics/get";
            using var response = await _httpClient.GetAsync(uri, cancellationToken);
            response.EnsureSuccessStatusCode();
            var visitingPages =
                await response.Content.ReadFromJsonAsync<List<VisitingPages>>(cancellationToken: cancellationToken);
            return visitingPages!;
        }

    }
}