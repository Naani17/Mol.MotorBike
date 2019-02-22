using Interfaces.Models;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDAO
    {
        IMotorbike CreateEmptyMotorBike();

        IProducer CreateEmptyProducer();

        IEnumerable<IMotorbike> GetAllMotorbike();

        IEnumerable<IProducer> GetAllProducers();

        IProducerService ProducerService();

        IMotorbikeService MotorbikeService();


    }
}
