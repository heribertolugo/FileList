﻿//    nVLC
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Declarations.Enums
{
    /// <summary>
    /// VLC sound formats
    /// </summary>
    public enum SoundType
    {
        /// <summary>
        /// 16 bits per sample signed native endian
        /// </summary>
        /// <remarks>The only type supported by IAudioRenderer (amem)</remarks>
        S16N,

        /// <summary>
        /// 32 bits per sample float
        /// </summary>
        /// <remarks>The only type supported by IMemoryInputMedia (imem)</remarks>
        f32l
    }
}
