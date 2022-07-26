using AutoMapper;
using ClientConsoleApp.Callouts;
using ClientConsoleApp.Helpers;
using ClientConsoleApp.Models;
using ClientConsoleApp.Repositories;
using ClientConsoleApp.Requests;
using ClientConsoleApp.Responses;
using Newtonsoft.Json;
using System.Text;

namespace ClientConsoleApp.Services
{
    public class ClientService : IClientService
    {
        #region Private Fields

        private IMapper _mapper;
        private ILoggerManager _logger;
        private readonly IClientRepository _repository;
        private readonly IClientCalloutService _calloutService;
        private string _error = "";

        #endregion Private Fields

        #region Public Constructor 

        public ClientService(IMapper mapper,
            ILoggerManager logger,
            IClientRepository repository,
            IClientCalloutService calloutService)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _calloutService = calloutService;
        }

        #endregion Public Constructor 

        #region Public Methods 

        public async Task<string> RunAsync()
        {
            var authentication = await GetTokenAsync();

            if (string.IsNullOrEmpty(authentication.Token))
            {
                _logger.Log(authentication.Message, LogType.Error);
                return authentication.Message;
            }

            var response = await GetDataAsync(authentication.Token);

            if (response.ResponseContent == null)
            {
                return response.ErrorMessage;
            }

            await StoreResponseAsync(response.ResponseContent);
            return response.ResponseContent;
        }

        #endregion Public Methods 

        #region Private Methods
        private async Task<AuthenticatedClient> GetTokenAsync()
        {
            var request = await GetTokenRequestBodyAsync();
            return await _calloutService.GetTokenAsync(request);
        }

        private async Task<HttpContent> GetTokenRequestBodyAsync()
        {
            var client = await _repository.GetClientCredentialsAsync();
            var tokenRequest = _mapper.Map<TokenRequest>(client);
            var payload = JsonConvert.SerializeObject(tokenRequest);
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }

        private async Task<DataResponse> GetDataAsync(string token)
        {
            var requstBody = await GetDataRequestBodyAsync();
            var response = await _calloutService.GetDataAsync(token, requstBody);

            if (response.ResponseContent == null)
            {
                response.ErrorMessage = "Failed to get response data.";
                _logger.Log(response.ErrorMessage, LogType.Error);
                return response;
            }

            return response;
        }

        private async Task<HttpContent> GetDataRequestBodyAsync()
        {
            var requestData = await _repository.GetClientRequestDataAsync();
            var payload = JsonConvert.SerializeObject(requestData);
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }

        private async Task<string> StoreResponseAsync(string response)
        {
            var dataResponse = new ClientResponseData
            {
                Content = response
            };

            var saved = await _repository.SaveResponseDataAsync(dataResponse);
            return saved ? "Saved successfully" : "Failed to save data.";
        }

        #endregion Private Methods

    }
}
