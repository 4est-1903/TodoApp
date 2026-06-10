using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.Data;
using TodoApp.API.Models;
using TodoApp.API.DTOs;
using FluentValidation;
using AutoMapper;

namespace TodoApp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        //Veritabanı bağlantısını buraya ekliyoruz
        public TodoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllTodos([FromQuery]string? search, [FromQuery] bool? isCompleted)
        {
            //Veritabanındaki tüm görevleri "query" olarak hazırlıyoruz (henüz MySql'e gitmedi)
            var query = _context.TodoItems.AsQueryable();

            //Eğer kullanıcı arama kutusunu doldurduysa (search kısmı boş değilse)
            if (!string.IsNullOrEmpty(search))
            {
                //Başlık veya açıklama içinde arama terimini içeren görevleri filtreliyoruz
                query = query.Where(t => t.Title.Contains(search) || t.Description.Contains(search));
            }

            //Eğer kullanıcı tamamlanmış veya tamamlanmamış görevleri filtrelemek istiyorsa
            if (isCompleted.HasValue)
            {
                //Eğer isCompleted parametresi sağlanmışsa, tamamlanmış veya tamamlanmamış görevlere göre filtreleme yapıyoruz
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }

            //Filtrelenmiş sorguyu veritabanına gönderiyoruz ve sonuçları alıyoruz
            var todos = await query.ToListAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTodoById(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound($"ID'si {id} olan bir görev bulunamadı.");
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(CreateTodoDto todoDto, [FromServices] IValidator<CreateTodoDto> validator)
        {
            var validationResult = await validator.ValidateAsync(todoDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var newTodo = _mapper.Map<TodoItem>(todoDto);
            newTodo.IsCompleted = false; // Yeni görevler varsayılan olarak tamamlanmamış olarak oluşturulur
            newTodo.CreatedAt = DateTime.UtcNow; // Oluşturulma tarihini UTC olarak ayarlıyoruz

            _context.TodoItems.Add(newTodo);
            await _context.SaveChangesAsync();

            return Ok(newTodo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound($"ID'si {id} olan bir görev bulunamadı.");
            }
            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return Ok($"ID'si {id} olan görev silindi.");

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(int id, UpdateTodoDto todoDto)
        {
            var existingTodo = await _context.TodoItems.FindAsync(id);
            if (existingTodo == null)
            {
                return NotFound($"Güncellenmek istenen ID'si {id} olan görev bulunamadı.");
            }

            _mapper.Map(todoDto, existingTodo);

            await _context.SaveChangesAsync();
            return Ok(existingTodo);

        }
    }

}