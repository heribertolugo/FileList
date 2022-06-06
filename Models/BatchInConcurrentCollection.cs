using Common.Helpers;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileList.Models
{

    public class BatchInConcurrentCollection<T> //: ICollection<T>
    {
        private ConcurrentDictionary<string, ICollection<T>> incoming;

        //public BatchInConcurrentCollection()
        //{
        //    this.incoming = new ConcurrentDictionary<string, ICollection<T>>();
        //}
        ////public T this[int index]
        ////{
        ////    get
        ////    {
        ////        return this._items[index];
        ////    }
        ////    set
        ////    {
        ////        IoHelper.WriteToConsole("requesting item lock @ this[int index] {0}", this.Name);
        ////        lock (this._itemLock)
        ////        {
        ////            this._items[index] = value;
        ////        }
        ////        IoHelper.WriteToConsole("released item lock @ this[int index] {0}", this.Name);
        ////    }
        ////}

        //public void Add(string insertKey, T item)
        //{

        //}
        //public void Add(string insertKey, IEnumerable<T> items)
        //{

        //}

        //public T Take()
        //{
        //    string key = this.incoming.Keys.FirstOrDefault();
        //    T value = default(T);


        //    if (key is null)
        //        return value;

        //    this.incoming.

        //    IoHelper.WriteToConsole("requesting item lock @ Take {0}", this.Name);
        //    T itm = default(T);
        //    lock (this._itemLock)
        //    {
        //        if (this._items.Count < 1)
        //            return default(T);
        //        T item = this._items[0];
        //        this._items.RemoveAt(0);
        //        if (this._enumerator.CurrentIndex > -1)
        //            this._enumerator.MoveBack();
        //        itm = item;
        //    }
        //    IoHelper.WriteToConsole("released item lock @ Take {0}", this.Name);
        //    return itm;
        //}

        //public T[] TakeBatch()
        //{
        //    IoHelper.WriteToConsole("requesting item lock @ TakeAll {0}", this.Name);
        //    T[] itms = null;
        //    lock (this._itemLock)
        //    {
        //        T[] items = this._items.ToArray();
        //        this.Clear();
        //        //this._enumerator = new ConcurrentCollectionEnumerator<T>(this);
        //        itms = items;
        //    }
        //    IoHelper.WriteToConsole("released item lock @ TakeAll {0}", this.Name);
        //    return itms;
        //}

        //#region ICollection Implementation
        //public int Count => this.incoming.Count;

        //public bool IsReadOnly => false;
        //[Obsolete("Add must be used with a batch unique key.", true)]
        //public void Add(T item)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Clear()
        //{
        //    this.incoming.Clear();
        //}

        //public bool Contains(T item)
        //{
        //    return this.incoming.Values.Contains(item);
        //}

        //public void CopyTo(T[] array, int arrayIndex)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerator<T> GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Remove(T item)
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion ICollection Implementation
    }
}
