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
            this._enumerator = new ConcurrentCollectionEnumerator<T>(that);
        }

        public ConcurrentCollection(string name):this()
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public T this[int index]
        {
            get
            {
                return this._items[index];
            }
            set
            {
                Console.WriteLine("requesting item lock @ this[int index] {0}", this.Name);
                lock (this._itemLock)
                {
                    this._items[index] = value;
                }
                Console.WriteLine("released item lock @ this[int index] {0}", this.Name);
            }
        }

        public int Count
        {
            get
            {
                Console.WriteLine("requesting item lock @ Count {0}", this.Name);
                int c = 0;
                lock (this._itemLock)
                {
                    c = this._items.Count;
                }
                Console.WriteLine("released item lock @ Count {0}", this.Name);
                return c;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(T item)
        {
            Console.WriteLine("requesting item lock @ Add(T item) {0}", this.Name);
            lock (this._itemLock)
            {
                this._items.Add(item);
            }
            Console.WriteLine("released item lock @ Add(T item) {0}", this.Name);
        }

        public void Add(T item, out int count)
        {
            Console.WriteLine("requesting item lock @ Add(T item, out int count) {0}", this.Name);
            lock (this._itemLock)
            {
                this._items.Add(item);
                count = this._items.Count;
            }
            Console.WriteLine("released item lock @ Add(T item, out int count) {0}", this.Name);
        }

        public void AddRange(IEnumerable<T> items)
        {
            Console.WriteLine("requesting item lock @ AddRange(IEnumerable<T> items) {0}", this.Name);
            lock (this._itemLock)
            {
                this._items.AddRange(items);
            }
            Console.WriteLine("released item lock @ AddRange(IEnumerable<T> items) {0}", this.Name);
        }

        public void AddRange(IEnumerable<T> items, out int count)
        {
            Console.WriteLine("requesting item lock @ AddRange(IEnumerable<T> items, out int count) {0}", this.Name);
            lock (this._itemLock)
            {
                this._items.AddRange(items);
                count = this._items.Count;
            }
            Console.WriteLine("released item lock @ AddRange(IEnumerable<T> items, out int count) {0}", this.Name);
        }

        public T Take()
        {
            Console.WriteLine("requesting item lock @ Take {0}", this.Name);
            T itm = default(T);
            lock (this._itemLock)
            {
                if (this._items.Count < 1)
                    return default(T);
                T item = this._items[0];
                this._items.RemoveAt(0);
                if (this._enumerator.CurrentIndex > -1)
                    this._enumerator.MoveBack();
                itm = item;
            }
            Console.WriteLine("released item lock @ Take {0}", this.Name);
            return itm;
        }

        public T[] TakeAll()
        {
            Console.WriteLine("requesting item lock @ TakeAll {0}", this.Name);
            T[] itms = null;
            lock (this._itemLock)
            {
                T[] items = this._items.ToArray();
                this.Clear();
                //this._enumerator = new ConcurrentCollectionEnumerator<T>(this);
                itms = items;
            }
            Console.WriteLine("released item lock @ TakeAll {0}", this.Name);
            return itms;
        }

        public void Clear()
        {
            Console.WriteLine("requesting item lock @ Clear {0}", this.Name);
            lock (this._itemLock)
            {
                this._enumerator.Reset();
                this._items.Clear();
            }
            Console.WriteLine("released item lock @ Clear {0}", this.Name);
        }

        public bool Contains(T item)
        {
            bool has = false;
            Console.WriteLine("requesting item lock @ Contains {0}", this.Name);
            lock (this._itemLock)
            {
                has = this._items.Contains(item);
            }
            Console.WriteLine("released item lock @ Contains {0}", this.Name);
            return has;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Console.WriteLine("requesting item lock @ CopyTo {0}", this.Name);
            lock (this._itemLock)
            {
                this._items.CopyTo(array, arrayIndex);
            }
            Console.WriteLine("released item lock @ CopyTo {0}", this.Name);
        }

        public IEnumerator<T> GetEnumerator()
        {
            ConcurrentCollectionEnumerator<T> enumer = null;
            Console.WriteLine("requesting item lock @ GetEnumerator {0}", this.Name);
            lock (this._itemLock)
            {
                enumer = this._enumerator; // new ConcurrentCollectionEnumerator<T>(this._items);
            }
            Console.WriteLine("released item lock @ GetEnumerator {0}", this.Name);
            return enumer;
        }

        public bool Remove(T item)
        {
            bool success = false;
            Console.WriteLine("requesting item lock @ Remove {0}", this.Name);
            lock (this._itemLock)
            {
                int index = this._items.IndexOf(item);
                success = this._items.Remove(item);
                if (success && index < this._enumerator.CurrentIndex)
                    this._enumerator.MoveBack();
            }
            Console.WriteLine("released item lock @ Remove {0}", this.Name);
            return success;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        internal object SyncRoot { get { return this._itemLock; } private set { } }
    }

    public class ConcurrentCollectionEnumerator<T> : IEnumerator<T>//, IEnumerator
    {
        private IEnumerable<T> _collection;
        private int _lastIndex;
        private object _collectionLock;
        public ConcurrentCollectionEnumerator(IEnumerable<T> collection)
        {
            this._collection = collection;
            this._lastIndex = -1;
        }
        public T Current
        {
            get
            {
                T c = default(T);
                Console.WriteLine("requesting SyncRoot lock @ Current");
                lock ((this._collection as ConcurrentCollection<T>).SyncRoot)
                {
                    if (!(this._lastIndex < 0 || (this._collection as ConcurrentCollection<T>).Count <= this._lastIndex))
                        c = this._collection.ElementAtOrDefault(this._lastIndex);
                }
                Console.WriteLine("released SyncRoot lock @ Current");
                return c;
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
            return (++this._lastIndex) >= (this._collection as ConcurrentCollection<T>).Count;
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
