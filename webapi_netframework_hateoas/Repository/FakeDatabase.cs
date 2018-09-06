using System;
using System.Collections.Generic;
using webapiexample.Models;

namespace webapiexample.Repository
{
    public class FakeDatabase
    {
        private static IList<Person> _lstPersons;

        public static IList<Person> GetDatabase()
        {
            if (_lstPersons != null)
            {
                return _lstPersons;
            }

            _lstPersons = new List<Person>();
            
            _lstPersons.Add(new Person()
            {
                Birthdate = new DateTime(1980, 5, 13),
                Id = 1,
                Name = "Pedro",
                Addresses = new List<PersonAddress>()
            });

            _lstPersons[0].Addresses.Add(new PersonAddress()
            {
                Id = 1,
                Number = 100,
                Province = "Bcn",
                Street = "Felip II",
                PersonId = 1
            });

            _lstPersons[0].Addresses.Add(new PersonAddress()
            {
                Id = 2,
                Number = 200,
                Province = "Bcn",
                Street = "Alvaro",
                PersonId = 1
            });



            _lstPersons.Add(new Person()
            {
                Birthdate = new DateTime(1984, 10, 2),
                Id = 2,
                Name = "Martin",
                Addresses = new List<PersonAddress>()
            });

            _lstPersons[1].Addresses.Add(new PersonAddress()
            {
                Id = 3,
                Number = 52,
                Province = "BdsAs",
                Street = "Meridiana",
                PersonId = 2
            });



            _lstPersons.Add(new Person()
            {
                Birthdate = new DateTime(2001, 1, 30),
                Id = 3,
                Name = "Juan",
                Addresses = new List<PersonAddress>()
            });

            _lstPersons[2].Addresses.Add(new PersonAddress()
            {
                Id = 4,
                Number = 856,
                Province = "Ros",
                Street = "Marina",
                PersonId = 3
            });

            _lstPersons[2].Addresses.Add(new PersonAddress()
            {
                Id = 5,
                Number = 1002,
                Province = "Bur",
                Street = "Pamplona",
                PersonId = 3
            });

            return _lstPersons;
        }
    }
}