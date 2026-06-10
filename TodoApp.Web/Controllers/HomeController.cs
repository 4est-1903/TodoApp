using Microsoft.AspNetCore.Mvc;
using TodoApp.Web.Models;
using TodoApp.Web.Services;

namespace TodoApp.Web.Models
{
    public class HomeController : Controller
    {
        private readonly TodoApiService _apiService;

        public HomeController(TodoApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(string? search, bool? isCompleted)
        {
            var todos = await _apiService.GetTodosAsync(search, isCompleted);
            ViewData["CurrentSearch"] = search;
            ViewData["CurrentFilter"] = isCompleted;

            return View(todos);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            await _apiService.CreateTodoAsync(model);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Toggle(int id, string title, string description, bool isCompleted)
        {   //Formdan o anki görev bilgilerini alýyoruz ve tamamlanma durumunu tersine çeviriyoruz
            //Gelen deðer true ise false yapacaðýz, false ise true yapacaðýz
            bool newStatus = !isCompleted;

            var model = new TodoViewModel
            {
                Id = id,
                Title = title,
                Description = description,
                IsCompleted = newStatus,
            };
            await _apiService.ToggleCompleteAsync(id, model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _apiService.DeleteTodoAsync(id);
            return RedirectToAction("Index");

        }
    }
}

