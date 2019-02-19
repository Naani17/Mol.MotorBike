using Interfaces.Models;

namespace DAOMock1.BO
{
    public class Producent : IProducer
    {
        public int ID { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortCutName { get; set; }

        public override string ToString()
        {
            return $"Nazwa: {Name} ";
        }
    }
}
