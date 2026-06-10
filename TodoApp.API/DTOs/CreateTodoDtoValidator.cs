using FluentValidation;


namespace TodoApp.API.DTOs
{
    public class CreateTodoDtoValidator : AbstractValidator<CreateTodoDto>
    {
        public CreateTodoDtoValidator()
        {
            //Başlık alanının boş olmaması ve maksimum 100 karakter olması gerektiğini belirtiyoruz
            RuleFor(x => x.Title)
                .NotNull().WithMessage("Başlık alanı boş bırakılamaz.")
                .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");
            //Açıklamanın minimum 5 karakter olması gerektiğini belirtiyoruz
            RuleFor(x => x.Description)
                .NotNull().WithMessage("Açıklama alanı boş bırakılamaz.")
                .NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.")
                .MinimumLength(5).WithMessage("Açıklama en az 5 karakter olmalı.");
        }
    }
}
