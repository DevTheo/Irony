using Irony.UI.ViewModels.Dialogs;
using System;
using System.Threading.Tasks;

namespace Irony.Avalonia.Services
{
    public class AvaloniaCommonDialogsHelperService : ICommonDialogsHelper
    {
        public Task<CommonDialogResult> MessageBoxShowAsync(string message, string title = "", CommonDialogButtons buttons = CommonDialogButtons.OK, CommonDialogIcons icons = CommonDialogIcons.None)
        {
            throw new NotImplementedException();
        }
    }
}
