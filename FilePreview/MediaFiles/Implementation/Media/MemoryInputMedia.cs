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

using Declarations;
using Declarations.Media;
using Implementation.Media;
using Implementation.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Implementation
{
    internal sealed unsafe class MemoryInputMedia : BasicMedia, IMemoryInputMedia
    {
        IntPtr m_pLock, m_pUnlock;
        List<Delegate> m_callbacks = new List<Delegate>();
        StreamInfo m_videoStreamInfo;
        BlockingCollection<FrameData> m_videoQueue;
        Action<Exception> m_excHandler;
        bool m_initilaized;
        BufferPool m_videoPool;
        
        public MemoryInputMedia(IntPtr hMediaLib)
            : base(hMediaLib)
        {           
            ImemGet pLock = OnImemGet;
            ImemRelease pUnlock = OnImemRelease;

            m_pLock = Marshal.GetFunctionPointerForDelegate(pLock);
            m_pUnlock = Marshal.GetFunctionPointerForDelegate(pUnlock);

            m_callbacks.Add(pLock);
            m_callbacks.Add(pUnlock);
        }

        public void Initialize(StreamInfo streamInfo, int maxItemsInQueue)
        {
            if (streamInfo == null)
            {
                throw new ArgumentNullException("streamInfo");
            }

            if (maxItemsInQueue < 2)
            {
                throw new ArgumentException("maxItemsInQueue");
            }

            streamInfo.Validate();
            m_videoStreamInfo = streamInfo;
            AddOptions(MediaOptions.ToList());
            m_videoQueue = new BlockingCollection<FrameData>(maxItemsInQueue);
            var settings = BufferPoolSettings.CreateDefault(m_videoStreamInfo.Size, maxItemsInQueue / 2);
            m_videoPool = new BufferPool(settings, "Video pool");
            m_initilaized = true;
        }

        public void AddFrame(FrameData frameData)
        {
            if (!m_initilaized)
            {
                throw new InvalidOperationException("The instance must be initialized first. Call Initialize method before adding frames");
            }

            frameData.Validate();
            if (frameData.DataSize > m_videoPool.BufferSize)
            {
                throw new InvalidOperationException(string.Format("Frame size ({0}) larger then expected size ({1})",
                    frameData.DataSize, m_videoPool.BufferSize));
            }
            m_videoQueue.Add(DeepClone(frameData));
        }

        public void AddFrame(byte[] data, long pts, long dts)
        {
            if (!m_initilaized)
            {
                throw new InvalidOperationException("The instance must be initialized first. Call Initialize method before adding frames");
            }

            if (data == null || data.Length == 0)
            {
                throw new ArgumentException("data buffer size must be greater than zero", "data");
            }

            if (pts <= 0)
            {
                throw new ArgumentException("Pts value must be greater than zero", "pts");
            }

            if (data.Length > m_videoPool.BufferSize)
            {
                throw new InvalidOperationException(string.Format("Frame size ({0}) larger then expected size ({1})",
                    data.Length, m_videoPool.BufferSize));
            }

            FrameData frame = DeepClone(data);
            frame.PTS = pts;
            frame.DTS = dts;
            m_videoQueue.Add(frame);
        }

        public void AddFrame(Bitmap bitmap, long pts, long dts)
        {
            if (!m_initilaized)
            {
                throw new InvalidOperationException("The instance must be initialized first. Call Initialize method before adding frames");
            }

            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            if (pts < 0)
            {
                throw new ArgumentException("Pts value must be greater than zero", "pts");
            }

            if (bitmap.PixelFormat != PixelFormat.Format24bppRgb &&
                bitmap.PixelFormat != PixelFormat.Format32bppRgb)
            {
                throw new ArgumentException("Supported pixel formats for bitmaps are Format24bppRgb and Format32bppRgb", "bitmap.PixelFormat");
            }

            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            if (bmpData.Stride * bmpData.Height > m_videoPool.BufferSize)
            {
                bitmap.UnlockBits(bmpData);
                throw new InvalidOperationException(string.Format("Frame size ({0}) larger then expected size ({1})",
                    bmpData.Stride * bmpData.Height, m_videoPool.BufferSize));
            }
            FrameData frame = DeepClone(bmpData.Scan0, bmpData.Stride * bmpData.Height);
            bitmap.UnlockBits(bmpData);
            frame.PTS = pts;
            frame.DTS = dts;
            m_videoQueue.Add(frame);
        }

        private FrameData DeepClone(byte[] buffer)
        {
            FrameData clone = new FrameData();
            clone.Data = new IntPtr(m_videoPool.GetBuffer());
            Marshal.Copy(buffer, 0, clone.Data, buffer.Length);
            clone.DataSize = buffer.Length;
            return clone;
        }

        private FrameData DeepClone(FrameData frameData)
        {
            FrameData clone = DeepClone(frameData.Data, frameData.DataSize);
            clone.DTS = frameData.DTS;
            clone.PTS = frameData.PTS;
            return clone;
        }

        private FrameData DeepClone(IntPtr data, int size)
        {
            FrameData clone = new FrameData();
            void* pBuffer = m_videoPool.GetBuffer();
            NativeMethods.CopyMemory(pBuffer, data.ToPointer(), size);
            clone.Data = new IntPtr(pBuffer);
            clone.DataSize = size;
            return clone;
        }

        private int OnImemGet(void* data, char* cookie, long* dts, long* pts, int* flags, uint* dataSize, void** ppData)
        {
            try
            {
                FrameData fdata = m_videoQueue.Take();
                if (fdata.IsEOS) // End of stream
                    return 1;

                *ppData = fdata.Data.ToPointer();
                *dataSize = (uint)fdata.DataSize;
                *pts = fdata.PTS;
                *dts = fdata.DTS;
                *flags = 0;
                return 0;
            }
            catch (Exception ex)
            {
                if (m_excHandler != null)
                {
                    m_excHandler(ex);
                }
                else
                {
                    throw new Exception("imem-get callback failed", ex);
                }
                return 1;
            }           
        }

        private void OnImemRelease(void* data, char* cookie, uint dataSize, void* pData)
        {
            try
            {
                if (pData == null || dataSize == 0)
                    return;

                m_videoPool.Free(pData);
            }
            catch (Exception ex)
            {
                if (m_excHandler != null)
                {
                    m_excHandler(ex);
                }
                else
                {
                    throw new Exception("imem-release callback failed", ex);
                }
            }
        }

        private IEnumerable<string> MediaOptions
        {
            get
            {
                yield return string.Format(":imem-get={0}", m_pLock.ToInt64());
                yield return string.Format(":imem-release={0}", m_pUnlock.ToInt64());
                yield return string.Format(":imem-codec={0}", EnumUtils.GetEnumDescription(m_videoStreamInfo.Codec));
                yield return string.Format(":imem-cat={0}", (int)m_videoStreamInfo.Category);
                yield return string.Format(":imem-id={0}", m_videoStreamInfo.ID);
                yield return string.Format(":imem-group={0}", m_videoStreamInfo.Group);
                yield return string.Format(":imem-fps={0}/1", m_videoStreamInfo.FPS);
                yield return string.Format(":imem-width={0}", m_videoStreamInfo.Width);
                yield return string.Format(":imem-height={0}", m_videoStreamInfo.Height);
                yield return string.Format(":imem-size={0}", m_videoStreamInfo.Size);
                yield return string.Format(":imem-dar={0}", EnumUtils.GetEnumDescription(m_videoStreamInfo.AspectRatio));
                yield return string.Format(":imem-cookie={0}", 0);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                m_callbacks = null;  
                if(m_videoQueue != null)
                    m_videoQueue.Dispose();
                if(m_videoPool != null)
                    m_videoPool.Dispose();
            }
        }

        public void SetExceptionHandler(Action<Exception> handler)
        {
            m_excHandler = handler;
        }

        public int PendingFramesCount
        {
            get 
            {
                if (m_videoQueue == null)
                {
                    return 0;
                }

                return m_videoQueue.Count;
            }
        }

        public bool IsInitialized
        {
            get 
            {
                return m_initilaized;
            }
        }
    }
}
