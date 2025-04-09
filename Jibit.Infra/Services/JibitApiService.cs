using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Jibit.Domain;


namespace Jibit.Infra.Services
{
        public class JibitApiService
        {
            private readonly HttpClient _httpClient;

            public JibitApiService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<IbanResponse> GetIbanInfoAsync(string iban, string token)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"https://napi.jibit.ir/ide/v1/ibans?value={iban}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IbanResponse>(content);
            }
        }

      

    public class TokenService
        {
            private readonly HttpClient _httpClient;
            private readonly JibitSettings _jibitSettings;

            public TokenService(HttpClient httpClient, IOptions<JibitSettings> jibitSettings)
            {
                _httpClient = httpClient;
                _jibitSettings = jibitSettings.Value;
            }

            public async Task<string> GenerateTokenAsync()
            {
                var requestBody = new
                {
                    apiKey = _jibitSettings.ApiKey,
                    secretKey = _jibitSettings.SecretKey
                };

                var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://napi.jibit.ir/ide/v1/tokens/generate", jsonContent);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
                return tokenResponse.AccessToken;
            }

            public async Task<string> RefreshTokenAsync(string accessToken, string refreshToken)
            {
                var requestBody = new
                {
                    accessToken,
                    refreshToken
                };

                var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://napi.jibit.ir/ide/v1/tokens/refresh", jsonContent);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
                return tokenResponse.AccessToken;
            }
        }



    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class IbanResponse
    {
        public string Value { get; set; }
        public string Bank { get; set; }
        public string DepositNumber { get; set; }
        public string Status { get; set; }
    }

}