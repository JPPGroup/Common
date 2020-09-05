using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Jpp.Common
{
    public class ObservableCollectionWrapper<T> : ICollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private ICollection<T> _underlyingCollection;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollectionWrapper(ICollection<T> wrappedCollection)
        {
            _underlyingCollection = wrappedCollection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _underlyingCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _underlyingCollection.Add(item);
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<T>() { item });
            CollectionChanged?.Invoke(this, args);
        }

        public void Clear()
        {
            List<T> removed = new List<T>();
            foreach (T item in _underlyingCollection)
            {
                removed.Add(item);
            }
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed);
            CollectionChanged?.Invoke(this, args);
            _underlyingCollection.Clear();
        }

        public bool Contains(T item)
        {
            return _underlyingCollection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _underlyingCollection.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            bool result = _underlyingCollection.Remove(item);
            if (result)
            {
                NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new List<T>() { item });
                CollectionChanged?.Invoke(this, args);
            }

            return result;
        }

        public int Count
        {
            get
            {
                return _underlyingCollection.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _underlyingCollection.IsReadOnly;
            }
        }
    }
}
