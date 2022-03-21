using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Irony.SampleExplorer.Avalonia.ViewModels
{
    public partial class SampleExplorerViewModel : ObservableObject
    {
        public SampleExplorerViewModel(ICommonUiSettings commonUiSettings)
        {
            if (commonUiSettings.IsInDesignMode)
            {
                // Set up viewmodel with dummy data
                ErrorsAreVisible = false;
                TreeviewIsVisible = true;
                //ParseTextIsEnabled = false;
                //GoButtonIsEnabled = false;

                ErrorsText = "There was an error!";

                Languages.Add(new GrammarItem(null)
                {
                    Name = "CSharp"
                });

                Languages.Add(new GrammarItem(null)
                {
                    Name = "Visual Basic"
                });
            }
            else
            {
                // Load the actual sample grammars
                LoadSampleGrammar();
            }
        }
        private void LoadSampleGrammar()
        {
            var sampleAssembly = typeof(Irony.Samples.SQL.SqlGrammar).Assembly;
            LoadAssemblysGrammars(sampleAssembly);
            // If you have additional assemblies, you could load them here
        }

        private void LoadAssemblysGrammars(Assembly sampleAssembly)
        {
            var types = sampleAssembly.GetTypes();
            Languages.Clear();
            var items = types.AsParallel()
                .Where(t => t.IsSubclassOf(typeof(Grammar)))
                .Select(t =>
                    new GrammarItem(t)
                ).ToList();

            foreach (var i in items.OrderBy(item => item.Name))
            {
                Languages.Add(i);
            }
        }

        #region visibility
        [ObservableProperty]
        private bool _errorsAreVisible;
        [ObservableProperty]
        private bool _treeviewIsVisible;
        #endregion

        #region Enabled
        public bool ParseTextIsEnabled => SelectedGrammarItem != null;

        private bool GoButtonIsEnabled => !string.IsNullOrWhiteSpace(ParseText);
        #endregion

        [ObservableProperty]
        private ObservableCollection<GrammarItem> _languages = new ObservableCollection<GrammarItem>();

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(ParseTextIsEnabled))]
        private GrammarItem? _selectedGrammarItem = null;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(GoButtonIsEnabled))]
        private string _parseText;
        [ObservableProperty]
        private string _errorsText;

        [ICommand]
        private void GoClick()
        {
            if (SelectedGrammarItem == null || string.IsNullOrWhiteSpace(ParseText))
            {
                return;
            }
            var grammar = SelectedGrammarItem!.GetGrammar();
            // TODO: Parse text
            // and retrieve AST Tree (I think)
        }
    }
}