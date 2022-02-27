using D5.Controllers;
using D5.Models;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC.Tests;

public class RookiesControllerTests
{
    private Mock<ILogger<RookiesController>>? _loggerMock;
    private Mock<IPersonService>? _personServiceMock;
    static List<Person> _people = new List<Person>
    {
        new Person
            {
                FirstName = "Hoang 01",
                LastName = "Phuong Viet",
                Gender = "Male",
                DateOfBirth = new DateTime(1999, 2, 5),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Hoang 02",
                LastName = "Phuong Viet",
                Gender = "Male",
                DateOfBirth = new DateTime(1999, 2, 5),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Hoang 03",
                LastName = "Phuong Viet",
                Gender = "Male",
                DateOfBirth = new DateTime(1999, 2, 5),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
    };
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<RookiesController>>();
        _personServiceMock = new Mock<IPersonService>();
        // Setup
        _personServiceMock.Setup(x => x.GetAll()).Returns(_people);
    }

    [Test]
    public void Index_ReturnsViewResult_WithAListOfPeople()
    {

        // Arrange
        var controller = new RookiesController(_loggerMock.Object, _personServiceMock.Object);
        var expectedCount = _people.Count;
        // Act
        var result = controller.Index();
        //Assert
        Assert.IsInstanceOf<ViewResult>(result, "Invalid return type.");

        var view = (ViewResult)result;
        Assert.IsAssignableFrom<List<Person>>(view.ViewData.Model, "Invalid view data model");

        var model = view.ViewData.Model as List<Person>;
        Assert.IsNotNull(model, "View data model must not be null");
        Assert.AreEqual(expectedCount, model?.Count, "Model count is not equal to expected count.");

        // var firstPerson = model?.First();
        // Assert.AreEqual("Phuong Viet Hoang 01", firstPerson.FullName, "Fullname is not equals!!!");
    }

    [Test]
    public void Detail_ValidIndex_ReturnViewResult_WithAPersonModel()
    {
        // Setup
        const int index = 2;
        _personServiceMock.Setup(x => x.GetOne(index)).Returns(_people[index - 1]);
        var expected = _people[index - 1];
        // Arrange
        var controller = new RookiesController(_loggerMock.Object, _personServiceMock.Object);
        // Act
        var result = controller.Detail(index);
        //Assert
        Assert.IsInstanceOf<ViewResult>(result, "Invalid return type.");

        var view = (ViewResult)result;
        Assert.IsAssignableFrom<Person>(view.ViewData.Model, "Invalid view data model");

        var model = view.ViewData.Model as Person;
        Assert.IsNotNull(model, "View data model must not be null");
        Assert.AreEqual(expected, model, "Model is not equal to expected.");

    }

    [Test]
    public void Detail_InvalidIndex_ReturnNotFoundObjectResult_WithStringMessage()
    {
        // Setup
        const int index = 6;
        const string message = "Index out of range.";
        _personServiceMock.Setup(x => x.GetOne(index)).Throws(new IndexOutOfRangeException(message));
        // var expected = _people[index-1];
        // Arrange
        var controller = new RookiesController(_loggerMock.Object, _personServiceMock.Object);
        // Act
        var result = controller.Detail(index);
        //Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result, "Invalid return type.");
        var view = result as NotFoundObjectResult;
        Assert.IsNotNull(view, "View must not be null");

        Assert.IsInstanceOf<string>(view?.Value, "Invalid data type.");

        Assert.AreEqual(message, view.Value?.ToString(), "Not equals!");
    }

    [Test]
    public void Detail_InvalidIndex_ThrowsException()
    {
        // Setup
        const int index = -2;
        const string message = "Index must be greater than zero.";
        _personServiceMock.Setup(x => x.GetOne(index)).Throws(new ArgumentException(message));
        // var expected = _people[index-1];
        // Arrange
        var controller = new RookiesController(_loggerMock.Object, _personServiceMock.Object);
        // Act
        // var result = controller.Detail(index);
        //Assert
        var exception = Assert.Throws<ArgumentException>(() => controller.Detail(index));
        Assert.IsNotNull(exception, "Exception must not be null");
        Assert.AreEqual(message, exception.Message, "Not equal.");
    }

    [Test]
    public void Create_InvalidModel_ReturnView_WithErrorInModelState()
    {
        const string key = "ERROR";
        const string message = "Invalid Model!!!";
        // Arrange
        var controller = new RookiesController(_loggerMock.Object, _personServiceMock.Object);
        controller.ModelState.AddModelError(key, message);

        // Act
        var result = controller.Create(null);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result, "Invalid return type.");
        var view = (ViewResult)result;
        var modelState = view.ViewData.ModelState;

        Assert.IsFalse(modelState.IsValid, "Invalid model state.");
        Assert.AreEqual(1, modelState.ErrorCount, "");
        modelState.TryGetValue(key, out var value);
        var error = value.Errors.First().ErrorMessage;
        Assert.AreEqual(message, error);
    }

    [Test]
    public void Create_ValidModel_ReturnView_WithErrorInModelState()
    {
        // Arrange
        var person = new Person
        {
            FirstName = "Hoang 04",
            LastName = "Phuong Viet",
            Gender = "Male",
            DateOfBirth = new DateTime(1999, 2, 5),
            PhoneNumber = "",
            BirthPlace = "Ha Noi",
            IsGraduated = false
        };
        _personServiceMock.Setup(x => x.Create(person)).Callback<Person>((Person p) =>
        {
            _people.Add(p);
        });
        var controller = new RookiesController(_loggerMock.Object, _personServiceMock.Object);
        var expected = _people.Count +1;
        // Act
        var result = controller.Create(person);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result, "Invalid return type.");
        var view = (RedirectToActionResult)result;
        Assert.AreEqual("Index", view.ActionName, "Invalid action name.");
        var actual = _people.Count;
        Assert.AreEqual(expected, actual, "Error");
        Assert.AreEqual(person, _people.Last(), "Not equal");
    }
    [TearDown]
    public void TearDown()
    {
        // _loggerMock = null;
        // _personServiceMock = null;
    }
}