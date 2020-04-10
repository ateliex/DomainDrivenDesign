using System.Windows;
using System.Windows.Data;

namespace Ateliex
{
    public partial class EventStoreWindow : Window
    {
        private readonly EventStoreViewModelCollection eventStoreCollection;

        public EventStoreWindow(EventStoreViewModelCollection eventStoreCollection)
        {
            InitializeComponent();

            this.eventStoreCollection = eventStoreCollection;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource eventStoreViewSource = ((CollectionViewSource)this.FindResource("eventStoreViewSource"));

            eventStoreViewSource.Source = eventStoreCollection;
        }
    }
}
