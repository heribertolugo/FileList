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
using Declarations.Enums;
using Declarations.Media;
using Implementation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Implementation.Media
{
    internal sealed unsafe class CompositeMemoryInputMedia : BasicMedia, ICompositeMemoryInputMedia, IPullMediaHandler
    {
        IntPtr m_pLock, m_pUnlock;
        List<Delegate> m_callbacks = new List<Delegate>();
        Action<Exception> m_excHandler;
        Dictionary<int, StreamContainer> m_containers = new Dictionary<int, StreamContainer>();
        IPullMediaHandler m_mediaHandler;
        const string VIDEO_TEMPLATE = "width={0}:height={1}:dar={2}:fps={3}/1:cookie={4}:codec={5}:cat=2";
        const string AUDIO_TEMPLATE = "cookie={0}:cat=1:codec={1}:samplerate={2}:channels={3}";
        const string SUBTITLES_TEMPLATE = "cookie={0}:codec={1}:cat=3";
        const string DATA_TEMPLATE = "cookie={0}:cat=4";

        public CompositeMemoryInputMedia(IntPtr hMediaLib)
            : base(hMediaLib)
        {
            ImemGet pLock = OnImemGet;
            ImemRelease pUnlock = OnImemRelease;

            m_pLock = Marshal.GetFunctionPointerForDelegate(pLock);
            m_pUnlock = Marshal.GetFunctionPointerForDelegate(pUnlock);

            m_callbacks.Add(pLock);
            m_callbacks.Add(pUnlock);

            m_mediaHandler = this;
            DeliveryMode = MediaDeliveryMode.Push;
        }

        public void AddOrUpdateStream(StreamInfo streamInfo, int maxItemsInQueue)
        {
            streamInfo.Validate();
            StreamContainer container = new StreamContainer(streamInfo, maxItemsInQueue);
            m_containers[streamInfo.ID] = container;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                foreach (var item in m_containers)
                {
                    item.Value.Dispose();
                }
                m_containers.Clear();
                m_containers = null;
            }
        }

        private void AddStreamOptions()
        {
            List<StreamInfo> si = new List<StreamInfo>(m_containers.Values.Select(c => c.Info));
            si.Sort(); // Sort the streams, so the first "master" stream is video stream

            List<string> options = new List<string>();
            GetMediaOptions(si[0].ID).ToList().ForEach(m => options.Add(m));

            for (int i = 1; i < si.Count; i++)
            {
                string option = GetOptionString(si[i]);
                if (string.IsNullOrEmpty(option))
                {
                    continue;
                }

                option = string.Format(":input-slave=imem://{0}", option);
                options.Add(option);
            }

            AddOptions(options);
        }

        private string GetOptionString(StreamInfo sInfo)
        {
            switch (sInfo.Category)
            {
                case StreamCategory.Audio:
                    return string.Format(AUDIO_TEMPLATE, sInfo.ID, sInfo.Codec, sInfo.Samplerate, sInfo.Channels);
                case StreamCategory.Video:
                    return string.Format(VIDEO_TEMPLATE, sInfo.Width, sInfo.Height, 
                        sInfo.AspectRatio, sInfo.FPS, sInfo.ID, sInfo.Codec);
                case StreamCategory.Subtitle:
                    return string.Format(SUBTITLES_TEMPLATE, sInfo.ID, sInfo.Codec);
                case StreamCategory.Data:
                    return string.Format(DATA_TEMPLATE, sInfo.ID);
                default:
                    return string.Empty;
            }
        }

        public void SetExceptionHandler(Action<Exception> handler)
        {
            m_excHandler = handler;
        }

        private int OnImemGet(void* data, char* cookie, long* dts, long* pts, int* flags, uint* dataSize, void** ppData)
        {
            try
            {
                int streamId = (int)char.GetNumericValue(*cookie);
                FrameData fdata = m_mediaHandler.GetFrame(streamId);
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

                int streamId = (int)char.GetNumericValue(*cookie);
                m_mediaHandler.ReleaseFrame(streamId, new FrameData() { Data = new IntPtr(pData), DataSize = (int)dataSize });
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

        public void AddFrame(int streamId, FrameData frame)
        {
            if (DeliveryMode == MediaDeliveryMode.Pull)
                throw new InvalidOperationException("Media delivery mode set to Pull based, use IPullMediaHandler to provide frames");

            frame.Validate();
            m_containers[streamId].AddFrame(frame);
        }

        public int GetNumberOfPendingFrames(int streamId)
        {
            return m_containers[streamId].NumberOfPendingItems;
        }

        public IEnumerable<StreamInfo> Streams
        {
            get
            {
                return new List<StreamInfo>(m_containers.Values.Select(c => c.Info));
            }
        }

        public void SetPullModeMediaHandler(IPullMediaHandler mediaHandler)
        {
            if (State == MediaState.Playing)
                throw new InvalidOperationException("Operation not allowed. Media is playing");

            if (mediaHandler == null)
                throw new ArgumentNullException("mediaHandler");

            m_mediaHandler = mediaHandler;
            DeliveryMode = MediaDeliveryMode.Pull;
        }

        public MediaDeliveryMode DeliveryMode { get; private set; }

        private IEnumerable<string> GetMediaOptions(int id)
        {
            yield return string.Format(":imem-get={0}", m_pLock.ToInt64());
            yield return string.Format(":imem-release={0}", m_pUnlock.ToInt64());
            yield return string.Format(":imem-codec={0}", EnumUtils.GetEnumDescription(m_containers[id].Info.Codec));
            yield return string.Format(":imem-cat={0}", (int)m_containers[id].Info.Category);
            yield return string.Format(":imem-id={0}", m_containers[id].Info.ID);
            yield return string.Format(":imem-group={0}", m_containers[id].Info.Group);
            yield return string.Format(":imem-fps={0}/1", m_containers[id].Info.FPS);
            yield return string.Format(":imem-width={0}", m_containers[id].Info.Width);
            yield return string.Format(":imem-height={0}", m_containers[id].Info.Height);
            yield return string.Format(":imem-size={0}", m_containers[id].Info.Size);
            yield return string.Format(":imem-dar={0}", EnumUtils.GetEnumDescription(m_containers[id].Info.AspectRatio));
            yield return string.Format(":imem-cookie={0}", m_containers[id].Info.ID);
        }

        private class StreamContainer : DisposableBase
        {
            internal StreamInfo Info;
            private BufferPool MemoryPool;
            private CircularQueue<FrameData> Queue;

            internal StreamContainer(StreamInfo streamInfo, int queueSize)
            {
                Info = streamInfo;
                if (queueSize == 0)
                    return;

                Queue = new CircularQueue<FrameData>(queueSize, d => FreeBuffer(d),
                    FullBufferEnqueueBehavior.DiscardOldest, EmptyBufferDequeueBehavior.ReturnDefault);
                var settings = BufferPoolSettings.CreateDefault(Info.Size, queueSize / 2);
                MemoryPool = new BufferPool(settings, string.Format("{0} pool", Info.Category));
            }

            internal void AddFrame(FrameData data)
            {
                FrameData clone = new FrameData();
                void* pBuffer = MemoryPool.GetBuffer();
                NativeMethods.CopyMemory(pBuffer, data.Data.ToPointer(), data.DataSize);
                clone.Data = new IntPtr(pBuffer);
                clone.DataSize = data.DataSize;
                clone.DTS = data.DTS;
                clone.PTS = data.PTS;
                Queue.Enqueue(clone);
            }

            internal FrameData GetFrame()
            {
                return Queue.Dequeue(); 
            }

            internal void FreeBuffer(FrameData data)
            {
                if (data.Data != IntPtr.Zero && data.DataSize > 0)
                    MemoryPool.Free(data.Data.ToPointer());
            }

            protected override void Dispose(bool disposing)
            {
                if(Queue != null)
                    Queue.Dispose();
                if(MemoryPool != null)
                    MemoryPool.Dispose();
            }

            internal int NumberOfPendingItems
            {
                get
                {
                    if (Queue == null)
                        return 0;

                    return Queue.Count;
                }
            }
        }

        FrameData IPullMediaHandler.GetFrame(int streamId)
        {
            return m_containers[streamId].GetFrame();
        }

        void IPullMediaHandler.ReleaseFrame(int streamId, FrameData data)
        {
            m_containers[streamId].FreeBuffer(data);
        }

        internal override void OnPlayerMediaSet()
        {
            if (m_containers.Count == 0)
                throw new InvalidOperationException("No elementary streams added, nothing to play");

            AddStreamOptions();
        }
    }
}
