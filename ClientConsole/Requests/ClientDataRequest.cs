using ClientConsoleApp.Models;

namespace ClientConsoleApp.Requests
{
    public class ClientDataRequest
    {
        public IEnumerable<ClientRequestData> RequestData { get; set; }
    }
}
