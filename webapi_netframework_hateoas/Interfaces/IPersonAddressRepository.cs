using System.Collections.Generic;
using webapiexample.Models;

namespace webapiexample.Interfaces
{
    public interface IPersonAddressRepository
    {
        IEnumerable<PersonAddress> GetByFilter(int? idPerson = null);

        PersonAddress GetById(int personId, int addressId);

        void Delete(int id);

        void Update(PersonAddress address);

        void Create(PersonAddress address);
    }
}