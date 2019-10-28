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
using System;
using System.Collections.Generic;

namespace Declarations.Media
{
    /// <summary>
    /// Represents multiple elementary streams (audio, video, subtitles or data) taken from memory buffers.
    /// Based on imem access module. Provides both push and pull modes of media delivery.
    /// </summary>
    /// <remarks>If you have single elementary stream use IMemoryInputMedia</remarks>
    public interface ICompositeMemoryInputMedia : IMedia
    {
        /// <summary>
        /// Add or update elementary stream information
        /// </summary>
        /// <param name="streamInfo"></param>
        /// <param name="maxItemsInQueue">Maximum number of media frames in queue. 
        /// Set to zero if no queueing is needed - Pull mode</param>
        void AddOrUpdateStream(StreamInfo streamInfo, int maxItemsInQueue);
        
        /// <summary>
        /// Sets handler for exceptions thrown by background threads
        /// </summary>
        /// <param name="handler"></param>
        void SetExceptionHandler(Action<Exception> handler);

        /// <summary>
        /// Adds media frame for given elementary stream id. This method is non-blocking and returns without
        /// waiting for the data to be consumed by media thread. Frame data is deep cloned and all its resources 
        /// may be released after this method returns. This method may be called only in Push based mode,
        /// otherwise it throws exception</summary>
        /// <param name="streamId"></param>
        /// <param name="frame"></param>
        /// <remarks>Throws exception if media set to Pull mode</remarks>
        void AddFrame(int streamId, FrameData frame);

        /// <summary>
        /// Gets number of frames queued for a given stream id
        /// </summary>
        /// <param name="streamId"></param>
        /// <returns></returns>
        int GetNumberOfPendingFrames(int streamId);

        /// <summary>
        /// Gets collection of elementary streams added to current media instance
        /// </summary>
        IEnumerable<StreamInfo> Streams { get; }

        /// <summary>
        /// Sets frame provider mode to pull based. The methods on IPullMediaHandler interface will be invoked 
        /// on background thread with stream id for every frame. This method must be called before playback is started.
        /// By default, media instance is push based, which means frames are pushed using AddFrame method and stored
        /// in queue until consumed by media thread.
        /// </summary>
        /// <param name="mediaHandler"></param>
        /// <remarks>If delegate execution is too long it will delay every elementary  
        /// stream within current media instance, since libVLC uses single background thread to push
        /// media samples from multiple elemnatry streams through its pipeline.</remarks>   
        void SetPullModeMediaHandler(IPullMediaHandler mediaHandler);

        /// <summary>
        /// Gets delivery mode used by current media instance. Default is Push mode.
        /// </summary>
        MediaDeliveryMode DeliveryMode { get; }
    }
}
