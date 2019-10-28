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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Declarations
{
    /// <summary>
    /// Interface used to provide media frames to pull based ICompositeMemoryInputMedia object.
    /// </summary>
    public interface IPullMediaHandler
    {
        /// <summary>
        /// Gets media frame data for provided stream Id. Return default(FrameData) to indicate end of stream.
        /// </summary>
        /// <param name="streamId"></param>
        /// <returns></returns>
        FrameData GetFrame(int streamId);

        /// <summary>
        /// Release resources of frame data after it was consumed by media thread.
        /// </summary>
        /// <param name="streamId"></param>
        /// <param name="data"></param>
        void ReleaseFrame(int streamId, FrameData data);
    }
}
