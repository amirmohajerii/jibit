using Jibit.Application.Interfaces;
using Jibit.Infra.Services;
using Jibit.Infra.Repositories;
using Jibit.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace Jibit.Application.Services
{
    public class IbanService : IIbanService
    {
        private readonly JibitApiService _apiService;
        private readonly TokenService _tokenService;
        private readonly RequestRepository _requestRepository;

        public IbanService(JibitApiService apiService, TokenService tokenService, RequestRepository requestRepository)
        {
            _apiService = apiService;
            _tokenService = tokenService;
            _requestRepository = requestRepository;
        }

        public async Task<IbanResponse> GetIbanInfoAsync(string iban)
        {
            var request = new Request
            {
                ServiceName = "IbanInquiry",
                Provider = "Jibit",
                CalledAt = DateTime.UtcNow,
                IsSuccessful = false
            };

            try
            {
                string token = await _tokenService.GenerateTokenAsync();

                var response = await _apiService.GetIbanInfoAsync(iban, token);

                request.IsSuccessful = true;
                return response;
            }
            catch (Exception ex)
            {
                request.ErrorMessage = ex.Message;
                throw;
            }
            finally
            {
                await _requestRepository.AddRequestAsync(request);
            }
        }
    }
}