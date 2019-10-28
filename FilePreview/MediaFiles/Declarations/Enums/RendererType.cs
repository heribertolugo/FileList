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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Declarations.Enums
{
    /// <summary>
    /// Network renderer type
    /// </summary>
    [Flags]
    public enum RendererType
    {
        /// <summary>
        /// The renderer can render audio
        /// </summary>
        Audio = 1,

        /// <summary>
        /// The renderer can render video
        /// </summary>
        Video = 2,

        /// <summary>
        /// The renderer can render audio and video
        /// </summary>
        AudioVideo = Audio | Video
    }
}
