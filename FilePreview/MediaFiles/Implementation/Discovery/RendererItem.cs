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
// =======================================================================

using Declarations;
using Declarations.Discovery;
using Declarations.Enums;
using LibVlcWrapper;
using System;
using System.Runtime.InteropServices;

namespace Implementation.Discovery
{
    class RendererItem : DisposableBase, IRendererItem, IReferenceCount, INativePointer
    {
        private readonly IntPtr m_hRenderer;
        public RendererItem(IntPtr hRenderer)
        {
            m_hRenderer = hRenderer;
        }

        public string Name
        {
            get
            {
                IntPtr pName = LibVlcMethods.libvlc_renderer_item_name(m_hRenderer);
                return Marshal.PtrToStringAnsi(pName);
            }
        }

        public string Type
        {
            get
            {
                IntPtr pName = LibVlcMethods.libvlc_renderer_item_type(m_hRenderer);
                return Marshal.PtrToStringAnsi(pName);
            }
        }

        public string IconUrl
        {
            get
            {
                IntPtr pName = LibVlcMethods.libvlc_renderer_item_icon_uri(m_hRenderer);
                return Marshal.PtrToStringAnsi(pName);
            }
        }

        public RendererType RendererType
        {
            get
            {
                return (RendererType)LibVlcMethods.libvlc_renderer_item_flags(m_hRenderer);
            }
        }

        public IntPtr Pointer
        {
            get
            {
                return m_hRenderer;
            }
        }

        public void AddRef()
        {
            LibVlcMethods.libvlc_renderer_item_hold(m_hRenderer);
        }

        public void Release()
        {
            LibVlcMethods.libvlc_renderer_item_release(m_hRenderer);
        }

        protected override void Dispose(bool disposing)
        {
            Release();
        }
    }
}
