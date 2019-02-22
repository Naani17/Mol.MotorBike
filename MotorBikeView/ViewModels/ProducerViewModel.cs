using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Interfaces;
using Interfaces.Models;
using Interfaces.Services;
using MotorBikeView.Bindable;
using MotorBikeView.Services;

namespace MotorBikeView.ViewModels
{
    internal class ProducerViewModel : ValidateBase
    {
        private IProducer _producer;
        private IDialogService _dialogService;
        private IBLC _blc;
        private string _guid;

        private string _name;
        private string _country;

        public ProducerViewModel(IProducer producer, string guid, IBLC blc, IDialogService dialogService)
        {
            PropertyChanged += Producer_PropertyChanged;

            _guid = guid;
            _producer = producer;
            _dialogService = dialogService;
            _blc = blc;
            Initialize();

        }

        private void Producer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Name) || e.PropertyName == nameof(Country))
            {
                OnPropertyChanged(nameof(SaveProducerCommand));
            }
        }

        public ICommand SaveProducerCommand => new RelayCommand((x) => SaveProducer(), () => CanSave());

        private bool CanSave() => !HasErrors;

        private void SaveProducer()
        {
            _producer.Name = Name;
            _producer.Country = Country;
            _producer.Id = _blc.ProducerService().GetNewId(_producer.Id);

            _dialogService.CloseWindow(_guid);
        }


        private void Initialize()
        {
            if (_producer.Id == 0)
            {
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Country));
            };

            Name = _producer.Name;
            Country = _producer.Country;
        }

        [Required(ErrorMessage = "Nazwa producenta jest wymagana")]
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }

        [Required(ErrorMessage = "Nazwa kraju jest wymagana")]
        public string Country
        {
            get => _country;
            set
            {
                SetProperty(ref _country, value);
            }
        }
    }
}
