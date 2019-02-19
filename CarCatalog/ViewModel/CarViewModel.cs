using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalog.ViewModel
{
    public class CarViewModel : INotifyPropertyChanged, ICar
    {

        public CarViewModel(ICar car)
        {
            this.car = car;
        }

        private ICar car;

        public int ID
        {
            get => car.ID;
            set
            {
                car.ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public string Name
        {
            get => car.Name;
            set
            {
                car.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int ProductionYear
        {
            get => car.ProductionYear;
            set
            {
                car.ProductionYear = value;
                OnPropertyChanged(nameof(ProductionYear));
            }
        }

        public IProducer Producer
        {
            get => car.Producer;
            set
            {
                car.Producer = value;
                OnPropertyChanged(nameof(Producer));
            }
        }

        public Core.TransmissionType Transmission
        {
            get => car.Transmission;
            set
            {
                car.Transmission = value;
                OnPropertyChanged(nameof(Transmission));
            }
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged( string propertyName)
        {
            if ( PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
