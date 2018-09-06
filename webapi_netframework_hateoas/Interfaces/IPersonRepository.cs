using System;
using System.Collections.Generic;
using webapiexample.Models;

namespace webapiexample.Interfaces
{
    interface IPersonRepository
    {
        IEnumerable<Person> GetByFilter(string name = null, DateTime? birthDate = null);

        Person GetById(int id);

        void Delete(int id);

        void Update(Person person);

        void Create(Person person);
    }
}
