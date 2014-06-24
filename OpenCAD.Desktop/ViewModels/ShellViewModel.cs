using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Xceed.Wpf.AvalonDock;

namespace OpenCAD.Desktop.ViewModels
{
    public class ShellViewModel : Conductor<Screen>
    {
        private readonly IEventAggregator _eventAggregator;
        private PropertyChangedBase _activeDocument;

        public PropertyChangedBase ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                if (Equals(value, _activeDocument)) return;
                _activeDocument = value;
                NotifyOfPropertyChange(() => ActiveDocument);
            }
        }

        public BindableCollection<PropertyChangedBase> Tabs { get; set; }
        public BindableCollection<PropertyChangedBase> Tools { get; set; }
        //public MenuViewModel Menu { get; set; }


        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Tabs = new BindableCollection<PropertyChangedBase>();
            Tools = new BindableCollection<PropertyChangedBase> {

            };
            //Menu = menu;

            InitializeEvents();
        }


        private void InitializeEvents()
        {
            var tabsChanged = Observable.FromEventPattern<NotifyCollectionChangedEventArgs>(Tabs, "CollectionChanged");
            var toolsChanged = Observable.FromEventPattern<NotifyCollectionChangedEventArgs>(Tools, "CollectionChanged");

            //tabsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Add).Subscribe(a => _eventAggregator.Publish(new TabAddedEvent { Args = a.EventArgs }));
            //tabsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Remove).Subscribe(a => _eventAggregator.Publish(new TabRemovedEvent { Args = a.EventArgs }));

            //toolsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Add).Subscribe(a => _eventAggregator.Publish(new ToolAddedEvent { Args = a.EventArgs }));
            //toolsChanged.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Remove).Subscribe(a => _eventAggregator.Publish(new ToolRemovedEvent { Args = a.EventArgs }));
        }

        public void DocumentClosed(DocumentClosedEventArgs e)
        {
            Tabs.Remove(e.Document.Content as Screen);
            var disposable = e.Document.Content as IDisposable;
            if (disposable != null) disposable.Dispose();
        }

        public void DocumentClosing(DocumentClosingEventArgs e) { }
    }
}
