using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Models;
using Interfaces.Services;

namespace DAOMock1.Services
{
    public class ProducerService : IProducerService
    {
        private List<IProducer> _producers;

        public ProducerService(List<IProducer> producers)
        {
            _producers = producers;
        }
        public void AddProducer(IProducer producer)
        {
            _producers.Add(producer);
        }

        public void DeleteProducer(IProducer producer)
        {
            _producers.Remove(producer);
        }

        public void UpdateProducer(IProducer producer)
        {
            var index = _producers.FindIndex(x => x.Id == producer.Id);
            if (index >= 0)
            {
                _producers[index] = producer;
            }
        }

        public int GetNewId(int id)
        {
            int? nextId = null;
            if (id == 0)
            {
                var lastId = _producers.OrderByDescending(x => x.Id).First()?.Id;
                nextId = lastId + 1 ?? 1;
            }

            return nextId ?? id;
        }
    }
}
