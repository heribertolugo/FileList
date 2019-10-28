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

using Declarations.Media;
using Implementation.Utils;
using LibVlcWrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Implementation.Media
{
    internal unsafe class StreamSourceMedia : BasicMedia, IStreamSourceMedia
    {
        private IntPtr m_openCb;
        private IntPtr m_readCb;
        private IntPtr m_seekCb;
        private IntPtr m_closeCb;
        private List<Delegate> m_callbacks = new List<Delegate>();
        private Stream m_source;
        private byte[] m_buffer;
        private Action<Exception> m_handler;
        private readonly object m_sync = new object();

        public StreamSourceMedia(IntPtr hMediaLib)
            : base(hMediaLib)
        {
            MediaOpenCallbackEventHandler openCB = OpenCb;
            MediaReadCallbackEventHandler readCB = ReadCb;
            MediaSeekCallbackEventHandler seekCB = SeekCb;
            MediaCloseCallbackEventHandler closeCB = CloseCb;

            m_callbacks.Add(openCB);
            m_callbacks.Add(readCB);
            m_callbacks.Add(seekCB);
            m_callbacks.Add(closeCB);

            m_openCb = Marshal.GetFunctionPointerForDelegate(openCB);
            m_readCb = Marshal.GetFunctionPointerForDelegate(readCB);
            m_seekCb = Marshal.GetFunctionPointerForDelegate(seekCB);
            m_closeCb = Marshal.GetFunctionPointerForDelegate(closeCB);

            m_buffer = new byte[100 * 1024];
        }

        public override string Input
        {
            get
            {
                return m_path;
            }
            set
            {
                m_path = value;
            }
        }

        private void Initialize(Stream source)
        {
            if (!source.CanRead)
                throw new InvalidOperationException("The stream must be readable, and optionaly seekable");

            m_source = source;
            m_hMedia = LibVlcMethods.libvlc_media_new_callbacks(m_hMediaLib, m_openCb, m_readCb, m_seekCb, m_closeCb, IntPtr.Zero);
        }

        /// 0 on success, non-zero on error. In case of failure, the other
        /// callbacks will not be invoked and any value stored in *datap and *sizep is discarded
        private int OpenCb(void* opaque, void** datap, UInt64* sizep)
        {
            try
            {
                *sizep = (UInt64)m_source.Length;
            }
            catch (Exception ex)
            {
                if (m_handler != null)
                    m_handler(ex);
                return -1;
            }
            
            return 0;
        }

        //strictly positive number of bytes read, 0 on end-of-stream, or -1 on non-recoverable error
        private int ReadCb(void *opaque, byte *buf, int len)
        {
            lock (m_sync)
            {
                if (len > m_buffer.Length)
                {
                    m_buffer = new byte[len];
                }

                try
                {
                    fixed (byte* pBuf = m_buffer)
                    {
                        int read = m_source.Read(m_buffer, 0, len);
                        NativeMethods.CopyMemory(buf, pBuf, read);
                        return read;
                    }
                }
                catch (Exception ex)
                {
                    if (m_handler != null)
                        m_handler(ex);
                    return -1;
                }
            }        
        }

        // 0 on success, -1 on error
        private int SeekCb(void *opaque, long offset)
        {
            if (!m_source.CanSeek)
                return -1;

            lock (m_sync)
            {
                long seek = m_source.Seek(offset, SeekOrigin.Begin);
                if (seek == offset)
                    return 0;

                return -1;
            }          
        }

        private void CloseCb(void *opaque)
        {

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (DisposeStreamOnMediaDispose)
                {
                    m_source.Close();
                    m_source = null;
                }
                m_callbacks.Clear();
            }
        }

        public void SetExceptionHandler(Action<Exception> handler)
        {
            m_handler = handler;
        }

        public object SyncRoot
        {
            get
            {
                return m_sync;
            }
        }

        public bool DisposeStreamOnMediaDispose { get; set; }

        public Stream Source
        {
            get 
            {
                return m_source;
            }
            set
            {
                Initialize(value);             
            }
        }
    }
}
