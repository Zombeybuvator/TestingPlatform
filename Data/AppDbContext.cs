using TestingPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace TestingPlatform.Data;

public class AppDbContext : DbContext
{
   public DbSet<User>     Users      => Set<User>();
   public DbSet<Student>  Students   => Set<Student>();
   public DbSet<Project>  Projects   => Set<Project>();
   public DbSet<Group>    Groups     => Set<Group>();
   public DbSet<Direction> Directions => Set<Direction>();
   public DbSet<Course>   Courses    => Set<Course>();
   
   public DbSet<Test> Tests => Set<Test>();

   public DbSet<Answer> Answers => Set<Answer>();

   public DbSet<Attempt> Attempts => Set<Attempt>();

   public DbSet<UserAttemptAnswer> UserAttemptAnswers => Set<UserAttemptAnswer>();

   public DbSet<UserSelectedOption> UserSelectedOptions => Set<UserSelectedOption>();

   public DbSet<UserTextAnswer> UserTextAnswers => Set<UserTextAnswer>();

   public DbSet<TestResult> TestResults => Set<TestResult>();


   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {

      modelBuilder.Entity<User>(e =>
      {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Login).IsUnique();
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Login).IsRequired();
            e.Property(x => x.Email).IsRequired();
            e.Property(x => x.FirstName).IsRequired();
            e.Property(x => x.LastName).IsRequired();   
            e.Property(x => x.Role).IsRequired();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            

            e.HasOne(x => x.Student)
               .WithOne(s => s.User)
               .HasForeignKey<Student>(s => s.UserId)
               .OnDelete(DeleteBehavior.Cascade);
            
      }); 


      modelBuilder.Entity<Student>(e =>
      {
            e.HasKey(x => x.Id);
            e.Property(x => x.Phone).HasMaxLength(30).IsRequired(); 
            e.Property(x => x.VkProfileLink).IsRequired();
            
            e.HasIndex(x => x.UserId).IsUnique();
            e.Property(x => x.UserId).IsRequired();

            e.HasMany(s => s.Groups)
               .WithMany(g => g.Students)
               .UsingEntity(j => j.ToTable("StudentGroupRelations"));
            
            e.HasMany(s => s.Tests)
               .WithMany(t => t.Students)
               .UsingEntity(j => j.ToTable("test_students")); 

            e.HasMany(s => s.Attempts)       
               .WithOne(a => a.Student)       
               .HasForeignKey(a => a.StudentId) 
               .OnDelete(DeleteBehavior.Cascade);
            
            e.HasMany(s => s.TestResults)
               .WithOne(a => a.Student)       
               .HasForeignKey(a => a.StudentId) 
               .OnDelete(DeleteBehavior.Cascade);


            
      }); // 
      
      modelBuilder.Entity<Direction>(e =>
      {
         e.HasKey(x => x.Id);

         e.Property(x => x.Name).IsRequired();
         e.HasIndex(x => x.Name).IsUnique();

         e.HasMany(s => s.Tests)
            .WithMany(t => t.Directions)
            .UsingEntity(j => j.ToTable("test_directions")); 
      });

      modelBuilder.Entity<Course>(e =>
      {
         e.HasKey(x => x.Id);
         
         e.Property(x => x.Name).IsRequired();
         e.HasIndex(x => x.Name).IsUnique();

         e.HasMany(c => c.Tests)
            .WithMany(t => t.Courses)
            .UsingEntity(j => j.ToTable("test_courses")); 
      });

      modelBuilder.Entity<Project>(e =>
      {
         e.HasKey(x => x.Id);

         e.Property(x => x.Name).IsRequired();
         e.HasIndex(x => x.Name).IsUnique();
         e.HasMany(s => s.Tests)
               .WithMany(t => t.Projects)
               .UsingEntity(j => j.ToTable("test_projects")); 
      
      });

      modelBuilder.Entity<Group>(e =>
      {
         e.HasKey(x => x.Id);
         
         e.Property(x => x.Name).IsRequired();
         e.HasIndex(x => x.Name).IsUnique();

         e.Property(x => x.ProjectId).IsRequired();


         e.Property(x => x.CourseId).IsRequired();


         e.Property(x => x.DirectionId).IsRequired();

         e.HasOne(x => x.Direction)
            .WithMany(d => d.Groups)
            .HasForeignKey(x => x.DirectionId)
            .OnDelete(DeleteBehavior.Restrict);

         e.HasOne(x => x.Course)
            .WithMany(c => c.Groups)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

         e.HasOne(x => x.Project)
            .WithMany(p => p.Groups)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
         e.HasMany(s => s.Tests)
               .WithMany(t => t.Groups)
               .UsingEntity(j => j.ToTable("test_groups")); 
      });
      modelBuilder.Entity<Test>(e =>
      { 
         e.HasKey(x => x.Id);

         e.Property(x => x.Title).IsRequired();

         e.Property(x => x.Description).IsRequired();

         e.Property(x => x.IsRepeatable).HasDefaultValue(false);

         e.Property(x => x.Type)
            .HasConversion<string>()
            .IsRequired();

         e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

         e.Property(x => x.PublishedAt).IsRequired();

         e.Property(x => x.Deadline).IsRequired();

         e.Property(x => x.IsPublic).HasDefaultValue(false);

         e.HasMany(s => s.Questions)
            .WithOne(t => t.Test)   
            .HasForeignKey(a => a.TestId) 
            .OnDelete(DeleteBehavior.Cascade);

      });

      modelBuilder.Entity<Question>(e =>
      {
         e.HasKey(x => x.Id);


         e.Property(x => x.Text).IsRequired();
         e.Property(x => x.Number).IsRequired();
         e.Property(x => x.TestId).IsRequired();


         e.Property(x => x.Description).HasMaxLength(2000);


         e.Property(x => x.AnswerType)
            .HasConversion<string>()
            .IsRequired();


         e.Property(x => x.IsScoring).HasDefaultValue(true);


         e.HasIndex(x => new { x.TestId, x.Number }).IsUnique();

         e.HasMany(a => a.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade); 

         e.HasMany(a => a.UserAttemptAnswers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
      });
      modelBuilder.Entity<Answer>(e=>
      {
         e.HasKey(x => x.Id);

         e.Property(x => x.Text).IsRequired();

         e.Property(x => x.IsCorrect).IsRequired();

         e.Property(x => x.QuestionId).IsRequired();

         e.HasMany(a => a.UserSelectedOptions)
            .WithOne(a => a.Answer)                
            .HasForeignKey(a => a.AnswerId)        
            .OnDelete(DeleteBehavior.Cascade); 
      });
      modelBuilder.Entity<Attempt>(e=> 
      {
         e.HasKey(x => x.Id);

         e.Property(x => x.StartedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

         e.Property(x => x.TestId).IsRequired();

         e.Property(x => x.StudentId).IsRequired();

         e.HasMany(a => a.UserAttemptAnswers)       
            .WithOne(a => a.Attempt) 
            .HasForeignKey(a => a.AttemptId)       
            .OnDelete(DeleteBehavior.Cascade);  

      });
      modelBuilder.Entity<UserAttemptAnswer>(e =>
      {
         e.HasKey(x => x.Id);


         e.Property(x => x.AttemptId).IsRequired();
         e.Property(x => x.QuestionId).IsRequired();
         e.Property(x => x.ScoreAwarded).IsRequired();


         e.HasIndex(x => new { x.AttemptId, x.QuestionId }).IsUnique();


         e.HasOne(x => x.Question)
            .WithMany(q => q.UserAttemptAnswers)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Restrict); 
      });

      modelBuilder.Entity<UserSelectedOption>(e =>
      {
         e.HasKey(x => x.Id);


         e.Property(x => x.UserAttemptAnswerId).IsRequired();
         e.Property(x => x.AnswerId).IsRequired();

 
         e.HasOne(x => x.UserAttemptAnswer)
            .WithMany(a => a.UserSelectedOptions) 
            .HasForeignKey(a => a.UserAttemptAnswerId)
            .OnDelete(DeleteBehavior.Cascade);


         e.HasOne(x => x.Answer)
            .WithMany(a => a.UserSelectedOptions) 
            .HasForeignKey(a => a.AnswerId)
            .OnDelete(DeleteBehavior.Restrict);  
      });
      modelBuilder.Entity<UserTextAnswer>(e =>
      {
      
      e.HasKey(x => x.Id);

      e.Property(x => x.TextAnswer).IsRequired();

      e.Property(x => x.UserAttemptAnswerId).IsRequired();

      e.HasOne(x => x.UserAttemptAnswer)
            .WithOne(ua => ua.UserTextAnswer)
            .HasForeignKey<UserTextAnswer>(x => x.UserAttemptAnswerId) 
            .OnDelete(DeleteBehavior.Cascade); 
      });
      modelBuilder.Entity<TestResult>(e =>
      {
         e.HasKey(x => x.Id);


         e.Property(x => x.Passed).IsRequired();
         e.Property(x => x.TestId).IsRequired();
         e.Property(x => x.AttemptId).IsRequired();
         e.Property(x => x.StudentId).IsRequired();


         e.HasIndex(x => new { x.TestId, x.StudentId, x.AttemptId }).IsUnique();


         e.HasOne(x => x.Test)
            .WithMany() 
            .HasForeignKey(x => x.TestId)
            .OnDelete(DeleteBehavior.Restrict); 

         e.HasOne(x => x.Attempt)
            .WithMany()
            .HasForeignKey(x => x.AttemptId)
            .OnDelete(DeleteBehavior.Cascade);

         e.HasOne(x => x.Student)
            .WithMany(s => s.TestResults) 
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade); 
      });

   }
}


