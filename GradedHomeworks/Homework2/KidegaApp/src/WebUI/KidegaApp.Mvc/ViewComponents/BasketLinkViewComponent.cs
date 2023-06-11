namespace KidegaApp.Mvc.ViewComponents
{
    public class BasketLinkViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var bookCollection = HttpContext.Session.GetJson<BookCollection>("basket");
            return View(bookCollection);
        }
    }
}
