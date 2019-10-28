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
using System.IO;

namespace Declarations.Media
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStreamSourceMedia : ICustomSourceMedia<Stream>
    {
        /// <summary>
        /// Determines whether to dispose underlying stream instance upon media instance disposal, default value is false
        /// </summary>
        bool DisposeStreamOnMediaDispose { get; set; }

        /// <summary>
        /// Sets handler for exceptions thrown by background threads
        /// </summary>
        /// <param name="handler"></param>
        void SetExceptionHandler(Action<Exception> handler);

        /// <summary>
        /// Synchronization object used to coordinate access to the stream read and seek operations from multiple threads
        /// </summary>
        object SyncRoot { get; }
    }
}
