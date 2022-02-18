using Microsoft.AspNetCore.Mvc;
using D5.Models;
using System.Globalization;
using Homework.Services;

namespace D5.Controllers;
// [Route("")]
// [Route("NashTech")]
public class RookiesController : Controller
{
    private readonly IPersonService _personService;
    public RookiesController(IPersonService personService)
    {
        _personService = personService;
    }
    // [Route("rookies")]
    public IActionResult Index()
    {
        var people = _personService.GetAll();
        return View(people);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Person model)
    {
        if (!ModelState.IsValid) return View();
        _personService.Create(model);
        return RedirectToAction("Index");
    }
   

    
     public IActionResult Edit(int index)
    {
        try
        {
            var person = _personService.GetOne(index);
            ViewBag.PersonIndex = index;
            return View(person);
        }
        catch (System.Exception)
        {
            return RedirectToAction("Index");
        }
    }
    [HttpPost]
    public IActionResult Edit(int index, Person model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.PersonIndex = index;
        }
        _personService.Update(index, model);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int index)
    {
        try
        {
            _personService.Delete(index);
        }
        catch (System.Exception)
        {

        }
        return RedirectToAction("Index");
    }
    public IActionResult Detail(int index)
    {
        try
        {
            var person = _personService.GetOne(index);
            ViewBag.PersonIndex = index;
            return View(person);
        }
        catch (System.Exception)
        {
            return RedirectToAction("Index");
        }
    }
    [HttpPost]
    public IActionResult DeleteWithResult(int index)
    {
        // var deletedUserName = string.Empty;
        try
        {
            var person = _personService.GetOne(index);
            // deletedUserName = person.FullName;
            HttpContext.Session.SetString("DELETED_USER_NAME", person.FullName);
            _personService.Delete(index);
        }
        catch (System.Exception)
        {

        }
        return View("Result");
    }
    public IActionResult Result()
    {
            var deletedUserName = HttpContext.Session.GetString("DELETED_USER_NAME");
            ViewBag.DeletedUserName = deletedUserName;
            return View();
    }
}