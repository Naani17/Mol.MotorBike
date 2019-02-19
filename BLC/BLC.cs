using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLC
{
    public class BLC
    {
        private IDAO dao;

        public BLC( string dbName)
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

        public IEnumerable<ICar> GetCars()
        {
            return dao.GetAllCars();
        }

        public IEnumerable<IProducer> GetProducers()
        {
            return dao.GetAllProducers();
        }

        public ICar CreateEmptyCar()
        {
            return dao.CreateEmptyCar();
        }
    }
}
