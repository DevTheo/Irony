using Avalonia.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irony.Avalonia.ViewModels
{
    public class GrammarExplorerViewModel : ObservableObject
    {
        public GrammarExplorerViewModel()
        {
            if (Design.IsDesignMode)
            {
                LoadDesignTimeData();
            }
        }

        private void LoadDesignTimeData()
        {
        }
    }
}
