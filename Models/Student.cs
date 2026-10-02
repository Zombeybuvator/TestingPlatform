namespace TestingPlatform.Models;
public class Student
{
   /// <summary>
   /// Идентификатор студента
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
   /// Номер телефона
   /// </summary>
   public string Phone { get; set; }
  
   /// <summary>
   /// Ссылка на профиль студента в ВК
   /// </summary>
   public string VkProfileLink { get; set; }

   /// <summary>
   /// Дата создания пользователя
   /// </summary>
   public DateTimeOffset CreatedAt { get; set; }

   public User User { get; set; } = null!;

}

