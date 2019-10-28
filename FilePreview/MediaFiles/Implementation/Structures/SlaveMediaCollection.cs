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

using Declarations.Enums;
using Declarations.Structures;
using Implementation.Exceptions;
using LibVlcWrapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Implementation.Structures
{
    internal unsafe class SlaveMediaCollection : ISlaveMediaCollection
    {
        private readonly IntPtr _hMedia;
        public SlaveMediaCollection(IntPtr hMedia)
        {
            _hMedia = hMedia;
        }

        public void AddSlave(SlaveMedia slave)
        {
            int res = LibVlcMethods.libvlc_media_slaves_add(_hMedia, (libvlc_media_slave_type_t)slave.SlaveType, slave.Priority, slave.Url.ToUtf8());
            if(res == -1)
            {
                throw new LibVlcException("Failed to add media slave");
            }
        }

        public IEnumerator<SlaveMedia> GetEnumerator()
        {
            libvlc_media_slave_t** pp_slaves;
            uint num = LibVlcMethods.libvlc_media_slaves_get(_hMedia, &pp_slaves);
            if(num == 0)
            {
                throw new LibVlcException("Failed to get media slaves");
            }

            List<SlaveMedia> slaves = new List<SlaveMedia>((int)num);
            for (int i = 0; i < num; i++)
            {
                slaves.Add(new SlaveMedia()
                {
                    Priority = pp_slaves[i]->i_priority,
                    SlaveType = (MediaSlaveType)pp_slaves[i]->i_type,
                    Url = Marshal.PtrToStringAuto(pp_slaves[i]->psz_uri)
                });
            }

            LibVlcMethods.libvlc_media_slaves_release(pp_slaves, num);
            return slaves.GetEnumerator();
        }

        public void RemoveAllSlaves()
        {
            LibVlcMethods.libvlc_media_slaves_clear(_hMedia);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
