using System.Text.Json.Serialization;
using TestingPlatform.Enums;

namespace TestingPlatform.Models;

/// <summary>
/// Пользователь
/// </summary>
public class User
{
   /// <summary>
   /// Идентификатор пользователя
   /// </summary>
   public int Id { get; set; }
  
   /// <summary>
   /// Логин
   /// </summary>
   public string Login { get; set; }
  
   /// <summary>
   /// Email
   /// </summary>
   public string Email { get; set; }

   /// <summary>
   /// Имя
   /// </summary>
   public string FirstName { get; set; }
  
   /// <summary>
   /// Отчество
   /// </summary>
   public string? MiddleName { get; set; }
  
   /// <summary>
   /// Фамилия
   /// </summary>
   public string LastName { get; set; }
  
   /// <summary>
   /// Роль (менеджер или студент)
   /// </summary>
   public UserRole Role { get; set; }
  
   /// <summary>
   /// Время создания
   /// </summary>
   public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
  
   [JsonIgnore]
   /// <summary>
   /// Если роль студент - должна быть запись в таблице Student
   /// </summary>
   public Student? Student { get; set; }
}

