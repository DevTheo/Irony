using CommunityToolkit.Mvvm.ComponentModel;

namespace Irony.UI.ViewModels
{
    public partial class ShowExceptionViewModel : ObservableObject
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

        [ObservableProperty]
        private string _message = "";
    }
}
