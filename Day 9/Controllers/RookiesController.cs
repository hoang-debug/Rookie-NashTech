using Microsoft.AspNetCore.Mvc;
using Day_9.Models;
using System.Globalization;
using Day_9.Services;
using Day_9.Common;

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
    // public IEnumerable<Person> GetAll()
    // {
    //     return _personService.GetAll().AsEnumerable();
    // }

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
        if (!_personService.Exits(id)) return NotFound();

        _personService.Remove(id);
        return Ok();
    }

    // [HttpGet]
    // [Route("filter/gender/{Gender}")]
    // public List<Person> FilterByGender(string Gender)
    // {
    //     var filteredResults = new List<Person>();
    //     filteredResults = _personService.FilterByGender(Gender);

    //     return filteredResults;
    // }

    // [HttpGet]
    // [Route("filter/full-name/{FullName}")]
    // public List<Person> FilterByFullName(string FullName)
    // {
    //     var filteredResults = new List<Person>();
    //     filteredResults = _personService.FilterByFullName(FullName);

    //     return filteredResults;
    // }

    // [HttpGet]
    // [Route("filter/birth-place/{BirthPlace}")]
    // public List<Person> FilterByBirthPlace(string BirthPlace)
    // {
    //     var filteredResults = new List<Person>();
    //     filteredResults = _personService.FilterByBirthPlace(BirthPlace);
    //     return filteredResults;
    // }    

    [HttpGet("filter-by-name")]
    public List<Person> FilterByName(string Name)
    {
        var people = _personService.GetAll();
        var result = from person in people
                     where (person.FirstName != null && person.FirstName.Contains(Name, StringComparison.CurrentCulture)) || person.LastName != null && person.LastName.Contains(Name, StringComparison.CurrentCulture)
                     select person;
        return result.ToList();
    }

    [HttpGet("filter-by-gender")]
    public List<Person> FilterByGender(string gender)
    {
        var people = _personService.GetAll();
        var result = from person in people
                     where person.Gender != null && person.Gender.Equals(gender, StringComparison.CurrentCultureIgnoreCase)
                     select person;
        return result.ToList();
    }

    [HttpGet("filter-by-birth-place")]
    public List<Person> FilterByBirthPlace(string birthPlace)
    {
        var people = _personService.GetAll();
        var result = from person in people
                     where person.BirthPlace != null && person.BirthPlace.Equals(birthPlace, StringComparison.CurrentCultureIgnoreCase)
                     select person;
        return result.ToList();
    }

    [HttpGet]
    [Route("filter-general")]
    public List<Person> GetAll(string? name, string? gender, string? birthPlace)
    {
        var people = _personService.GetAll();
        Func<Person, bool> predicate = x => true;

        if (!string.IsNullOrEmpty(name))
        {
            Func<Person, bool> filterByName = x => (x.FirstName != null && x.FirstName.
            Contains(name, StringComparison.CurrentCulture)) || x.LastName != null && x.LastName.
            Contains(name, StringComparison.CurrentCulture);
            predicate = predicate.And(filterByName);
        }

        if (!string.IsNullOrEmpty(gender))
        {
            Func<Person, bool> filterByGender = x => (x.Gender != null && x.Gender.
            Equals(gender, StringComparison.CurrentCultureIgnoreCase));
            
            predicate = predicate.And(filterByGender);
        }

        if (!string.IsNullOrEmpty(birthPlace))
        {
            Func<Person, bool> filterByBirthPlace = x => (x.BirthPlace != null && x.BirthPlace.
            Equals(birthPlace, StringComparison.CurrentCultureIgnoreCase));
            
            predicate = predicate.And(filterByBirthPlace);
        }

        var results = people.Where(predicate);
        return results.ToList();
    }
}
