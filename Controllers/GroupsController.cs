using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Data;
using TestingPlatform.Models;

namespace TestingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")] // базовый маршрут: /api/groups
public class GroupsController : ControllerBase
{
    private readonly AppDbContext _db;

    // Внедряем контекст базы данных через конструктор
    public GroupsController(AppDbContext db)
    {
        _db = db;
    }

    // 1. GET: Получить список всех групп
    [HttpGet]
    public IActionResult GetAllGroups()
    {
        var groups = _db.Groups.ToList();
        return Ok(groups); // HTTP 200
    }

    // 2. GET: Поиск конкретной группы по ID
    [HttpGet("{id:int}")]
    public IActionResult GetGroupById(int id)
    {
        if (id <= 0)
            return BadRequest("Некорректный id группы"); // HTTP 400

        var group = _db.Groups.FirstOrDefault(g => g.Id == id);
        if (group is null)
            return NotFound(); // HTTP 404

        return Ok(group); // HTTP 200
    }

    // 3. POST: Создание новой группы (с защитой от дубликатов по имени Name)
    [HttpPost]
    public IActionResult CreateGroup([FromBody] Group group)
    {
        // Проверяем по свойству Name, нет ли уже группы с таким названием
        var groupExists = _db.Groups.Any(g => g.Name == group.Name);
        if (groupExists)
            return Conflict("Группа с таким названием уже существует"); // HTTP 409

        _db.Groups.Add(group);
        _db.SaveChanges(); // База SQLite сама сгенерирует новый уникальный ID!

        return Created(); // HTTP 201
    }

    // 4. PUT: Полное обновление данных группы
    [HttpPut("{id:int}")]
    public IActionResult UpdateGroup([FromRoute] int id, [FromBody] Group group)
    {
        if (id != group.Id)
            return BadRequest("id в пути и в теле запроса не совпадают"); // HTTP 400

        if (id <= 0)
            return BadRequest("Некорректный id группы"); // HTTP 400

        var exists = _db.Groups.Any(g => g.Id == id);
        if (!exists)
            return NotFound(); // HTTP 404

        // Проверяем, чтобы при переименовании мы случайно не заняли чужое существующее имя Name
        var nameTaken = _db.Groups.Any(g => g.Name == group.Name && g.Id != id);
        if (nameTaken)
            return Conflict("Группа с таким названием уже существует"); // HTTP 409

        _db.Groups.Update(group);
        _db.SaveChanges();

        return NoContent(); // HTTP 204
    }

    // 5. DELETE: Удаление группы из базы данных
    [HttpDelete("{id:int}")]
    public IActionResult DeleteGroup(int id)
    {
        var group = _db.Groups.Find(id);
        if (group is null)
            return NotFound(); // HTTP 404

        _db.Groups.Remove(group);
        _db.SaveChanges(); // Стираем строку из файла базы данных на диске намертво

        return NoContent(); // HTTP 204
    }
}
