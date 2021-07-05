using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ICM.Taxes.Core.Interfaces;
using ICM.Taxes.Core.Models.DTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ICM.Taxes.TaxJar.Client
{
    public class TaxCalculator: ITaxCalculator
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiBaseAddress;
        private readonly string _apiKey;
        public TaxCalculator(IHttpClientFactory clientFactory, IOptions<AppSettings> appSettings)
        {
            _clientFactory = clientFactory;
            _apiBaseAddress = appSettings.Value.TaxJarBaseAddres;
            _apiKey = appSettings.Value.TaxJarApiKey;
        }

        public async Task<dynamic> CalcTaxes(dynamic payLoad)
        {
            
            try
            {
                var client = SetUpClientRequest();
                var finalUrl = _apiBaseAddress + "taxes";
                var request = new HttpRequestMessage(HttpMethod.Post, finalUrl);

                var content = new StringContent(JsonConvert.SerializeObject(payLoad), Encoding.UTF8, "application/json");

                request.Content = content;


                var response = await client.SendAsync(request).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return await ProcessHttpResponse(response);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<dynamic> GetRates(string zipCode, string optQS)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentException("The zipcode is required", nameof(zipCode));
            try
            {
                var finalUrl = _apiBaseAddress + $"rates/{zipCode}?" + (optQS ?? "");

                var request = new HttpRequestMessage(HttpMethod.Get, finalUrl);

                var client = SetUpClientRequest();

                var response = await client.SendAsync(request).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return await ProcessHttpResponse(response);
                }
                //We can log this result;
                return null;

            }
            catch (Exception)
            {
                //We can log this result;
                return null;
            }
        }

        private HttpClient SetUpClientRequest()
        {
            var client = _clientFactory.CreateClient("TaxJar");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            return client;

        }

        private async Task<dynamic> ProcessHttpResponse(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(responseString);
            return result;
        }


    }
}
