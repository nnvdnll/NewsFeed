using Microsoft.AspNetCore.Mvc;
using MVS.Data;
using MVS.Models;
using System.Diagnostics;

namespace MVS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, MyApplicationContext context)
        {
            _context = context;
            _logger = logger;
        }

        #region -- главная страница и поиск (Index)

        // Обычный запуск страницы (GET)
        [HttpGet]
        public IActionResult Index()
        {
            var news = _context.News.ToList();
            return View(news);
        }

        // Обработка формы поиска (POST) — сделано как у преподавателя через AsQueryable
        [HttpPost]
        public IActionResult Index(string Title, string Category)
        {
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
            return View(news);
        }

        #endregion

        #region -- добавление

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string Title, string Category, string Content)
        {
            var news = new News
            {
                Title = Title,
                Category = Category,
                Content = Content
            };
            _context.News.Add(news);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        #endregion 

        #region -- редактирование

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);
            return View(news);
        }
      
        [HttpPost]
        public IActionResult Edit(string Title, string Category, string Content, int id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);
            
            if (news != null)
            {
                // Исправлено: теперь поля обновляются, а не удаляются
                news.Title = Title;
                news.Category = Category;
                news.Content = Content;

                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region -- удаление

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);
       
            if (news != null)
            {
                _context.News.Remove(news);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}