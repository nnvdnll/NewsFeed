using Microsoft.AspNetCore.Mvc;
using NewsClientApp.Data;
using NewsClientApp.Models;
using System.Text.Json;

namespace NewsClientApp.Services
{
    public class NewsClientServices
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };


        public NewsClientServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task AddNews(NewsAddDto request)
        {
            var response = await _httpClient.PutAsJsonAsync("/News", request);

            return;
        }


        public async Task Remove(int id)
        {

            var response = await _httpClient.DeleteAsync($"/News?newsId={id}");

            return;


        }

        public async Task<News> GetNewsByid(int id)
        {
            var response = await _httpClient.GetAsync($"/News/{id}");
            var responseText = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<News>(responseText, _jsonOptions);

            return responseData;
        }
        public async Task Edit(NewsEditDto request)
        {
            var response = await _httpClient.PatchAsJsonAsync("/News", request);

            return;
        }

        public async Task<List<Source>> GetSourceAll()
        {
            var response = await _httpClient.GetAsync("/Source");
            var responseText = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<List<Source>>(responseText, _jsonOptions);

            return responseData;
        }
    }
}
