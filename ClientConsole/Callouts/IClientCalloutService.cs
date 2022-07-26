using ClientConsoleApp.Models;
using ClientConsoleApp.Responses;

namespace ClientConsoleApp.Callouts
{
    public interface IClientCalloutService
    {
        Task<DataResponse> GetDataAsync(string token, HttpContent request);
        Task<AuthenticatedClient> GetTokenAsync(HttpContent body);
    }
}
