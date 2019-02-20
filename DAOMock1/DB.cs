using Interfaces;
using Interfaces.Models;
using System.Collections.Generic;

namespace DAOMock1
{
    public class DB : IDAO
    {
        private List<IProducer> _producers;
        private List<IMotorbike> _motorbikes;

        public DB()
        {
            _producers = new List<IProducer>()
            {
                new BO.Producent{ ID=1, Name="Opel", Country="USA" },
                new BO.Producent{ ID=2, Name="Fiat", Country="France" },
                new BO.Producent{ ID=3, Name="Volvo", Country="Germany" }
            };

            _motorbikes = new List<IMotorbike>()
            {
                new BO.Motorbike
                {
                    Id =1,
                    Name ="Corsa",
                    Producer = _producers[0],
                    ProductionYear =1999,
                    Transmission =Core.TransmissionType.Automatic
                },
                new BO.Motorbike
                {
                    Id =2,
                    Name ="Astra",
                    Producer = _producers[0],
                    ProductionYear =2009,
                    Transmission =Core.TransmissionType.Automatic
                },
                new BO.Motorbike
                {
                    Id =3,
                    Name ="Panda",
                    Producer = _producers[1],
                    ProductionYear =2000,
                    Transmission =Core.TransmissionType.Manual
                },
                new BO.Motorbike
                {
                    Id =4,
                    Name ="Multipla",
                    Producer = _producers[0],
                    ProductionYear = 2007,
                    Transmission =Core.TransmissionType.Manual
                },
                new BO.Motorbike
                {
                    Id =5,
                    Name ="S40",
                    Producer = _producers[2],
                    ProductionYear =2002,
                    Transmission =Core.TransmissionType.Automatic
                }
            };
        }

        public IMotorbike CreateEmptyMotorBike()
        {
            return new BO.Motorbike();
        }

        public IEnumerable<IMotorbike> GetAllCars()
        {
            return _motorbikes;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return _producers;
        }
    }
}
