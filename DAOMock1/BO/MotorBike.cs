using Core;
using Interfaces.Models;

namespace DAOMock1.BO
{
    public class Motorbike : IMotorbike
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductionYear { get; set; }
        public IProducer Producer { get; set; }
        public TransmissionType Transmission { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} - {ProductionYear} [ {Producer.Name} ] {Transmission} ";
        }
    }
}
