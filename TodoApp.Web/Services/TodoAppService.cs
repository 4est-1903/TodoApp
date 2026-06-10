using System.Net.Http.Json;
using TodoApp.Web.Models;

namespace TodoApp.Web.Services
{
    public class TodoApiService
    {
        private readonly HttpClient _httpClient;

        public TodoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //API'den görevleri almak için bir yöntem oluşturuyoruz
        public async Task<List<TodoViewModel>> GetTodosAsync (string? search, bool? isCompleted)
        {
            var url = $"api/Todo?search={search}&isCompleted={isCompleted}";
            var response = await _httpClient.GetFromJsonAsync<List<TodoViewModel>>(url);
            return response ?? new List<TodoViewModel>();
        }

        public async Task<bool?> CreateTodoAsync(CreateTodoViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Todo", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Todo/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ToggleCompleteAsync(int id, TodoViewModel model)
        {
            var updateDto = new
            {
                Title = model.Title,
                Description = model.Description,
                IsCompleted = model.IsCompleted
            };
            var response = await _httpClient.PutAsJsonAsync($"api/Todo/{id}", updateDto);
            return response.IsSuccessStatusCode;

        }

    }
}
