using Interfaces;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using Interfaces.Services;

namespace BLC
{
    public class BLC : IBLC
    {
        private readonly IDAO _dao;

        public BLC(string dbName)
        {
            var a = Assembly.UnsafeLoadFrom(dbName);

            Type daoToCreate = null;
            var daoType = typeof(IDAO);

            foreach (var t in a.GetTypes())
            {
                foreach (var i in t.GetInterfaces())
                {
                    if (i == daoType)
                    {
                        daoToCreate = t;
                    }
                }
            }
            _dao = (IDAO)Activator.CreateInstance(daoToCreate ?? throw new InvalidOperationException(), new object[] { });
        }

        public IEnumerable<IMotorbike> GetMotorbike() => _dao.GetAllMotorbike();

        public IEnumerable<IProducer> GetProducers() => _dao.GetAllProducers();

        public IMotorbike CreateEmptyMotorBike() => _dao.CreateEmptyMotorBike();

        public IProducer CreateEmptyProducer() => _dao.CreateEmptyProducer();

        public IProducerService ProducerService() => _dao.ProducerService();

        public IMotorbikeService MotorbikeService() => _dao.MotorbikeService();
    }
}
