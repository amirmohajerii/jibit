using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jibit.Application.Interfaces;
using System.Threading.Tasks;
using Jibit.Infra.Services;
using Jibit.Domain.Aggregates;
using Jibit.Infra.Repositories;

namespace Jibit.Application.Services
{
    public class CardMatchingService : ICardMatchingService
    {
        private readonly ExternalApiService _externalApiService;
        private readonly RequestRepository _requestRepository;

        public CardMatchingService(ExternalApiService externalApiService, RequestRepository requestRepository)
        {
            _externalApiService = externalApiService;
            _requestRepository = requestRepository;
        }

        public async Task<bool> MatchCardWithNationalCodeAsync(string cardNumber, string nationalCode, DateTime birthDate)
        {
            var request = new Request
            {
                ServiceName = "CardMatching",
                Provider = "Jibit",
                CalledAt = DateTime.UtcNow,
                IsSuccessful = false,
            };

            try
            {
                var response = await _externalApiService.ValidateCardWithNationalCodeAsync(cardNumber, nationalCode, birthDate);
                request.IsSuccessful = response.IsValid;

                if (!response.IsValid)
                {
                    request.ErrorMessage = "Card and National Code validation failed.";
                }
                return response.IsValid;
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