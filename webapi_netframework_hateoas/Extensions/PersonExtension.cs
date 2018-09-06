using webapiexample.Dto;
using webapiexample.Models;

namespace webapiexample.Extensions
{
    public static class PersonExtension
    {
        public static PersonDtoResponse ToPersonDto(this Person person)
        {
            var result = new PersonDtoResponse()
            {
                Birthdate = person.Birthdate,
                Id = person.Id,
                Name = person.Name
            };

            return result;
        }
    }
}