using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Irony.UI.ViewModels.Dialogs
{
    public enum SelectAssemblyDialogResult
    {
        None, OK, Cancel
    }
    public interface ISelectAssemblyDialog
    {
        string FileName { get; }

        Task<SelectAssemblyDialogResult> ShowDialogAsync();
    }
}
