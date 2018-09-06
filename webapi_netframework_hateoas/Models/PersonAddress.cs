using System;

namespace webapiexample.Models
{
    public class PersonAddress : ICloneable
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }

        public string Province { get; set; }

        public virtual int PersonId { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}