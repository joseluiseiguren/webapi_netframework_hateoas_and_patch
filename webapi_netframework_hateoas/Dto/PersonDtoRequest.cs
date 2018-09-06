using System;

namespace webapiexample.Dto
{
    //this class is used to receive post, put and patch
    public class PersonDtoRequest
    {
        public string Name { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}