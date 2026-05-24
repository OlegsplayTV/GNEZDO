using FluentValidation;
using GNEZDO.Models;

namespace GNEZDO.Validation;

/// <summary>
/// Валидатор для регистрации пользователя
/// </summary>
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обязателен")
            .EmailAddress().WithMessage("Некорректный email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен")
            .MinimumLength(6).WithMessage("Пароль должен быть не менее 6 символов");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Имя обязательно")
            .MaximumLength(50).WithMessage("Имя не должно превышать 50 символов");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Фамилия обязательна")
            .MaximumLength(50).WithMessage("Фамилия не должна превышать 50 символов");
    }
}

/// <summary>
/// Валидатор для входа
/// </summary>
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обязателен")
            .EmailAddress().WithMessage("Некорректный email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен");
    }
}

/// <summary>
/// Валидатор для консультации
/// </summary>
public class ConsultationDtoValidator : AbstractValidator<ConsultationDto>
{
    public ConsultationDtoValidator()
    {
        RuleFor(x => x.DateTime)
            .NotEmpty().WithMessage("Дата и время обязательны")
            .GreaterThan(DateTime.Now).WithMessage("Дата должна быть в будущем");

        RuleFor(x => x.DurationMinutes)
            .InclusiveBetween(30, 120).WithMessage("Длительность должна быть от 30 до 120 минут");

        RuleFor(x => x.SpecialistId)
            .GreaterThan(0).WithMessage("Специалист не выбран");
    }
}