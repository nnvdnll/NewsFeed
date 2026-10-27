using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsClientApp.Data;
using NewsClientApp.Models;
using NewsClientApp.Services;
using System.Diagnostics;
using System.Text.Json;

namespace NewsClientApp.Controllers
{
    [Authorize] // нужна для того , что бы только авторизованные пользователи могли видеть содержимое
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly NewsClientServices _newsClientService;

        public HomeController(ILogger<HomeController> logger, NewsClientServices newsClientServices)
        {
            _logger = logger;
            _newsClientService = newsClientServices;
        }

        // Единый универсальный метод Index для отображения ленты и поиска (GET)
        [HttpGet]
        public async Task<IActionResult> Index(string? Title, string? Category)
        {
            var httpClient = new HttpClient();
            var url = "https://localhost:7178/News/filter";
            var queryParams = new List<string>();

            // Если ввели заголовок, добавляем к запросу
            if (!string.IsNullOrEmpty(Title))
            {
                queryParams.Add($"title={Uri.EscapeDataString(Title)}");
            }
            // Если ввели категорию, добавляем к запросу
            if (!string.IsNullOrEmpty(Category))
            {
                queryParams.Add($"category={Uri.EscapeDataString(Category)}");
            }

            // Склеиваем параметры в строку запроса (Query string)
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            // Делаем GET-запрос к API с учетом фильтров
            var response = await httpClient.GetAsync(url);
            var responseText = await response.Content.ReadAsStringAsync();

            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var responseData = JsonSerializer.Deserialize<List<News>>(responseText, jsonOptions) ?? new List<News>();

            // Сохраняем введенный текст в полях, чтобы он не стирался после поиска
            ViewBag.SearchTitle = Title;
            ViewBag.SearchCategory = Category;

            // Возвращаем ту же страницу Index с отфильтрованным или полным списком
            return View(responseData);
        }

        #region -- добавление

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var Sources = await _newsClientService.GetSourceAll();

            return View(Sources);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewsAddDto request)
        {
            await _newsClientService.AddNews(request);

            return RedirectToAction(nameof(Index));
        }

        #endregion 

        #region -- редактирование

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var responseData = await _newsClientService.GetNewsByid(id);

            return View(responseData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewsEditDto request)
        {
            await _newsClientService.Edit(request);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region -- удаление

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await _newsClientService.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}