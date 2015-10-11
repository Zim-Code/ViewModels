using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ZimCode.ViewModels
{
    /// <summary>
    /// Allows you to wrap wrap a collection and replicate it here with the items wrapped in a view model.
    /// </summary>
    public class ViewModelCollection : INotifyCollectionChanged, IEnumerable
    {
        IEnumerable collection;
        Func<object, object> viewModelGenerator;
        ObservableCollection<object> internalCollection;
        readonly ReadOnlyObservableCollection<object> readOnlyInternalCollection;

        /// <summary>
        /// Initialize a new <see cref="ZimCode.ViewModels.ViewModelCollection"/>.
        /// </summary>
        /// <param name="collection">The collection to wrap.</param>
        /// <param name="viewModelGenerator">The func used to generate the view models.</param>
        public ViewModelCollection(IEnumerable collection, Func<object, object> viewModelGenerator)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (viewModelGenerator == null)
                throw new ArgumentNullException(nameof(viewModelGenerator));

            this.collection = collection;
            this.viewModelGenerator = viewModelGenerator;

            internalCollection = new ObservableCollection<object>(collection.Select(viewModelGenerator));
            readOnlyInternalCollection = new ReadOnlyObservableCollection<object>(internalCollection);
            

            INotifyCollectionChanged collectionChanged = collection as INotifyCollectionChanged;
            if (collectionChanged != null)
                collectionChanged.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                int offset = e.NewStartingIndex;
                int i = 0;
                foreach (var item in e.NewItems)
                    internalCollection.Insert(offset + i++, viewModelGenerator.Invoke(item));
            }
            else if (e.Action == NotifyCollectionChangedAction.Move)
            {
                var viewToMove = internalCollection[e.OldStartingIndex];
                internalCollection.RemoveAt(e.OldStartingIndex);

                int index = e.NewStartingIndex;
                if (e.NewStartingIndex < e.OldStartingIndex) --index;
                internalCollection.Insert(index, viewToMove);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                internalCollection.RemoveAt(e.OldStartingIndex);
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                internalCollection[e.NewStartingIndex] = viewModelGenerator.Invoke(e.NewItems[0]);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                internalCollection.Clear();
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator() => internalCollection.GetEnumerator();

        /// <summary>
        /// Raised when the collecion has changed.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { (readOnlyInternalCollection as INotifyCollectionChanged).CollectionChanged += value; }
            remove { (readOnlyInternalCollection as INotifyCollectionChanged).CollectionChanged -= value; }
        }
    }
}
