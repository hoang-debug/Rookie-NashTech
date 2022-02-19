using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Day_9.Models;
using Person = Day_9.Models.Person;

namespace Day_9.Services
{
    public interface IPersonService
    {
        List<Person> GetAll();
        Person? GetOne(Guid id);
        Person Add(Person person);
        Person? Edit (Person person);
        void Remove(Guid id);
        bool Exits(Guid id);
        
    }
}