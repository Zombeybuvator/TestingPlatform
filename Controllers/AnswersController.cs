using Microsoft.AspNetCore.Mvc;

namespace TestingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnswersController: ControllerBase
{
    [HttpGet]
    public IActionResult GetAllAnswer() => Ok("Список всех ответов");

    [HttpGet("{id}")]
    public IActionResult GetAnswerById(int id)
    {
        if (id == 1) return Ok("Ответ 1");
        return NotFound();
    }

    [HttpGet("by-question/{questionId:int}")]
    public IActionResult GetAnswersQuestionId(int questionId) 
        => Ok($"Ответы для теста {questionId}");

    [HttpPost]
    public IActionResult CreateAnswer() => Created("/api/answers/1", "Ответ создан");

    [HttpPut("{id}")]
    public IActionResult UpdateAnswer(int id) => NoContent();

    [HttpDelete("{id}")]
    public IActionResult DeleteAnswer(int id) => NoContent();
}
