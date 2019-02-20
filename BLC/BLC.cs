using Interfaces;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BLC
{
    public class BLC : IBLC
    {
        private IDAO dao;

        public BLC(string dbName)
        {
            Assembly a = Assembly.UnsafeLoadFrom(dbName);

            Type daoToCreate = null;
            Type daoType = typeof(IDAO);

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
            dao = (IDAO)Activator.CreateInstance(daoToCreate, new object[] { });
        }

        public IEnumerable<IMotorbike> GetMotorbike()
        {
            return dao.GetAllCars();
        }

        public IEnumerable<IProducer> GetProducers()
        {
            return dao.GetAllProducers();
        }

        public IMotorbike CreateEmptyMotorBike()
        {
            return dao.CreateEmptyMotorBike();
        }
    }
}
