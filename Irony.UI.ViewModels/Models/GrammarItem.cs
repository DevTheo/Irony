#region License
/* **********************************************************************************
 * Copyright (c) Roman Ivantsov
 * This source code is subject to terms and conditions of the MIT License
 * for Irony. A copy of the license can be found in the License.txt file
 * at the root of this distribution. 
 * By using this source code in any fashion, you are agreeing to be bound by the terms of the 
 * MIT License.
 * You must not remove this notice from this software.
 * **********************************************************************************/
#endregion

using System;
using System.Xml;
using System.Reflection;
using Irony.Parsing;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Irony.UI.ViewModels.Models
{
    //Helper classes for supporting showing grammar list in top combo, saving list on exit and loading on start
    public partial class GrammarItem : ObservableObject
    {
        public readonly string Caption;
        public readonly string LongCaption;
        public readonly string Location; //location of assembly containing the grammar
        public readonly string TypeName; //full type name
        internal bool _loading;

        [ObservableProperty]
        private bool _isChecked = false;

        [ObservableProperty]
        private string _text;

        internal GrammarItem()
        {

        }
        public GrammarItem(string caption, string location, string typeName)
        {
            Caption = caption;
            Location = location;
            TypeName = typeName;
        }
        public GrammarItem(Type grammarClass, string assemblyLocation)
        {
            _loading = true;
            Location = assemblyLocation;
            TypeName = grammarClass.FullName;
            //Get language name from Language attribute
            Caption = grammarClass.Name; //default caption
            LongCaption = Caption;
            var langAttr = LanguageAttribute.GetValue(grammarClass);
            if (langAttr != null)
            {
                Caption = langAttr.LanguageName;
                if (!string.IsNullOrEmpty(langAttr.Version))
                    Caption += ", version " + langAttr.Version;
                LongCaption = Caption;
                if (!string.IsNullOrEmpty(langAttr.Description))
                    LongCaption += ": " + langAttr.Description;
            }
        }

        public GrammarItem(XmlElement element)
        {
            Caption = element.GetAttribute("Caption");
            Location = element.GetAttribute("Location");
            TypeName = element.GetAttribute("TypeName");
        }
        public void Save(XmlElement toElement)
        {
            toElement.SetAttribute("Caption", Caption);
            toElement.SetAttribute("Location", Location);
            toElement.SetAttribute("TypeName", TypeName);
        }
        public Grammar CreateGrammar()
        {
            Assembly asm = Assembly.LoadFrom(Location);
            Type type = asm.GetType(TypeName, true, true);
            var grammar = (Grammar)Activator.CreateInstance(type);
            return grammar;
        }
        public override string ToString()
        {
            return _loading ? LongCaption : Caption;
        }

    }//class
}
