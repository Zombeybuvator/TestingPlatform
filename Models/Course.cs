using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TestingPlatform.Models;

public class Course
{
   /// <summary>
   /// Идентификатор
   /// </summary>
   public int Id { get; set; }
  
   /// <summary>
   /// Название курса
   /// </summary>
   public string Name { get; set; }
  
   /// <summary>
   /// Список групп внутри курса
   /// </summary>
   public List<Group> Groups { get; set; } = new();

   public List<Test> Tests { get; set; } = new();
}
