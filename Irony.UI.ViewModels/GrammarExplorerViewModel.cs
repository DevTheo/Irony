using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Parsing;
using Irony.UI.ViewModels.Dialogs;
using Irony.UI.ViewModels.Interfaces;
using Irony.UI.ViewModels.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Irony.UI.ViewModels
{
    public partial class GrammarExplorerViewModel : ObservableObject
    {
        private CommonData _commonData;

        private IMessenger _messenger => StrongReferenceMessenger.Default;

        [ObservableProperty]
        private Grammar _grammar;

        [ObservableProperty]
        private LanguageData _language;

        [ObservableProperty]
        private Parser _parser;

        [ObservableProperty]
        private ParseTree _parseTree;

        [ObservableProperty]
        private RuntimeException _runtimeError;

        [ObservableProperty]
        private bool _loaded;

        [ObservableProperty]
        private string _languageText;

        [ObservableProperty]
        private string _languageVersionText;

        [ObservableProperty]
        private string _languageDescrText;

        [ObservableProperty]
        private string _grammarCommentsText;

        [ObservableProperty]
        private string _srcLineCountText;

        [ObservableProperty]
        private string _srcTokenCountText;

        [ObservableProperty]
        private string _parseTimeText;

        [ObservableProperty]
        private string _parseErrorCountText;

        [ObservableProperty]
        private ObservableCollection<Token> _tokensItems = new ObservableCollection<Token>();

        [ObservableProperty]
        private int _tokensSelectedIndex = -1;

        [ObservableProperty]
        private ObservableCollection<CompilerErrorRow> _compileErrorsRows = new ObservableCollection<CompilerErrorRow>();

        [ObservableProperty]
        private ObservableCollection<ParserTraceRow> _parserTraceRows = new ObservableCollection<ParserTraceRow>();

        [ObservableProperty]
        private ObservableCollection<object> _parseTreeNodes = new ObservableCollection<object>();

        [ObservableProperty]
        private ObservableCollection<object> _astNodes = new ObservableCollection<object>();

        [ObservableProperty]
        private string _parserStateCountText = string.Empty;

        [ObservableProperty]
        private string _parserConstrTimeText = string.Empty;

        [ObservableProperty]
        private string _parserStatesText = string.Empty;

        // TODO:
        private const int pageParserOutput = 0;
        private const int pageParserTrace = 1;
        private const int pageLanguage = 3;
        private const int pageGrammarErrors = 4;
        private const int pageOutput = 5;

        [ObservableProperty]
        private int _tabBottomSelectedTabIndex = 0;

        [ObservableProperty]
        private bool _parserTraceChecked;

        [ObservableProperty]
        private bool _excludeCommentsChecked;

        [ObservableProperty]
        private ObservableCollection<object> _grammarErrorsRows = new ObservableCollection<object>();

        [ObservableProperty]
        private string _termsText = "";

        [ObservableProperty]
        private string _nonTermsText = "";

        [ObservableProperty]
        private int _sourceSelectionStart = -1;

        [ObservableProperty]
        private int _sourceSelectionLength = -1;

        private const int pageTest = 0;
        private const int pageParserStates = 1;

        [ObservableProperty]
        private int _tabGrammarSelectedTabIndex = 0;

        [ObservableProperty]
        public bool _showErrLocationEnabled = false;
        [ObservableProperty]
        public bool _showErrStackEnabled = false;
        [ObservableProperty]
        public string _outputText = string.Empty;

        [ObservableProperty]
        private bool _removeEnabled = false;

        [ObservableProperty]
        private ObservableCollection<GrammarItem> _grammarsItems = new ObservableCollection<GrammarItem>();
        [ObservableProperty]
        private GrammarItem _selectedGrammar;

        [ObservableProperty]
        bool _runEnabled;

        [ObservableProperty]
        string _sourceText;

        [ObservableProperty]
        bool _disableHiliChecked;

        #region DI
        private readonly ISelectAssemblyDialog _selectAssemblyDialog;
        private readonly ICommonDialogsHelper _commonDialogs;
        private readonly IRichTextBoxHighlighterFactory _richTextBoxHighlighterFactory;

        #endregion

        public GrammarExplorerViewModel(
            CommonData commonData,
            ISelectAssemblyDialog selectAssemblyDialog,
            ICommonDialogsHelper commonDialogs,
            IRichTextBoxHighlighterFactory richTextBoxHighlighterFactory)
        {
            if (commonData.IsDesignMode)
            {
                LoadDesignTimeData();
            }
            _commonData = commonData;
            _commonData.PropertyChanged += _commonData_PropertyChanged;
            _selectAssemblyDialog = selectAssemblyDialog;
            _commonDialogs = commonDialogs;
            _richTextBoxHighlighterFactory = richTextBoxHighlighterFactory;
        }

        private void _commonData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CommonData.LoadedGrammar))
            {
                Grammar = _commonData.LoadedGrammar;
            }
        }

        private void LoadDesignTimeData()
        {
        }

        #region Show... methods
        private void ClearLanguageInfo()
        {
            LanguageText = string.Empty;
            LanguageVersionText = string.Empty;
            LanguageDescrText = string.Empty;
            GrammarCommentsText = string.Empty;
        }

        private void ClearParserOutput()
        {
            SrcLineCountText = string.Empty;
            SrcTokenCountText = "";
            ParseTimeText = "";
            ParseErrorCountText = "";

            TokensItems.Clear();
            CompileErrorsRows.Clear();
            ParserTraceRows.Clear();
            ParseTreeNodes.Clear();
            AstNodes.Clear();
            //TODO
            CommonData.ApplicationDoEvents();
        }


        private void ShowLanguageInfo()
        {
            if (_grammar == null) return;
            var langAttr = LanguageAttribute.GetValue(_grammar.GetType());
            if (langAttr == null) return;
            LanguageText = langAttr.LanguageName;
            LanguageVersionText = langAttr.Version;
            LanguageDescrText = langAttr.Description;
            GrammarCommentsText = _grammar.GrammarComments;
        }

        private void ShowCompilerErrors()
        {
            CompileErrorsRows.Clear();
            if (_parseTree == null || _parseTree.ParserMessages.Count == 0) return;

            foreach (var err in _parseTree.ParserMessages)
            {
                CompileErrorsRows.Add(new CompilerErrorRow(err.Location, err.ParserState));
            }
            var needPageSwitch = TabBottomSelectedTabIndex != pageParserOutput &&
              !(TabBottomSelectedTabIndex == pageParserTrace && ParserTraceChecked);
            if (needPageSwitch)
                TabBottomSelectedTabIndex = pageParserOutput;

        }

        private void ShowParseTrace()
        {
            ParserTraceRows.Clear();
            foreach (var entry in _parser.Context.ParserTrace)
            {
                ParserTraceRows.Add(new ParserTraceRow(entry.State, entry.StackTop, entry.Input, entry.Message));
                if (entry.IsError)
                    ParserTraceRows[ParserTraceRows.Count - 1].ForeColor = System.Drawing.Color.Red;
            }

            //Show tokens
            foreach (Token tkn in _parseTree.Tokens)
            {
                if (ExcludeCommentsChecked && tkn.Category == TokenCategory.Comment) continue;
                TokensItems.Add(tkn);
            }
        }

        private void ShowCompileStats()
        {
            if (_parseTree == null) return;
            SrcLineCountText = string.Empty;
            if (_parseTree.Tokens.Count > 0)
                SrcLineCountText = (_parseTree.Tokens[_parseTree.Tokens.Count - 1].Location.Line + 1).ToString();
            SrcLineCountText = _parseTree.Tokens.Count.ToString();
            ParseTimeText = _parseTree.ParseTime.ToString();
            ParseErrorCountText = _parseTree.ParserMessages.Count.ToString();
            //TODO
            CommonData.ApplicationDoEvents();
            //Note: this time is "pure" parse time; actual delay after cliking "Compile" includes time to fill ParseTree, AstTree controls 
        }

        private void ShowParseTree()
        {
            ParseTreeNodes.Clear();
            if (_parseTree == null) return;
            AddParseNodeRec(null, _parseTree.Root);
        }

        private void AddParseNodeRec(FakeTreeNode parent, ParseTreeNode node)
        {
            if (node == null) return;
            string txt = node.ToString();
            FakeTreeNode tvNode;
            if (parent == null)
            {
                tvNode = new FakeTreeNode(txt);
                ParseTreeNodes.Add(tvNode);
            }
            else
            {
                tvNode = parent.AddChild(txt);
            }
            tvNode.Tag = node;
            foreach (var child in node.ChildNodes)
            {
                AddParseNodeRec(tvNode, child);
            }
        }

        private void ShowAstTree()
        {
            AstNodes.Clear();
            if (_parseTree == null || _parseTree.Root == null || _parseTree.Root.AstNode == null) return;
            AddAstNodeRec(null, _parseTree.Root.AstNode);
        }

        private void AddAstNodeRec(FakeTreeNode parent, object astNode)
        {
            if (astNode == null) return;
            string txt = astNode.ToString();
            FakeTreeNode newNode;
            if (parent == null)
            {
                newNode = new FakeTreeNode(txt);
                AstNodes.Add(newNode);
            }
            else
            {
                newNode = parent.AddChild(txt);
            }
            newNode.Tag = astNode;
            var iBrowsable = astNode as IBrowsableAstNode;
            if (iBrowsable == null) return;
            var childList = iBrowsable.GetChildNodes();
            foreach (var child in childList)
                AddAstNodeRec(newNode, child);
        }

        private void ShowParserConstructionResults()
        {
            ParserStateCountText = _language.ParserData.States.Count.ToString();
            ParserConstrTimeText = _language.ConstructionTime.ToString();
            ParserStatesText = string.Empty;
            GrammarErrorsRows.Clear();
            TermsText = string.Empty;
            NonTermsText = string.Empty;
            ParserStatesText = string.Empty;
            TabBottomSelectedTabIndex = pageLanguage;
            if (_parser == null) return;

            TermsText = ParserDataPrinter.PrintTerminals(_parser.Language);
            NonTermsText = ParserDataPrinter.PrintNonTerminals(_parser.Language);
            ParserStatesText = ParserDataPrinter.PrintStateList(_parser.Language);
            ShowGrammarErrors();
        }//method

        private void ShowGrammarErrors()
        {
            GrammarErrorsRows.Clear();
            var errors = _parser.Language.Errors;
            if (errors.Count == 0) return;
            foreach (var err in errors)
                GrammarErrorsRows.Add(new GrammarErrorRow(err.Level.ToString(), err.Message, err.State));
            if (TabBottomSelectedTabIndex != pageGrammarErrors)
                TabBottomSelectedTabIndex = pageGrammarErrors;
        }

        private void ShowSourceLocation(SourceLocation location, int length)
        {
            if (location.Position < 0) return;
            SourceSelectionStart = location.Position;
            SourceSelectionLength = length;

            //txtSource.ScrollToCaret();
            if (TabGrammarSelectedTabIndex != pageTest)
                TabGrammarSelectedTabIndex = pageTest;
            //txtSource.Focus();
        }

        private void ShowSourceLocationAndTraceToken(SourceLocation location, int length)
        {
            ShowSourceLocation(location, length);
            //find token in trace
            for (int i = 0; i < TokensItems.Count; i++)
            {
                var tkn = TokensItems[i];
                if (tkn.Location.Position == location.Position)
                {
                    TokensSelectedIndex = i;
                    return;
                }//if
            }//for i
        }

        private void LocateParserState(ParserState state)
        {
            if (state == null) return;
            if (TabGrammarSelectedTabIndex != pageParserStates)
                TabGrammarSelectedTabIndex = pageParserStates;
            //first scroll to the bottom, so that scrolling to needed position brings it to top
            // txtParserStates.SelectionStart = txtParserStates.Text.Length - 1;
            // txtParserStates.ScrollToCaret();
            // DoSearch(ParserStates, "State " + state.Name, 0);
        }

        private void ShowRuntimeError(RuntimeException error)
        {
            _runtimeError = error;
            ShowErrLocationEnabled = _runtimeError != null;
            ShowErrStackEnabled = ShowErrLocationEnabled;
            if (_runtimeError != null)
            {
                //the exception was caught and processed by Interpreter
                WriteOutput("Error: " + error.Message + " At " + _runtimeError.Location.ToUiString() + ".");
                ShowSourceLocation(_runtimeError.Location, 1);
            }
            else
            {
                //the exception was not caught by interpreter/AST node. Show full exception info
                WriteOutput("Error: " + error.Message);
                ShowException(error);
            }
            TabBottomSelectedTabIndex = pageOutput;
        }

        private void ShowException(RuntimeException error)
        {
            _messenger.Send(new ShowExceptionMessage
            {
                From = nameof(GrammarExplorerViewModel),
                Exception = error
            });
        }

        private void ClearRuntimeInfo()
        {
            ShowErrLocationEnabled = false;
            ShowErrStackEnabled = false;
            _runtimeError = null;
            OutputText = string.Empty;
        }

        #endregion

        #region Grammar combo menu commands
        [ICommand]
        private void Grammars_Opening()
        {
            RemoveEnabled = GrammarsItems.Count > 0;
        }

        [ICommand]
        private async Task Add_Click()
        {
            if ((await _selectAssemblyDialog.ShowDialogAsync()) != SelectAssemblyDialogResult.OK) return;
            string location = _selectAssemblyDialog.FileName;
            if (string.IsNullOrEmpty(location)) return;
            var oldGrammars = new GrammarItemList();
            foreach (var item in GrammarsItems)
                oldGrammars.Add(item);
            var grammars = await SelectGrammarsAsync(location, oldGrammars);
            if (grammars == null) return;
            foreach (GrammarItem item in grammars)
                GrammarsItems.Add(item);
        }

        private async Task<GrammarItemList> SelectGrammarsAsync(string location, GrammarItemList oldGrammars)
        {
            var correlationId = Guid.NewGuid();
            SelectGrammarsResult selectGrammarResult = null;

            var recipient = new SelectGrammarsResultListener(correlationId, ref selectGrammarResult);

            _messenger.Register<SelectGrammarsResult>(recipient);
            _messenger.Send(new SelectGrammars
            {
                From = nameof(GrammarExplorerViewModel),
                Location = location,
                Grammars = oldGrammars
            });
            while (!recipient.Complete)
            {
                await Task.Delay(100);
            }
            _messenger.Unregister<SelectGrammarsResult>(recipient);
            return selectGrammarResult.Grammars;
        }

        private void miRemove_Click(object sender, EventArgs e)
        {
            if (_commonDialogs.MessageBoxShowAsync("Are you sure you want to remove grammmar " + SelectedGrammar.Text + "?",
              "Confirm", CommonDialogButtons.YesNo, CommonDialogIcons.Warning).Result == CommonDialogResult.Yes)
            {
                GrammarsItems.Remove(SelectedGrammar);
                _parser = null;
                if (GrammarsItems.Count > 0)
                {
                    SelectedGrammar = GrammarsItems[0];
                }
            }
        }

        private void miRemoveAll_Click(object sender, EventArgs e)
        {
            if (_commonDialogs.MessageBoxShowAsync("Are you sure you want to remove all grammmars in the list?",
              "Confirm", CommonDialogButtons.YesNo, CommonDialogIcons.Warning).Result == CommonDialogResult.Yes)
            {
                GrammarsItems.Clear();
                SelectedGrammar = null;
                _parser = null;
            }
        }

        #endregion

        #region Parsing and running
        private void CreateGrammar()
        {
            GrammarItem selItem = SelectedGrammar as GrammarItem;
            _grammar = selItem.CreateGrammar();
        }

        private void CreateParser()
        {
            StopHighlighter();
            RunEnabled = false;
            OutputText = string.Empty;
            _parseTree = null;

            RunEnabled = _grammar.FlagIsSet(LanguageFlags.CanRunSample);
            _language = new LanguageData(_grammar);
            _parser = new Parser(_language);
            ShowParserConstructionResults();
            StartHighlighter();
        }

        private void ParseSample()
        {
            ClearParserOutput();
            if (_parser == null || !_parser.Language.CanParse()) return;
            _parseTree = null;
            GC.Collect(); //to avoid disruption of perf times with occasional collections
            _parser.Context.SetOption(ParseOptions.TraceParser, ParserTraceChecked);
            try
            {
                _parser.Parse(SourceText, "<source>");
            }
            catch (Exception ex)
            {
                CompileErrorsRows.Add(new CompilerErrorRow(null, ex.Message, null));
                TabBottomSelectedTabIndex = pageParserOutput;
                throw;
            }
            finally
            {
                _parseTree = _parser.Context.CurrentParseTree;
                ShowCompilerErrors();
                if (ParserTraceChecked)
                {
                    ShowParseTrace();
                }
                ShowCompileStats();
                ShowParseTree();
                ShowAstTree();
            }
        }

        private void RunSample()
        {
            ClearRuntimeInfo();
            //Stopwatch sw = new Stopwatch();
            OutputText = "";
            try
            {
                if (_parseTree == null)
                    ParseSample();
                if (_parseTree.ParserMessages.Count > 0) return;
                GC.Collect(); //to avoid disruption of perf times with occasional collections
                //sw.Start();
                string output = _grammar.RunSample(_parseTree);
                //sw.Stop();
                //RunTimeText = sw.ElapsedMilliseconds.ToString();
                WriteOutput(output);
                TabBottomSelectedTabIndex = pageOutput;
            }
            catch (RuntimeException ex)
            {
                ShowRuntimeError(ex);
            }
            finally
            {
                //sw.Stop();
            }//finally
        }//method

        #endregion

        private void WriteOutput(string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            OutputText += text + Environment.NewLine;
            //txtOutput.Select(txtOutput.Text.Length - 1, 0);

            // Unsure about this
            _messenger.Send(new SetTextSelectionMessage
            {
                From = nameof(GrammarExplorerViewModel),
                DataName = nameof(OutputText),
                StartLocation = 0,
                EndLocation = OutputText.Length - 1,
            });
        }
        #region miscellaneous: LoadSourceFile, Search, Source highlighting
        private void LoadSourceFile(string path)
        {
            _parseTree = null;
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(path);
                SourceText = null;  //to clear any old formatting
                SourceText = reader.ReadToEnd();
                // Unsure about this
                _messenger.Send(new ClearTextSelectionMessage
                {
                    From = nameof(GrammarExplorerViewModel),
                    DataName = nameof(SourceText),
                });
            }
            catch (Exception e)
            {
                _commonDialogs.MessageBoxShowAsync(e.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        //Source highlighting 
        IRichTextBoxHighlighter _highlighter;
        private void StartHighlighter()
        {
            if (_highlighter != null)
            {
                StopHighlighter();
            }
            if (DisableHiliChecked) return;
            if (!_parser.Language.CanParse()) return;
            _highlighter = _richTextBoxHighlighterFactory.CreateRichTextBoxHighlighter(nameof(SourceText), _language);
            _highlighter.AdapterActivate();
        }
        private void StopHighlighter()
        {
            if (_highlighter == null) return;
            _highlighter.Dispose();
            _highlighter = null;
            ClearHighlighting();
        }
        private void ClearHighlighting()
        {
            var txt = SourceText;
            SourceText = String.Empty;
            SourceText = txt; //remove all old highlighting
        }
        private void EnableHighlighter(bool enable)
        {
            if (_highlighter != null)
            {
                StopHighlighter();
            }
            if (enable)
            {
                StartHighlighter();
            }
        }
        //The following methods are contributed by Andrew Bradnan; pasted here with minor changes
        [ICommand]
        private void DoSearch()
        {
            //SearchErrorVisible = false;
            //TextBoxBase textBox = GetSearchContentBox();
            //if (textBox == null) return;
            //int idxStart = textBox.SelectionStart + textBox.SelectionLength;
            //if (!DoSearch(textBox, txtSearch.Text, idxStart))
            //{
            //    lblSearchError.Text = "Not found.";
            //    lblSearchError.Visible = true;
            //}
        }//method

        //private bool DoSearch(TextBoxBase textBox, string fragment, int start)
        //{
        //    textBox.SelectionLength = 0;
        //    // Compile the regular expression.
        //    Regex r = new Regex(fragment, RegexOptions.IgnoreCase);
        //    // Match the regular expression pattern against a text string.
        //    Match m = r.Match(textBox.Text.Substring(start));
        //    if (m.Success)
        //    {
        //        int i = 0;
        //        Group g = m.Groups[i];
        //        CaptureCollection cc = g.Captures;
        //        Capture c = cc[0];
        //        textBox.SelectionStart = c.Index + start;
        //        textBox.SelectionLength = c.Length;
        //        textBox.Focus();
        //        textBox.ScrollToCaret();
        //        return true;
        //    }
        //    return false;
        //}//method

        //public TextBoxBase GetSearchContentBox()
        //{
        //    switch (tabGrammar.SelectedIndex)
        //    {
        //        case 0:
        //            return txtTerms;
        //        case 1:
        //            return txtNonTerms;
        //        case 2:
        //            return txtParserStates;
        //        case 4:
        //            return txtSource;
        //        default:
        //            return null;
        //    }//switch
        //}

        #endregion
    }
}
