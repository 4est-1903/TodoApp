using System.ComponentModel.DataAnnotations;

namespace TodoApp.Web.Models
{
    public class CreateTodoViewModel
    {
        [Required(ErrorMessage = "Görev başlığı boş bırakılamaz.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Görev açıklaması boş bırakılamaz")]
        public string Description { get; set; } = string.Empty;
    }
}
