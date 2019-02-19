namespace Interfaces.Models
{
    public interface IMotorbike : IEntity
    {
        int ProductionYear { get; set; }
        IProducer Producer { get; set; }
        Core.TransmissionType Transmission { get; set; }
    }
}
