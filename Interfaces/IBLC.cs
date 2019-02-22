using Interfaces.Models;
using System.Collections.Generic;
using Interfaces.Services;

namespace Interfaces
{
    public interface IBLC
    {
        IEnumerable<IMotorbike> GetMotorbike();
        IEnumerable<IProducer> GetProducers();
        IMotorbike CreateEmptyMotorBike();
        IProducer CreateEmptyProducer();
        IProducerService ProducerService();
        IMotorbikeService MotorbikeService();
    }
}
