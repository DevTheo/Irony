using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Irony.UI.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irony.Avalonia.Services
{
    public partial class AvaloniaSelectAssemblyDialogService : ObservableObject, ISelectAssemblyDialog
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AvaloniaSelectAssemblyDialogService()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            if (Design.IsDesignMode)
            {
                FileName = "TestAssembly.dll";
            }
            else
            {

            }
        }

        [ObservableProperty]
        private string _fileName;

        public Task<SelectAssemblyDialogResult> ShowDialogAsync()
        {
            throw new NotImplementedException();
        }
    }
}
