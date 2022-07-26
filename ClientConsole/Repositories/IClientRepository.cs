using ClientConsoleApp.Models;

namespace ClientConsoleApp.Repositories
{
    public interface IClientRepository
    {
        Task<Client?> GetClientCredentialsAsync();
        Task<IEnumerable<ClientRequestData>> GetClientRequestDataAsync();
        Task<bool> SaveResponseDataAsync(ClientResponseData responseData);
    }
}
