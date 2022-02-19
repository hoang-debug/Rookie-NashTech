using Microsoft.AspNetCore.Mvc;
using Day_9.Models;
using System.Globalization;
using Day_9.Services;

namespace Day_9.Controllers;

[ApiController]
[Route("[controller]")]
public class RookiesController : ControllerBase
{
    private readonly IPersonService _personService;

    private readonly ILogger<RookiesController> _logger;

    public RookiesController(ILogger<RookiesController> logger, IPersonService personService)
    {
        _logger = logger;
        _personService = personService;
    }

    [HttpGet]
    public IEnumerable<Person> GetAll()
    {
       return _personService.GetAll().AsEnumerable();
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetOne(Guid id)
    {
        var person = _personService.GetOne(id);
        if (person == null) return NotFound();

        return new JsonResult(person);
    }

    [HttpPost]
    public Person Add(PersonCreateModel model)
    {
        var person = new Person
        {
            Id = Guid.NewGuid(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            Gender = model.Gender,
            DateOfBirth = model.DateOfBirth,
            PhoneNumber = model.PhoneNumber,
            BirthPlace = model.BirthPlace
        };
        return _personService.Add(person);
    }

    [HttpDelete]
    [Route("{id:guid}")]

    public IActionResult Delete(Guid id)
    {
        if(!_personService.Exits(id)) return NotFound();

        _personService.Remove(id);
        return Ok();
    }

}
