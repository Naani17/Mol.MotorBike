using BLC;
using Interfaces;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBikeView.ViewModels
{
    internal class MainViewModel
    {
        private ObservableCollection<IProducer> _producers;
        private ObservableCollection<IMotorbike> _motorbikes;
        private IBLC _blc;

        public MainViewModel()
        {
            LoadBLC();
            LoadCollection();
        }

        private void LoadCollection()
        {
            _producers = new ObservableCollection<IProducer>(_blc.GetProducers());
            _motorbikes = new ObservableCollection<IMotorbike>(_blc.GetMotorbike());
        }

        private void LoadBLC()
        {
            Properties.Settings sett = new Properties.Settings();
            var nameDLL = sett.DBNameConf;
            _blc = new BLC.BLC(nameDLL);
        }

        public ObservableCollection<IProducer> Producers { get => _producers; set => _producers = value; }
        public ObservableCollection<IMotorbike> Motorbikes { get => _motorbikes; set => _motorbikes = value; }
    }
}
