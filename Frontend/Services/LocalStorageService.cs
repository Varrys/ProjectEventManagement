using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Frontend.Services
{
    public interface ILocalStorageService
    {
        Task RemoveAsync(string key);
        Task SaveStringAsync(string key, string value);
        Task<string> GetStringAsync(string key);
        Task SaveStringArrayAsync(string key, string[] values);
        Task<string[]> GetStringArrayAsync(string key);
    }

    public class LocalStorageService : ILocalStorageService
    {
        private readonly HttpClient httpClient;

        public LocalStorageService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task RemoveAsync(string key)
        {
            await httpClient.DeleteAsync($"api/localstorage/{key}");
        }

        public async Task SaveStringAsync(string key, string value)
        {
            var content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            await httpClient.PostAsync($"api/localstorage/{key}", content);
        }

        public async Task<string> GetStringAsync(string key)
        {
            var response = await httpClient.GetAsync($"api/localstorage/{key}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<string>(content);
            }
            return null;
        }

        public async Task SaveStringArrayAsync(string key, string[] values)
        {
            await SaveStringAsync(key, values == null ? "" : JsonSerializer.Serialize(values));
        }

        public async Task<string[]> GetStringArrayAsync(string key)
        {
            var data = await GetStringAsync(key);
            if (!string.IsNullOrEmpty(data))
                return JsonSerializer.Deserialize<string[]>(data);
            return null;
        }
    }
}