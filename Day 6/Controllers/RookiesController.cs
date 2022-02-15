using Microsoft.AspNetCore.Mvc;
using D5.Models;
using System.Globalization;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace D5.Controllers;
// [Route("")]
// [Route("NashTech")]
public class RookiesController : Controller
{
    static List<Person> people = new List<Person>
        {
            new Person
            {
                FirstName = "Phuong",
                LastName = "Nguyen Nam",
                Gender = "Male",
                DateOfBirth = new DateTime(2001, 1, 22),
                PhoneNumber = "",
                BirthPlace = "Phu Tho",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Nam",
                LastName = "Nguyen Thanh",
                Gender = "Male",
                DateOfBirth = new DateTime(2001, 1, 20),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Son",
                LastName = "Do Hong",
                Gender = "Male",
                DateOfBirth = new DateTime(2000, 11, 6),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Huy",
                LastName = "Nguyen Duc",
                Gender = "Male",
                DateOfBirth = new DateTime(1996, 1, 26),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Hoang",
                LastName = "Phuong Viet",
                Gender = "Male",
                DateOfBirth = new DateTime(1999, 2, 5),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Long",
                LastName = "Lai Quoc",
                Gender = "Male",
                DateOfBirth = new DateTime(1997, 5, 30),
                PhoneNumber = "",
                BirthPlace = "Bac Giang",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Thanh",
                LastName = "Tran Chi",
                Gender = "Male",
                DateOfBirth = new DateTime(2000, 9, 18),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
             new Person
            {
                FirstName = "Manh",
                LastName = "Le Duc",
                Gender = "Male",
                DateOfBirth = new DateTime(2001, 4, 22),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
             new Person
            {
                FirstName = "Dung",
                LastName = "Dao Tan",
                Gender = "Male",
                DateOfBirth = new DateTime(2000, 12, 7),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Linh",
                LastName = "Nguyen",
                Gender = "Female",
                DateOfBirth = new DateTime(1996, 1, 27),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            }
        };
    // [Route("rookies")]
    public IActionResult Index()
    {
        return View(people);
    }


    [Route("rookies/male")]
    [Route("rookies/male-members")]
    public IActionResult GetMaleMembers()
    {
        var results = from person in people where person.Gender == "Male" select person;
        // return new ObjectResult(results);
        return Json(results);
    }

    [Route("rookies/oldest")]
    public IActionResult GetOldestMember()
    {
        var maxAge = people.Max(m => m.Age);
        var oldest = people.First(m => m.Age == maxAge);
        // return new ObjectResult(oldest);
        return Json(oldest);
    }

    [Route("rookies/full-name")]
    public IActionResult GetFullName()
    {
        var fullNames = people.Select(m => m.FullName);
        return Json(fullNames);
    }

    [Route("rookies/split-to-group-by-birth-year/")]
    public IActionResult SplitMemberByBirthYear(int year)
    {
        var results = from person in people
                      group person by person.DateOfBirth.Year.CompareTo(year) into grp
                      select new
                      {
                          Key = grp.Key switch
                          {
                              -1 => $"Birth year less than {year}",
                              0 => $"Birth year equal to {year}",
                              1 => $"Birth year greater than {year}",
                              _ => string.Empty
                          },
                          Data = grp.ToList()
                      };

        return Json(results);
    }

    public IActionResult Export()
    {
        var buffer = WriteCsvToMemory(people);
        var memoryStream = new MemoryStream(buffer);
        return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "export.csv" };
    }

    public byte[] WriteCsvToMemory(List<Person> data)
    {
        using (var stream = new MemoryStream())
        using (var writer = new StreamWriter(stream))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            var options = new TypeConverterOptions { Formats = new[] { "dd/MM/yyy" } };
            csvWriter.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);

            csvWriter.WriteRecords(data);
            writer.Flush();
            return stream.ToArray();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Person model)
    {
        if (!ModelState.IsValid) return View();
        people.Add(model);
        return RedirectToAction("Index");
    }
    public IActionResult Edit(int index)
    {
        if (index <= 0 && index > people.Count)
            return RedirectToAction("Index");
        var person = people[index - 1];
        var model = new PersonEditModel(person);
        model.Index = index;
        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(PersonEditModel model)
    {
        if (!ModelState.IsValid) return View();
        people[model.Index - 1] = model;
        return RedirectToAction("Index");
    }

    // [HttpPost]
    public IActionResult Delete(int index)
    {
        if (index <= 0 && index > people.Count)
            return RedirectToAction("Index");
        people.RemoveAt(index-1);
        return RedirectToAction("Index");
    }
}