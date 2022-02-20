using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day_9.Models;
using Person = Day_9.Models.Person;


namespace Day_9.Services
{
    public class PersonService : IPersonService
    {
        private static List<Person> _people = new List<Person>
        {
            new Person
            {
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
                FirstName = "Linh",
                LastName = "Nguyen",
                Gender = "Female",
                DateOfBirth = new DateTime(1996, 1, 27),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            }
        };
        public List<Person> GetAll()
        {
            return _people;
        }
        public Person? GetOne(Guid id)
        {
            return _people.FirstOrDefault(o => o.Id == id);
        }
        public Person Add(Person person)
        {
            _people.Add(person);
            return person;
        }
        public Person? Edit(Person person)
        {
            var current = _people.FirstOrDefault(d => d.Id == person.Id);
            if (current == null) return null;

            current.FirstName = person.FirstName;
            current.LastName = person.LastName;
            current.Gender = person.Gender;
            current.DateOfBirth = person.DateOfBirth;
            current.BirthPlace = person.BirthPlace;

            return current;
        }
        public void Remove(Guid id)
        {
            var current = _people.FirstOrDefault(o => o.Id == id);
            if (current != null)
            {
                _people.Remove(current);
            }
        }
        public bool Exits(Guid id)
        {
            return _people.Any(d => d.Id == id);
        }
        public List<Person> FilterByGender(string Gender)
        {
            var results = _people
               .Where(x => x.Gender?.ToLower() == Gender.ToLower())
               .ToList();
           return results;
        }

        public List<Person> FilterByFullName(string FullName)
        {
            var results = GetAll()
               .Where(x => x.FullName?.ToLower() == FullName.ToLower())
               .ToList();
           return results;
        }

        public List<Person> FilterByBirthPlace(string BirthPlace)
        {
            var results = GetAll()
               .Where(x => x.BirthPlace?.ToLower() == BirthPlace.ToLower())
               .ToList();
           return results;
        }
    }
}