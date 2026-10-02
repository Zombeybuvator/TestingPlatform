namespace TestingPlatform.Models;

public class Project
{
   /// <summary>
   /// Идентификатор
   /// </summary>
   public int Id { get; set; }
  
   /// <summary>
   /// Название проекта.
   /// </summary>
   public string Name { get; set; }
  
   /// <summary>
   /// Список групп внутри проекта
   /// </summary>
   public List<Group> Groups { get; set; }
}

