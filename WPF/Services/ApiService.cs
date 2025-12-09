using DataTransferLayer.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;


namespace WPF.Services
{
    public class ApiService
    {
        private readonly HttpClient client;

        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7083/")
            };
        }

        public async Task<Lægehus?> GetLægehusByYdernummer(string ydernummer)
        {
            if (string.IsNullOrWhiteSpace(ydernummer)) return null;

            var url = $"api/Lægehus/{ydernummer}";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();

            var lægehus = await client.GetFromJsonAsync<Lægehus>(url);
            return lægehus;
        }

        public async Task<Recept?> GetReceptById(int id)
        {
            if (id <= 0) return null;

            var url = $"api/recept/id/{id}";

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Recept>();
        }

        public async Task<bool> OpretRecept(Recept recept)
        {
            if (recept == null) return false;

            try
            {
                var response = await client.PostAsJsonAsync("api/Recept", recept);

                if (response.IsSuccessStatusCode)
                {
                    return true; // Gem lykkedes
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API fejl: {response.StatusCode}\n{content}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Fejl ved oprettelse af recept: " + ex.Message);
            }
        }

        public async Task<ObservableCollection<Recept>> GetLægehusRecepter(int id)
        {
            if (id == null) return null;

            var url = $"api/recept/lægehus/{id}";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();

            var receptList = await client.GetFromJsonAsync<List<Recept>>(url);
            return new ObservableCollection<Recept>(receptList);
        }

    }
}
