using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAOMock1.BO;
using Interfaces.Models;
using Interfaces.Services;

namespace DAOMock1.Services
{
    public class MotorbikeService: IMotorbikeService
    {
        private List<IMotorbike> _motorbikes;
        public MotorbikeService(List<IMotorbike> motorbikes)
        {
            _motorbikes = motorbikes;
        }
        public void AddMotorbike(IMotorbike motorbike)
        {
            _motorbikes.Add(motorbike);
        }

        public void DeleteMotorbike(IMotorbike motorbike)
        {
            _motorbikes.Remove(motorbike);
        }

        public void UpdateMotorbike(IMotorbike motorbike)
        {
            var index = _motorbikes.FindIndex(x => x.Id == motorbike.Id);
            if (index >= 0)
            {
                _motorbikes[index] = motorbike;
            }
        }

        public int GetNewId(int id)
        {
            int? nextId = null;
            if (id == 0)
            {
                var lastId = _motorbikes.OrderByDescending(x => x.Id).First()?.Id;
                nextId = lastId + 1 ?? 1;
            }

            return nextId ?? id;
        }
    }
}
