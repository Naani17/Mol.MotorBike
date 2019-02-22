using Interfaces.Models;

namespace Interfaces.Services
{
    public interface IProducerService
    {
        void AddProducer(IProducer producer);
        void DeleteProducer(IProducer producer);
        void UpdateProducer(IProducer producer);
        int GetNewId(int id);
    }
}
