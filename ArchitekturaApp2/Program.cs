using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitekturaApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Properties.Settings sett = new Properties.Settings();
            BLC.BLC blc = new BLC.BLC( sett.dbNameConf);

            foreach (var prod in blc.GetProducers())
            {
                Console.WriteLine(prod);
            }

            Console.WriteLine("####################");
            foreach (var car in blc.GetCars())
            {
                Console.WriteLine(car);
            }

        }
    }
}
