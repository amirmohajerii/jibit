using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace Jibit.Infra.Services
{
    public class ExternalApiService
    {
        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ValidationResponse> ValidateCardWithNationalCodeAsync(string cardNumber, string nationalCode, DateTime birthDate)
        {
            var requestBody = new
            {
                cardNumber,
                nationalCode,
                birthDate
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://external-api-url.com/validate-card", jsonContent);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ValidationResponse>(responseBody);
        }

        public async Task<ValidationResponse> ValidateNationalCodeWithMobileAsync(string nationalCode, string mobileNumber)
        {
            var requestBody = new
            {
                nationalCode,
                mobileNumber
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://external-api-url.com/validate-national-code", jsonContent);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ValidationResponse>(responseBody);
        }
    }

    public class ValidationResponse
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}