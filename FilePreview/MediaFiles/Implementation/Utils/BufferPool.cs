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
using System.Diagnostics;

namespace Implementation.Utils
{
    /// <summary>
    /// Container of preallocated memory buffers capable of shrinking and expanding within given range.
    /// </summary>
    [DebuggerDisplay("Name={PoolName}, Total buffers={TotalBuffers}, Free buffers={FreeBuffers}, Buffer size={BufferSize}")]
    unsafe class BufferPool : DisposableBase
    {
        private readonly PtrStack m_buffers;
        private int m_totalMemory;
        private readonly string m_poolName;
        private int m_totalBuffers;
        private readonly BufferPoolSettings m_settings;

        public BufferPool(BufferPoolSettings settings, string poolName = "")
        {
            settings.Validate();
            m_settings = settings;
            m_poolName = poolName;
            m_buffers = new PtrStack();
            Allocate(settings.BufferSize, settings.NumOfBuffers);
        }

        private void Allocate(int bufferSize, int numOfBuffers)
        {
            for (int i = 0; i < numOfBuffers; i++)
            {
                void* buffer = MemoryHeap.Alloc(bufferSize);
                m_buffers.Push(buffer);
            }

            int allocatedBytes = bufferSize * numOfBuffers;
            m_totalMemory += allocatedBytes;
            m_totalBuffers += numOfBuffers;
            GC.AddMemoryPressure(allocatedBytes);
        }

        private void Deallocate(void* pointer)
        {
            MemoryHeap.Free(pointer);
            m_totalBuffers--;
            m_totalMemory -= m_settings.BufferSize;
            GC.RemoveMemoryPressure(m_settings.BufferSize);
        }

        public void* GetBuffer()
        {
            lock (m_buffers)
            {
                if (m_buffers.Count == 0)
                {
                    if (m_settings.GrowthRatio == 0.0)
                    {
                        throw new InvalidOperationException("No more free buffers");
                    }

                    int buffersToAllocate = (int)Math.Ceiling(m_totalBuffers * m_settings.GrowthRatio);
                    if (buffersToAllocate + m_totalBuffers > m_settings.MaxBuffers)
                    {
                        throw new InvalidOperationException("Maximum numbers of buffers exceedded");
                    }

                    Allocate(m_settings.BufferSize, buffersToAllocate);                   
                }
                
                return m_buffers.Pop();
            }
        }

        public void Free(void* pointer)
        {
            lock (m_buffers)
            {
                if (m_settings.ShrinkRatio > 0 && m_buffers.Count > m_settings.NumOfBuffers * m_settings.ShrinkRatio)
                    Deallocate(pointer);
                else
                    m_buffers.Push(pointer);
            }
        }

        protected override void Dispose(bool disposing)
        {
            while (m_buffers.Count > 0)
            {
                void* ptr = m_buffers.Pop();
                if(ptr != null)
                    MemoryHeap.Free(ptr);
            }
            GC.RemoveMemoryPressure(m_totalMemory);
        }

        public int FreeBuffers
        {
            get
            {
                lock (m_buffers)
                {
                    return m_buffers.Count;
                }
            }
        }

        public int TotalBuffers
        {
            get
            {
                lock (m_buffers)
                {
                    return m_totalBuffers;
                }               
            }
        }

        public int BufferSize
        {
            get
            {
                return m_settings.BufferSize;
            }
        }

        public string PoolName
        {
            get
            {
                return m_poolName;
            }
        }
    }
}
