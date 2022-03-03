using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irony.UI.ViewModels
{
    public class GrammarExplorerViewModel : ObservableObject
    {
        public GrammarExplorerViewModel(CommonData commonData)
        {
            if (commonData.IsDesignMode)
            {
                LoadDesignTimeData();
            }
        }

        private void LoadDesignTimeData()
        {
        }
    }
}
