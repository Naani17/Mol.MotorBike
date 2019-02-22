using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    internal class MotorbikeViewModel : ValidateBase
    {
        private readonly IMotorbike _motorbike;
        private readonly IBLC _blc;
        private ObservableCollection<IProducer> _producers;
        private TransmissionType _selectedTransmission;
        private IDialogService _dialogService;
        private string _guid;

        private string _name;
        private int _productionYear;
        private IProducer _selectedProducer;
        private TransmissionType _transmissionType;

        public MotorbikeViewModel(IMotorbike motorbike, string guid, IBLC blc, IDialogService dialogService)
        {
            PropertyChanged += Motorbike_PropertyChanged;
            _motorbike = motorbike;
            _guid = guid;
            _blc = blc;
            _dialogService = dialogService;
            Initialize();

            _selectedProducer = Producers.FirstOrDefault();
        }

        private void Motorbike_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Name) || e.PropertyName == nameof(SelectedProducer) || e.PropertyName == nameof(ProductionYear))
            {
                OnPropertyChanged(nameof(SaveMotorbikeCommand));
            }
        }

        public ICommand SaveMotorbikeCommand => new RelayCommand((x) => SaveMotorbike(), () => CanSave());

        private bool CanSave() => !HasErrors;

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
            if (_motorbike.Id == 0)
            {
            }

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
            Enum.GetValues(typeof(TransmissionType)).Cast<TransmissionType>().Where(x => x != TransmissionType.All);



        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        [Required(ErrorMessage = "Rok produkcji jest wymagany")]
        [Range(1900,2100,ErrorMessage ="Rok produkcji musi być w zakresie 1900-2100")]
        public int ProductionYear
        {
            get => _productionYear;
            set => SetProperty(ref _productionYear, value);
        }

        [Required(ErrorMessage = "Producent jest wymagany")]
        public IProducer SelectedProducer
        {
            get => _selectedProducer;
            set => SetProperty(ref _selectedProducer, value);
        }

        public TransmissionType TransmissionType
        {
            get => _transmissionType;
            set => _transmissionType = value;
        }


    }
}
