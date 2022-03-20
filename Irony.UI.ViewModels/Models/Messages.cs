using Irony.Interpreter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Irony.UI.ViewModels.Models
{

    public abstract class MessageBase
    {
        public string From { get; set; }

        protected MessageBase() { }
    }

    public abstract class UIFieldMessageBase : MessageBase
    {
        public string DataName { get; set; }

        protected UIFieldMessageBase() { }
    }

    public class SetFocusMessage : UIFieldMessageBase
    {
    }

    public class SetTextSelectionMessage : UIFieldMessageBase
    {
        public int StartLocation { get; set; }
        public int EndLocation { get; set; }

    }

    public class ClearTextSelectionMessage : UIFieldMessageBase
    {

    }


    public class WriteOutputMessage : MessageBase
    {
        public string Output { get; set; }
    }

    public class ShowExceptionMessage : MessageBase
    {
        public RuntimeException Exception { get; set; }
    }

    public class SelectGrammars : MessageBase
    {
        public string Location { get; set; }
        public GrammarItemList Grammars { get; set; }

        public SelectGrammars()
        {

        }

    }

    public class SelectGrammarsResult : MessageBase
    {
        public Guid CorrelationId { get; set; }
        public GrammarItemList Grammars { get; set; }
    }

}
