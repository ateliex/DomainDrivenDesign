using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    public class ExtendedObservableCollection<T> : ObservableCollection<T>
        where T : ObservableObject
    {
        private readonly IList<T> deletedItems;

        public ExtendedObservableCollection()
        {
            deletedItems = new List<T>();
        }

        public ExtendedObservableCollection(IList<T> list)
            : base(list)
        {
            deletedItems = new List<T>();
        }

        protected override void InsertItem(int index, T item)
        {
            OnAddNew(item);

            base.InsertItem(index, item);
        }

        protected virtual void OnAddNew(T viewModel)
        {
            viewModel.State = ObjectState.New;
        }

        public virtual async Task SaveChanges()
        {
            deletedItems.Clear();

            await Task.FromResult(true);
        }

        protected override void RemoveItem(int index)
        {
            var item = this[index];

            OnRemoveItem(item);

            base.RemoveItem(index);
        }

        protected virtual void OnRemoveItem(T viewModel)
        {
            viewModel.State = ObjectState.Deleted;

            deletedItems.Add(viewModel);
        }

        public IEnumerable<T> GetItemsBy(ObjectState state)
        {
            IEnumerable<T> items;

            if (state == ObjectState.Deleted)
            {
                items = this.deletedItems;
            }
            else
            {
                items = this.Where(p => p.State == state);
            }

            return items;
        }

        public delegate void StatusChangedHandler(string status);

        public event StatusChangedHandler StatusChanged;

        protected void SetStatus(string status)
        {
            if (StatusChanged != null)
            {
                StatusChanged(status);
            }

            //mainToolStripStatusLabel.Text = value;

            //statusBarTimer.Enabled = true;
        }
    }
}
