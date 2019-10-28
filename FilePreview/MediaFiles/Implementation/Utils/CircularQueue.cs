//    nVLC
//    
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//     
// ========================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Implementation.Utils
{
    /// <summary>
    /// Enqueue behavior when the queue is full 
    /// </summary>
    public enum FullBufferEnqueueBehavior
    {
        /// <summary>
        /// Oldest items in the queue discarded.
        /// </summary>
        DiscardOldest,
        /// <summary>
        /// Newest item that passed to Enqueue method will be discarded
        /// </summary>
        DiscardNewest,
        /// <summary>
        /// Enqueue method will block until another item dequeued and slot becomes available
        /// </summary>
        BlockUntilItemDequeued
    }

    /// <summary>
    /// Dequeue behavior when the queue is empty
    /// </summary>
    public enum EmptyBufferDequeueBehavior
    {
        /// <summary>
        /// Dequeue method will block until item added to the queue
        /// </summary>
        BlockUntilItemEnqueued,
        /// <summary>
        /// Returns default value [default(T)]
        /// </summary>
        ReturnDefault
    }

    /// <summary>
    /// Thread safe circular queue with fixed capacity.
    /// Insertion and removal of items is O(1) operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerDisplay("Count={Count}")]
    public sealed class CircularQueue<T> : DisposableBase, IDisposable
    {
        private readonly LinkedList<T> _buffer;
        private readonly int _maxSize;
        private readonly object _locker = new object();
        private volatile int _pendingProducers, _pendingConsumers;
        private readonly Action<T> _disposer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxCapacity"></param>
        /// <param name="disposer"></param>
        /// <param name="enqueueBehaviour"></param>
        /// <param name="dequeueBehaviour"></param>
        public CircularQueue(int maxCapacity, Action<T> disposer = null,
            FullBufferEnqueueBehavior enqueueBehaviour = FullBufferEnqueueBehavior.DiscardOldest,
            EmptyBufferDequeueBehavior dequeueBehaviour = EmptyBufferDequeueBehavior.ReturnDefault)
        {
            _buffer = new LinkedList<T>();
            _maxSize = maxCapacity;
            EnqueueBehaviour = enqueueBehaviour;
            DequeueBehaviour = dequeueBehaviour;
            _disposer = disposer;
        }

        /// <summary>
        /// 
        /// </summary>
        public FullBufferEnqueueBehavior EnqueueBehaviour { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public EmptyBufferDequeueBehavior DequeueBehaviour { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            lock (_locker)
            {
                switch (EnqueueBehaviour)
                {
                    case FullBufferEnqueueBehavior.DiscardOldest:
                        if (_buffer.Count >= _maxSize)
                        {
                            T first = _buffer.First.Value;
                            if(_disposer != null)
                                _disposer(first);
                            _buffer.RemoveFirst();
                        }

                        _buffer.AddLast(item);
                        if (_pendingConsumers > 0)
                        {
                            Monitor.PulseAll(_locker);
                        }
                        break;

                    case FullBufferEnqueueBehavior.DiscardNewest:
                        if (_buffer.Count >= _maxSize)
                        {
                            if (_disposer != null)
                                _disposer(item);
                            return;
                        }

                        _buffer.AddLast(item);
                        if (_pendingConsumers > 0)
                        {
                            Monitor.PulseAll(_locker);
                        }
                        break;

                    case FullBufferEnqueueBehavior.BlockUntilItemDequeued:
                        while (_buffer.Count >= _maxSize)
                        {
                            _pendingProducers++;
                            Monitor.Wait(_locker);
                            _pendingProducers--;
                        }

                        _buffer.AddLast(item);
                        if (_pendingConsumers > 0)
                        {
                            Monitor.PulseAll(_locker);
                        }
                        break;

                    default:
                        throw new InvalidOperationException("Unknown behaviour : " + EnqueueBehaviour);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            lock (_locker)
            {
                T item = default(T);
                switch (DequeueBehaviour)
                {
                    case EmptyBufferDequeueBehavior.BlockUntilItemEnqueued:
                        while (_buffer.Count == 0)
                        {
                            _pendingConsumers++;
                            Monitor.Wait(_locker);
                            _pendingConsumers--;
                        }

                        item = _buffer.First.Value;
                        _buffer.RemoveFirst();
                        if (_pendingProducers > 0)
                        {
                            Monitor.PulseAll(_locker);
                        }
                        break;

                    case EmptyBufferDequeueBehavior.ReturnDefault:
                        if (_buffer.Count > 0)
                        {
                            item = _buffer.First.Value;
                            _buffer.RemoveFirst();
                            if (_pendingProducers > 0)
                            {
                                Monitor.PulseAll(_locker);
                            }
                        }
                        break;

                    default:
                        throw new InvalidOperationException("Unknown behaviour : " + DequeueBehaviour);
                }
                return item;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                lock (_locker)
                {
                    return _buffer.Count;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            while (_buffer.Count > 0)
            {
                if (_disposer != null)
                    _disposer(_buffer.First.Value);
                _buffer.RemoveFirst();
            }
        }
    }
}
