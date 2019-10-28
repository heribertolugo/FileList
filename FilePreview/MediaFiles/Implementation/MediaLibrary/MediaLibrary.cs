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
using Declarations;
using Declarations.Media;
using Declarations.MediaLibrary;
using Implementation.Media;
using LibVlcWrapper;

namespace Implementation.MediaLibrary
{
    internal class MediaLibraryImpl : DisposableBase, IReferenceCount, INativePointer, IMediaLibrary
    {
        private IntPtr m_hMediaLib = IntPtr.Zero;

        public MediaLibraryImpl(IntPtr mediaLib)
        {
            m_hMediaLib = LibVlcMethods.libvlc_media_library_new(mediaLib);
        }

        protected override void Dispose(bool disposing)
        {
            Release();
        }

        public void Load()
        {
            int result = LibVlcMethods.libvlc_media_library_load(m_hMediaLib);
        }

        public IMediaList MediaList
        {
            get
            {
                return new MediaList(LibVlcMethods.libvlc_media_library_media_list(m_hMediaLib));
            }
        }

        public void AddRef()
        {
            LibVlcMethods.libvlc_media_library_retain(m_hMediaLib);
        }

        public void Release()
        {
            LibVlcMethods.libvlc_media_library_release(m_hMediaLib);
        }

        public IntPtr Pointer
        {
            get 
            {
                return m_hMediaLib;
            }
        }
    }
}
