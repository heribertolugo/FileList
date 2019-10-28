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
using Declarations.Events;
using Declarations.Media;
using Declarations.Players;
using Implementation.Events;
using Implementation.Exceptions;
using Implementation.Media;
using LibVlcWrapper;
using System;
using System.Threading.Tasks;

namespace Implementation.Players
{
    internal class BasicPlayer : DisposableBase, IPlayer, IEventProvider, IReferenceCount, INativePointer
    {
        protected IntPtr m_hMediaPlayer;
        protected IntPtr m_hMediaLib;
        private readonly EventBroker m_events;
        IntPtr m_hEventManager = IntPtr.Zero;
        
        public BasicPlayer(IntPtr hMediaLib)
        {
            m_hMediaLib = hMediaLib;
            m_hMediaPlayer = LibVlcMethods.libvlc_media_player_new(m_hMediaLib);
            //AddRef();
            m_events = new EventBroker(this);
        }

        #region IPlayer Members

        public virtual void Open(IMedia media)
        {
            BasicMedia baseMedia = media as BasicMedia;
            if(baseMedia == null)
            {
                throw new InvalidOperationException(
                    string.Format("Media of type {0} is not derivative of BasicMedia type", media.GetType().Name));
            }

            baseMedia.OnPlayerMediaSet();

            LibVlcMethods.libvlc_media_player_set_media(m_hMediaPlayer, baseMedia.Pointer);
        }

        public virtual void Play()
        {
            LibVlcMethods.libvlc_media_player_play(m_hMediaPlayer);
        }

        public void Pause()
        {
            LibVlcMethods.libvlc_media_player_pause(m_hMediaPlayer);
        }

        public void Stop()
        {
            LibVlcMethods.libvlc_media_player_stop(m_hMediaPlayer);
        }

        public long Time
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_get_time(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_media_player_set_time(m_hMediaPlayer, value);
            }
        }

        public float Position
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_get_position(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_media_player_set_position(m_hMediaPlayer, value);
            }
        }

        public long Length
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_get_length(m_hMediaPlayer);
            }
        }

        public IEventBroker Events
        {
            get
            {
                return m_events;
            }
        }

        public bool IsPlaying
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_is_playing(m_hMediaPlayer) == 1;
            }
        }

        public IMedia CurrentMedia
        {
            get
            {
                IntPtr hMedia = LibVlcMethods.libvlc_media_player_get_media(m_hMediaPlayer);
                if (hMedia == IntPtr.Zero)
                    return null;

                return new BasicMedia(hMedia, ReferenceCountAction.None);
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            m_events.Dispose();
            Release();
        }

        #region IEventProvider Members

        public IntPtr EventManagerHandle
        {
            get
            {
                if (m_hEventManager == IntPtr.Zero)
                {
                    m_hEventManager = LibVlcMethods.libvlc_media_player_event_manager(m_hMediaPlayer);
                }

                return m_hEventManager;
            }
        }

        #endregion

        #region IReferenceCount Members

        public void AddRef()
        {
            LibVlcMethods.libvlc_media_player_retain(m_hMediaPlayer);
        }

        public void Release()
        {
            LibVlcMethods.libvlc_media_player_release(m_hMediaPlayer);
        }

        #endregion

        #region IEqualityComparer<IPlayer> Members

        public bool Equals(IPlayer x, IPlayer y)
        {
            INativePointer x1 = (INativePointer)x;
            INativePointer y1 = (INativePointer)y;

            return x1.Pointer == y1.Pointer;
        }

        public int GetHashCode(IPlayer obj)
        {
            return ((INativePointer)obj).Pointer.GetHashCode();
        }

        #endregion

        #region INativePointer Members

        public IntPtr Pointer
        {
            get { return m_hMediaPlayer; }
        }

        #endregion

        public override bool Equals(object obj)
        {
            return this.Equals((IPlayer)obj, this);
        }

        public override int GetHashCode()
        {
            return m_hMediaPlayer.GetHashCode();
        }

        public Task StopAsync()
        {
            return Task.Factory.StartNew(() => Stop(), TaskCreationOptions.PreferFairness);
        }

        public MediaPlayerRole Role
        {
            get
            {
                return (MediaPlayerRole)LibVlcMethods.libvlc_media_player_get_role(m_hMediaPlayer);
            }
            set
            {
                int res = LibVlcMethods.libvlc_media_player_set_role(m_hMediaPlayer, (libvlc_media_player_role)value);
                if(res == -1)
                {
                    throw new LibVlcException("Failed to set media role");
                }
            }
        }
    }
}
