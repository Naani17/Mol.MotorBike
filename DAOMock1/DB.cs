using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOMock1
{
    public class DB : IDAO
    {
        private List<IProducer> _producers;
        private List<ICar> _cars;

        public DB()
        {
            _producers = new List<IProducer>()
            {
                new BO.Producent{ ID=1, Name="Opel", Founded=1940 },
                new BO.Producent{ ID=2, Name="Fiat", Founded=1950 },
                new BO.Producent{ ID=3, Name="Volvo", Founded=1930}
            };

            _cars = new List<ICar>()
            {
                new BO.Motorbike
                {
                    ID =1,
                    Name ="Corsa",
                    Producer = _producers[0],
                    ProductionYear =1999,
                    Transmission =Core.TransmissionType.Automatic
                },
                new BO.Motorbike
                {
                    ID =2,
                    Name ="Astra",
                    Producer = _producers[0],
                    ProductionYear =2009,
                    Transmission =Core.TransmissionType.Automatic
                },
                new BO.Motorbike
                {
                    ID =3,
                    Name ="Panda",
                    Producer = _producers[1],
                    ProductionYear =2000,
                    Transmission =Core.TransmissionType.Manual
                },
                new BO.Motorbike
                {
                    ID =4,
                    Name ="Multipla",
                    Producer = _producers[0],
                    ProductionYear = 2007,
                    Transmission =Core.TransmissionType.Manual
                },
                new BO.Motorbike
                {
                    ID =5,
                    Name ="S40",
                    Producer = _producers[2],
                    ProductionYear =2002,
                    Transmission =Core.TransmissionType.Automatic
                }


            };
        }

        public ICar CreateEmptyCar()
        {
            return new BO.Motorbike();
        }

        public IEnumerable<ICar> GetAllCars()
        {
            return _cars;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return _producers;
        }
    }
}
