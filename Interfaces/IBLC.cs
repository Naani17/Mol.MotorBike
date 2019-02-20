using Interfaces.Models;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IBLC
    {
        IEnumerable<IMotorbike> GetMotorbike();

        IEnumerable<IProducer> GetProducers();

        IMotorbike CreateEmptyMotorBike();
    }
}
