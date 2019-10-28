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

using Declarations.Discovery;
using Declarations.Events;
using Implementation.Events;
using Implementation.Exceptions;
using LibVlcWrapper;
using System;

namespace Implementation.Discovery
{
    internal class RendererDiscovery : DisposableBase, IRendererDiscovery, IEventProvider
    {
        private RendererDiscoveryEventManager m_eventMngr;
        private IntPtr m_hDiscovery = IntPtr.Zero;

        public RendererDiscovery(IntPtr hLib, string serviceName)
        {
            m_hDiscovery = LibVlcMethods.libvlc_renderer_discoverer_new(hLib, serviceName.ToUtf8());
        }

        public IRendererDiscoveryEvents Events
        {
            get
            {
                if(m_eventMngr == null)
                {
                    m_eventMngr = new RendererDiscoveryEventManager(this);
                }

                return m_eventMngr;
            }
        }

        public IntPtr EventManagerHandle
        {
            get
            {
                return LibVlcMethods.libvlc_renderer_discoverer_event_manager(m_hDiscovery);
            }
        }

        public void StartDiscovery()
        {
            int res = LibVlcMethods.libvlc_renderer_discoverer_start(m_hDiscovery);
            if(res == -1)
            {
                throw new LibVlcException("Failed to start discovery");
            }
        }

        public void StopDiscovery()
        {
            LibVlcMethods.libvlc_renderer_discoverer_stop(m_hDiscovery);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing && m_eventMngr != null)
                m_eventMngr.Dispose();

            LibVlcMethods.libvlc_renderer_discoverer_release(m_hDiscovery);
        }
    }
}
