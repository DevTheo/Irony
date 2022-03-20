using System.Threading.Tasks;

namespace Irony.UI.ViewModels.Dialogs
{
    public enum CommonDialogButtons
    {
        OK = 0,
        OkCancel,
        YesNo,
    }

    public enum CommonDialogIcons
    {
        None = 0,
        Warning,
    }

    public enum CommonDialogResult
    {
        Cancel = 0,
        OK,
        No,
        Yes
    }

    public interface ICommonDialogsHelper
    {
        Task<CommonDialogResult> MessageBoxShowAsync(string message, string title = "", CommonDialogButtons buttons = CommonDialogButtons.OK, CommonDialogIcons icons = CommonDialogIcons.None);
    }
}
