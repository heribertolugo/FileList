using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FileList.Models
{
    public class ConcurrentCollection<T> : ICollection<T>
    {
        private volatile List<T> _items;
        private readonly object _itemLock;
        private ConcurrentCollectionEnumerator<T> _enumerator;
        public ConcurrentCollection()
        {
            this._items = new List<T>();
            this._itemLock = new object();
            ConcurrentCollection<T> that = this;
            this._enumerator = new ConcurrentCollectionEnumerator<T>(ref that);
        }

        public T this[int index]
        {
            get
            {
                return this._items[index];
            }
            set
            {
                lock (this._itemLock)
                {
                    this._items[index] = value;
                }
            }
        }

        public int Count
        {
            get
            {
                lock (this._itemLock)
                {
                    return this._items.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(T item)
        {
            lock (this._itemLock)
            {
                this._items.Add(item);
            }
        }

        public T Take()
        {
            lock (this._itemLock)
            {
                if (this._items.Count < 1)
                    return default(T);
                T item = this._items[0];
                this._items.RemoveAt(0);
                if (this._enumerator.CurrentIndex > -1)
                    this._enumerator.MoveBack();
                return item;
            }
        }

        public void Clear()
        {
            lock (this._itemLock)
            {
                this._enumerator.Reset();
                this._items.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (this._itemLock)
            {
                return this._items.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (this._itemLock)
            {
                this._items.CopyTo(array, arrayIndex);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (this._itemLock)
            {
                return this._enumerator;
            }
        }

        public bool Remove(T item)
        {
            lock (this._itemLock)
            {
                int index = this._items.IndexOf(item);
                bool success = this._items.Remove(item);
                if (success && index < this._enumerator.CurrentIndex)
                    this._enumerator.MoveBack();
                return success;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        internal object SyncRoot { get { return this._itemLock; } private set { } }
    }

    public class ConcurrentCollectionEnumerator<T> : IEnumerator<T>//, IEnumerator
    {
        private ConcurrentCollection<T> _collection;
        private int _lastIndex;
        private object _collectionLock;
        public ConcurrentCollectionEnumerator(ref ConcurrentCollection<T> collection)
        {
            this._collection = collection;
            this._lastIndex = -1;
        }
        public T Current
        {
            get
            {
                lock (this._collection.SyncRoot)
                {
                    if (this._lastIndex < 0 || this._collection.Count <= this._lastIndex)
                        return default(T);
                    return this._collection.ElementAtOrDefault(this._lastIndex);
                }
            }
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            this._lastIndex = -1;
            //this._collection = null;
        }

        public bool MoveNext()
        {
            return (++this._lastIndex) >= this._collection.Count;
        }

        public bool MoveBack()
        {
            return (--this._lastIndex) > -1;
        }

        public void Reset()
        {
            this._lastIndex = -1;
        }

        public int CurrentIndex
        {
            get { return this._lastIndex; }
            private set { }
        }
    }
}
