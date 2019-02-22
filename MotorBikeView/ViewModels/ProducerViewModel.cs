using System;
using System.Collections.Generic;
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
    internal class ProducerViewModel
    {
        private IProducer _producer;
        private IDialogService _dialogService;
        private IBLC _blc;
        private string _guid;
        public ProducerViewModel(IProducer producer, string guid,IBLC blc, IDialogService dialogService)
        {
            _guid = guid;
            _producer = producer;
            _dialogService = dialogService;
            _blc = blc;
            Initialize();
        }

        public ICommand  SaveProducerCommand => new RelayCommand((x) => SaveProducer());

        private void SaveProducer()
        {
            _producer.Name = Name;
            _producer.Country = Country;
            _producer.Id = _blc.ProducerService().GetNewId(_producer.Id);

            _dialogService.CloseWindow(_guid);
        }


        private void Initialize()
        {
            if (_producer.Id == 0) return;

            Name = _producer.Name;
            Country = _producer.Country;
        }


        public string Name { get; set; }
        public string Country { get; set; }
    }
}
