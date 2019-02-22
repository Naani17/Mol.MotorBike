using System;
using System.Collections.Generic;
using Core;
using Interfaces;
using Interfaces.Models;
using Interfaces.Services;
using MotorBikeView.Bindable;
using MotorBikeView.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

namespace MotorBikeView.ViewModels
{
    internal class MainViewModel : ValidateBase
    {
        private ObservableCollection<IProducer> _producers;
        private ObservableCollection<IMotorbike> _motorbikes;

        private IBLC _blc;
        private IDialogService _dialogService;
        private IProducer _selectedProducer;
        private IMotorbike _selectedMotorbike;
        private TransmissionType _selectedTransmission;
        private string _nameMotorBike;

        public MainViewModel()
        {
            LoadBLC();
            LoadCollection();
            Initialize();
        }

        private void Initialize()
        {
            _dialogService = new DialogService();
            SelectedTransmission = TransmissionType.All;
        }

        private void LoadCollection()
        {
            Producers = new ObservableCollection<IProducer>(_blc.GetProducers());
            Motorbikes = new ObservableCollection<IMotorbike>(_blc.GetMotorbike());
        }

        private void LoadBLC()
        {
            Properties.Settings sett = new Properties.Settings();
            var nameDLL = sett.DBNameConf;
            _blc = new BLC.BLC(nameDLL);
        }

        public RelayCommand AddProducerCommand => new RelayCommand((x) => OpenDialog(WindowType.ProducerWindow, _blc.CreateEmptyProducer()));
        public RelayCommand DeleteProducerCommand => new RelayCommand((x) => DeleteProcuder(), () => CanChangeProducer);
        public RelayCommand UpdateProducerCommand => new RelayCommand((x) => OpenDialog(WindowType.ProducerWindow, _selectedProducer, true), () => CanChangeProducer);

        private bool CanChangeProducer => _selectedProducer != null && _selectedProducer.Id != -1;
        private void DeleteProcuder()
        {
            var countProducers = _motorbikes.Count(x => x.Producer == SelectedProducer);

            if (countProducers != 0)
            {
                _dialogService.ShowError("Nie można usunąć producenta, który jest przypisany do motocykla");
                return;
            }

            _blc.ProducerService().DeleteProducer(SelectedProducer);
            _producers.Remove(_selectedProducer);
        }

        public RelayCommand AddMotorbikeCommand => new RelayCommand((x) => OpenDialog(WindowType.MotorbikeWindow, _blc.CreateEmptyMotorBike()));
        public RelayCommand DeleteMotorbikeCommand => new RelayCommand((x) => DeleteMotorbike(), () => _selectedMotorbike != null);
        public RelayCommand UpdateMotorbikeCommand => new RelayCommand((x) => OpenDialog(WindowType.MotorbikeWindow, _selectedMotorbike, true));

        private void DeleteMotorbike()
        {
            _blc.MotorbikeService().DeleteMotorbike(_selectedMotorbike);
            _motorbikes.Remove(_selectedMotorbike);
        }

        public ObservableCollection<IProducer> Producers
        {
            get
            {
                var allProducers = _blc.CreateEmptyProducer();
                allProducers.Name = "Wszyscy";
                allProducers.Id = -1;
                var tmpProducer = new ObservableCollection<IProducer>(_producers);
                tmpProducer.Add(allProducers);

                return tmpProducer;
            }
            set
            {
                _producers = value;
                OnPropertyChanged(nameof(Producers));
            }
        }

        public ObservableCollection<IMotorbike> Motorbikes
        {
            get => _motorbikes;
            set
            {
                _motorbikes = value;
                OnPropertyChanged(nameof(Motorbikes));
            }
        }

        public IProducer SelectedProducer
        {
            get => _selectedProducer;
            set
            {
                _selectedProducer = value;
                MotorbikeFilterProducer();
                OnPropertyChanged(nameof(DeleteProducerCommand));
                OnPropertyChanged(nameof(UpdateProducerCommand));
                OnPropertyChanged(nameof(SelectedProducer));
                OnPropertyChanged(nameof(Motorbikes));
            }
        }

        public IMotorbike SelectedMotorbike
        {
            get => _selectedMotorbike;
            set
            {
                _selectedMotorbike = value;
                OnPropertyChanged(nameof(DeleteMotorbikeCommand));

            }
        }

        public TransmissionType SelectedTransmission
        {
            get => _selectedTransmission;
            set
            {
                _selectedTransmission = value;
                MotorbikeFilterTransmission();
                OnPropertyChanged(nameof(Motorbikes));
            }
        }

        public IEnumerable<TransmissionType> TransmissionTypes =>
            Enum.GetValues(typeof(TransmissionType)).Cast<TransmissionType>();

        public string NameMotorBike
        {
            get => _nameMotorBike;
            set
            {
                _nameMotorBike = value;
                MotoribikeFilterName();
                OnPropertyChanged(nameof(NameMotorBike));
                OnPropertyChanged(nameof(Motorbikes));
            }
        }

        private void OpenDialog(WindowType windowType, IEntity entity, bool isEditEntity = false)
        {
            _dialogService.OpenWindow(entity, windowType, _blc, _dialogService);


            if (entity.Id == 0)
            {
                return;
            }

            if (isEditEntity)
            {
                SaveUpdateEntity(entity);
                return;
            }


            SaveNewEntity(entity);
        }

        private void SaveNewEntity(IEntity entity)
        {
            var type = entity.GetType().GetInterfaces();

            if (type.Contains(typeof(IProducer)))
            {
                _producers.Add((IProducer)entity);
                _blc.ProducerService().AddProducer((IProducer)entity);
            }
            else if (type.Contains(typeof(IMotorbike)))
            {
                _motorbikes.Add((IMotorbike)entity);
                _blc.MotorbikeService().AddMotorbike((IMotorbike)entity);
            }
        }

        private void SaveUpdateEntity(IEntity entity)
        {
            var type = entity.GetType().GetInterfaces();

            if (type.Contains(typeof(IProducer)))
            {
                _blc.ProducerService().UpdateProducer((IProducer)entity);
                LoadCollection();
                SelectedProducer = null;
            }
            else if (type.Contains(typeof(IMotorbike)))
            {
                _blc.MotorbikeService().UpdateMotorbike((IMotorbike)entity);
                LoadCollection();
            }
        }

        private void MotorbikeFilterTransmission()
        {
            Motorbikes = new ObservableCollection<IMotorbike>(_blc.GetMotorbike().Where(x =>
                SelectedTransmission == TransmissionType.All || x.Transmission == SelectedTransmission));
        }

        private void MotoribikeFilterName()
        {
            Motorbikes = new ObservableCollection<IMotorbike>(_blc.GetMotorbike().Where(x =>
                NameMotorBike.Length == 0 || x.Name.ToLower().StartsWith(NameMotorBike.ToLower())));
        }

        private void MotorbikeFilterProducer()
        {
            Motorbikes = new ObservableCollection<IMotorbike>(_blc.GetMotorbike().Where(x =>
               SelectedProducer.Id == -1 || x.Producer.Id == SelectedProducer.Id));
        }





    }
}
