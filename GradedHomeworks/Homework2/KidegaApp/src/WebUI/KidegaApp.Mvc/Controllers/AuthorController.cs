using KidegaApp.Services.AuthorService;
using Microsoft.AspNetCore.Mvc;

namespace KidegaApp.Mvc.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public async Task<IActionResult> Index(int authorId)
        {
            var author = await _authorService.GetByIdAsync(authorId);
            return View(author);
        }
    }
}
