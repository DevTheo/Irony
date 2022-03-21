using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irony.SampleExplorer.Avalonia.ViewModels
{

    internal class CommonUiSettings : ICommonUiSettings
    {
        public CommonUiSettings()
        {
            IsInDesignMode = Design.IsDesignMode;
        }
        public bool IsInDesignMode { get; private set; }
    }
}
