using System;
using webapiexample.Hatetoas;

namespace webapiexample.Dto
{
    public class PersonDtoResponse : LinkHelper
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}