using Avalonia.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace Irony.Avalonia.ViewModels
{
    public class ShowExceptionViewModel : ObservableObject
    {
        public ShowExceptionViewModel()
        {
            if (Design.IsDesignMode)
            {
                LoadDesignTimeData();
            }
        }

        private void LoadDesignTimeData()
        {
            _message = "(txtException)";
        }


        private string _message = "";
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
    }
}
