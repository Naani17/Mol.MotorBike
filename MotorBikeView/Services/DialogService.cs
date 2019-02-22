using Interfaces.Models;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using Core;
using Interfaces;
using MotorBikeView.ViewModels;
using MotorBikeView.Views;

namespace MotorBikeView.Services
{
    internal class DialogService : IDialogService
    {
        private Dictionary<string, Window> _windows;

        public DialogService()
        {
            _windows = new Dictionary<string, Window>();
        }

        public void CloseWindow(string guid)
        {
            if (_windows.TryGetValue(guid, out var window))
            {
                _windows.Remove(guid);
                window.Close();
            }
        }

        public void OpenWindow(IEntity entity, WindowType windowsType,IBLC blc, IDialogService dialogService)
        {
            var guid = Guid.NewGuid().ToString();
            Window window;

            switch (windowsType)
            {
                case WindowType.MotorbikeWindow:
                {
                    window = new MotorbikeView();
                    var viewModel = new MotorbikeViewModel((IMotorbike)entity,guid,blc,dialogService);

                    window.DataContext = viewModel;
                    break;
                }
                case WindowType.ProducerWindow:
                {
                    window = new ProducerView();
                    var viewModel = new ProducerViewModel((IProducer)entity,guid,blc,dialogService);

                    window.DataContext = viewModel;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(windowsType), windowsType, null);
            }


            _windows.Add(guid,window);
            window.ShowDialog();
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message);
        }


    }
}
