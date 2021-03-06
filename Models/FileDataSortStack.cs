﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FileList.Models
{
    /// <summary>
    /// Keeps track of multiple Filter attached to IComparer&lt;TreeNode&gt;, called a comparer. 
    /// Comparers can have order changed. For example:
    /// Comparer A is added, Comparer B is added. This would make the sort operation A then B. 
    /// If B is pushed back into the stack, B will get placed at the top and new order would be B, A.
    /// The enumerator is reset everytime it reaches the end, or a push operation takes place.
    /// </summary>
    public class FileDataSortStack : ICloneable, IEnumerable<KeyValuePair<Filter, IComparer<TreeNode>>>, IEnumerable<IComparer<TreeNode>>
    {
        private List<KeyValuePair<Filter, IComparer<TreeNode>>> stack;
        private FileDataSortStackEnumerator enumerator;

        public FileDataSortStack()
        {
            this.stack = new List<KeyValuePair<Filter, IComparer<TreeNode>>>();
            this.Populate();
            this.enumerator = new FileDataSortStackEnumerator(this);
        }

        private void Populate()
        {
            foreach (Filter key in Enum.GetValues(typeof(Filter)))
            {
                if (key == Filter.None)
                    continue;
                this.stack.Add(new KeyValuePair<Filter, IComparer<TreeNode>>(key, new CompareFiledataNodeByName(SortOrder.None)));
            }
        }

        public KeyValuePair<Filter, IComparer<TreeNode>> this[int index]
        {
            get { return this.stack[index]; }
            private set { }
        }

        public IComparer<TreeNode> this[Filter filter]
        {
            get
            {
                return this.stack.FirstOrDefault(s => s.Key.Equals(filter)).Value;
            }
            private set
            {
            }
        }

        public bool MoveNext()
        {
            bool hasNext = this.enumerator.MoveNext();
            if (!hasNext)
                this.enumerator.Reset();
            return hasNext;
        }

        public KeyValuePair<Filter, IComparer<TreeNode>> GetNext()
        {
            this.MoveNext();
            return this.enumerator.Current;
        }

        public IComparer<TreeNode> GetNextComparer()
        {
            this.MoveNext();
            return this.enumerator.Current.Value;
        }

        public KeyValuePair<Filter, IComparer<TreeNode>> Current
        {
            get
            {
                return this.enumerator.Current;
            }
            private set
            {
            }
        }

        public IComparer<TreeNode> CurrentComparer
        {
            get
            {
                return this.enumerator.Current.Value;
            }
            private set
            {
            }
        }

        public void Push(Filter filter)
        {
            if (!Enum.IsDefined(typeof(Filter), filter))
                throw new ArgumentException("Filter provided is not valid");
            KeyValuePair<Filter, IComparer<TreeNode>> keyValuePair = this.stack.FirstOrDefault(s => s.Key.Equals(filter));
            this.stack.Remove(keyValuePair);
            if (filter != Filter.None)
                this.stack.Insert(0, keyValuePair);
            this.enumerator.Reset();
        }

        public void Push(Filter filter, IComparer<TreeNode> comparer)
        {
            if (!Enum.IsDefined(typeof(Filter), filter))
                throw new ArgumentException("Filter provided is not valid");
            this.stack.Remove(this.stack.FirstOrDefault(s => s.Key.Equals(filter)));
            if (filter != Filter.None)
                this.stack.Insert(0, new KeyValuePair<Filter, IComparer<TreeNode>>(filter, comparer));
            this.enumerator.Reset();
        }

        public object Clone()
        {
            FileDataSortStack fileDataSortStack = new FileDataSortStack();
            for (int index = this.stack.Count - 1; index > -1; --index)
            {
                KeyValuePair<Filter, IComparer<TreeNode>> keyValuePair = new KeyValuePair<Filter, IComparer<TreeNode>>(this.stack[index].Key, this.stack[index].Value);
                fileDataSortStack.Push(keyValuePair.Key, keyValuePair.Value);
            }

            return fileDataSortStack;
        }

        public void Clear()
        {
            this.stack.Clear();
            this.Populate();
            this.enumerator.Reset();
        }

        public IEnumerator<KeyValuePair<Filter, IComparer<TreeNode>>> GetEnumerator()
        {
            return new FileDataSortStackEnumerator(this); // this.enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FileDataSortStackEnumerator(this); // this.enumerator;
        }

        IEnumerator<IComparer<TreeNode>> IEnumerable<IComparer<TreeNode>>.GetEnumerator()
        {
            return new FileDataSortStackEnumerator2(this);
        }


        #region Enumerators
        public class FileDataSortStackEnumerator : IEnumerator<KeyValuePair<Filter, IComparer<TreeNode>>>
        {
            private FileDataSortStack _collection;
            private int currentIndex = -1;

            public FileDataSortStackEnumerator(FileDataSortStack collection)
            {
                this._collection = collection;
            }

            public bool MoveNext()
            {
                if ((++this.currentIndex) >= this._collection.stack.Count)
                {
                    this.currentIndex = -1;
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                this.currentIndex = -1;
            }

            void IDisposable.Dispose() { }

            public KeyValuePair<Filter, IComparer<TreeNode>> Current
            {
                get { return this._collection.stack[currentIndex]; }
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

        }
        public class FileDataSortStackEnumerator2 : IEnumerator<IComparer<TreeNode>>
        {
            private FileDataSortStack _collection;
            private int currentIndex = -1;

            public FileDataSortStackEnumerator2(FileDataSortStack collection)
            {
                this._collection = collection;
            }

            public bool MoveNext()
            {
                if ((++this.currentIndex) >= this._collection.stack.Count)
                {
                    this.currentIndex = -1;
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                this.currentIndex = -1;
            }

            void IDisposable.Dispose() { }

            public IComparer<TreeNode> Current
            {
                get { return this._collection.stack[currentIndex].Value; }
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

        }
        #endregion
    }

}
