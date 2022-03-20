using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Irony.UI.ViewModels.Models
{
    public partial class FakeTreeNode : ObservableObject
    {
        [ObservableProperty()]
        private FakeTreeNode _parent;

        [ObservableProperty()]
        private string _text;

        [ObservableProperty()]
        private ObservableCollection<FakeTreeNode> children = new ObservableCollection<FakeTreeNode>();

        [ObservableProperty()]
        private object _tag;


        public FakeTreeNode() { }
        public FakeTreeNode(string text) { Text = text; }

        internal FakeTreeNode AddChild(string text)
        {
            var child = new FakeTreeNode(text)
            {
                Parent = this
            };

            OnPropertyChanging(nameof(Children));
            Children.Add(child);
            OnPropertyChanged(nameof(Children));

            return child;
        }
    }
}
