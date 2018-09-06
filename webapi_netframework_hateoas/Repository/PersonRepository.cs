using System;
using System.Collections.Generic;
using System.Linq;
using webapiexample.Interfaces;
using webapiexample.Models;

namespace webapiexample.Repository
{
    public class PersonRepository : IPersonRepository
    {
        public IEnumerable<Person> GetByFilter(string name = null, DateTime? birthDate = null)
        {
            //TODO: Apply the filters
            return FakeDatabase.GetDatabase().Select(p => p.Clone() as Person);
        }

        public void Delete(int id)
        {
            var person = FakeDatabase.GetDatabase().Where(p => p.Id == id).FirstOrDefault();
            if (person == null)
            {
                return;
            }

            FakeDatabase.GetDatabase().Remove(person);
        }

        public void Update(Person person)
        {
            var personfromDB = FakeDatabase.GetDatabase().Where(p => p.Id == person.Id).FirstOrDefault();
            if (personfromDB == null)
            {
                return;
            }

            //TODO: use automapper
            personfromDB.Addresses = person.Addresses;
            personfromDB.Birthdate = person.Birthdate;
            personfromDB.Id = person.Id;
            personfromDB.Name = person.Name;
        }

        public Person GetById(int id)
        {
            return FakeDatabase.GetDatabase().Where(p => p.Id == id).FirstOrDefault()?.Clone() as Person;
        }

        public void Create(Person person)
        {
            person.Id = FakeDatabase.GetDatabase().Count() + 1;
            FakeDatabase.GetDatabase().Add(person);            
        }
    }
}