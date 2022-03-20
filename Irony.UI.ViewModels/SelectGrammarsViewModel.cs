using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irony.UI.ViewModels.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Irony.UI.ViewModels
{
    public partial class SelectGrammarsViewModel : ObservableObject
    {
        public SelectGrammarsViewModel(CommonData commonData)
        {
            if (commonData.IsDesignMode)
            {
                LoadDesignTimeData();
            }
        }

        private void LoadDesignTimeData()
        {
            foreach (var item in new[] {
                new GrammarItem { Text = "Basic" },
                new GrammarItem { Text = "C#" },
                new GrammarItem { Text = "C++" },
                new GrammarItem { Text = "F#" },
                new GrammarItem { Text = "Java" },
                new GrammarItem { Text = "Perl" },
                new GrammarItem { Text = "Python" },
                new GrammarItem { Text = "Ruby" },
                new GrammarItem { Text = "Rust" },
                new GrammarItem { Text = "Scheme" },
                new GrammarItem { Text = "Sql", IsChecked=true } })

                Items.Add(item);

        }

        [ObservableProperty]
        private ObservableCollection<GrammarItem> _items = new ObservableCollection<GrammarItem>();


        [ICommand]
        private void CheckAll() => CheckUncheck(true);
        [ICommand]
        private void UncheckAll() => CheckUncheck(false);

        private void CheckUncheck(bool checkAll)
        {
            foreach (var item in _items)
            {
                item.IsChecked = checkAll;
            }
        }

        [ICommand]
        private void Ok() { }
        [ICommand]
        private void Cancel() { }
    }
}