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

using Declarations.Events;
using Declarations.Structures;
using Implementation.Discovery;
using LibVlcWrapper;
using System;
using System.Runtime.InteropServices;

namespace Implementation.Events
{
    internal unsafe class RendererDiscoveryEventManager : EventManager, IRendererDiscoveryEvents
    {
        public RendererDiscoveryEventManager(IEventProvider eventProvider)
            : base(eventProvider)
        {

        }

        protected override void MediaPlayerEventOccured(ref libvlc_event_t libvlc_event, IntPtr userData)
        {
            switch(libvlc_event.type)
            {
                case libvlc_event_e.libvlc_RendererDiscovererItemAdded:
                    if(m_rendererDiscovererItemAdded != null)
                    {
                        var item = libvlc_event.MediaDescriptor.renderer_discoverer_item_added.item;
                        RendererItem renderItem = new RendererItem(item);
                        m_rendererDiscovererItemAdded(this, new RendererItemChanged(renderItem));
                    }
                    break;

                case libvlc_event_e.libvlc_RendererDiscovererItemDeleted:
                    if (m_rendererDiscovererItemDeleted != null)
                    {
                        var item = libvlc_event.MediaDescriptor.renderer_discoverer_item_deleted.item;
                        RendererItem renderItem = new RendererItem(item);
                        m_rendererDiscovererItemDeleted(this, new RendererItemChanged(renderItem));

                    }
                    break;
            }
        }

        private event EventHandler<RendererItemChanged> m_rendererDiscovererItemAdded;
        public event EventHandler<RendererItemChanged> RendererDiscovererItemAdded
        {
            add
            {
                if (m_rendererDiscovererItemAdded == null)
                {
                    Attach(libvlc_event_e.libvlc_RendererDiscovererItemAdded);
                }
                m_rendererDiscovererItemAdded += value;
            }
            remove
            {
                if (m_rendererDiscovererItemAdded != null)
                {
                    m_rendererDiscovererItemAdded -= value;
                    if (m_rendererDiscovererItemAdded == null)
                    {
                        Dettach(libvlc_event_e.libvlc_RendererDiscovererItemAdded);
                    }
                }
            }
        }

        private event EventHandler<RendererItemChanged> m_rendererDiscovererItemDeleted;
        public event EventHandler<RendererItemChanged> RendererDiscovererItemDeleted
        {
            add
            {
                if (m_rendererDiscovererItemDeleted == null)
                {
                    Attach(libvlc_event_e.libvlc_RendererDiscovererItemDeleted);
                }
                m_rendererDiscovererItemDeleted += value;
            }
            remove
            {
                if (m_rendererDiscovererItemDeleted != null)
                {
                    m_rendererDiscovererItemDeleted -= value;
                    if (m_rendererDiscovererItemDeleted == null)
                    {
                        Dettach(libvlc_event_e.libvlc_RendererDiscovererItemDeleted);
                    }
                }
            }
        }
    }
}
