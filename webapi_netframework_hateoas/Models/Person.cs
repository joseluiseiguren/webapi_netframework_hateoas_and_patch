﻿using System;
using System.Collections.Generic;

namespace webapiexample.Models
{
    public class Person : ICloneable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Birthdate { get; set; }

        public virtual List<PersonAddress> Addresses { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}