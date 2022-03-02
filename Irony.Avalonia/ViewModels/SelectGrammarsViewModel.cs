using Avalonia.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace Irony.Avalonia.ViewModels
{
    public class SelectGrammarsViewModel : ObservableObject
    {
        public SelectGrammarsViewModel()
        {
            if (Design.IsDesignMode)
            {
                LoadDesignTimeData();
            }
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
