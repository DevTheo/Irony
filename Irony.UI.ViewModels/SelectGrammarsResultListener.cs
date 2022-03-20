using CommunityToolkit.Mvvm.Messaging;
using Irony.UI.ViewModels.Models;
using System;

namespace Irony.UI.ViewModels
{
    internal class SelectGrammarsResultListener : IRecipient<SelectGrammarsResult>
    {
        private Guid _correlationId;
        private SelectGrammarsResult _selectGrammarResult;
        public bool Complete { get; private set; }

        public SelectGrammarsResultListener(Guid correlationId, ref SelectGrammarsResult selectGrammarResult)
        {
            _correlationId = correlationId;
            _selectGrammarResult = selectGrammarResult;
            Complete = false;
        }

        public void Receive(SelectGrammarsResult message)
        {
            if (message.CorrelationId == _correlationId)
            {
                Complete = true;
                _selectGrammarResult = message;
            }
        }
    }
}
