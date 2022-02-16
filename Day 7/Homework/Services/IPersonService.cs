using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using D5.Models;

namespace Homework.Services
{
    public interface IPersonService
    {
        List<Person> GetAll();
        Person GetOne(int index);
        void Create(Person person);
        void Update(int index, Person person);
        void Delete(int index);
    }
}