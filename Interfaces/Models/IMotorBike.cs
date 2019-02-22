using Core;

namespace Interfaces.Models
{
    public interface IMotorbike : IEntity
    {
        int ProductionYear { get; set; }
        IProducer Producer { get; set; }
        TransmissionType Transmission { get; set; }
    }
}
