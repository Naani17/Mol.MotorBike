using Interfaces;
using Interfaces.Models;
using System.Collections.Generic;
using DAOMock1.BO;
using DAOMock1.Services;
using Interfaces.Services;

namespace DAOMock1
{
    public class DB : IDAO
    {
        private readonly List<IProducer> _producers;
        private readonly List<IMotorbike> _motorbikes;
        private readonly IProducerService _producerService;
        private IMotorbikeService _motorbikeService;

        public DB()
        {
            _producers = new List<IProducer>()
            {
                new BO.Producent{ Id=1, Name="Opel", Country="USA" },
                new BO.Producent{ Id=2, Name="Fiat", Country="France" },
                new BO.Producent{ Id=3, Name="Volvo", Country="Germany" }
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

            _producerService = new ProducerService(_producers);
            _motorbikeService = new MotorbikeService(_motorbikes);


        }

        public IMotorbike CreateEmptyMotorBike() => new Motorbike();

        public IProducer CreateEmptyProducer() => new Producent();

        public IEnumerable<IMotorbike> GetAllMotorbike() => _motorbikes;

        public IEnumerable<IProducer> GetAllProducers() => _producers;

        public IProducerService ProducerService() => _producerService;

        public IMotorbikeService MotorbikeService() => _motorbikeService;
    }
}
