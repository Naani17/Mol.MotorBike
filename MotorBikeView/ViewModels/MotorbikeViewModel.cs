using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Core;
using Interfaces;
using Interfaces.Models;
using Interfaces.Services;
using MotorBikeView.Bindable;

namespace MotorBikeView.ViewModels
{
    internal class MotorbikeViewModel
    {
        private readonly IMotorbike _motorbike;
        private readonly IBLC _blc;
        private ObservableCollection<IProducer> _producers;
        private TransmissionType _selectedTransmission;
        private IDialogService _dialogService;
        private string _guid;
        public MotorbikeViewModel(IMotorbike motorbike, string guid, IBLC blc,IDialogService dialogService)
        {
            _motorbike = motorbike;
            _guid = guid;
            _blc = blc;
            _dialogService = dialogService;
            Initialize();
        }

        public ICommand SaveMotorbikeCommand => new RelayCommand((x) => SaveMotorbike());

        private void SaveMotorbike()
        {
            _motorbike.Name = Name;
            _motorbike.Producer = SelectedProducer;
            _motorbike.ProductionYear = ProductionYear;
            _motorbike.Transmission = SelectedTransmission;
            _motorbike.Id = _blc.MotorbikeService().GetNewId(_motorbike.Id);

            _dialogService.CloseWindow(_guid);
        }

        private void Initialize()
        {
            _producers = new ObservableCollection<IProducer>(_blc.GetProducers());
            if (_motorbike.Id == 0) return;

            Name = _motorbike.Name;
            ProductionYear = _motorbike.ProductionYear;
            SelectedProducer = _motorbike.Producer;
            TransmissionType = _motorbike.Transmission;

        }

        public ObservableCollection<IProducer> Producers => _producers;
        public TransmissionType SelectedTransmission
        {
            get => _selectedTransmission;
            set { _selectedTransmission = value; }
        }

        public IEnumerable<TransmissionType> TransmissionTypes =>
            Enum.GetValues(typeof(TransmissionType)).Cast<TransmissionType>().Where(x=> x != TransmissionType.All);

        public string Name { get; set; }
        public int ProductionYear { get; set; }
        public IProducer SelectedProducer { get; set; }
        public TransmissionType TransmissionType { get; set; }


    }
}
