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
                //ErrorsAreVisible = false;
                // TreeviewIsVisible = true;
                //ParseTextIsEnabled = false;
                //GoButtonIsEnabled = false;

                ErrorsText = "There was an error!";
                ParseTree.Add(new TreeNodeData()
                {
                    Text = "(Empty)"
                });


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
        public bool ErrorsAreVisible => !string.IsNullOrWhiteSpace(ErrorsText);

        public bool TreeviewIsVisible => true; //!ParseTree.Any();
        #endregion

        #region Enabled
        public bool ParseTextIsEnabled => SelectedGrammarItem != null;

        public bool GoButtonIsEnabled => !string.IsNullOrWhiteSpace(ParseText);
        #endregion

        [ObservableProperty]
        private ObservableCollection<GrammarItem> _languages = new ObservableCollection<GrammarItem>();


        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(TreeviewIsVisible))]
        private ObservableCollection<TreeNodeData> _parseTree = new ObservableCollection<TreeNodeData>(); // EmptyTree();

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(ParseTextIsEnabled))]
        private GrammarItem? _selectedGrammarItem = null;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(GoButtonIsEnabled))]
        private string _parseText;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(ErrorsAreVisible))]
        private string _errorsText;

        [ICommand]
        private void GoClick()
        {
            if (SelectedGrammarItem == null || string.IsNullOrWhiteSpace(ParseText))
            {
                return;
            }
            ParseTree.Clear();
            var grammar = SelectedGrammarItem!.GetGrammar();
            if (grammar == null)
            {
                return;
            }
            var language = new LanguageData(grammar);
            var parser = new Parser(language);
            if (parser == null || !parser.Language.CanParse())
            {
                return;
            }
            GC.Collect(); //to avoid disruption of perf times with occasional collections
                          //parser.Context.SetOption(ParseOptions.TraceParser, chkParserTrace.Checked);
            try
            {
                parser.Parse(ParseText, "<source>");
            }
            catch (Exception ex)
            {
                _errorsText = ex.Message;
                //gridCompileErrors.Rows.Add(null, ex.Message, null);
                throw;
            }
            finally
            {
                ParseTree.Add(TreeNodeData.FromParseTree(parser.Context.CurrentParseTree));

                //ShowCompilerErrors();
                //if (chkParserTrace.Checked)
                //{
                //    ShowParseTrace();
                //}
                //ShowCompileStats();
                //ShowParseTree();
                //ShowAstTree();
            }

        }
    }

    public partial class TreeNodeData : ObservableObject
    {
        public static TreeNodeData FromParseTree(ParseTree tree)
        {
            var root = new TreeNodeData()
            {
                ParseTree = tree,
                Text = tree.Root?.ToString() ?? "(Root)"
            };
            if (tree.Root != null && tree.Root.ChildNodes != null && tree.Root.ChildNodes.Count > 0)
            {
                root.LoadChildren(tree.Root.ChildNodes);
            }
            return root;
        }

        internal void LoadChildren(ParseTreeNodeList nodes)
        {
            foreach (var node in nodes)
            {
                var child = new TreeNodeData()
                {
                    Text = node.ToString()
                };
                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    child.LoadChildren(node.ChildNodes);
                }
                Children.Add(child);
            }
        }

        [ObservableProperty]
        private string? _text;

        public ParseTree? ParseTree;

        [ObservableProperty]
        private ObservableCollection<TreeNodeData> _children = new ObservableCollection<TreeNodeData>();
    }
}