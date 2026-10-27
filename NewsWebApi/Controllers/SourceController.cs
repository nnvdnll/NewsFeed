using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsWebApi.Data;
using NewsWebApi.Models;

namespace NewsWebApi.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize]

public class SourceController : ControllerBase
{
    private readonly MyApplicationContext _context;

    public SourceController(ILogger<NewsController> logger, MyApplicationContext context)
    {
        _context = context;
    }
    [HttpGet]
    public List<Source> Get()
    {
        return _context.Sources
            .ToList();
    }
}
