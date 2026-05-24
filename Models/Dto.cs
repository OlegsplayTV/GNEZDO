using System.ComponentModel.DataAnnotations;

namespace GNEZDO.Models;

/// <summary>
/// DTO для регистрации пользователя
/// </summary>
public class RegisterDto
{
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Пароль обязателен")]
    [MinLength(6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имя обязательно")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Фамилия обязательна")]
    public string LastName { get; set; } = string.Empty;

    public string? ParentType { get; set; }
    public string? ChildrenAges { get; set; }
}

/// <summary>
/// DTO для входа
/// </summary>
public class LoginDto
{
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Пароль обязателен")]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}

/// <summary>
/// DTO для создания сообщения на форуме
/// </summary>
public class ForumPostDto
{
    [Required(ErrorMessage = "Сообщение обязательно")]
    [MinLength(10, ErrorMessage = "Сообщение должно быть не менее 10 символов")]
    public string Content { get; set; } = string.Empty;

    public int ForumTopicId { get; set; }
}

/// <summary>
/// DTO для записи на консультацию
/// </summary>
public class ConsultationDto
{
    [Required(ErrorMessage = "Дата и время обязательны")]
    public DateTime DateTime { get; set; }

    [Range(30, 120, ErrorMessage = "Длительность должна быть от 30 до 120 минут")]
    public int DurationMinutes { get; set; } = 60;

    public int SpecialistId { get; set; }

    public string? ClientComment { get; set; }
}