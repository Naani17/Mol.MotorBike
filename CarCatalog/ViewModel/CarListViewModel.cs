using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalog.ViewModel
{
    public class CarListViewModel : INotifyPropertyChanged
    {
        private BLC.BLC blc;

        private ObservableCollection<ICar> cars;

        public ObservableCollection<ICar> Cars
        {
            get => cars;
            set
            {
                cars = value;
                OnPropertyChanged(nameof(Cars));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CarListViewModel()
        {
            Properties.Settings sett = new Properties.Settings();
            cars = new ObservableCollection<ICar>();
            blc = new BLC.BLC(sett.DBNameConf);

            GetAllCars();
            createEmptyCarCommand = new RelayCommand(new Action<object>(this.CreateEmptyCar));
            producers = new ObservableCollection<IProducer>(blc.GetProducers());
        }

        private void GetAllCars()
        {
            foreach( var c in blc.GetCars())
            {
                cars.Add(new CarViewModel(c));
            }
        }

        private void CreateEmptyCar(object o)
        {
            ICar car = blc.CreateEmptyCar();
            CarViewModel cvm = new CarViewModel(car);
            cars.Add(cvm);
            SelectedCar = cvm;
        }

        private RelayCommand createEmptyCarCommand;

        public RelayCommand CreateEmptyCarCommand
        {
            get => createEmptyCarCommand;
        }

        private CarViewModel selectedCar;

        public CarViewModel SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        private ObservableCollection<IProducer> producers;

        public ObservableCollection<IProducer> Producers
        {
            get => producers;
        }
    }
}
