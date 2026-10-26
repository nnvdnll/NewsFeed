using Microsoft.AspNetCore.Mvc;
using NewsWebApi.Data;
using NewsWebApi.Models;

namespace NewsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {

        private readonly ILogger<NewsController> _logger;

        private readonly MyApplicationContext _context;

        public NewsController(ILogger<NewsController> logger, MyApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<News> Get()
        {
            _logger.LogInformation("Получения списка новостей");
            return _context.News.ToList();
        }


        [HttpGet("{id}")]
        public News? Get(int id)
        {
            return _context.News
                .FirstOrDefault(x => x.Id == id);
        }

        // Метод поиска/фильтрации (поддерживает и GET, и сохраняет совместимость)
        [HttpGet("filter")]
        public IEnumerable<News> Filter(string? title, string? category)
        {
            _logger.LogInformation("Получения списка новостей с фильтром");
            var query = _context.News.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Category.Contains(category));
            }

            return query.ToList();
        }

        [HttpPost]
        public IEnumerable<News> Post(string? Title, string? Category)
        {
            _logger.LogInformation("Получения списка новостей с фильтром (POST)");
            var query = _context.News.AsQueryable();

            if (!string.IsNullOrEmpty(Title))
            {
                query = query.Where(x => x.Title.Contains(Title));
            }

            if (!string.IsNullOrEmpty(Category))
            {
                query = query.Where(x => x.Category.Contains(Category));
            }

            var news = query.ToList();

            return news;
        }

        [HttpPut]
        public News Put(NewsAdd request)
        {
            _logger.LogInformation("Добавляю новые новости");


            var news = new News
            {
                Title = request.Title,
                Category = request.Category,
                Content = request.Content,
                SourceId = request.SourceId

            };
            _context.News.Add(news);
            _context.SaveChanges();

            return news;
        }

        [HttpDelete]
        public IActionResult Delete(int newsId)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == newsId);
            if (news != null)
            {
                _context.Remove(news);
                _context.SaveChanges();

                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        public IActionResult Patch(News model)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == model.Id);
            if (news != null)
            {
                news.Category = model.Category;
                news.Title = model.Title;
                news.Content = model.Content;

                _context.Update(news);
                _context.SaveChanges();

                return Ok();
            }
            return NotFound();
        }
    }
}