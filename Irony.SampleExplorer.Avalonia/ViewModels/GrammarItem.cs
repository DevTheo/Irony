using CommunityToolkit.Mvvm.ComponentModel;
using Irony.Parsing;
using System;
using System.Linq;

namespace Irony.SampleExplorer.Avalonia.ViewModels
{
    public partial class GrammarItem : ObservableObject
    {
        public GrammarItem(Type? grammarType)
        {
            GrammarType = grammarType;

            if (grammarType == null)
            {
                return;
            }

            var type = grammarType;
            var language = type.GetCustomAttributes(typeof(LanguageAttribute), false)
                .FirstOrDefault() as LanguageAttribute;
            if (language != null)
            {
                Name = language.LanguageName;
                return;
            }
            Name = type.Name;
        }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private Type? _grammarType;

        public Grammar? GetGrammar()
        {
            if (GrammarType == null) return null;

            var ctor = GrammarType.GetConstructor(Type.EmptyTypes);
            return ctor?.Invoke(new object[0]) as Grammar;
        }
    }
}