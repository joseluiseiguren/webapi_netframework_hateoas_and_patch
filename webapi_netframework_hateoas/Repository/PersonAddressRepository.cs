using System;
using System.Collections.Generic;
using System.Linq;
using webapiexample.Interfaces;
using webapiexample.Models;

namespace webapiexample.Repository
{
    public class PersonAddressRepository : IPersonAddressRepository
    {
        public void Create(PersonAddress address)
        {
            address.Id = new Random().Next(100, int.MaxValue);

            var person = FakeDatabase.GetDatabase().Where(p => p.Id == address.PersonId).FirstOrDefault();

            person.Addresses.Add(address);
        }

        public void Delete(int id)
        {
            var lstAddress = (FakeDatabase.GetDatabase()
                                    .Where(p => p.Addresses.Any(a => a.Id == id))
                                    .Select(p => p.Addresses)
                                    .FirstOrDefault());

            var address = lstAddress.Where(p => p.Id == id).FirstOrDefault();

            lstAddress.Remove(address);
        }

        public IEnumerable<PersonAddress> GetByFilter(int? idPerson = null)
        {
            return FakeDatabase.GetDatabase()
                                    .Where(p => (idPerson == null || p.Id == idPerson.Value)) 
                                    .Select(p => p.Addresses)
                                    .FirstOrDefault();
        }

        public PersonAddress GetById(int personId, int addressId)
        {
            return (FakeDatabase.GetDatabase()
                                    .Where(p => p.Id == personId)
                                    .Where(p => p.Addresses.Any(a => a.Id == addressId))
                                    .Select(p => p.Addresses)
                                    .FirstOrDefault())?
                        .Where(p => p.Id == addressId).FirstOrDefault().Clone() as PersonAddress;            
                                    
        }

        public void Update(PersonAddress address)
        {
            var lstAddress = (FakeDatabase.GetDatabase()
                                    .Where(p => p.Addresses.Any(a => a.Id == address.Id))
                                    .Select(p => p.Addresses)
                                    .FirstOrDefault());

            var addressFromDb = lstAddress.Where(p => p.Id == address.Id).FirstOrDefault();
            if (addressFromDb == null)
            {
                return;
            }

            //TODO: use automapper
            addressFromDb.Number = address.Number;
            addressFromDb.Province = address.Province;
            addressFromDb.Street = address.Street;
        }
    }
}