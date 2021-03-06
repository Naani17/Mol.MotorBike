﻿using Interfaces.Models;

namespace DAOMock1.BO
{
    public class Producent : IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return $"Nazwa: {Name} ";
        }
    }
}
