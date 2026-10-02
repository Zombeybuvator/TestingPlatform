using TestingPlatform.Enums;
using TestingPlatform.Controllers;
using TestingPlatform.Models;
namespace TestingPlatform.Models;

/// <summary>
/// Тест
/// </summary>
public class Test
{
   /// <summary>
   /// Идентификатор
   /// </summary>
   public int Id { get; set; }
  
   /// <summary>
   /// Название теста
   /// </summary>
   public string Title { get; set; }
  
   /// <summary>
   /// Описание теста
   /// </summary>
   public string Description { get; set; }
  
   /// <summary>
   /// Можно ли тест пройти повторно или есть только одна попытка
   /// </summary>
   public bool IsRepeatable { get; set; }
  
   /// <summary>
   /// Тип теста - образовательный, дополнительные активности, другое
   /// </summary>
   public TestType Type { get; set; }
  
   /// <summary>
   /// Тип ответа на вопрос
   /// </summary>

  
   /// <summary>
   /// Когда создан
   /// </summary>
   public DateTimeOffset CreatedAt { get; set; }
  
   /// <summary>
   /// Когда опубликован (стал доступен студентам)
   /// </summary>
   public DateTimeOffset PublishedAt { get; set; }
  
   /// <summary>
   /// Срок выполнения теста
   /// </summary>
   public DateTimeOffset Deadline { get; set; }
  
   /// <summary>
   /// Время на выполнение теста
   /// </summary>
   public int? DurationMinutes { get; set; }
  
   /// <summary>
   /// Опубликован ли (доступен студнтам для прохождения)
   /// </summary>
   public bool IsPublic { get; set; }
  
   /// <summary>
   /// Проходной балл (достигнув которого тест больше нельзя пройти)
   /// </summary>
   public int? PassingScore { get; set; }
  
   /// <summary>
   /// Максимальное количество попыток прохождения теста
   /// </summary>
   public int? MaxAttempts { get; set; }
  
   /// <summary>
   /// Список вопросов в тесте
   /// </summary>
   public List<Question> Questions { get; set; } = new();
  
   /// <summary>
   /// Список студентов, для которых доступен тест
   /// </summary>
   public List<Student> Students { get; set; } = new();
  
   /// <summary>
   /// Список проектов, для которых доступен тест
   /// </summary>
   public List<Project> Projects { get; set; } = new();
  
   /// <summary>
   /// Список курсов, для которых доступен тест
   /// </summary>
   public List<Course> Courses { get; set; } = new();
  
   /// <summary>
   /// Список групп, для которых доступен тест
   /// </summary>
   public List<Group> Groups { get; set; } = new();
  
   /// <summary>
   /// Список направлений, для которых доступен тест
   /// </summary>
   public List<Direction> Directions { get; set; } = new();

   
}