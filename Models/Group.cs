namespace TestingPlatform.Models;
public class Group
{

   public int Id { get; set; }
  
   /// <summary>
   /// Название группы
   /// </summary>
   public string Name { get; set; }
  
   /// <summary>
   /// Направление группы
   /// </summary>
   public int DirectionId { get; set; }
   public Direction Direction { get; set; }
  
   /// <summary>
   /// Курс группы
   /// </summary>
   public int CourseId { get; set; }
   public Course Course { get; set; }
  
   /// <summary>
   /// Проект группы
   /// </summary>
   public int ProjectId { get; set; }
   public Project Project { get; set; }
  
   /// <summary>
   /// Студенты, которые состоят в группе
   /// </summary>
   public List<Student> Students { get; set; }
}