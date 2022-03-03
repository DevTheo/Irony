using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace Irony.UI.ViewModels
{
    public class ShowExceptionViewModel : ObservableObject
    {
        public ShowExceptionViewModel(CommonData commonData)
        {
            if (commonData.IsDesignMode)
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
