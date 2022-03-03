using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Irony.UI.ViewModels
{
    public class SelectGrammarsViewModel : ObservableObject
    {
        public SelectGrammarsViewModel(CommonData commonData)
        {
            if (commonData.IsDesignMode)
            {
                LoadDesignTimeData();
            }
            CheckAllCommand = new RelayCommand(CheckAll);
            UncheckAllCommand = new RelayCommand(UncheckAll);

            OkCommand = new RelayCommand(OkClick);
            CancelCommand = new RelayCommand(CancelClick);
        }

        private void LoadDesignTimeData()
        {
            _items.AddRange(new[] {
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
                new GrammarItem { Text = "Sql", IsChecked=true }
            });

        }

        private List<GrammarItem> _items = new List<GrammarItem>();
        public List<GrammarItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }


        public ICommand CheckAllCommand { get; }

        private void CheckAll() => CheckUncheck(true);

        public ICommand UncheckAllCommand { get; }
        private void UncheckAll() => CheckUncheck(false);

        public RelayCommand OkCommand { get; }
        private void OkClick()
        {

        }

        public RelayCommand CancelCommand { get; }
        private void CancelClick()
        {

        }


        private void CheckUncheck(bool checkAll)
        {
            foreach (var item in _items)
            {
                item.IsChecked = checkAll;
            }
        }

    }

    public class GrammarItem : ObservableObject
    {
        private bool _isChecked = false;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

    }
}
