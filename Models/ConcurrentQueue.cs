using System.Collections.Generic;
using System.Linq;

namespace FileList.Models
{
    public class ConcurrentQueue<T>
    {
        private List<T> _inComing;
        private List<T> _outGoing;
        private object _inComingLock;
        private object _outGoingLock;

        public ConcurrentQueue()
        {
            this._inComing = new List<T>();
            this._outGoing = new List<T>();
            this._inComingLock = new object();
            this._outGoingLock = new object();
        }

        public ConcurrentQueue(string name) : this()
        {
            this.Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        public void Add(T item)
        {
            lock (this._inComingLock)
            {
                System.Threading.Interlocked.Increment(ref this._count);
                this._inComing.Add(item);
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            lock (this._inComingLock)
            {
                T[] itemArray = items.ToArray(); // hhhhh... hate this, but have to make sure our count doesnt change
                System.Threading.Interlocked.Add(ref this._count, itemArray.Length);
                this._inComing.AddRange(itemArray);
            }
        }

        public T Take()
        {
            lock (this._outGoingLock)
            {
                T item = this.TakeFirstOutGoing();

                if (!Equals(item, default(T)))
                {
                    System.Threading.Interlocked.Decrement(ref this._count);
                    return item;
                }

                lock (this._inComingLock)
                {
                    if (this._inComing.Count > 0)
                    {
                        this.TransferIncoming();

                        System.Threading.Interlocked.Decrement(ref this._count);
                        return this.TakeFirstOutGoing();
                    }
                }

                return item;
            }
        }

        private volatile int _count;
        public int Count
        {
            get
            {
                return System.Threading.Interlocked.CompareExchange(ref this._count, 0, 0);
            }
            private set { }
        }

        private T TakeFirstOutGoing()
        {
            T item = default(T);

            if (this._outGoing.Count > 0)
            {
                item = this._outGoing[0];
                this._outGoing.RemoveAt(0);
            }

            return item;
        }

        private void TransferIncoming()
        {
            for (int index = 0; index < this._inComing.Count; index++)
            {
                this._outGoing.Add(this._inComing[0]);
                this._inComing.RemoveAt(0);
            }
        }
    }
}
