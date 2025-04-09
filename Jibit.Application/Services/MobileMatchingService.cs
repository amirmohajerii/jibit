using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jibit.Application.Interfaces;
using Jibit.Domain.Aggregates;
using Jibit.Infra.Repositories;
using Jibit.Infra.Services;

namespace Jibit.Application.Services
{
    public class MobileMatchingService : IMobileMatchingService
    {
        private readonly ExternalApiService _externalApiService;
        private readonly RequestRepository _requestRepository;

        public MobileMatchingService(ExternalApiService externalApiService, RequestRepository requestRepository)
        {
            _externalApiService = externalApiService;
            _requestRepository = requestRepository;
        }

        public async Task<bool> MatchNationalCodeWithMobileAsync(string nationalCode, string mobileNumber)
        {
            var request = new Request
            {
                ServiceName = "ShahkarValidation",
                Provider = "Jibit",
                CalledAt = DateTime.UtcNow,
                IsSuccessful = false,
            };

            try
            {
                var response = await _externalApiService.ValidateNationalCodeWithMobileAsync(nationalCode, mobileNumber);
                request.IsSuccessful = response.IsValid;

                if (!response.IsValid)
                {
                    request.ErrorMessage = "National Code and Mobile Number validation failed.";
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